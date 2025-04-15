namespace WebSharper.FullCalendar.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.FullCalendar

[<JavaScript>]
module Client =
    // The templates are loaded from the DOM, so you just can edit index.html
    // and refresh your browser, no need to recompile unless you add or remove holes.
    type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

    [<SPAEntryPoint>]
    let Main () =
        let renderCalendar() =
            let calendarEl = JS.Document.GetElementById("calendar") |> As<HTMLElement>
            let calendarOptions = CalendarOptions(
                InitialView = "dayGridMonth",
                Selectable = true,
                Editable = true,
                Events = [|
                    EventRefiners(
                        Title = "Meeting",
                        Start = "2025-04-12"
                    );
                    EventRefiners(
                        Title = "Birthday Party",
                        Start = "2025-04-18"
                    )
                |],
                DateClick = (fun (info: DateClickArg) ->
                    let title = JS.Window.Prompt("Enter your name:")
                    if not(isNull title) then
                        info.View.Calendar.AddEvent(
                            EventRefiners(
                                Title = title,
                                Start = info.DateStr,
                                AllDay = true
                            )
                        ) |> ignore
                ),
                Plugins = [|
                    FullCalendar.InteractionPlugin;
                    FullCalendar.DayGridPlugin
                |]
            )

            let calendar = Calendar(calendarEl, calendarOptions)
            calendar.Render()

        IndexTemplate.Main()
            .PageInit(fun () ->
                renderCalendar()
            )
            .Doc()
        |> Doc.RunById "main"
