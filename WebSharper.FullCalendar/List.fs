namespace WebSharper.FullCalendar

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module ListPlugin = 

    module CoreInterface = Core.Interfaces   

    let ListViewState = 
        Pattern.Config "ListViewState" {
            Required = [
                "timeHeaderId", T<string>
                "eventHeaderId", T<string>
                "dateHeaderIdRoot", T<string>
            ]
            Optional = []
        }

    let ListView =
        Class "ListView"
        |=> Inherits CoreInterface.DateComponent.[CoreInterface.ViewProps, Core.DictionaryObj]
        |+> Instance [
            "state" =@ ListViewState
            "render" => T<unit> ^-> Core.VNode
            "setRootEl" => (!?T<HTMLElement>)?rootEl ^-> T<unit>
            "renderEmptyMessage" => T<unit> ^-> Core.VNode
            "renderSegList" => (!| CoreInterface.Seg)?allSegs * (!| Core.DateMarker)?dayDates ^-> Core.VNode
            "_eventStoreToSegs" => CoreInterface.EventStore?eventStore * CoreInterface.EventUiHash?eventUiBases * (!| CoreInterface.DateRange)?dayRanges ^-> !| CoreInterface.Seg
            "eventRangesToSegs" => (!| CoreInterface.EventRenderRange)?eventRanges * (!| CoreInterface.DateRange)?dayRanges ^-> !| T<obj>
            "eventRangeToSegs" => CoreInterface.EventRenderRange?eventRange * (!| CoreInterface.DateRange)?dayRanges ^-> !| T<obj>
        ]
        |> Import "ListView" "@fullcalendar/list"