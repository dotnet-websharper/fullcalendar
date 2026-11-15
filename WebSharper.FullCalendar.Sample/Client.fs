namespace WebSharper.FullCalendar.Sample

open System
open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.FullCalendar

[<JavaScript>]
module Client =

    // The template is the whole document, loaded from the DOM.
    type IndexTemplate = Template<"index.html", ClientLoad.FromDocument>

    // -------------------------------------------------------------------
    // Page state
    // -------------------------------------------------------------------

    let themeStorageKey = "websharper-fullcalendar-theme"
    let theme = Var.Create "light"
    let themeLabel = Var.Create "🌙 Dark mode"

    let status = Var.Create "Ready — the calendar is created on load."
    let logText = Var.Create ""
    let info = Var.Create "-"
    let selected = Var.Create "none"
    let version = Var.Create "-"

    let mutable calendar : option<Calendar> = None
    let mutable externalDrag : option<ExternalDraggable> = None
    let mutable eventCounter = 0

    // -------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------

    let appendLog (line: string) =
        let stamp = DateTime.Now.ToString("HH:mm:ss")
        let entry = stamp + "  " + line
        logText.Value <-
            if logText.Value = "" then entry
            else logText.Value + "\n" + entry

    let setStatus (line: string) =
        status.Value <- line
        appendLog line

    let withCalendar (f: Calendar -> unit) =
        match calendar with
        | Some c -> f c
        | None -> setStatus "No calendar instance yet."

    let applyTheme () =
        JS.Document.DocumentElement.SetAttribute("data-theme", theme.Value)
        themeLabel.Value <-
            if theme.Value = "dark" then "☀️ Light mode" else "🌙 Dark mode"

    let toggleTheme () =
        theme.Value <- (if theme.Value = "dark" then "light" else "dark")
        try JS.Window.LocalStorage.SetItem(themeStorageKey, theme.Value) with _ -> ()
        applyTheme ()

    let refreshInfo () =
        withCalendar (fun c ->
            let v = c.View
            let events = c.GetEvents()
            info.Value <-
                "view: " + v.Type
                + " · title: \"" + v.Title + "\""
                + " · current: " + v.CurrentStart.ToString() + " → " + v.CurrentEnd.ToString()
                + " · events: " + string events.Length
                + " · sources: " + string (c.GetEventSources().Length)
                + " · option initialView = " + string (c.GetOption "initialView"))

    // -------------------------------------------------------------------
    // Event data
    // -------------------------------------------------------------------

    let seedEvents () =
        [|
            EventRefiners(
                Id = "kickoff",
                Title = "Kickoff",
                Start = "2026-11-16T09:30:00",
                End = "2026-11-16T10:30:00",
                BackgroundColor = "#4f46e5",
                BorderColor = "#4338ca"
            )
            EventRefiners(
                Title = "Design review",
                Start = "2026-11-18T14:00:00",
                End = "2026-11-18T15:30:00",
                BackgroundColor = "#0891b2",
                BorderColor = "#0e7490"
            )
            EventRefiners(
                Title = "Release day",
                Start = "2026-11-20",
                AllDay = true,
                BackgroundColor = "#16a34a",
                BorderColor = "#15803d"
            )
            EventRefiners(
                Title = "Conference",
                Start = "2026-11-23",
                End = "2026-11-26",
                AllDay = true,
                BackgroundColor = "#ea580c",
                BorderColor = "#c2410c"
            )
        |]

    let nextEvent () =
        eventCounter <- eventCounter + 1
        EventRefiners(
            Title = "New event " + string eventCounter,
            Start = DateTime.Today.ToString("yyyy-MM-dd"),
            AllDay = true,
            BackgroundColor = "#9333ea",
            BorderColor = "#7e22ce"
        )

    // -------------------------------------------------------------------
    // Listeners (kept as named functions so the options record stays
    // readable).
    // -------------------------------------------------------------------

    let onDateClick (arg: DateClickArg) =
        appendLog ("dateClick " + arg.DateStr + " (allDay=" + string arg.AllDay + ")")
        withCalendar (fun c ->
            c.AddEvent(
                EventRefiners(
                    Title = "Clicked " + arg.DateStr,
                    Start = arg.DateStr,
                    AllDay = true,
                    BackgroundColor = "#0ea5e9"
                )
            )
            |> ignore
            refreshInfo ())

    let onSelect (arg: DateSelectArg) =
        appendLog ("select " + arg.StartStr + " → " + arg.EndStr + " (" + arg.View.Type + ")")
        withCalendar (fun c ->
            c.AddEvent(
                EventRefiners(
                    Title = "Selection",
                    Start = arg.StartStr,
                    End = arg.EndStr,
                    AllDay = arg.AllDay,
                    BackgroundColor = "#0d9488"
                )
            )
            |> ignore
            refreshInfo ())

    let onUnselect (_: DateUnselectArg) =
        appendLog "unselect"

    let onEventClick (arg: EventClickArg) =
        selected.Value <- arg.Event.Title
        appendLog ("eventClick \"" + arg.Event.Title + "\" (id " + arg.Event.Id + ", " + arg.View.Type + ")")

    let onEventMouseEnter (arg: EventHoveringArg) =
        appendLog ("eventMouseEnter \"" + arg.Event.Title + "\"")

    let onEventMouseLeave (_: EventHoveringArg) = ()

    let onEventDrop (arg: EventDropArg) =
        appendLog (
            "eventDrop \"" + arg.Event.Title + "\" by "
            + string arg.Delta.Days + "d / " + string arg.Delta.Milliseconds + "ms"
        )
        refreshInfo ()

    let onEventResize (arg: EventResizeDoneArg) =
        appendLog ("eventResize \"" + arg.Event.Title + "\" start Δ " + string arg.StartDelta.Days + "d")
        refreshInfo ()

    let onEventAdd (arg: EventAddArg) =
        appendLog ("eventAdd \"" + arg.Event.Title + "\" (" + string arg.RelatedEvents.Length + " related)")
        refreshInfo ()

    let onEventChange (arg: EventChangeArg) =
        appendLog ("eventChange \"" + arg.Event.Title + "\"")
        refreshInfo ()

    let onEventRemove (arg: EventRemoveArg) =
        appendLog ("eventRemove \"" + arg.Event.Title + "\"")
        refreshInfo ()

    let onDatesSet (arg: DatesSetArg) =
        appendLog ("datesSet " + arg.StartStr + " → " + arg.EndStr + " (" + arg.TimeZone + ")")
        refreshInfo ()

    let onEventsSet (events: EventApi[]) =
        appendLog ("eventsSet " + string events.Length + " event(s)")

    let onLoading (isLoading: bool) =
        if isLoading then appendLog "loading…"

    let onWindowResize (_: WindowResizeProps) = refreshInfo ()

    // -------------------------------------------------------------------
    // Content generators
    // -------------------------------------------------------------------

    let dayHeaderContent (arg: DayHeaderContentArg) : obj =
        let cls = if arg.IsToday then "dh dh-today" else "dh"
        box (ObjCustomContent(Html = "<span class=\"" + cls + "\">" + arg.Text + "</span>"))

    let dayCellClassNames (arg: DayCellContentArg) : obj =
        box (
            if arg.IsOther then "cell-other"
            elif arg.IsToday then "cell-today"
            else "")

    let slotLabelContent (arg: SlotLabelContentArg) : obj =
        box (ObjCustomContent(Html = "<span class=\"slot-label\">" + arg.Text + "</span>"))

    let eventContent (arg: EventContentArg) : obj =
        let time =
            if arg.TimeText = "" then ""
            else "<span class=\"evt-time\">" + arg.TimeText + "</span>"
        box (ObjCustomContent(Html = "<span class=\"evt\"><span class=\"evt-title\">" + arg.Event.Title + "</span>" + time + "</span>"))

    let eventDidMount (arg: EventMountArg) =
        (As<HTMLElement> arg.El).SetAttribute("title", arg.Event.Title + " · click for details")

    let viewDidMount (arg: ViewMountArg) =
        appendLog ("viewDidMount " + arg.View.Type)

    // -------------------------------------------------------------------
    // Calendar options
    // -------------------------------------------------------------------

    let calendarOptions () =
        CalendarOptions(
            InitialView = "dayGridMonth",
            // The sample drives navigation from its own toolbar.
            HeaderToolbar = false,
            FooterToolbar = false,
            Selectable = true,
            SelectMirror = true,
            Editable = true,
            EventResizableFromStart = true,
            DayMaxEvents = true,
            NowIndicator = true,
            WeekNumbers = true,
            WeekNumberCalculation = WeekNumberCalculationEnums.ISO,
            SlotMinTime = "07:00:00",
            SlotMaxTime = "21:00:00",
            BusinessHours = [| EventRefiners(DaysOfWeek = [| 1; 2; 3; 4; 5 |], StartTime = "09:00", EndTime = "17:00") |],
            TitleFormat = NativeFormatterOptions(Year = NumericStyle.Numeric, Month = MonthStyle.Long),
            EventTimeFormat = NativeFormatterOptions(Hour = NumericStyle.TwoDigit, Minute = NumericStyle.TwoDigit, Hour12 = false),
            EventOrder = "start,-duration,allDay,title",
            Events = seedEvents (),
            Plugins = [|
                FullCalendar.InteractionPlugin
                FullCalendar.DayGridPlugin
                FullCalendar.TimegridPlugin
                FullCalendar.ListPlugin
                FullCalendar.MultimonthPlugin
                FullCalendar.CreatePlugin(PluginDefInput("sample-noop"))
            |],
            DateClick = onDateClick,
            Select = onSelect,
            Unselect = onUnselect,
            EventClick = onEventClick,
            EventMouseEnter = onEventMouseEnter,
            EventMouseLeave = onEventMouseLeave,
            EventDrop = onEventDrop,
            EventResize = onEventResize,
            EventAdd = onEventAdd,
            EventChange = onEventChange,
            EventRemove = onEventRemove,
            DatesSet = onDatesSet,
            EventsSet = onEventsSet,
            Loading = onLoading,
            WindowResize = onWindowResize,
            DayHeaderContent = dayHeaderContent,
            DayCellClassNames = dayCellClassNames,
            SlotLabelContent = slotLabelContent,
            EventContent = eventContent,
            EventDidMount = eventDidMount,
            ViewDidMount = viewDidMount
        )

    // -------------------------------------------------------------------
    // Calendar lifecycle
    // -------------------------------------------------------------------

    let createCalendar () =
        match JS.Document.GetElementById "calendar" with
        | null -> setStatus "The calendar container is missing."
        | el ->
            let cal = Calendar(As<HTMLElement> el, calendarOptions ())
            cal.Render()
            calendar <- Some cal
            setStatus "Calendar rendered with dayGrid, timeGrid, list and multiMonth plugins."

    let destroyCalendar () =
        withCalendar (fun c ->
            c.Destroy()
            calendar <- None
            info.Value <- "-"
            setStatus "Calendar destroyed.")

    // -------------------------------------------------------------------
    // Navigation and view switching (CalendarApi)
    // -------------------------------------------------------------------

    let changeView (name: string) =
        withCalendar (fun c ->
            c.ChangeView name
            setStatus ("changeView \"" + name + "\" → " + c.View.Type)
            refreshInfo ())

    let prev () = withCalendar (fun c -> c.Prev(); refreshInfo ())
    let next () = withCalendar (fun c -> c.Next(); refreshInfo ())
    let prevYear () = withCalendar (fun c -> c.PrevYear(); refreshInfo ())
    let nextYear () = withCalendar (fun c -> c.NextYear(); refreshInfo ())
    let today () = withCalendar (fun c -> c.Today(); refreshInfo ())

    let incrementDate () =
        withCalendar (fun c ->
            c.IncrementDate("1d")
            setStatus ("incrementDate \"1d\" → " + c.GetDate().ToDateString())
            refreshInfo ())

    let gotoDate () =
        withCalendar (fun c ->
            c.GotoDate("2026-12-25")
            c.ChangeView "dayGridMonth"
            setStatus ("gotoDate 2026-12-25 · formatIso = " + c.FormatIso(c.GetDate()))
            refreshInfo ())

    let selectRange () =
        withCalendar (fun c ->
            c.Select("2026-11-20", "2026-11-23")
            setStatus "select 2026-11-20 → 2026-11-23"
            refreshInfo ())

    let unselect () =
        withCalendar (fun c ->
            c.Unselect()
            setStatus "unselect() called")

    let updateSize () =
        withCalendar (fun c ->
            c.UpdateSize()
            setStatus "updateSize() called")

    let batchRender () =
        withCalendar (fun c ->
            c.BatchRendering(fun () ->
                c.ChangeView "timeGridWeek"
                c.GotoDate("2026-11-18"))
            setStatus "BatchRendering: changeView + gotoDate in one batch."
            refreshInfo ())

    let toggleNowIndicator () =
        withCalendar (fun c ->
            let next = not (As<bool> (c.GetOption "nowIndicator"))
            c.SetOption("nowIndicator", next)
            setStatus ("setOption nowIndicator = " + string next)
            refreshInfo ())

    let setTitle () =
        withCalendar (fun c ->
            c.SetOption(
                "headerToolbar",
                ToolbarInput(Left = "prev,next today", Center = "title", Right = "dayGridMonth,timeGridWeek,listWeek")
            )
            setStatus "setOption headerToolbar — FullCalendar's own toolbar is now shown."
            refreshInfo ())

    // -------------------------------------------------------------------
    // Events (CalendarApi + EventApi)
    // -------------------------------------------------------------------

    let addEvent () =
        withCalendar (fun c ->
            let added = c.AddEvent(nextEvent ())
            if Optional.isDefined added then
                let e = added.Value
                e.SetExtendedProp("addedBy", box "sample")
                setStatus ("addEvent \"" + e.Title + "\" (id " + e.Id + ")")
            else setStatus "addEvent returned nothing."
            refreshInfo ())

    let removeAll () =
        withCalendar (fun c ->
            c.RemoveAllEvents()
            setStatus "removeAllEvents()"
            refreshInfo ())

    let findFirstEvent () =
        withCalendar (fun c ->
            let found = c.GetEventById "kickoff"
            if Optional.isDefined found && not (isNull (box found.Value)) then
                let e = found.Value
                let json = e.ToJSON()
                setStatus ("getEventById \"kickoff\" → \"" + e.Title + "\" · toJSON: " + string (JSON.Stringify(json).Length) + " chars")
                info.Value <- string (e.FormatRange(NativeFormatterOptions(Month = MonthStyle.Short, Day = NumericStyle.Numeric)))
            else setStatus "No event with id \"kickoff\".")

    let mutateFirstEvent () =
        withCalendar (fun c ->
            let events = c.GetEvents()
            if events.Length = 0 then setStatus "There are no events to mutate."
            else
                let e = events.[0]
                let title = e.Title
                e.SetProp("title", box (title + " ✓"))
                e.MoveDates("1d")
                e.SetAllDay(true)
                setStatus ("Mutated \"" + title + "\" via setProp/moveDates/setAllDay.")
                refreshInfo ())

    let inspectFirstEvent () =
        withCalendar (fun c ->
            let events = c.GetEvents()
            if events.Length = 0 then setStatus "There are no events to inspect."
            else
                let e = events.[0]
                let start =
                    if Optional.isDefined e.Start && not (isNull (box e.Start.Value)) then e.Start.Value.ToDateString()
                    else "-"
                let finish =
                    if Optional.isDefined e.End && not (isNull (box e.End.Value)) then e.End.Value.ToDateString()
                    else "-"
                let range = e.FormatRange(NativeFormatterOptions(Month = MonthStyle.Short, Day = NumericStyle.Numeric))
                info.Value <-
                    "first event: " + e.Title
                    + " · start " + start
                    + " · end " + finish
                    + " · allDay " + string e.AllDay
                    + " · formatRange " + string range
                    + " · plain " + string (JSON.Stringify(e.ToPlainObject()).Length) + " chars"
                setStatus ("Inspected \"" + e.Title + "\"."))

    let removeEvent () =
        withCalendar (fun c ->
            let events = c.GetEvents()
            if events.Length = 0 then setStatus "There is nothing to remove."
            else
                let e = events.[0]
                let title = e.Title
                e.Remove()
                setStatus ("Removed \"" + title + "\" with EventApi.remove().")
                refreshInfo ())

    // -------------------------------------------------------------------
    // Event sources
    // -------------------------------------------------------------------

    let addEventSource () =
        withCalendar (fun c ->
            let source =
                c.AddEventSource(
                    EventSourceInputObject(
                        Id = "sample-source",
                        Events = [|
                            EventRefiners(Title = "From event source", Start = "2026-11-19", AllDay = true, BackgroundColor = "#64748b")
                            EventRefiners(Title = "Also from source", Start = "2026-11-21", AllDay = true, BackgroundColor = "#64748b")
                        |]
                    )
                )
            setStatus ("addEventSource → id \"" + source.Id + "\" with url \"" + source.Url + "\"")
            refreshInfo ())

    let refetchSources () =
        withCalendar (fun c ->
            c.RefetchEvents()
            setStatus "refetchEvents() on every source."
            refreshInfo ())

    let listSources () =
        withCalendar (fun c ->
            let sources = c.GetEventSources()
            info.Value <-
                sources
                |> Array.map (fun s -> "source " + s.Id + " (format " + s.Format + ")")
                |> String.concat " · "
            setStatus ("getEventSources → " + string sources.Length + " source(s)."))

    // -------------------------------------------------------------------
    // Formatting helpers (CalendarApi + top-level functions)
    // -------------------------------------------------------------------

    let formatDemo () =
        withCalendar (fun c ->
            let opts = NativeFormatterOptions(Year = NumericStyle.Numeric, Month = MonthStyle.Long, Day = NumericStyle.Numeric, Weekday = TextStyle.Long)
            let one = c.FormatDate("2026-11-16", opts)
            let range = c.FormatRange("2026-11-16", "2026-11-20", FormatRangeOptions(Separator = " until "))
            let iso = c.FormatIso(c.GetDate(), false)
            let topRange =
                FullCalendar.FormatRange("2026-12-24", "2026-12-26", FormatRangeOptions(Year = NumericStyle.Numeric, Month = MonthStyle.Short, Day = NumericStyle.Numeric))
            let codes = c.GetAvailableLocaleCodes()
            info.Value <-
                "formatDate: " + one
                + "\nformatRange: " + range
                + "\nformatIso: " + iso
                + "\nFullCalendar.formatRange: " + topRange
                + "\nlocales: " + string codes.Length + " (\"" + (if codes.Length > 0 then codes.[0] else "") + "\", …)"
            setStatus "Exercised formatDate / formatRange / formatIso / getAvailableLocaleCodes.")

    // -------------------------------------------------------------------
    // External draggable (interaction plugin)
    // -------------------------------------------------------------------

    let setupExternalDrag () =
        match JS.Document.GetElementById "external-events" with
        | null -> setStatus "The external events container is missing."
        | el ->
            let drag =
                ExternalDraggable(
                    As<HTMLElement> el,
                    ExternalDraggableSettings(ItemSelector = ".external-event", MinDistance = 2)
                )
            externalDrag <- Some drag
            setStatus "ExternalDraggable installed — drag the chips onto the calendar."

    let destroyExternalDrag () =
        match externalDrag with
        | Some drag ->
            drag.Destroy()
            externalDrag <- None
            setStatus "ExternalDraggable destroyed."
        | None -> setStatus "No external draggable installed."

    // -------------------------------------------------------------------
    // Entry point
    // -------------------------------------------------------------------

    [<SPAEntryPoint>]
    let Main () =
        // Restore the stored theme before the first render.
        let stored = try JS.Window.LocalStorage.GetItem(themeStorageKey) with _ -> null
        if stored = "dark" || stored = "light" then theme.Value <- stored
        applyTheme ()

        version.Value <-
            FullCalendar.Version
            + " · " + string FullCalendar.GlobalPlugins.Length + " global plugin(s)"
            + " · " + string FullCalendar.GlobalLocales.Length + " global locale(s)"

        IndexTemplate()
            .status(status.V)
            .logText(logText.V)
            .info(info.V)
            .selected(selected.V)
            .version(version.V)
            .themeLabel(themeLabel.V)
            .ToggleTheme(fun _ -> toggleTheme ())
            .CreateCalendar(fun _ -> createCalendar ())
            .DestroyCalendar(fun _ -> destroyCalendar ())
            .MonthView(fun _ -> changeView "dayGridMonth")
            .WeekView(fun _ -> changeView "timeGridWeek")
            .DayView(fun _ -> changeView "timeGridDay")
            .ListView(fun _ -> changeView "listWeek")
            .MultiMonthView(fun _ -> changeView "multiMonthYear")
            .Prev(fun _ -> prev ())
            .Next(fun _ -> next ())
            .PrevYear(fun _ -> prevYear ())
            .NextYear(fun _ -> nextYear ())
            .Today(fun _ -> today ())
            .IncrementDate(fun _ -> incrementDate ())
            .GotoDate(fun _ -> gotoDate ())
            .SelectRange(fun _ -> selectRange ())
            .Unselect(fun _ -> unselect ())
            .UpdateSize(fun _ -> updateSize ())
            .BatchRender(fun _ -> batchRender ())
            .ToggleNowIndicator(fun _ -> toggleNowIndicator ())
            .SetTitle(fun _ -> setTitle ())
            .AddEvent(fun _ -> addEvent ())
            .RemoveAll(fun _ -> removeAll ())
            .FindFirstEvent(fun _ -> findFirstEvent ())
            .MutateFirstEvent(fun _ -> mutateFirstEvent ())
            .InspectFirstEvent(fun _ -> inspectFirstEvent ())
            .RemoveEvent(fun _ -> removeEvent ())
            .AddEventSource(fun _ -> addEventSource ())
            .RefetchSources(fun _ -> refetchSources ())
            .ListSources(fun _ -> listSources ())
            .FormatDemo(fun _ -> formatDemo ())
            .SetupExternalDrag(fun _ -> setupExternalDrag ())
            .DestroyExternalDrag(fun _ -> destroyExternalDrag ())
            .Bind()

        // The template is bound now: `#calendar` and `#external-events` exist.
        createCalendar ()
        refreshInfo ()
        appendLog "Open the console: every action above is logged here."