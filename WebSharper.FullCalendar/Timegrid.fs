namespace WebSharper.FullCalendar

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module TimegridPlugin = 

    module CoreInterface = Core.Interfaces

    let AllDaySplitterKeyInfo = 
        Pattern.Config "AllDaySplitterKeyInfo" {
            Required = []
            Optional = [
                "allDay", T<obj>
                "timed", T<obj>
            ]
        }

    let AllDaySplitter =
        Class "AllDaySplitter"
        |=> Inherits Core.Splitter.[CoreInterface.SplittableProps]
        |+> Instance [
            "getKeyInfo" => T<unit> ^-> AllDaySplitterKeyInfo
            "getKeysForDateSpan" => CoreInterface.DateSpan?dateSpan ^-> !| T<string>
            "getKeysForEventDef" => CoreInterface.EventDef?eventDef ^-> !| T<string>
        ]

    let TimeSlatMeta =
        Class "TimeSlatMeta"
        |+> Instance [
            "date" =@ Core.DateMarker
            "time" =@ CoreInterface.Duration
            "key" =@ T<string>
            "isoTimeStr" =@ T<string>
            "isLabeled" =@ T<bool>
        ]
        |> Import "TimeSlatMeta" "@fullcalendar/timegrid"

    let TimeColsSlatsCoords =
        Class "TimeColsSlatsCoords"
        |+> Static [
            Constructor (Core.PositionCache?positions * CoreInterface.DateProfile?dateProfile * CoreInterface.Duration?slotDuration)
        ]
        |+> Instance [
            "positions" =@ Core.PositionCache
            "safeComputeTop" => Core.DateMarker?date ^-> T<float>
            "computeDateTop" => Core.DateMarker?``when`` * !?Core.DateMarker?startOfDayDate ^-> T<float>
            "computeTimeTop" => CoreInterface.Duration?duration ^-> T<float>
        ]
        |> Import "TimeColsSlatsCoords" "@fullcalendar/timegrid"

    let TimeColsViewState =
        Pattern.Config "TimeColsViewState" {
            Required = []
            Optional = [
                "slatCoords", TimeColsSlatsCoords.Type
            ]
        }

    let MaxEventProps =
        Pattern.Config "MaxEventProps" {
            Required = []
            Optional = [
                "dayMaxEvents", T<int> + T<bool>
                "dayMaxEventRows", T<int> + T<bool>
            ]
        }

    let State = 
        Pattern.Config "State" {
            Required = []
            Optional = [
                "slatCoords", T<obj>
            ]
        }

    let TimeColsView =

        let contentArgCallback = (CoreInterface.ChunkContentCallbackArgs?contentArg ^-> T<obj>)

        Class "TimeColsView"
        |=> Inherits CoreInterface.DateComponent.[CoreInterface.ViewProps, TimeColsViewState]
        |+> Instance [
            "allDaySplitter" =@ AllDaySplitter
            "headerElRef" =@ T<obj>
            "state" =@ State

            "renderSimpleLayout" =>
                T<obj>?headerRowContent * contentArgCallback?allDayContent * contentArgCallback?timeContent ^-> T<obj>
            "renderHScrollLayout" =>
                (T<obj>?headerRowContent * contentArgCallback?allDayContent * contentArgCallback?timeContent * T<int>?colCnt * T<int>?dayMinWidth * (!| TimeSlatMeta)?slatMetas * !?TimeColsSlatsCoords?slatCoords ^-> T<obj>)
            "handleScrollTopRequest" => T<float>?scrollTop ^-> T<unit>
            "getAllDayMaxEventProps" => T<unit> ^-> MaxEventProps
            "renderHeadAxis" => T<string>?rowKey * !?Core.CssDimValue?frameHeight ^-> T<obj>
            "renderTableRowAxis" => !?T<int>?rowHeight ^-> T<obj>
            "handleSlatCoords" => TimeColsSlatsCoords?slatCoords ^-> T<unit>
        ]
        |> Import "TimeColsView" "@fullcalendar/timegrid"

    let DayTimeColsView =
        Class "DayTimeColsView"
        |=> Inherits TimeColsView
        |+> Instance [
            "render" => T<unit> ^-> T<obj>
        ]
        |> Import "DayTimeColsView" "@fullcalendar/timegrid"

    let TimeColsSeg =
        Pattern.Config "TimeColsSeg" {
            Required = [
                "col", T<int>
                "start", T<Date>
                "end", T<Date>
            ]
            Optional = CoreInterface.SegFields
        }
        |> Import "TimeColsSeg" "@fullcalendar/timegrid"

    let DayTimeColsProps =
        Pattern.Config "DayTimeColsProps" {
            Required = [
                "dateProfile", CoreInterface.DateProfile.Type
                "dayTableModel", Core.DayTableModel.Type
                "axis", T<bool>
                "slotDuration", CoreInterface.Duration.Type
                "slatMetas", !| TimeSlatMeta
                "businessHours", CoreInterface.EventStore.Type
                "eventStore", CoreInterface.EventStore.Type
                "eventUiBases", CoreInterface.EventUiHash
                "dateSelection", !?CoreInterface.DateSpan
                "eventSelection", T<string>
                "eventDrag",!?CoreInterface.EventInteractionState
                "eventResize", !?CoreInterface.EventInteractionState
                "tableColGroupNode", Core.VNode
                "tableMinWidth", Core.CssDimValue
                "clientWidth", !?T<int>
                "clientHeight", !?T<int>
                "expandRows", T<bool>
                "forPrint", T<bool>
            ]
            Optional = [
                "onScrollTopRequest", (T<int>?scrollTop ^-> T<unit>)
                "onSlatCoords", (TimeColsSlatsCoords?slatCoords ^-> T<unit>)
            ]
        }


    let DayTimeCols =
        Class "DayTimeCols"
        |=> Inherits CoreInterface.DateComponent.[DayTimeColsProps, Core.DictionaryObj]
        |+> Instance [
            "render" => T<unit> ^-> T<obj>
        ]
        |> Import "DayTimeCols" "@fullcalendar/timegrid"

    let DayTimeColsSlicer =
        Class "DayTimeColsSlicer"
        |=> Inherits Core.Slicer.[TimeColsSeg, !|(!|CoreInterface.DateRange)]
        |+> Instance [
            "sliceRange" => CoreInterface.DateRange?range * (!| CoreInterface.DateRange)?dayRanges ^-> !| TimeColsSeg
        ]
        |> Import "DayTimeColsSlicer" "@fullcalendar/timegrid"

    let TimeColsProps =
        Pattern.Config "TimeColsProps" {
            Required = [
                "cells", !| CoreInterface.DayTableCell.Type
                "dateProfile", CoreInterface.DateProfile.Type
                "slotDuration", CoreInterface.Duration.Type
                "nowDate", Core.DateMarker
                "todayRange", CoreInterface.DateRange.Type
                "businessHourSegs", !| TimeColsSeg
                "bgEventSegs", !| TimeColsSeg
                "fgEventSegs", !| TimeColsSeg
                "dateSelectionSegs", !| TimeColsSeg
                "eventSelection", T<string>
                "eventDrag", !?CoreInterface.EventSegUiInteractionState
                "eventResize", !?CoreInterface.EventSegUiInteractionState
                "tableColGroupNode", Core.VNode
                "tableMinWidth", Core.CssDimValue
                "clientWidth", !?T<int>
                "clientHeight", !?T<int>
                "expandRows", T<bool>
                "nowIndicatorSegs", !| TimeColsSeg
                "forPrint", T<bool>
                "axis", T<bool>
                "slatMetas", !| TimeSlatMeta
            ]
            Optional = [
                "onScrollTopRequest", (T<int>?scrollTop ^-> T<unit>)
                "onSlatCoords", (TimeColsSlatsCoords?slatCoords ^-> T<unit>)
                "isHitComboAllowed", (CoreInterface.Hit?hit0 * CoreInterface.Hit?hit1 ^-> T<bool>)
            ]
        }

    let TimeColsState =
        Pattern.Config "TimeColsState" {
            Required = []
            Optional = [
                "slatCoords", !?TimeColsSlatsCoords
            ]
        }

    let TimeCols =
        Class "TimeCols"
        |=> Inherits CoreInterface.DateComponent.[TimeColsProps, TimeColsState]
        |+> Instance [
            "state" =@ State

            "render" => T<unit> ^-> T<obj>
            "handleRootEl" => !?T<HTMLElement>?el ^-> T<unit>
            "componentDidMount" => T<unit> ^-> T<unit>
            "componentDidUpdate" => TimeColsProps?prevProps ^-> T<unit>
            "componentWillUnmount" => T<unit> ^-> T<unit>
            "handleScrollRequest" => CoreInterface.ScrollRequest?request ^-> T<bool>
            "handleColCoords" => !?Core.PositionCache?colCoords ^-> T<unit>
            "handleSlatCoords" => !?TimeColsSlatsCoords?coords ^-> T<unit>
            "queryHit" => T<float>?left * T<float>?top ^-> CoreInterface.Hit
        ]
        |> Import "TimeCols" "@fullcalendar/timegrid"
