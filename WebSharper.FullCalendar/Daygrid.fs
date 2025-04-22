namespace WebSharper.FullCalendar

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module DaygridPlugin = 
    module CoreInterfaces = Core.Interfaces
        
    let TableSeg = 
        Pattern.Config "TableSeg" {
            Required = []
            Optional = [
                "row", T<int>
                "firstCol", T<int>
                "lastCol", T<int>
            ] @ CoreInterfaces.SegFields // Inherits CoreInterfaces.Seg
        }
        |> Import "TableSeg" "@fullcalendar/daygrid"


    let DayTableProps =
        Pattern.Config "DayTableProps" {
            Required = []
            Optional = [
                "renderRowIntro", T<unit> ^-> Core.VNode
                "headerAlignElRef", T<HTMLElement>
                "clientWidth", T<int>
                "clientHeight", T<int>
                "dateProfile", CoreInterfaces.DateProfile.Type
                "dayTableModel", Core.DayTableModel.Type
                "nextDayThreshold", CoreInterfaces.Duration.Type
                "businessHours", CoreInterfaces.EventStore.Type
                "eventStore", CoreInterfaces.EventStore.Type
                "eventUiBases", CoreInterfaces.EventUiHash
                "dateSelection", !?CoreInterfaces.DateSpan.Type
                "eventSelection", T<string>
                "eventDrag", !?CoreInterfaces.EventInteractionState.Type
                "eventResize", !?CoreInterfaces.EventInteractionState.Type
                "colGroupNode", Core.VNode
                "tableMinWidth", Core.CssDimValue
                "dayMaxEvents", T<bool> + T<int>
                "dayMaxEventRows", T<bool> + T<int>
                "expandRows", T<bool>
                "showWeekNumbers", T<bool>
                "forPrint", T<bool>
            ]
        }

    let DayTable =
        Class "DayTable"
        |=> Inherits CoreInterfaces.DateComponent.[DayTableProps, CoreInterfaces.ViewContext]
        |+> Instance [
            "render" => T<unit> ^-> Core.VNode
        ]
        |> Import "DayTable" "@fullcalendar/daygrid"

    let DayTableSlicer =
        Class "DayTableSlicer"
        |=> Inherits Core.Slicer.[TableSeg, !| Core.DayTableModel]
        |+> Instance [
            "forceDayIfListItem" =@ T<bool>
            "sliceRange" => CoreInterfaces.DateRange?dateRange * Core.DayTableModel?model ^-> !| TableSeg
        ]
        |> Import "DayTableSlicer" "@fullcalendar/daygrid"

    let TableDateProfileGenerator =
        Class "TableDateProfileGenerator"
        |=> Inherits Core.DateProfileGenerator
        |+> Instance [
            "buildRenderRange" => T<obj>?currentRange * T<obj>?unit * T<obj>?isRangeAllDay ^-> CoreInterfaces.DateRange
        ]

    let BuildDayTableRenderRangeProps =
        Pattern.Config "BuildDayTableRenderRangeProps" {
            Required = [
                "currentRange", CoreInterfaces.DateRange.Type
                "snapToWeek", T<bool>
                "fixedWeekCount", T<bool>
                "dateEnv", Core.DateEnv.Type
            ]
            Optional = []
        }

    let buildDayTableRenderRange = BuildDayTableRenderRangeProps?props ^-> CoreInterfaces.DateRange

    let TableRowsPropsFields = [
        "dateProfile", CoreInterfaces.DateProfile.Type
        "cells", !| (!| CoreInterfaces.DayTableCell)
        "showWeekNumbers", T<bool>
        "clientWidth", T<int>
        "clientHeight", T<int>
        "businessHourSegs", !| TableSeg
        "bgEventSegs", !| TableSeg
        "fgEventSegs", !| TableSeg
        "dateSelectionSegs", !| TableSeg
        "eventSelection", T<string>
        "eventDrag", !?CoreInterfaces.EventSegUiInteractionState
        "eventResize", !?CoreInterfaces.EventSegUiInteractionState
        "dayMaxEvents", T<bool> + T<int>
        "dayMaxEventRows", T<bool> + T<int>
        "forPrint", T<bool>
        "renderRowIntro", T<unit> ^-> Core.VNode
        "isHitComboAllowed", CoreInterfaces.Hit?hit0 * CoreInterfaces.Hit?hit1 ^-> T<bool>
    ]

    let TableRowsProps =
        Pattern.Config "TableRowsProps" {
            Required = []
            Optional = TableRowsPropsFields
        }

    let TableRows =
        Class "TableRows"
        |=> Inherits CoreInterfaces.DateComponent.[TableRowsProps, Core.DictionaryObj]
        |+> Instance [
            "render" => T<unit> ^-> Core.VNode
            "componentDidMount" => T<unit> ^-> T<unit>
            "componentDidUpdate" => T<unit> ^-> T<unit>
            "componentWillUnmount" => T<unit> ^-> T<unit>
            "registerInteractiveComponent" => T<unit> ^-> T<unit>
            "prepareHits" => T<unit> ^-> T<unit>
            "queryHit" => T<float>?left * T<float>?top ^-> CoreInterfaces.Hit
        ]
        |> Import "TableRows" "@fullcalendar/daygrid"

    let TableProps = 
        Pattern.Config "TableProps" {
            Required = []
            Optional = (
                [
                    "colGroupNode", Core.VNode
                    "tableMinWidth", Core.CssDimValue
                    "expandRows", T<bool>
                    "headerAlignElRef", T<HTMLElement>
                ] @
                TableRowsPropsFields
            )
        }

    let Table =
        Class "Table"
        |=> Inherits CoreInterfaces.DateComponent.[TableProps, Core.DictionaryObj]
        |+> Instance [
            "render" => T<unit> ^-> Core.VNode
            "componentDidMount" => T<unit> ^-> T<unit>
            "componentDidUpdate" => TableProps?prevProps ^-> T<unit>
            "requestScrollReset" => T<unit> ^-> T<unit>
            "flushScrollReset" => T<unit> ^-> T<unit>
        ]
        |> Import "Table" "@fullcalendar/daygrid"

    let TableView =
        Generic - fun state ->
        Class "TableView"            
        |=> Inherits CoreInterfaces.DateComponent.[CoreInterfaces.ViewProps, state]
        |+> Instance [
            "renderSimpleLayout" => CoreInterfaces.ChunkConfigRowContent?headerRowContent * (CoreInterfaces.ChunkContentCallbackArgs ^-> Core.VNode)?bodyContent ^-> Core.VNode
            "renderHScrollLayout" => CoreInterfaces.ChunkConfigRowContent?headerRowContent * (CoreInterfaces.ChunkContentCallbackArgs ^-> Core.VNode)?bodyContent * T<int>?colCnt * T<int>?dayMinWidth^-> Core.VNode
        ]
        |> Import "TableView" "@fullcalendar/daygrid"

    let DayTableView =
        Class "DayTableView"
        |=> Inherits TableView.[Core.DictionaryObj]
        |+> Instance [
            "render" => T<unit> ^-> Core.VNode
        ]
        |> Import "DayTableView" "@fullcalendar/daygrid"