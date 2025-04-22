namespace WebSharper.FullCalendar

open WebSharper.InterfaceGenerator

module Definition =

    module CoreInterfaces = Core.Interfaces
    module CoreEnums = Core.Enums

    CoreInterfaces.CalendarListenerRefinersOptionalFields.AddRange InteractionPlugin.ListenerRefinersOptionalFields

    CoreInterfaces.CalendarOptions 
        |+> Pattern.RequiredFields []
        |+> Pattern.OptionalFields (
            CoreInterfaces.CalendarOptionRefinersOptionalFields @
            (CoreInterfaces.CalendarListenerRefinersOptionalFields |> List.ofSeq) @
            CoreInterfaces.BaseOptionsOptionalFields
        )
        |> ignore

    CoreInterfaces.LocaleInput
        |+> Pattern.OptionalFields (
            CoreInterfaces.CalendarOptionRefinersOptionalFields @
            (CoreInterfaces.CalendarListenerRefinersOptionalFields |> List.ofSeq) @
            CoreInterfaces.BaseOptionsOptionalFields
        )
        |> ignore

    let FullCalendar = 
        Class "FullCalendar"
        |+> Static [
            "interactionPlugin" =? CoreInterfaces.PluginDef
            |> ImportDefault "@fullcalendar/interaction"

            "dayGridPlugin" =? CoreInterfaces.PluginDef
            |> ImportDefault "@fullcalendar/daygrid"

            "listPlugin" =? CoreInterfaces.PluginDef
            |> ImportDefault "@fullcalendar/list"

            "multimonthPlugin" =? CoreInterfaces.PluginDef
            |> ImportDefault "@fullcalendar/multimonth"

            "timegridPlugin" =? CoreInterfaces.PluginDef
            |> ImportDefault "@fullcalendar/timegrid"
        ]

    let Assembly =
        Assembly [
            Namespace "WebSharper.FullCalendar" [
                FullCalendar
                // Core Classes
                Core.NamedTimeZoneImpl
                Core.DateEnv
                Core.CalendarImpl
                Core.EventImpl
                Core.EventSourceImpl
                Core.DateProfileGenerator
                Core.Emitter
                Core.ViewImpl
                Core.Theme
                Core.ScrollResponder
                Core.CalendarDataManager
                Core.Calendar
                Core.DelayedRunner
                Core.ContentContainer
                Core.MoreLinkContainer
                Core.DayCellContainer
                Core.ViewContainer
                Core.Store
                Core.CustomRenderingStore
                Core.JsonRequestError
                Core.PositionCache
                Core.ScrollController
                Core.ElementScrollController
                Core.WindowScrollController
                Core.SegHierarchy
                Core.CalendarRoot
                Core.DayHeader
                Core.TableDateCell
                Core.TableDowCell
                Core.DaySeriesModel
                Core.DayTableModel
                Core.Scroller
                Core.RefMap
                Core.SimpleScrollGrid
                Core.NowTimer
                Core.StandardEvent
                Core.EventContainer
                Core.BgEvent
                Core.Slicer
                Core.Splitter

                // Core Enums
                CoreEnums.MoreLinkSimpleActionEnums
                CoreEnums.WeekNumberCalculationEnums
                CoreEnums.Direction
                CoreEnums.WeekFormat
                CoreEnums.MeridiemFormat
                CoreEnums.OverflowValue

                // Core Interfaces
                CoreInterfaces.NoEventsContentArg
                CoreInterfaces.NoEventsMountArg
                CoreInterfaces.CalendarApi
                CoreInterfaces.EventApi
                CoreInterfaces.CalendarListenerRefiners
                CoreInterfaces.PluginDef
                CoreInterfaces.DateSpanApi
                CoreInterfaces.CalendarOptions
                CoreInterfaces.ViewContext
                CoreInterfaces.DateProfileGeneratorProps
                CoreInterfaces.PluginHooks
                CoreInterfaces.CalendarData
                CoreInterfaces.EventStore
                CoreInterfaces.DateRangeInput
                CoreInterfaces.DurationObjectInput
                CoreInterfaces.DatePointApi
                CoreInterfaces.Duration
                CoreInterfaces.ZonedMarker
                CoreInterfaces.Week
                CoreInterfaces.OrderSpec
                CoreInterfaces.BaseOptions
                CoreInterfaces.ViewOptions
                CoreInterfaces.ButtonTextCompoundInput
                CoreInterfaces.ButtonHintCompoundInput
                CoreInterfaces.EventRefiners
                CoreInterfaces.Locale
                CoreInterfaces.CalendarSystem
                CoreInterfaces.ExpandedZonedMarker
                CoreInterfaces.VerboseFormattingArg
                CoreInterfaces.DateFormattingContext
                CoreInterfaces.DateFormatter
                CoreInterfaces.NativeFormatterOptions
                CoreInterfaces.DragMetaRefiners
                CoreInterfaces.ButtonIconsInput
                CoreInterfaces.CustomButtonInput
                CoreInterfaces.ToolbarInput
                CoreInterfaces.ObjCustomContent
                CoreInterfaces.ViewApi
                CoreInterfaces.DateMeta
                CoreInterfaces.DayHeaderContentArg
                CoreInterfaces.DayCellContentArg
                CoreInterfaces.SlotLaneContentArg
                CoreInterfaces.SlotLabelContentArg
                CoreInterfaces.AllDayContentArg
                CoreInterfaces.WeekNumberContentArg
                CoreInterfaces.ViewContentArg
                CoreInterfaces.NowIndicatorContentArg
                CoreInterfaces.LocaleInput
                CoreInterfaces.RangeApi
                CoreInterfaces.EventUiRefiners
                CoreInterfaces.EventContentArg
                CoreInterfaces.DayHeaderMountArg
                CoreInterfaces.DayCellMountArg
                CoreInterfaces.WeekNumberMountArg
                CoreInterfaces.ViewMountArg
                CoreInterfaces.NowIndicatorMountArg
                CoreInterfaces.EventMountArg
                CoreInterfaces.SlotLaneMountArg
                CoreInterfaces.SlotLabelMountArg
                CoreInterfaces.AllDayMountArg
                CoreInterfaces.ParsedMarkerResult
                CoreInterfaces.GreatestUnit
                CoreInterfaces.NamedTimeZoneImplClass
                CoreInterfaces.DateEnvSettings
                CoreInterfaces.DateMarkerMeta
                CoreInterfaces.DateOptions
                CoreInterfaces.DateRangeFormatOptions
                CoreInterfaces.WindowResizeProps
                CoreInterfaces.RangeApiWithTimeZone
                CoreInterfaces.DatesSetArg
                CoreInterfaces.EventAddArg
                CoreInterfaces.EventChangeArg
                CoreInterfaces.EventDropArg
                CoreInterfaces.EventRemoveArg
                CoreInterfaces.EventClickArg
                CoreInterfaces.EventHoveringArg
                CoreInterfaces.DateSelectArg
                CoreInterfaces.DateUnselectArg
                CoreInterfaces.OpenDateRange
                CoreInterfaces.DateRange
                CoreInterfaces.DateProfile
                CoreInterfaces.RecurringDef
                CoreInterfaces.EventUi
                CoreInterfaces.EventDef
                CoreInterfaces.EventInstance
                CoreInterfaces.EventSource
                CoreInterfaces.OpenDateSpan
                CoreInterfaces.DateSpan
                CoreInterfaces.EventInteractionState
                CoreInterfaces.CalendarDataManagerState
                CoreInterfaces.ViewProps
                CoreInterfaces.ViewSpec
                CoreInterfaces.CalendarOptionsData
                CoreInterfaces.AdjustedRange
                CoreInterfaces.CurrentRangeInfo
                CoreInterfaces.RangeFromDayCount
                CoreInterfaces.DateProfileOptions
                CoreInterfaces.DateProfileGeneratorClass
                CoreInterfaces.CalendarCurrentViewData
                CoreInterfaces.CalendarDataBase
                CoreInterfaces.CalendarContext
                CoreInterfaces.ReducerFuncContext
                CoreInterfaces.EventRefined
                CoreInterfaces.PointerDragEvent
                CoreInterfaces.EventMutation
                CoreInterfaces.Rect
                CoreInterfaces.ScrollRequest
                CoreInterfaces.Hit
                CoreInterfaces.PureComponent
                CoreInterfaces.BaseComponent
                CoreInterfaces.DateComponent
                CoreInterfaces.InteractionSettingsInput
                CoreInterfaces.SpecificViewContentArg
                CoreInterfaces.SpecificViewMountArg
                CoreInterfaces.CalendarContentProps
                CoreInterfaces.ViewPropsTransformer
                CoreInterfaces.ViewPropsTransformerClass
                CoreInterfaces.SplittableProps
                CoreInterfaces.DragMeta
                CoreInterfaces.InteractionSettings
                CoreInterfaces.Interaction
                CoreInterfaces.InteractionClass
                CoreInterfaces.CalendarInteraction
                CoreInterfaces.CalendarInteractionClass
                CoreInterfaces.EventSourceFuncArg
                CoreInterfaces.EventSourceRefiners
                CoreInterfaces.EventSourceInputObject
                CoreInterfaces.CalendarOptionRefiners
                CoreInterfaces.EventSourceRefined
                CoreInterfaces.EventSourceFetchArg
                CoreInterfaces.EventSourceFetcherRes
                CoreInterfaces.EventSourceDef
                CoreInterfaces.ParsedRecurring
                CoreInterfaces.RecurringType
                CoreInterfaces.ChunkContentCallbackArgs
                CoreInterfaces.SectionConfig
                CoreInterfaces.ChunkConfig
                CoreInterfaces.ColProps
                CoreInterfaces.ScrollGridChunkConfig
                CoreInterfaces.ScrollGridSectionConfig
                CoreInterfaces.ColGroupConfig
                CoreInterfaces.ScrollGridProps
                CoreInterfaces.ScrollGridImpl
                CoreInterfaces.ElementDragging
                CoreInterfaces.ElementDraggingClass
                CoreInterfaces.EventSegment
                CoreInterfaces.MoreLinkArg
                CoreInterfaces.MoreLinkContentArg
                CoreInterfaces.MoreLinkMountArg
                CoreInterfaces.ElAttrs
                CoreInterfaces.ElAttrsProps
                CoreInterfaces.ElProps
                CoreInterfaces.CustomRendering
                CoreInterfaces.RawLocaleInfo
                CoreInterfaces.ProcessRawCalendarOptionsResult
                CoreInterfaces.ProcessRawViewOptionsResult
                CoreInterfaces.EventSetStart
                CoreInterfaces.EventSetEnd
                CoreInterfaces.EventSetDates
                CoreInterfaces.EventSetAllDay
                CoreInterfaces.EventPlainObjectSettings
                CoreInterfaces.CalendarDataManagerProps
                CoreInterfaces.EventSourceApi
                CoreInterfaces.ContentGeneratorProps
                CoreInterfaces.ContentContainerProps
                CoreInterfaces.MoreLinkContainerState
                CoreInterfaces.EventTuple
                CoreInterfaces.EventRenderRange
                CoreInterfaces.Seg
                CoreInterfaces.MoreLinkContainerProps
                CoreInterfaces.DayCellContainerProps
                CoreInterfaces.DayCellRenderPropsInput
                CoreInterfaces.ViewContainerProps
                CoreInterfaces.SegSpan
                CoreInterfaces.SegEntry
                CoreInterfaces.SegInsertion
                CoreInterfaces.SegRect
                CoreInterfaces.SegEntryGroup
                CoreInterfaces.CalendarRootProps
                CoreInterfaces.CalendarRootState
                CoreInterfaces.DayHeaderProps
                CoreInterfaces.TableDateCellProps
                CoreInterfaces.TableDowCellProps
                CoreInterfaces.DaySeriesSeg
                CoreInterfaces.DayTableSeg
                CoreInterfaces.DayTableCell
                CoreInterfaces.ScrollerProps
                CoreInterfaces.ScrollerLike
                CoreInterfaces.SimpleScrollGridSection
                CoreInterfaces.SimpleScrollGridProps
                CoreInterfaces.SimpleScrollGridState
                CoreInterfaces.ScrollerDims
                CoreInterfaces.NowTimerProps
                CoreInterfaces.NowTimerState
                CoreInterfaces.StandardEventProps
                CoreInterfaces.MinimalEventProps
                CoreInterfaces.EventContainerProps
                CoreInterfaces.BgEventProps
                CoreInterfaces.SliceableProps
                CoreInterfaces.EventSegUiInteractionState
                CoreInterfaces.SlicedProps

                // Daygrid Plugin
                DaygridPlugin.TableSeg
                DaygridPlugin.DayTableProps
                DaygridPlugin.DayTable
                DaygridPlugin.DayTableSlicer
                DaygridPlugin.TableDateProfileGenerator
                DaygridPlugin.BuildDayTableRenderRangeProps
                DaygridPlugin.TableRowsProps
                DaygridPlugin.TableRows
                DaygridPlugin.TableProps
                DaygridPlugin.Table
                DaygridPlugin.TableView
                DaygridPlugin.DayTableView

                // Interaction Plugin
                InteractionPlugin.PointerDragging
                InteractionPlugin.ElementMirror
                InteractionPlugin.ScrollGeomCache
                InteractionPlugin.AutoScroller
                InteractionPlugin.FeaturefulElementDragging
                InteractionPlugin.DateClickArg
                InteractionPlugin.EventDragStopArg
                InteractionPlugin.EventDragStartArg
                InteractionPlugin.EventResizeStartArg
                InteractionPlugin.EventResizeStopArg
                InteractionPlugin.EventResizeDoneArg
                InteractionPlugin.DropArg
                InteractionPlugin.EventReceiveArg
                InteractionPlugin.EventLeaveArg
                InteractionPlugin.ListenerRefiners
                InteractionPlugin.ExternalDraggableSettings
                InteractionPlugin.ExternalDraggable
                InteractionPlugin.InferredElementDragging
                InteractionPlugin.ThirdPartyDraggableSettings
                InteractionPlugin.ThirdPartyDraggable

                // List Plugin
                ListPlugin.ListView
                ListPlugin.ListViewState

                // Timegrid Plugin
                TimegridPlugin.TimeCols
                TimegridPlugin.TimeColsState
                TimegridPlugin.TimeColsProps
                TimegridPlugin.DayTimeColsSlicer
                TimegridPlugin.DayTimeCols
                TimegridPlugin.DayTimeColsProps
                TimegridPlugin.TimeColsSeg
                TimegridPlugin.DayTimeColsView
                TimegridPlugin.TimeColsView
                TimegridPlugin.State
                TimegridPlugin.MaxEventProps
                TimegridPlugin.TimeColsViewState
                TimegridPlugin.TimeColsSlatsCoords
                TimegridPlugin.TimeSlatMeta
                TimegridPlugin.AllDaySplitter
                TimegridPlugin.AllDaySplitterKeyInfo
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
