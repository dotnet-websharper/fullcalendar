﻿namespace WebSharper.FullCalendar

open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Core = 

    // Core
    let DateInput = T<Date> + T<string> + T<int> + T<int[]>
    let DateMarker = T<Date>
    let LocaleCodeArg = T<string> + T<string[]>
    let ClassNamesInput = T<string> + T<string[]>
    let CssDimValue = T<string> + T<int>
    let Dictionary t1 t2 = T<System.Collections.Generic.Dictionary<_, _>>.[t1, t2]
    let DictionaryObj = Dictionary T<string> T<obj>
    let ComponentChild = T<obj>
    let VNode = T<obj>
    let ComponentChildren = T<obj>

    let DateEnv = 
        Class "DateEnv" 
        |> Import "DateEnv" "@fullcalendar/core"

    let CalendarImpl =
        Class "CalendarImpl"  
        |> Import "CalendarImpl" "@fullcalendar/core"

    let EventImpl = 
        Class "EventImpl"
        |> Import "EventImpl" "@fullcalendar/core"

    let EventSourceImpl = 
        Class "EventSourceImpl"

    let DateProfileGenerator =
        Class "DateProfileGenerator"
        |> Import "DateProfileGenerator" "@fullcalendar/core"

    let Emitter =
        Generic - fun handlerFuncs ->
        Class "Emitter"
        |+> Instance [
            "setOptions" => !?handlerFuncs?options ^-> T<unit>
            "on" => T<string>?``type`` * (handlerFuncs + T<obj>)?handler ^-> T<unit>
            "off" => T<string>?``type`` * !?(handlerFuncs + T<obj>)?handler ^-> T<unit>
        ]       
        |> Import "Emitter" "@fullcalendar/core"

    let ViewImpl =
        Class "ViewImpl"

    let Theme =
        Class "Theme"
        |> Import "Theme" "@fullcalendar/core"

    let ScrollResponder =
        Class "ScrollResponder"
        |> Import "ScrollResponder" "@fullcalendar/core"

    let NamedTimeZoneImpl =
        Class "NamedTimeZoneImpl"
        |> Import "NamedTimeZoneImpl" "@fullcalendar/core"

    module Enums = 
        // Enums
        let MoreLinkSimpleActionEnums =
            Pattern.EnumStrings "MoreLinkSimpleActionEnums" [
                "popover"
                "week"
                "day"
                "timeGridWeek"
                "timeGridDay"
            ]

        let WeekNumberCalculationEnums = 
            Pattern.EnumStrings "WeekNumberCalculationEnums" [
                "local"
                "ISO"
            ]
    
        let Direction = 
            Pattern.EnumStrings "Direction" [
                "ltr" 
                "rtl"
            ]

        let WeekFormat =
            Pattern.EnumStrings "WeekFormat" ["long"; "short"; "narrow"; "numeric"]

        let MeridiemFormat = 
            Pattern.EnumStrings "MeridiemFormat" ["lowercase"; "short"; "narrow"]

        let OverflowValue =
            Pattern.EnumStrings "OverflowValue" [ "auto"; "hidden"; "scroll"; "visible" ]
            |> Import "OverflowValue" "@fullcalendar/core"

        // Enums

    module Interfaces = 

        // Interface

        module E = Enums

        let CalendarApi = 
            Class "CalendarApi"
            |> Import "CalendarApi" "@fullcalendar/core"

        let EventApi = 
            Class "EventApi"
            |> Import "EventApi" "@fullcalendar/core"

        let CalendarListenerRefiners = 
            Class "CalendarListenerRefiners"
            |> Import "CalendarListenerRefiners" "@fullcalendar/core"

        let PluginDef = 
            Class "PluginDef"
            |> Import "PluginDef" "@fullcalendar/core"

        let DateSpanApi = 
            Class "DateSpanApi"
            |> Import "DateSpanApi" "@fullcalendar/core"

        let ViewContext =
            Class "ViewContext" 
            |> Import "ViewContext" "@fullcalendar/core"

        let DateProfileGeneratorProps =
            Class "DateProfileGeneratorProps"

        let PluginHooks = 
            Class "PluginHooks"

        let CalendarData = 
            Class "CalendarData" 
            |> Import "CalendarData" "@fullcalendar/core"

        let EventStore =
            Class "EventStore" 
            |> Import "EventStore" "@fullcalendar/core"

        let DateRangeInput = 
            Pattern.Config "DateRangeInput" {
                Required = []
                Optional = [
                    "start", DateInput
                    "end", DateInput
                ]
            }
            |> Import "DateRangeInput" "@fullcalendar/core"

        let DurationObjectInput =
            Pattern.Config "DurationObjectInput" {
                Required = []
                Optional = [
                    "years", T<int>
                    "year", T<int>
                    "months", T<int>
                    "month", T<int>
                    "weeks", T<int>
                    "week", T<int>
                    "days", T<int>
                    "day", T<int>
                    "hours", T<int>
                    "hour", T<int>
                    "minutes", T<int>
                    "minute", T<int>
                    "seconds", T<int>
                    "second", T<int>
                    "milliseconds", T<int>
                    "millisecond", T<int>
                    "ms", T<int>
                ]
            }

        let DatePointApi =
            Pattern.Config "DatePointApi" {
                Required = []
                Optional = [
                    "date", T<Date>
                    "dateStr", T<string>
                    "allDay", T<bool>
                ]
            }
            |> Import "DatePointApi" "@fullcalendar/core"

        let Duration =
            Pattern.Config "Duration" {
                Required = []
                Optional = [
                    "years", T<int>
                    "months", T<int>
                    "days", T<int>
                    "milliseconds", T<int>
                    "specifiedWeeks", T<bool>
                ]
            }
            |> Import "Duration" "@fullcalendar/core"

        let ZonedMarker =
            Pattern.Config "ZonedMarker" {
                Required = [
                    "marker", DateMarker
                    "timeZoneOffset", T<int>
                ]
                Optional = []
            }

        let Week =
            Pattern.Config "Week" {
                Required = []
                Optional = [
                    "dow", T<int>
                    "doy", T<int>
                ]
            }           

        let CalendarListenersLoose = CalendarListenerRefiners

        let FieldSpecInputFunc t = t?a * t?b ^-> T<int>
        let FieldSpecInput t = T<string> + T<string[]> + FieldSpecInputFunc t + !| (FieldSpecInputFunc t)

        let OrderSpec = 
            Generic - fun t ->
            Pattern.Config "OrderSpec" {
                Required = []
                Optional = [
                    "field", T<string>
                    "order", T<int>
                    "func", FieldSpecInputFunc t
                ]
            }

        let BaseOptions = 
            Class "BaseOptions"
            |> Import "BaseOptionRefiners" "@fullcalendar/core"

        let CalendarOptions = 
            Class "CalendarOptions"
            |> Import "CalendarOptions" "@fullcalendar/core"

        let ViewOptions = 
            Class "ViewOptions"
            |> Import "ViewOptionsRefined" "@fullcalendar/core"

        let ViewOptionsMap = Dictionary T<string> ViewOptions

        let ButtonTextCompoundInput =
            Pattern.Config "ButtonTextCompoundInput" {
                Required = []
                Optional = [
                    "prev", T<string>
                    "next", T<string>
                    "prevYear", T<string>
                    "nextYear", T<string>
                    "today", T<string>
                    "month", T<string>
                    "week", T<string>
                    "day", T<string>
                    "viewOrCustomButton", T<obj[]>
                ]
            }
            |> Import "ButtonTextCompoundInput" "@fullcalendar/core"

        let ButtonHintCallback = T<obj[]>?args ^-> T<string>

        let ButtonHintCompoundInput =
            Pattern.Config "ButtonHintCompoundInput" {
                Required = []
                Optional = [
                    "prev", T<string> + ButtonHintCallback
                    "next", T<string> + ButtonHintCallback
                    "prevYear", T<string> + ButtonHintCallback
                    "nextYear", T<string> + ButtonHintCallback
                    "today", T<string> + ButtonHintCallback
                    "month", T<string> + ButtonHintCallback
                    "week", T<string> + ButtonHintCallback
                    "day", T<string> + ButtonHintCallback
                    "viewOrCustomButton", T<obj[]>
                ]
            }
            |> Import "ButtonHintCompoundInput" "@fullcalendar/core"

        let EventRefiners = 
            Pattern.Config "EventRefiners" {
                Required = []
                Optional = [
                    "extendedProps", DictionaryObj
                    "start", DateInput
                    "end", DateInput
                    "date", DateInput
                    "allDay", T<bool>
                    "id", T<string>
                    "groupId", T<string>
                    "title", T<string>
                    "url", T<string>
                    "interactive", T<bool>
                ]
            }
            |> Import "EventRefiners" "@fullcalendar/core"

        let AllowFunc = DateSpanApi?span * EventImpl?movingEvent ^-> T<bool>
        let ParseClassNames = ClassNamesInput?raw ^-> !|T<string>            

        let CalendarOptionsRefined = CalendarOptions
            
        let Locale =
            Pattern.Config "Locale" {
                Required = []
                Optional = [
                    "codeArg", LocaleCodeArg
                    "codes", T<string[]>
                    "week", Week.Type
                    "simpleNumberFormat", T<int>
                    "options", CalendarOptionsRefined.Type
                ]
            }

        let CalendarSystem = 
            Class "CalendarSystem"
            |+> Instance [
                "getMarkerYear" => DateMarker?d ^-> T<int>
                "getMarkerMonth" => DateMarker?d ^-> T<int>
                "getMarkerDay" => DateMarker?d ^-> T<int>
                "arrayToMarker" => T<int[]>?arr ^-> DateMarker
                "markerToArray" => DateMarker?d ^-> T<int[]>
            ]

        let ExpandedZonedMarker =
            Pattern.Config "ExpandedZonedMarker" {
                Required = []
                Optional = [
                    "array", T<int[]>
                    "year", T<int>
                    "month", T<int>
                    "day", T<int>
                    "hour", T<int>
                    "minute", T<int>
                    "second", T<int>
                    "millisecond", T<int>

                    // inherits ZonedMarker
                    "marker", DateMarker
                    "timeZoneOffset", T<int>
                ]
            }

        let VerboseFormattingArg =
            Pattern.Config "VerboseFormattingArg" {
                Required = []
                Optional = [
                    "date", ExpandedZonedMarker.Type
                    "start", ExpandedZonedMarker.Type
                    "end", ExpandedZonedMarker.Type
                    "timeZone", T<string>
                    "localeCodes", T<string[]>
                    "defaultSeparator",T<string>
                ]
            }
            |> Import "VerboseFormattingArg" "@fullcalendar/core"

        let CmdFormatterFunc = T<string>?cmd * VerboseFormattingArg?arg ^-> T<string>

        let DateFormattingContext =
            Pattern.Config "DateFormattingContext" {
                Required = []
                Optional = [
                    "timeZone", T<string>
                    "locale", Locale.Type
                    "calendarSystem", CalendarSystem.Type
                    "computeWeekNumber", DateMarker?d ^-> T<int>
                    "weekText", T<string>
                    "weekTextLong", T<string>
                    "cmdFormatter", CmdFormatterFunc
                    "defaultSeparator", T<string>
                ]
            }

        let DateFormatter =
            Class "DateFormatter" 
            |+> Instance [
                "format" => ZonedMarker?date * DateFormattingContext?context ^-> T<string>
                "formatRange" => (ZonedMarker?start * ZonedMarker?``end`` * DateFormattingContext?context * !?T<string>?betterDefaultSeparator) ^-> T<string>
            ]
            |> Import "DateFormatter" "@fullcalendar/core"

        let NativeFormatterOptionsOptionalFields = [
            "week", E.WeekFormat.Type
            "meridiem", E.MeridiemFormat + T<bool>                        
            "omitZeroMinute", T<bool>
            "omitCommas", T<bool>
            "separator", T<string>
        ]

        let NativeFormatterOptions =
            Pattern.Config "NativeFormatterOptions" {
                Required = []
                Optional = NativeFormatterOptionsOptionalFields
            }
            |> Import "NativeFormatterOptions" "@fullcalendar/core"

        let FormatDateOptionsOptionalFields = [
            "locale", T<string>
        ]

        let FormatDateOptions =
            Pattern.Config "FormatDateOptions" {
                Required = []
                Optional = FormatDateOptionsOptionalFields @ NativeFormatterOptionsOptionalFields
            }
            |> Import "FormatDateOptions" "@fullcalendar/core"

        let FormatRangeOptions =
            Pattern.Config "FormatRangeOptions" {
                Required = []
                Optional = [
                    "separator", T<string>
                    "isEndExclusive", T<bool>
                ] @ FormatDateOptionsOptionalFields @ NativeFormatterOptionsOptionalFields
            }
            |> Import "FormatRangeOptions" "@fullcalendar/core"

        let DurationInput = DurationObjectInput + T<string> + T<float>
        let CreateDuration = DurationInput?input * !?T<string>?unit ^-> !?Duration
        let FuncFormatterFunc = VerboseFormattingArg?arg ^-> T<string>
        let FormatterInput = NativeFormatterOptions + T<string> + FuncFormatterFunc   
        let CreateFormatter = FormatterInput?input ^-> DateFormatter 

        let DragMetaRefiners = 
            Pattern.Config "DragMetaRefiners" {
                Required = []
                Optional = [
                    "startTime", CreateDuration
                    "duration", CreateDuration
                    "create", T<bool>
                ]
            }

        let DragMetaInput = DragMetaRefiners + T<obj[]>

        let ButtonIconsInput = 
            Pattern.Config "ButtonIconsInput" {
                Required = []
                Optional = [
                    "prev", T<string>
                    "next", T<string>
                    "prevYear", T<string>
                    "nextYear", T<string>
                    "today", T<string>
                    "viewOrCustomButton", T<obj[]>
                ]
            }
            |> Import "ButtonIconsInput" "@fullcalendar/core"

        let CustomButtonInput = 
            Pattern.Config "CustomButtonInput" {
                Required = []
                Optional = [
                    "text", T<string>
                    "hint", T<string>
                    "icon", T<string>
                    "themeIcon", T<string>
                    "bootstrapFontAwesome", T<string>
                    "click", T<Dom.MouseEvent>?ev * T<HTMLElement>?element ^-> T<unit>
                ]
            }
            |> Import "CustomButtonInput" "@fullcalendar/core"

        let ToolbarInput = 
            Pattern.Config "ToolbarInput" {
                Required = []
                Optional = [
                    "left", T<string>
                    "center", T<string>
                    "right", T<string>
                    "start", T<string>
                    "end", T<string>
                ]
            }
            |> Import "ToolbarInput" "@fullcalendar/core"

        let ObjCustomContent = 
            Pattern.Config "ObjCustomContent" {
                Required = []
                Optional = [
                    "html", T<string>
                    "domNodes", T<obj[]>
                ]
            }

        let ViewApi = 
            Pattern.Config "ViewApi" {
                Required = []
                Optional = [
                    "calendar", CalendarApi.Type
                    "type", T<string>
                    "title", T<string>
                    "activeStart", T<Date>
                    "activeEnd", T<Date>
                    "currentStart", T<Date>
                    "currentEnd", T<Date>
                    "getOption", (T<string>?name) ^-> T<obj>
                ]
            }
            |> Import "ViewApi" "@fullcalendar/core"

        let DateMetaFields = [
            "dow", T<int>
            "isDisabled", T<bool>
            "isOther", T<bool>
            "isToday", T<bool>
            "isPast", T<bool>
            "isFuture", T<bool>
        ]

        let DateMeta = 
            Pattern.Config "DateMeta" {
                Required = DateMetaFields
                Optional = []
            }         
            
        let DayHeaderContentArgFields = [
            "date", T<Date>
            "view", ViewApi.Type
            "text", T<string>
            "otherProp", T<obj[]>
        ]

        let DayHeaderContentArg = 
            Pattern.Config "DayHeaderContentArg" {
                Required = []
                Optional = (
                    DayHeaderContentArgFields @ 
                    DateMetaFields // Inherits DateMeta
                )
            }
            |> Import "DayHeaderContentArg" "@fullcalendar/core"

        let DayCellContentArgFields = [
            "date", T<Date>
            "view", ViewApi.Type
            "dayNumberText", T<string>
            "extraProp", T<obj[]>
        ]

        let DayCellContentArg = 
            Pattern.Config "DayCellContentArg" {
                Required = []
                Optional = (
                    DayCellContentArgFields @ 
                    DateMetaFields // Inherits DateMeta
                )
            }
            |> Import "DayCellContentArg" "@fullcalendar/core"

        let SlotLaneContentArg = 
            Pattern.Config "SlotLaneContentArg" {
                Required = []
                Optional = (
                    [
                        "time", Duration.Type
                        "date", T<Date>
                        "view", ViewApi.Type
                    ] @ 
                    DateMetaFields // Inherits DateMeta
                )
            }
            |> Import "SlotLaneContentArg" "@fullcalendar/core"

        let SlotLabelContentArg = 
            Pattern.Config "SlotLabelContentArg" {
                Required = []
                Optional = [
                    "level", T<int>
                    "time", Duration.Type
                    "date", T<Date>
                    "view", ViewApi.Type
                    "text", T<string>
                ]
            }
            |> Import "SlotLabelContentArg" "@fullcalendar/core"

        let AllDayContentArg = 
            Pattern.Config "AllDayContentArg" {
                Required = []
                Optional = [
                    "view", ViewApi.Type
                    "text", T<string>
                ]
            }
            |> Import "AllDayContentArg" "@fullcalendar/core"

        let WeekNumberContentArgFields = [
            "num", T<int>
            "text", T<string>
            "date", T<Date>
        ]

        let WeekNumberContentArg = 
            Pattern.Config "WeekNumberContentArg" {
                Required = []
                Optional = WeekNumberContentArgFields
            }
            |> Import "WeekNumberContentArg" "@fullcalendar/core"

        let CustomContent = ComponentChildren + ObjCustomContent

        let WeekNumberCalculation = E.WeekNumberCalculationEnums + (T<Date>?m ^-> T<int>)

        let ViewContentArg = 
            Pattern.Config "ViewContentArg" {
                Required = []
                Optional = [
                    "view", ViewApi.Type
                ]
            }
            |> Import "ViewContentArg" "@fullcalendar/core"

        let NowIndicatorContentArg = 
            Pattern.Config "NowIndicatorContentArg" {
                Required = []
                Optional = [
                    "isAxis", T<bool>
                    "date", T<Date>
                    "view", ViewApi.Type
                ]
            }
            |> Import "NowIndicatorContentArg" "@fullcalendar/core"

        let LocaleInput = 
            Pattern.Config "LocaleInput" {
                Required = [
                    "code", T<string>
                ]
                Optional = []
            }
            |> Import "LocaleInput" "@fullcalendar/core"

        let RangeApi =
            Pattern.Config "RangeApi" {
                Required = []
                Optional = [
                    "start", T<Date>
                    "end", T<Date>
                    "startStr", T<string>
                    "endStr", T<string>
                ]
            }   
            
        let EventUiRefinersOptionalFields = [
            "display", T<string>
            "editable", T<bool>
            "startEditable", T<bool>
            "durationEditable", T<bool>
            "constraint", T<obj>
            "overlap", T<bool>
            "allow", AllowFunc
            "className", ParseClassNames
            "classNames", ParseClassNames
            "color", T<string>
            "backgroundColor", T<string>
            "borderColor", T<string>
            "textColor", T<string>
        ]

        let EventUiRefiners = 
            Pattern.Config "EventUiRefiners" {
                Required = []
                Optional = EventUiRefinersOptionalFields
            }

        let EventContentArgFields = [
            "event", EventImpl.Type
            "timeText", T<string>
            "backgroundColor", T<string>
            "borderColor", T<string>
            "textColor", T<string>
            "isDraggable", T<bool>
            "isStartResizable", T<bool>
            "isEndResizable", T<bool>
            "isMirror", T<bool>
            "isStart", T<bool>
            "isEnd", T<bool>
            "isPast", T<bool>
            "isFuture", T<bool>
            "isToday", T<bool>
            "isSelected", T<bool>
            "isDragging", T<bool>
            "isResizing", T<bool>
            "view", ViewApi.Type
        ]

        let EventContentArg = 
            Pattern.Config "EventContentArg" {
                Required = []
                Optional = EventContentArgFields
            }
            |> Import "EventContentArg" "@fullcalendar/core"

        let EventUiInput = EventUiRefiners
        let EventInput = EventUiInput + T<obj> + EventRefiners

        let DayHeaderMountArg = 
            Pattern.Config "DayHeaderMountArg" {
                Required = []
                Optional = (
                    DayHeaderContentArgFields @ [
                        "el", T<HTMLElement>
                    ]
                )
            }
            |> Import "DayHeaderMountArg" "@fullcalendar/core"

        let DayCellMountArg = 
            Pattern.Config "DayCellMountArg" {
                Required = []
                Optional = (
                    DayCellContentArgFields @ [
                        "el", T<HTMLElement>
                    ]
                )
            }
            |> Import "DayCellMountArg" "@fullcalendar/core"

        let WeekNumberMountArg = 
            Pattern.Config "WeekNumberMountArg" {
                Required = []
                Optional = (
                    WeekNumberContentArgFields @ [
                        "el", T<HTMLElement>
                    ]
                )
            }
            |> Import "WeekNumberMountArg" "@fullcalendar/core"

        let ViewMountArg = 
            Pattern.Config "ViewMountArg" {
                Required = []
                Optional = [
                    "el", T<HTMLElement>
                    "view", ViewApi.Type
                ]
            }
            |> Import "ViewMountArg" "@fullcalendar/core"

        let NowIndicatorMountArg = 
            Pattern.Config "NowIndicatorMountArg" {
                Required = []
                Optional = [
                    "el", T<HTMLElement>
                    "isAxis", T<bool>
                    "date", T<Date>
                    "view", ViewApi.Type
                ]
            }
            |> Import "NowIndicatorMountArg" "@fullcalendar/core"

        let EventMountArg = 
            Pattern.Config "EventMountArg" {
                Required = []
                Optional = (
                    EventContentArgFields @ [
                        "el", T<HTMLElement>
                    ]
                )
            }
            |> Import "EventMountArg" "@fullcalendar/core"

        let SlotLaneMountArg = 
            Pattern.Config "SlotLaneMountArg" {
                Required = []
                Optional = [
                    "el", T<HTMLElement>
                    "time", Duration.Type
                    "date", T<Date>
                    "view", ViewApi.Type
                ]
            }
            |> Import "SlotLaneMountArg" "@fullcalendar/core"

        let SlotLabelMountArg = 
            Pattern.Config "SlotLabelMountArg" {
                Required = []
                Optional = [
                    "el", T<HTMLElement>
                    "level", T<int>
                    "time", Duration.Type
                    "date", T<Date>
                    "view", ViewApi.Type
                    "text", T<string>
                ]
            }
            |> Import "SlotLabelMountArg" "@fullcalendar/core"

        let AllDayMountArg = 
            Pattern.Config "AllDayMountArg" {
                Required = []
                Optional = [
                    "el", T<HTMLElement>
                    "view", ViewApi.Type
                    "text", T<string>
                ]
            }
            |> Import "AllDayMountArg" "@fullcalendar/core"

        let ParsedMarkerResult = 
            Pattern.Config "ParsedMarkerResult" {
                Required = [
                    "marker", T<Date>
                    "isTimeUnspecified", T<bool>
                    "forcedTzo", T<obj>
                ]
                Optional = []
            }

        let GreatestUnit =
            Pattern.Config "GreatestUnit" {
                Required = [ 
                    "unit", T<string>
                    "value", T<int> ]
                Optional = []
            }

        NamedTimeZoneImpl
            |+> Instance [
                "timeZoneName" =@ T<string>
                "offsetForArray" => T<float[]>?array ^-> T<int>
                "timestampToArray" => T<float>?ms ^-> !|T<float>
            ]
            |+> Static [
                Constructor (T<string>?timeZoneName)
            ]
            |> ignore            

        let NamedTimeZoneImplClass =
            Class "NamedTimeZoneImplClass"
            |+> Static [
                Constructor (T<string>?timeZoneName ^-> NamedTimeZoneImpl)
            ]

        let DateEnvSettings =
            Pattern.Config "DateEnvSettings" {
                Required = [
                    "timeZone", T<string>
                    "calendarSystem", T<string>
                    "locale", Locale.Type
                ]
                Optional = [
                    "namedTimeZoneImpl", NamedTimeZoneImplClass.Type
                    "weekNumberCalculation", WeekNumberCalculation
                    "firstDay", T<int>
                    "weekText", T<string>
                    "weekTextLong", T<string>
                    "cmdFormatter", CmdFormatterFunc
                    "defaultSeparator", T<string>
                ]
            }

        let DateMarkerMeta =
            Pattern.Config "DateMarkerMeta" {
                Required = [
                    "marker", DateMarker
                    "isTimeUnspecified", T<bool>
                    "forcedTzo", T<int>
                ]
                Optional = []
            }

        let DateOptions =
            Pattern.Config "DateOptions" {
                Required = []
                Optional = ["forcedTzo", T<int>]
            }

        let DateRangeFormatOptions = 
            Pattern.Config "DateRangeFormatOptions" {
                Required = []
                Optional = [
                    "forcedStartTzo", T<int>
                    "forcedEndTzo", T<int>
                    "isEndExclusive", T<bool>
                    "defaultSeparator", T<string>
                ]
            }                

        let WindowResizeProps = 
            Pattern.Config "WindowResizeProps" {
                Required = [
                    "view", ViewApi.Type
                ]
                Optional = []
            }

        let RangeApiWithTimeZone = 
            Pattern.Config "RangeApiWithTimeZone" {
                Required = []
                Optional = [
                    "timeZone", T<string>

                    // Inherits RangeApi
                    "start", T<Date>
                    "end", T<Date>
                    "startStr", T<string>
                    "endStr", T<string>
                ]
            }

        let DatesSetArg = 
            Pattern.Config "DatesSetArg" {
                Required = []
                Optional = [
                    "view", ViewApi.Type

                    // Inherits RangeApiWithTimeZone
                    "start", T<Date>
                    "end", T<Date>
                    "startStr", T<string>
                    "endStr", T<string>
                    "timeZone", T<string>
                ]
            }
            |> Import "DatesSetArg" "@fullcalendar/core"

        let EventAddArg =
            Pattern.Config "EventAddArg" {
                Required = [
                    "event", EventImpl.Type
                    "relatedEvents", !| EventImpl
                    "revert", T<unit> ^-> T<unit>
                ]
                Optional = []
            }
            |> Import "EventAddArg" "@fullcalendar/core"

        let EventChangeArg =
            Pattern.Config "EventChangeArg" {
                Required = [
                    "oldEvent", EventImpl.Type
                    "event", EventImpl.Type
                    "relatedEvents", !| EventImpl
                    "revert", T<unit> ^-> T<unit>
                ]
                Optional = []
            }
            |> Import "EventChangeArg" "@fullcalendar/core"

        let EventDropArg =
            Pattern.Config "EventDropArg" {
                Required = [
                    "el", T<HTMLElement>
                    "delta", Duration.Type
                    "jsEvent", T<Dom.MouseEvent>
                    "view", ViewApi.Type
                ]
                Optional = []
            }
            |> Import "EventDropArg" "@fullcalendar/core"

        let EventRemoveArg =
            Pattern.Config "EventRemoveArg" {
                Required = [
                    "event", EventImpl.Type
                    "relatedEvents", !| EventImpl
                    "revert", T<unit> ^-> T<unit>
                ]
                Optional = []
            }
            |> Import "EventRemoveArg" "@fullcalendar/core"

        let EventClickArg =
            Pattern.Config "EventClickArg" {
                Required = [
                    "el", T<HTMLElement>
                    "event", EventImpl.Type
                    "jsEvent", T<Dom.MouseEvent>
                    "view", ViewApi.Type
                ]
                Optional = []
            }
            |> Import "EventClickArg" "@fullcalendar/core"

        let EventHoveringArg =
            Pattern.Config "EventHoveringArg" {
                Required = [
                    "el", T<HTMLElement>
                    "event", EventImpl.Type
                    "jsEvent", T<Dom.MouseEvent>
                    "view", ViewApi.Type
                ]
                Optional = []
            }
            |> Import "EventHoveringArg" "@fullcalendar/core"

        let DateSelectArg = 
            Pattern.Config "DateSelectArg" {
                Required = []
                Optional = [
                    "jsEvent", !?T<Dom.MouseEvent>
                    "view", ViewApi.Type

                    // Inherits DateSpanApi
                    "allDay", T<bool>
                ]
            }
            |> Import "DateSelectArg" "@fullcalendar/core"

        let DateUnselectArg =
            Pattern.Config "DateUnselectArg" {
                Required = [
                    "jsEvent", T<Dom.MouseEvent>
                    "view", ViewApi.Type
                ]
                Optional = []
            }
            |> Import "DateUnselectArg" "@fullcalendar/core"

        let CalendarListeners = CalendarListenersLoose

        let OpenDateRange =
            Pattern.Config "OpenDateRange" {
                Required = []
                Optional = [
                    "start", !?DateMarker
                    "end", !?DateMarker
                ]
            }

        let DateRange =
            Pattern.Config "DateRange" {
                Required = [
                    "start", DateMarker
                    "end", DateMarker
                ]
                Optional = []
            }
            |> Import "DateRange" "@fullcalendar/core"

        let DateProfile =
            Pattern.Config "DateProfile" {
                Required = [
                    "currentDate", DateMarker
                    "isValid", T<bool>
                    "validRange", OpenDateRange.Type
                    "renderRange", DateRange.Type
                    "currentRange", DateRange.Type
                    "currentRangeUnit", T<string>
                    "isRangeAllDay", T<bool>
                    "dateIncrement", Duration.Type
                    "slotMinTime", Duration.Type
                    "slotMaxTime", Duration.Type
                ]
                Optional = [
                    "activeRange", !?DateRange.Type
                ]
            }
            |> Import "DateProfile" "@fullcalendar/core"

        let RecurringDef =
            Pattern.Config "RecurringDef" {
                Required = [
                    "typeId", T<int>
                    "typeData", T<obj>
                    "duration", !?Duration
                ]
                Optional = []
            }

        let Constraint = T<string> + EventStore + T<bool>

        let EventUi =
            Pattern.Config "EventUi" {
                Required = [
                    "backgroundColor", T<string>
                    "borderColor", T<string>
                    "textColor", T<string>
                    "classNames", !| T<string>
                ]
                Optional = [
                    "display", T<string>
                    "startEditable", T<bool>
                    "durationEditable", T<bool>
                    "constraints", !| Constraint
                    "overlap", T<bool>
                    "allows", !| AllowFunc
                ]
            }
            |> Import "EventUi" "@fullcalendar/core"

        let EventUiHash = !| (Dictionary T<string> EventUi.Type)

        let EventDef =
            Pattern.Config "EventDef" {
                Required = [
                    "defId", T<string>
                    "sourceId", T<string>
                    "publicId", T<string>
                    "groupId", T<string>
                    "allDay", T<bool>
                    "hasEnd", T<bool>
                    "recurringDef", !?RecurringDef
                    "title", T<string>
                    "url", T<string>
                    "ui", EventUi.Type
                    "extendedProps", T<obj>
                ]
                Optional = [
                    "interactive", T<bool>
                ]
            }
            |> Import "EventDef" "@fullcalendar/core"

        let EventDefHash = Dictionary T<string> EventDef

        let EventInstance =
            Pattern.Config "EventInstance" {
                Required = [
                    "instanceId", T<string>
                    "defId", T<string>
                    "range", DateRange.Type
                ]
                Optional = [
                    "forcedStartTzo", !?T<int>
                    "forcedEndTzo", !?T<int>
                ]
            }
            |> Import "EventInstance" "@fullcalendar/core"

        let EventInstanceHash = Dictionary T<string> EventInstance

        let EventInputTransformer = EventInput?input ^-> EventInput
        let EventSourceSuccessResponseHandler = CalendarImpl?this * T<obj>?rawData * T<obj>?response ^-> T<unit> + !|EventInput 
        let EventSourceErrorResponseHandler = T<Error>?error ^-> T<unit>

        let EventSource = Generic - fun t ->
            Pattern.Config "EventSource" {
                Required = [
                    "_raw", T<obj>
                    "sourceId", T<string>
                    "sourceDefId", T<int>
                    "meta", t.Type
                    "publicId", T<string>
                    "isFetching", T<bool>
                    "latestFetchId", T<string>
                    "eventDataTransform", EventInputTransformer
                    "ui", EventUi.Type
                    "extendedProps", DictionaryObj
                ]
                Optional = [
                    "fetchRange", !?DateRange.Type
                    "defaultAllDay", !?T<bool>
                    "success", EventSourceSuccessResponseHandler
                    "failure", EventSourceErrorResponseHandler
                ]
            }

        let EventSourceHash = Dictionary T<string> EventSource.[T<obj>]

        let OpenDateSpan =
            Pattern.Config "OpenDateSpan" {
                Required = [
                    "range", OpenDateRange.Type
                    "allDay", T<bool>
                ]
                Optional = [
                    "otherProp",T<obj[]>
                ]
            }

        let DateSpan = 
            Pattern.Config "DateSpan" {
                Required = []
                Optional = [
                    "allDay", T<bool>
                    "range", DateRange.Type
                    "otherProp", T<obj[]>
                ]
            }
            |> Import "DateSpan" "@fullcalendar/core"

        let EventInteractionState =
            Pattern.Config "EventInteractionState" {
                Required = [
                    "affectedEvents", EventStore.Type
                    "mutatedEvents", EventStore.Type
                    "isEvent", T<bool>
                ]
                Optional = []
            }
            |> Import "EventInteractionState" "@fullcalendar/core"

        let CalendarDataManagerStateFields = [
            "dynamicOptionOverrides", CalendarOptions.Type
            "currentViewType", T<string>
            "currentDate", DateMarker
            "dateProfile", DateProfile.Type
            "businessHours", EventStore.Type
            "eventSources", EventSourceHash
            "eventUiBases", EventUiHash
            "eventStore", EventStore.Type
            "renderableEventStore", EventStore.Type
            "eventSelection", T<string>
            "selectionConfig", EventUi.Type
            "dateSelection", DateSpan.Type
            "eventDrag", EventInteractionState.Type
            "eventResize", EventInteractionState.Type
        ]

        let CalendarDataManagerState =
            Pattern.Config "CalendarDataManagerState" {
                Required = []
                Optional = CalendarDataManagerStateFields
            }      
            
        let ViewPropsFields = [
            "dateProfile", DateProfile.Type
            "businessHours", EventStore.Type
            "eventStore", EventStore.Type
            "eventUiBases", EventUiHash
            "eventSelection", T<string>
            "isHeightAuto", T<bool>
            "forPrint", T<bool>
            "dateSelection", DateSpan.Type
            "eventDrag", EventInteractionState.Type
            "eventResize", EventInteractionState.Type
        ]

        let ViewProps =
            Pattern.Config "ViewProps" {
                Required = []
                Optional = ViewPropsFields
            }
            |> Import "ViewProps" "@fullcalendar/core"

        let ViewComponentType = ViewProps.Type + T<obj>

        let ViewConfigInput = ViewComponentType + ViewOptions

        let ViewSpec =
            Pattern.Config "ViewSpec" {
                Required = [
                    "type", T<string>
                    "component", ViewComponentType
                    "duration", Duration.Type
                    "durationUnit", T<string>
                    "singleUnit", T<string>
                    "optionDefaults", ViewOptions.Type
                    "optionOverrides", ViewOptions.Type
                    "buttonTextOverride", T<string>
                    "buttonTextDefault", T<string>
                    "buttonTitleOverride", T<string> + (T<obj[]> ^-> T<string>)
                    "buttonTitleDefault", T<string> + (T<obj[]> ^-> T<string>)
                ]
                Optional = []
            }
            |> Import "ViewSpec" "@fullcalendar/core"

        let ViewSpecHash = !| (Dictionary T<string> ViewSpec)

        let CalendarOptionsDataFields = [
            "localeDefaults", CalendarOptions.Type
            "calendarOptions", CalendarOptionsRefined.Type
            "toolbarConfig", T<obj>
            "availableRawLocales", T<obj>
            "dateEnv", DateEnv.Type
            "theme", Theme.Type
            "pluginHooks", PluginHooks.Type
            "viewSpecs", ViewSpecHash
        ]

        let CalendarOptionsData =
            Pattern.Config "CalendarOptionsData" {
                Required = []
                Optional = CalendarOptionsDataFields
            }

        let ViewOptionsRefined = ViewOptions

        let AdjustedRange =
            Pattern.Config "AdjustedRange" {
                Required = [
                    "start", T<Date>
                    "end", T<Date>
                ]
                Optional = []
            }

        let CurrentRangeInfo =
            Pattern.Config "CurrentRangeInfo" {
                Required = [
                    "duration", T<obj>
                    "unit", T<obj>
                    "range", T<obj>
                ]
                Optional = []
            }

        let RangeFromDayCount =
            Pattern.Config "RangeFromDayCount" {
                Required = [
                    "start", T<Date>
                    "end", T<Date>
                ]
                Optional = []
            }

        let DateProfileOptionsOptionalFields = [
            "slotMinTime", Duration.Type
            "slotMaxTime", Duration.Type
            "showNonCurrentDates", T<bool>
            "dayCount", T<int>
            "dateAlignment", T<string>
            "dateIncrement", Duration.Type
            "hiddenDays", !| T<int>
            "weekends", T<bool>
            "nowInput", DateInput + (T<unit> ^-> DateInput)
            "validRangeInput", DateRangeInput + (CalendarImpl * T<Date> ^-> DateRangeInput)
            "visibleRangeInput", DateRangeInput + (CalendarImpl * T<Date> ^-> DateRangeInput)
            "fixedWeekCount", T<bool>
        ]

        let DateProfileOptions =
            Pattern.Config "DateProfileOptions" {
                Required = []
                Optional = [
                    "slotMinTime", Duration.Type
                    "slotMaxTime", Duration.Type
                    "showNonCurrentDates", T<bool>
                    "dayCount", T<int>
                    "dateAlignment", T<string>
                    "dateIncrement", Duration.Type
                    "hiddenDays", !| T<int>
                    "weekends", T<bool>
                    "nowInput", DateInput + (T<unit> ^-> DateInput)
                    "validRangeInput", DateRangeInput + (CalendarImpl * T<Date> ^-> DateRangeInput)
                    "visibleRangeInput", DateRangeInput + (CalendarImpl * T<Date> ^-> DateRangeInput)
                    "fixedWeekCount", T<bool>
                ]
            }

        let DateProfileGeneratorClass =
            Class "DateProfileGeneratorClass" 
            |+> Static [
                Constructor (DateProfileGeneratorProps?props ^-> DateProfileGenerator)
            ]

        let CalendarCurrentViewDataFields = [
            "viewSpec", ViewSpec.Type
            "options", ViewOptionsRefined.Type
            "viewApi", ViewImpl.Type
            "dateProfileGenerator", DateProfileGenerator.Type
        ]

        let CalendarCurrentViewData =
            Pattern.Config "CalendarCurrentViewData" {
                Required = []
                Optional = CalendarCurrentViewDataFields
            }

        let CalendarDataBase = 
            Pattern.Config "CalendarDataBase" {
                Required = []
                Optional = (
                    CalendarOptionsDataFields @
                    CalendarCurrentViewDataFields @
                    CalendarDataManagerStateFields
                )
            }

        let CalendarContextOptionalFields = [
            "dateEnv", DateEnv.Type
            "options", BaseOptions.Type
            "pluginHooks", PluginHooks.Type
            "emitter", Emitter.[CalendarListeners]
            "dispatch", T<obj>?action ^-> T<unit>
            "getCurrentData", T<unit> ^-> CalendarData
            "calendarApi", CalendarImpl.Type
        ]

        let CalendarContext = 
            Pattern.Config "CalendarContext" {
                Required = []
                Optional = CalendarContextOptionalFields
            }
            |> Import "CalendarContext" "@fullcalendar/core"

        let LocaleSingularArg = LocaleCodeArg + LocaleInput
            
        let BusinessHoursInput = T<bool> + EventInput + !|EventInput
        let OverlapFunc = EventImpl?stillEvent * EventImpl?movingEvent ^-> T<bool>
        let ConstraintInput = T<string> + EventInput + !|EventInput  

        let ReducerFuncContext = 
            Pattern.Config "ReducerFuncContext" {
                Required = []
                Optional = (
                    CalendarContextOptionalFields @
                    CalendarDataManagerStateFields // Inherits CalendarDataManagerState
                )
            }

        let ReducerFunc = (DictionaryObj)?currentState * T<obj>?action * ReducerFuncContext?context ^-> DictionaryObj

        let GenericRefiners = Dictionary T<string> (T<obj>?input ^-> T<obj>)

        let EventUiRefined = EventUiRefiners

        let EventRefined = 
            Pattern.Config "EventRefined" {
                Required = []
                Optional = (
                    [
                        // Inherits EventRefiners
                        "extendedProps", DictionaryObj
                        "start", DateInput
                        "end", DateInput
                        "date", DateInput
                        "allDay", T<bool>
                        "id", T<string>
                        "groupId", T<string>
                        "title", T<string>
                        "url", T<string>
                        "interactive", T<bool>
                    ] @ EventUiRefinersOptionalFields
                )
            }
            |> Import "EventRefined" "@fullcalendar/core"

        let EventDefMemberAdder = (EventRefined?refined) ^-> EventDef

        let PointerDragEvent =
            Pattern.Config "PointerDragEvent" {
                Required = [
                    "origEvent", T<Dom.UIEvent>
                    "isTouch", T<bool>
                    "subjectEl", T<Dom.EventTarget>
                    "pageX", T<float>
                    "pageY", T<float>
                    "deltaX", T<float>
                    "deltaY", T<float>
                ]
                Optional = []
            }
            |> Import "PointerDragEvent" "@fullcalendar/core"

        let EventMutation =
            Pattern.Config "EventMutation" {
                Required = []
                Optional = [
                    "datesDelta", Duration.Type
                    "startDelta", Duration.Type
                    "endDelta", Duration.Type
                    "standardProps", T<obj>
                    "extendedProps", T<obj>
                ]
            }
            |> Import "EventMutation" "@fullcalendar/core"

        let Rect =
            Pattern.Config "Rect" {
                Required = [
                    "left", T<float>
                    "right", T<float>
                    "top", T<float>
                    "bottom", T<float>
                ]
                Optional = []
            }
            |> Import "Rect" "@fullcalendar/core"

        let ScrollRequest =
            Pattern.Config "ScrollRequest" {
                Required = []
                Optional = [
                    "time", Duration.Type
                    "otherProp", T<obj[]>
                ]
            }
            |> Import "ScrollRequest" "@fullcalendar/core"

        let ResizeHandler = T<bool>?force ^-> T<unit>
        let ScrollRequestHandler = ScrollRequest?request ^-> T<bool>

        let Hit =
            Pattern.Config "Hit" {
                Required = [
                    "dateProfile", DateProfile.Type
                    "dateSpan", DateSpan.Type
                    "dayEl", T<HTMLElement>
                    "rect", Rect.Type
                    "layer", T<int>
                ]
                Optional = [
                    "componentId", T<string>
                    "context", ViewContext.Type
                    "largeUnit", T<string>
                ]
            }
            |> Import "Hit" "@fullcalendar/core"

        let EqualityFunc t = t?a * t?b ^-> T<bool>
        let EqualityThing t = EqualityFunc t + T<bool>
        let EqualityFuncs t = Dictionary T<string> (EqualityThing t)

        let PureComponent =
            Generic -- fun props state ->
                Class "PureComponent"
                |+> Static [
                    "addPropsEquality" =@ T<obj>
                    "addStateEquality" =@ T<obj>
                    "contextType" =@ T<obj>
                ]
                |+> Instance [
                    "context" =@ ViewContext
                    "propEquality" =@ EqualityFuncs props
                    "stateEquality" =@ EqualityFuncs state
                    "debug" =@ T<bool>
                    "shouldComponentUpdate" => props?nextProps * state?nextState ^-> T<bool>
                    "safeSetState" => state?newState ^-> T<unit>
                ]

        let BaseComponent =
            Generic -- fun props state ->
            Class "BaseComponent"
            |=> Inherits PureComponent.[props, state]
            |+> Static [
                "contextType" =@ T<obj>
            ]
            |+> Instance [
                "context" =@ ViewContext
            ]
            |> Import "BaseComponent" "@fullcalendar/core"

        let DateComponent =
            Generic -- fun props state ->
            Class "DateComponent"
            |=> Inherits BaseComponent.[props, state]
            |+> Instance [
                "uid" =@ T<string>

                "prepareHits" => T<unit> ^-> T<unit>
                "queryHit" =>
                    T<float>?positionLeft *
                    T<float>?positionTop *
                    T<float>?elWidth *
                    T<float>?elHeight
                    ^-> !?Hit

                "isValidSegDownEl" => T<HTMLElement>?el ^-> T<bool>
                "isValidDateDownEl" => T<HTMLElement>?el ^-> T<bool>
            ]
            |> Import "DateComponent" "@fullcalendar/core"

        let InteractionSettingsInput =
            Pattern.Config "InteractionSettingsInput" {
                Required = [
                    "el", T<HTMLElement>
                ]
                Optional = [
                    "useEventCenter", T<bool>
                    "isHitComboAllowed", (Hit?hit0 * Hit?hit1 ^-> T<bool>)
                ]
            }
            |> Import "InteractionSettings" "@fullcalendar/core"

        let EventDragMutationMassager = EventMutation?mutation * Hit?hit0 * Hit?hit1 ^-> T<unit>
        let EventDropTransformer = EventMutation?mutation * CalendarContext?context ^-> DictionaryObj
        let EventIsDraggableTransformer = T<bool>?``val`` * EventDef?eventDef * EventUi?eventUi * CalendarContext?context ^-> T<bool>
        let EventDefMutationApplier = EventDef?eventDef * EventMutation?mutation * CalendarContext?context ^-> T<unit>
        let DateSelectionJoinTransformer = Hit?hit0 * Hit?hit1 ^-> T<obj>
        let DatePointTransform = DateSpan?dateSpan * CalendarContext?context ^-> T<obj>
        let DateSpanTransform = DateSpan?dateSpan * CalendarContext?context ^-> T<obj>
        let ViewConfigInputHash = Dictionary T<string> ViewConfigInput

        let SpecificViewContentArg =
            Pattern.Config "SpecificViewContentArg" {
                Required = []
                Optional = [
                    "nextDayThreshold", Duration.Type
                ] @ ViewPropsFields // Inherits ViewProps
            }
            |> Import "SpecificViewContentArg" "@fullcalendar/core"

        let SpecificViewMountArg =
            Pattern.Config "SpecificViewMountArg" {
                Required = []
                Optional = [
                    "nextDayThreshold", Duration.Type
                    "el", T<HTMLElement>
                ] @ ViewPropsFields // Inherits ViewProps
            }

        let CalendarDataFields = [
            "viewTitle", T<string>
            "calendarApi", CalendarImpl.Type
            "dispatch", T<obj>?action ^-> T<unit>
            "emitter", Emitter.[CalendarListeners]
            "getCurrentData", T<unit> ^-> TSelf
        ]

        let CalendarContentProps =
            Pattern.Config "CalendarContentProps" {
                Required = []
                Optional = (
                    [
                        "forPrint", T<bool>
                        "isHeightAuto", T<bool>
                    ] @
                    // Inherits CalendarData
                    CalendarDataManagerStateFields @
                    CalendarDataFields @
                    CalendarOptionsDataFields @
                    CalendarCurrentViewDataFields
                )
            }
            |> Import "CalendarContentProps" "@fullcalendar/core"

        let ViewPropsTransformer =
            Class "ViewPropsTransformer"
            |+> Instance [
                "transform" => ViewProps?viewProps * CalendarContentProps?calendarProps ^-> T<obj>
            ]
            |> Import "ViewPropsTransformer" "@fullcalendar/core"

        let ViewPropsTransformerClass = 
            Class "ViewPropsTransformerClass"
            |+> Static [
                Constructor (T<unit> ^-> ViewPropsTransformer)
            ]

        let SplittableProps =
            Pattern.Config "SplittableProps" {
                Required = [
                    "eventStore", EventStore.Type
                    "eventUiBases", EventUiHash
                    "eventSelection", T<string>
                ]
                Optional = [
                    "businessHours", EventStore.Type
                    "dateSelection", DateSpan.Type
                    "eventDrag", EventInteractionState.Type
                    "eventResize", EventInteractionState.Type
                ]
            }
            |> Import "SplittableProps" "@fullcalendar/core"

        let IsPropsValidTester = SplittableProps?props * CalendarContext?context ^-> T<bool>

        let DragMeta =
            Pattern.Config "DragMeta" {
                Required = [
                    "create", T<bool>
                    "sourceId", T<string>
                    "leftoverProps", DictionaryObj
                ]
                Optional = [
                    "startTime", Duration.Type
                    "duration", Duration.Type
                ]
            }
            |> Import "DragMeta" "@fullcalendar/core"

        let ExternalDefTransform = DateSpan?dateSpan * DragMeta?dragMeta ^-> T<obj>

        let ViewContainerAppend = CalendarContext?context ^-> ComponentChildren 
        let EventDropTransformers = EventMutation?mutation * CalendarContext?context ^-> DictionaryObj

        let InteractionSettings =
            Pattern.Config "InteractionSettings" {
                Required = [
                    "component", DateComponent.[T<obj>, DictionaryObj]
                    "el", T<HTMLElement>
                    "useEventCenter", T<bool>
                ]
                Optional = [
                    "isHitComboAllowed", (Hit?hit0 * Hit?hit1 ^-> T<bool>)
                ]
            }

        let Interaction =
            Class "Interaction"
            |+> Instance [
                "component" =@ DateComponent.[T<obj>, T<obj>]
                "isHitComboAllowed" =@ !?(Hit?hit0 * Hit?hit1 ^-> T<bool>)
                "destroy" => T<unit> ^-> T<unit>
            ]
            |+> Static [
                Constructor (InteractionSettings?settings)
            ]
            |> Import "Interaction" "@fullcalendar/core"

        let InteractionClass =
            Class "InteractionClass"
            |+> Static [
                Constructor (InteractionSettings?settings ^-> Interaction)
            ]

        let CalendarInteraction =
            Pattern.Config "CalendarInteraction" {
                Required = [
                    "destroy", T<unit> ^-> T<unit>
                ]
                Optional = []
            }

        let CalendarInteractionClass =
            Class "CalendarInteractionClass"
            |+> Static [
                Constructor (CalendarContext?context ^-> CalendarInteraction)
            ]

        let EventSourceFuncArg =
            Pattern.Config "EventSourceFuncArg" {
                Required = [
                    "start", T<Date>
                    "end", T<Date>
                    "startStr", T<string>
                    "endStr", T<string>
                    "timeZone", T<string>
                ]
                Optional = []
            }
            |> Import "EventSourceFuncArg" "@fullcalendar/core"

        let EventSourceFunc =

            let successCallback = (!|EventInput)?eventInput ^-> T<unit>
            let failureCallback = T<Error>?error ^-> T<unit>

            (EventSourceFuncArg?arg * successCallback?successCallback * failureCallback?failureCallback ^-> T<unit>) +
            (EventSourceFuncArg?arg ^-> T<Promise<_>>[!| EventInput])

        let EventSourceRefinersOptionalFields = [
            "method", T<string>
            "extraParams", (DictionaryObj + (T<unit> ^-> DictionaryObj))
            "startParam", T<string>
            "endParam", T<string>
            "timeZoneParam", T<string>

            "id", T<string>
            "defaultAllDay", T<bool>
            "url", T<string>
            "format", T<string>
            "events", (!| EventInput) + EventSourceFunc 
            "eventDataTransform", EventInputTransformer
            "success", EventSourceSuccessResponseHandler
            "failure", EventSourceErrorResponseHandler
        ]

        let EventSourceRefiners =
            Pattern.Config "EventSourceRefiners" {
                Required = []
                Optional = EventSourceRefinersOptionalFields
            }
            |> Import "EventSourceRefiners" "@fullcalendar/core"

        let EventSourceInputObject = 
            Pattern.Config "EventSourceInputObject" {
                Required = []
                Optional = (
                    EventSourceRefinersOptionalFields @
                    EventUiRefinersOptionalFields // inherits EventUiRefiners
                )
            }

        let EventSourceInput = EventSourceInputObject + !| EventInput + EventSourceFunc

        let CalendarOptionRefinersOptionalFields = [
            "buttonText", ButtonTextCompoundInput.Type
            "buttonHints", ButtonHintCompoundInput.Type
            "views", ViewOptionsMap
            "initialEvents", EventSourceInput
            "events", EventSourceInput
            "eventSources", (!| EventSourceInput)
        ]

        let CalendarOptionRefiners =
            Generic - fun t ->
            Pattern.Config "CalendarOptionRefiners" {
                Required = []
                Optional = CalendarOptionRefinersOptionalFields @ [
                    "plugins", (!| PluginDef)
                ]
            }

        let EventSourceRefined = 
            Pattern.Config "EventSourceRefined" {
                Required = []
                Optional = (
                    EventSourceRefinersOptionalFields @
                    EventUiRefinersOptionalFields // Inherits EventUiRefined
                )
            }
            |> Import "EventSourceRefined" "@fullcalendar/core"

        let EventSourceFetchArg =
            Generic - fun meta ->
            Pattern.Config "EventSourceFetchArg" {
                Required = [
                    "eventSource", EventSource.[meta]
                    "range", DateRange.Type
                    "isRefetch", T<bool>
                    "context", CalendarContext.Type
                ]
                Optional = []
            }

        let EventSourceFetcherRes =
            Pattern.Config "EventSourceFetcherRes" {
                Required = [
                    "rawEvents", !| EventInput
                ]
                Optional = [
                    "response", T<Response>
                ]
            }

        let EventSourceFetcher (meta:CodeModel.TypeParameter) = 
                
            let successCallback = EventSourceFetcherRes?res ^-> T<unit>
            let errorCallback = T<Error>?error ^-> T<unit>

            EventSourceFetchArg.[meta]?arg * successCallback?successCallback * errorCallback?errorCallback ^-> T<unit>

        let EventSourceDef =
            Generic - fun meta ->
            Pattern.Config "EventSourceDef" {
                Required = [
                    "parseMeta", EventSourceRefined?refined ^-> !?meta
                    "fetch", EventSourceFetcher meta
                ]
                Optional = [
                    "ignoreRange", T<bool>
                ]
            }
            |> Import "EventSourceDef" "@fullcalendar/core"

        let ParsedRecurring = 
            Generic - fun recurringData ->
                Pattern.Config "ParsedRecurring" {
                    Required = [
                        "typeData", recurringData.Type
                    ]
                    Optional = [
                        "allDayGuess", T<bool>
                        "duration", Duration.Type
                    ]
                }

        let RecurringType =
            Generic - fun recurringData ->
            Pattern.Config "RecurringType" {
                Required = [
                    "parse", EventRefined?refined * DateEnv?dateEnv ^-> !?ParsedRecurring.[recurringData]
                    "expand", T<obj>?typeData * DateRange?framingRange * DateEnv?dateEnv ^-> !| DateMarker
                ]
                Optional = []
            }
            |> Import "RecurringType" "@fullcalendar/core"

        let ChunkContentCallbackArgs =
            Pattern.Config "ChunkContentCallbackArgs" {
                Required = [
                    "tableColGroupNode", VNode
                    "tableMinWidth", CssDimValue
                    "expandRows", T<bool>
                    "syncRowHeights", T<bool>
                    "rowSyncHeights", !| T<int>
                    "reportRowHeightChange", (T<HTMLElement>?rowEl * T<bool>?isStable ^-> T<unit>)
                ]
                Optional = [
                    "clientWidth", T<int>
                    "clientHeight", T<int>
                ]
            }
            |> Import "ChunkContentCallbackArgs" "@fullcalendar/core"

        let OptionChangeHandler = T<obj>?propValue * CalendarContext?context ^-> T<unit>
        let OptionChangeHandlerMap = Dictionary T<string> OptionChangeHandler
            
        let ChunkConfigContent = ChunkContentCallbackArgs?contentProps ^-> VNode
        let ChunkConfigRowContent = VNode + ChunkConfigContent

        let SectionConfigOptionalFields = [
            "type", T<string>
            "outerContent", VNode
            "className", T<string>
            "maxHeight", T<int>
            "liquid", T<bool>
            "expandRows", T<bool>
            "syncRowHeights", T<bool>
            "isSticky", T<bool>
        ]

        let SectionConfig =
            Pattern.Config "SectionConfig" {
                Required = []
                Optional = SectionConfigOptionalFields
            }

        let ChunkConfigOptionalFields = [
            "elRef", T<obj>
            "outerContent", VNode
            "content", ChunkConfigContent
            "rowContent", ChunkConfigRowContent
            "scrollerElRef", T<obj>
            "tableClassName", T<string>
        ]

        let ChunkConfig =
            Pattern.Config "ChunkConfig" {
                Required = []
                Optional = ChunkConfigOptionalFields
            }

        let ColProps =
            Pattern.Config "ColProps" {
                Required = []
                Optional = [
                    "width", CssDimValue
                    "minWidth", CssDimValue
                    "span", T<int>
                ]
            }
            |> Import "ColProps" "@fullcalendar/core"

        let ScrollGridChunkConfig = 
            Pattern.Config "ScrollGridChunkConfig" {
                Required = [
                    "key", T<string>
                ]
                Optional = ChunkConfigOptionalFields // Inherits ChunkConfig
            }
            |> Import "ScrollGridChunkConfig" "@fullcalendar/core"

        let ScrollGridSectionConfig = 
            Pattern.Config "ScrollGridSectionConfig" {
                Required = [
                    "key", T<string>
                ]
                Optional = (
                    [ "chunks", !| ScrollGridChunkConfig ] @
                    SectionConfigOptionalFields // Inherits SectionConfig
                )
            }
            |> Import "ScrollGridSectionConfig" "@fullcalendar/core"

        let ColGroupConfig =
            Pattern.Config "ColGroupConfig" {
                Required = [
                    "cols", !| ColProps
                ]
                Optional = [
                    "width", CssDimValue
                ]
            }
            |> Import "ColGroupConfig" "@fullcalendar/core"

        let ScrollGridProps =
            Pattern.Config "ScrollGridProps" {
                Required = [
                    "sections", !| ScrollGridSectionConfig
                    "liquid", T<bool>
                    "forPrint", T<bool>
                    "collapsibleWidth", T<bool>
                ]
                Optional = [
                    "elRef", T<obj>
                    "colGroups", !| ColGroupConfig
                ]
            }
            |> Import "ScrollGridProps" "@fullcalendar/core"

        let ScrollGridImpl =
            Class "ScrollGridImpl" 
            |+> Static [
                Constructor (ScrollGridProps?props * ViewContext?context ^-> T<obj>)
            ]

        let GenericListenerRefiners = Dictionary T<string> (CalendarApi?this * T<obj[]>?args ^-> T<unit>)

        let ElementDragging =
            Class "ElementDragging"
            |+> Static [
                Constructor (T<HTMLElement>?el * !?T<string>?selector)
            ]
            |+> Instance [
                "emitter" =@ Emitter.[T<obj>]
                "destroy" => T<unit> ^-> T<unit>
                "setIgnoreMove" => T<bool>?value ^-> T<unit>
                "setMirrorIsVisible" => T<bool>?value ^-> T<unit>
                "setMirrorNeedsRevert" => T<bool>?value ^-> T<unit>
                "setAutoScrollEnabled" => T<bool>?value ^-> T<unit>
            ]
            |> Import "ElementDragging" "@fullcalendar/core"

        let ElementDraggingClass =
            Class "ElementDraggingClass"
            |+> Static [
                Constructor (T<HTMLElement>?el * !?T<string>?selector ^-> ElementDragging)
            ]

        let MoreLinkSimpleAction = E.MoreLinkSimpleActionEnums + T<string>

        let EventSegment =
            Pattern.Config "EventSegment" {
                Required = [
                    "event", EventApi.Type
                    "start", T<Date>
                    "end", T<Date>
                    "isStart", T<bool>
                    "isEnd", T<bool>
                ]
                Optional = []
            }
            |> Import "EventSegment" "@fullcalendar/core"

        let MoreLinkArg =
            Pattern.Config "MoreLinkArg" {
                Required = [
                    "date", T<Date>
                    "allDay", T<bool>
                    "allSegs", !| EventSegment.Type
                    "hiddenSegs", !| EventSegment.Type
                    "jsEvent", T<Dom.UIEvent>
                    "view", ViewApi.Type
                ]
                Optional = []
            }
            |> Import "MoreLinkArg" "@fullcalendar/core"

        let MoreLinkHandler = MoreLinkArg?arg ^-> MoreLinkSimpleAction + T<unit>
        let MoreLinkAction = MoreLinkSimpleAction + MoreLinkHandler            

        let MoreLinkContentArg =
            Pattern.Config "MoreLinkContentArg" {
                Required = [
                    "num", T<int>
                    "text", T<string>
                    "shortText", T<string>
                    "view", ViewApi.Type
                ]
                Optional = []
            }
            |> Import "MoreLinkContentArg" "@fullcalendar/core"

        let MoreLinkMountArg = 
            Pattern.Config "MoreLinkMountArg" {
                Required = []
                Optional = [
                    "el", T<HTMLElement>
                    // Inherits MoreLinkContentArg
                    "num", T<int>
                    "text", T<string>
                    "shortText", T<string>
                    "view", ViewApi.Type
                ]
            }
            |> Import "MoreLinkMountArg" "@fullcalendar/core"

        let ElRef = T<HTMLElement> + T<obj>

        let ElAttrs = 
            Pattern.Config "ElAttrs" {
                Required = []
                Optional = [
                    "ref", ElRef
                ]
            }

        let ElAttrsType = ElAttrs + T<obj>

        let ElAttrsPropsOptionalFields = [
            "elRef", ElRef
            "elClasses", !| T<string>
            "elStyle", T<obj> 
            "elAttrs", ElAttrsType
        ]

        let ElAttrsProps =
            Pattern.Config "ElAttrsProps" {
                Required = []
                Optional = ElAttrsPropsOptionalFields
            }

        let ElProps = 
            Pattern.Config "ElProps" {
                Required = [
                    "elTag", T<string>
                ]
                Optional = ElAttrsPropsOptionalFields // inherits ElAttrsProps
            }
            |> Import "ElProps" "@fullcalendar/core"

        let CustomRendering = 
            Generic - fun t ->
            Pattern.Config "CustomRendering" {
                Required = []
                Optional = [
                    "id", T<string>
                    "isActive", T<bool>
                    "containerEl", T<HTMLElement>
                    "reportNewContainerEl", (!?T<HTMLElement>?el ^-> T<unit>)
                    "generatorName", T<string>
                    "generatorMeta", T<obj>
                    "renderProps", t.Type
                    // Inherits ElProps
                    "elTag", T<string>
                ] @ ElAttrsPropsOptionalFields // Inherits ElProps
            }

        let CustomRenderingHandler (t:Type.Type) = CustomRendering.[t]?customRender ^-> T<unit>

        let LocaleInputMap = Dictionary T<string> LocaleInput

        let RawLocaleInfo =
            Pattern.Config "RawLocaleInfo" {
                Required = [
                    "map", LocaleInputMap
                    "defaultCode", T<string>
                ]
                Optional = []
            }

        let ProcessRawCalendarOptionsResult =
            Pattern.Config "ProcessRawCalendarOptionsResult" {
                Required = []
                Optional = [
                    "rawOptions", CalendarOptions.Type
                    "refinedOptions", CalendarOptionsRefined.Type
                    "pluginHooks", PluginHooks.Type
                    "availableLocaleData", RawLocaleInfo.Type
                    "localeDefaults", CalendarOptionsRefined.Type
                    "extra", T<obj>
                ]
            }

        let ProcessRawViewOptionsResult = 
            Pattern.Config "ProcessRawViewOptionsResult" {
                Required = []
                Optional = [
                    "rawOptions", ViewOptions.Type
                    "refinedOptions", ViewOptionsRefined.Type
                    "extra", T<obj>
                ]
            }   

        let EventSetStart = 
            Pattern.Config "EventSetStart" {
                Required = []
                Optional = [
                    "granularity", T<string>
                    "maintainDuration", T<bool>
                ]
            }

        let EventSetEnd = 
            Pattern.Config "EventSetEnd" {
                Required = []
                Optional = [
                    "granularity", T<string>
                ]
            }

        let EventSetDates = 
            Pattern.Config "EventSetDates" {
                Required = []
                Optional = [
                    "granularity", T<string>
                    "allDay", T<bool>
                ]
            }

        let EventSetAllDay = 
            Pattern.Config "EventSetAllDay" {
                Required = []
                Optional = [
                    "maintainDuration", T<bool>
                ]
            }

        let EventPlainObjectSettings = 
            Pattern.Config "EventPlainObjectSettings" {
                Required = []
                Optional = [
                    "collapseExtendedProps", T<bool>
                    "collapseColor", T<bool>
                ]
            }

        let CalendarDataManagerProps =
            Pattern.Config "CalendarDataManagerProps" {
                Required = [
                    "optionOverrides", CalendarOptions.Type
                    "calendarApi", CalendarImpl.Type
                ]
                Optional = [
                    "onAction", T<obj>?action ^-> T<unit>
                    "onData", CalendarData?data ^-> T<unit>
                ]
            }

        let EventSourceApi =
            Class "EventSourceApi"
            |+> Instance [
                "id" =@ T<string>
                "url" =@ T<string>
                "format" =@ T<string>
                "remove" => T<unit> ^-> T<unit>
                "refetch" => T<unit> ^-> T<unit>  
            ]
            |> Import "EventSourceApi" "@fullcalendar/core"

        // createFalsableFormatter from List plugin
        let createFalsableFormatter = (FormatterInput + T<bool>)?input ^-> DateFormatter

        let NoEventsContentArgRequiredFields = [
            "text", T<string>
            "view", ViewApi.Type
        ]

        // NoEventsContentArg from List plugin
        let NoEventsContentArg =
            Pattern.Config "NoEventsContentArg" {
                Required = NoEventsContentArgRequiredFields
                Optional = []
            }
            |> Import "NoEventsContentArg" "@fullcalendar/list"

        // NoEventsMountArg from List plugin
        let NoEventsMountArg = 
            Pattern.Config "NoEventsMountArg" {
                Required = NoEventsContentArgRequiredFields @ [
                    "el", T<HTMLElement>
                ]
                Optional = []
            }
            |> Import "NoEventsMountArg" "@fullcalendar/list"

        let BaseOptionsOptionalFields = [
            // Base options
            "navLinkDayClick", T<string> + (CalendarApi?this * T<Date>?date * T<Dom.UIEvent>?jsEvent ^-> T<unit>)
            "navLinkWeekClick", T<string> + (CalendarApi?this * T<Date>?weekStart * T<Dom.UIEvent>?jsEvent ^-> T<unit>)
            "duration", CreateDuration
            "bootstrapFontAwesome", T<bool> + ButtonIconsInput
            "buttonIcons", T<bool> + ButtonIconsInput
            "customButtons", Dictionary T<string> CustomButtonInput
            "defaultAllDayEventDuration", CreateDuration
            "defaultTimedEventDuration", CreateDuration
            "nextDayThreshold", CreateDuration
            "scrollTime", CreateDuration
            "scrollTimeReset", T<bool>
            "slotMinTime", CreateDuration
            "slotMaxTime", CreateDuration
            "dayPopoverFormat", CreateFormatter
            "slotDuration", CreateDuration
            "snapDuration", CreateDuration
            "headerToolbar", T<bool> + ToolbarInput
            "footerToolbar", T<bool> + ToolbarInput
            "defaultRangeSeparator", T<string>
            "titleRangeSeparator", T<string>
            "forceEventDuration", T<bool>
            "dayHeaders", T<bool>
            "dayHeaderFormat", CreateFormatter
            "dayHeaderClassNames", DayHeaderContentArg.Type
            "dayHeaderContent", DayHeaderContentArg.Type
            "dayHeaderDidMount", DayHeaderMountArg.Type
            "dayHeaderWillUnmount", DayHeaderMountArg.Type
            "dayCellClassNames",  DayCellContentArg.Type
            "dayCellContent", DayCellContentArg.Type
            "dayCellDidMount",  DayCellMountArg.Type
            "dayCellWillUnmount", DayCellMountArg.Type
            "initialView", T<string>
            "aspectRatio", T<float>
            "weekends", T<bool>
            "weekNumberCalculation", WeekNumberCalculation
            "weekNumbers", T<bool>
            "weekNumberClassNames", WeekNumberContentArg.Type
            "weekNumberContent", WeekNumberContentArg.Type
            "weekNumberDidMount",  WeekNumberMountArg.Type
            "weekNumberWillUnmount", WeekNumberMountArg.Type
            "editable", T<bool>
            "viewClassNames", ViewContentArg.Type
            "viewDidMount", ViewMountArg.Type
            "viewWillUnmount", ViewMountArg.Type
            "nowIndicator", T<bool>
            "nowIndicatorClassNames", NowIndicatorContentArg.Type
            "nowIndicatorContent", NowIndicatorContentArg.Type
            "nowIndicatorDidMount",  NowIndicatorMountArg.Type
            "nowIndicatorWillUnmount", NowIndicatorMountArg.Type
            "showNonCurrentDates", T<bool>
            "lazyFetching", T<bool>
            "startParam", T<string>
            "endParam", T<string>
            "timeZoneParam", T<string>
            "timeZone", T<string>
            "locales", !|LocaleInput
            "locale", LocaleSingularArg
            "themeSystem", T<string>
            "dragRevertDuration", T<int>
            "dragScroll", T<bool>
            "allDayMaintainDuration", T<bool>
            "unselectAuto", T<bool>
            "dropAccept", T<string> + (CalendarApi?this * T<obj>?draggable ^-> T<bool>) 
            "eventOrder", T<string> + T<string[]> + T<obj>
            "eventOrderStrict", T<bool>
            "handleWindowResize", T<bool>
            "windowResizeDelay", T<int>
            "longPressDelay", T<int>
            "eventDragMinDistance", T<int>
            "expandRows", T<bool>
            "height", CssDimValue
            "contentHeight", CssDimValue
            "direction", E.Direction.Type
            "weekNumberFormat", CreateFormatter
            "eventResizableFromStart", T<bool>
            "displayEventTime", T<bool>
            "displayEventEnd", T<bool>
            "weekText", T<string>
            "weekTextLong", T<string>
            "progressiveEventRendering", T<bool>
            "businessHours", BusinessHoursInput
            "initialDate", DateInput
            "now", DateInput + (CalendarApi?this ^-> DateInput)
            "eventDataTransform", EventInputTransformer
            "stickyHeaderDates", T<bool> + T<string>
            "stickyFooterScrollbar", T<bool> + T<string>
            "viewHeight", CssDimValue
            "defaultAllDay", T<bool>
            "eventSourceFailure", (CalendarApi?this * T<obj>?error) ^-> T<unit>
            "eventSourceSuccess", (CalendarApi?this * (!|EventInput)?eventsInput * !?T<Response>?response) ^-> !|EventInput
            "eventDisplay", T<string>
            "eventStartEditable", T<bool>
            "eventDurationEditable", T<bool>
            "eventOverlap", T<bool> + OverlapFunc
            "eventConstraint", ConstraintInput
            "eventAllow", AllowFunc
            "eventBackgroundColor", T<string>
            "eventBorderColor", T<string>
            "eventTextColor", T<string>
            "eventColor", T<string>
            "eventClassNames", EventContentArg.Type
            "eventContent", EventContentArg.Type
            "eventDidMount",  EventMountArg.Type
            "eventWillUnmount", EventMountArg.Type
            "selectConstraint", ConstraintInput
            "selectOverlap", T<bool> + OverlapFunc
            "selectAllow", AllowFunc
            "droppable", T<bool>
            "unselectCancel", T<string>
            "slotLabelFormat", FormatterInput + !|FormatterInput
            "slotLaneClassNames",  SlotLaneContentArg.Type
            "slotLaneContent", SlotLaneContentArg.Type
            "slotLaneDidMount", SlotLaneMountArg.Type
            "slotLaneWillUnmount", SlotLaneMountArg.Type
            "slotLabelClassNames",  SlotLabelContentArg.Type
            "slotLabelContent", SlotLabelContentArg.Type
            "slotLabelDidMount", SlotLabelMountArg.Type
            "slotLabelWillUnmount", SlotLabelMountArg.Type
            "dayMaxEvents", T<int> + T<bool>
            "dayMaxEventRows", T<int> + T<bool>
            "dayMinWidth", T<int>
            "slotLabelInterval", CreateDuration
            "allDayText", T<string>
            "allDayClassNames",  AllDayContentArg.Type
            "allDayContent", AllDayContentArg.Type
            "allDayDidMount", AllDayMountArg.Type
            "allDayWillUnmount", AllDayMountArg.Type
            "slotMinWidth", T<int>
            "navLinks", T<bool>
            "eventTimeFormat", CreateFormatter
            "rerenderDelay", T<int>
            "moreLinkText", T<string> + (CalendarApi?this * T<int>?num ^-> T<string>)
            "moreLinkHint", T<string> + (CalendarApi?this * T<int>?num ^-> T<string>)
            "selectMinDistance", T<int>
            "selectable", T<bool>
            "selectLongPressDelay", T<int>
            "eventLongPressDelay", T<int>
            "selectMirror", T<bool>
            "eventMaxStack", T<int>
            "eventMinHeight", T<int>
            "eventMinWidth", T<int>
            "eventShortHeight", T<int>
            "slotEventOverlap", T<bool>
            "plugins", !|PluginDef
            "firstDay", T<int>
            "dayCount", T<int>
            "dateAlignment", T<string>
            "dateIncrement", CreateDuration
            "hiddenDays", T<int[]>
            "fixedWeekCount", T<bool>
            "validRange", DateRangeInput + (CalendarApi?this * T<Date>?nowDate ^-> DateRangeInput)
            "visibleRange", DateRangeInput + (CalendarApi?this * T<Date>?currentDate ^-> DateRangeInput)
            "titleFormat", FormatterInput
            "eventInteractive", T<bool>
            "noEventsText", T<string>
            "viewHint", T<string> + (T<obj[]>?args ^-> T<string>)
            "navLinkHint", T<string> + (T<obj[]>?args ^-> T<string>)
            "closeHint", T<string>
            "timeHint", T<string>
            "eventHint", T<string>

            "moreLinkClick", MoreLinkAction
            "moreLinkClassNames", MoreLinkContentArg.Type
            "moreLinkContent", MoreLinkContentArg.Type
            "moreLinkDidMount", MoreLinkMountArg.Type
            "moreLinkWillUnmount", MoreLinkMountArg.Type
            "monthStartFormat",  CreateFormatter
            "handleCustomRendering", CustomRenderingHandler T<obj>
            "customRenderingMetaMap", T<obj[]>
            "customRenderingReplaces", T<bool>  
            // OptionRefiners from Interaction plugin
            "fixedMirrorParent", T<HTMLElement>

            // ExtraOptionRefiners from List plugin
            "listDayFormat",  createFalsableFormatter
            "listDaySideFormat",  createFalsableFormatter
            "noEventsClassNames", NoEventsContentArg.Type
            "noEventsContent", NoEventsContentArg.Type
            "noEventsDidMount", NoEventsMountArg.Type
            "noEventsWillUnmount", NoEventsMountArg.Type        

            // ExtraOptionRefiners from Multimonth plugin
            "multiMonthTitleFormat", CreateFormatter
            "multiMonthMaxColumns", T<int>
            "multiMonthMinWidth", T<int>

            // ExtraOptionRefiners from timegrid plugin
            "allDaySlot", T<bool>
        ]

        BaseOptions
        |+> Pattern.RequiredFields []
        |+> Pattern.OptionalFields BaseOptionsOptionalFields
        |> ignore
            
        // CalendarApi members
        CalendarApi 
            |+> Instance [
                "view" =@ ViewApi.Type
                "updateSize" => T<unit> ^-> T<unit>
                "setOption" => T<string>?name * CalendarOptions?value ^-> T<unit>
                "getOption" => T<string>?name ^-> CalendarOptions
                "getAvailableLocaleCodes" => T<unit> ^-> !| T<string>
                "on" => T<string>?handlerName * CalendarListeners?handler ^-> T<unit>
                "off" => T<string>?handlerName * CalendarListeners?handler ^-> T<unit>
                "trigger" => T<string>?handlerName * (!|CalendarListeners)?args ^-> T<unit>
                "changeView" => T<string>?viewType * !?(DateRangeInput + DateInput)?dateOrRange ^-> T<unit>
                "zoomTo" => T<Date>?dateMarker * !?T<string>?viewType ^-> T<unit>
                "prev" => T<unit> ^-> T<unit>
                "next" => T<unit> ^-> T<unit>
                "prevYear" => T<unit> ^-> T<unit>
                "nextYear" => T<unit> ^-> T<unit>
                "today" => T<unit> ^-> T<unit>
                "gotoDate" => DateInput?zonedDateInput ^-> T<unit>
                "incrementDate" => DurationInput?deltaInput ^-> T<unit>
                "getDate" => T<unit> ^-> T<Date>
                "formatDate" => DateInput?d * FormatterInput?formatter ^-> T<string>
                "formatRange" => DateInput?d0 * DateInput?d1 * T<obj>?settings ^-> T<string>
                "formatIso" => DateInput?d * !?T<bool>?omitTime ^-> T<string>
                "select" => (DateInput + T<obj>)?dateOrObj * !?DateInput?endDate ^-> T<unit>
                "unselect" => T<unit> ^-> T<unit>
                "addEvent" => EventInput?eventInput * !?(EventSourceApi + T<string> + T<bool>)?sourceInput ^-> !?EventApi
                "getEventById" => T<string>?id ^-> !?EventApi
                "getEvents" => T<unit> ^-> !| EventApi
                "removeAllEvents" => T<unit> ^-> T<unit>
                "getEventSources" => T<unit> ^-> !| EventSourceApi
                "getEventSourceById" => T<string>?id ^-> !?EventSourceApi
                "addEventSource" => EventSourceInput?sourceInput ^-> EventSourceApi
                "removeAllEventSources" => T<unit> ^-> T<unit>
                "refetchEvents" => T<unit> ^-> T<unit>
                "scrollToTime" => DurationInput?timeInput ^-> T<unit>
            ]
            |> ignore

        EventApi 
        |+> Instance [
            "source" =@ EventSourceApi
            "start" =@ !?T<Date>
            "end" =@ !?T<Date>
            "startStr" =@ T<string>
            "endStr" =@ T<string>
            "id" =@ T<string>
            "groupId" =@ T<string>
            "allDay" =@ T<bool>
            "title" =@ T<string>
            "url" =@ T<string>
            "display" =@ T<string>
            "startEditable" =@ T<bool>
            "durationEditable" =@ T<bool>
            "constraint" =@ T<obj>
            "overlap" =@ T<bool>
            "allow" =@ T<obj>
            "backgroundColor" =@ T<string>
            "borderColor" =@ T<string>
            "textColor" =@ T<string>
            "classNames" =@ T<string[]>
            "extendedProps" =@ DictionaryObj

            "setProp" => T<string>?name * T<obj>?value ^-> T<unit>
            "setExtendedProp" => T<string>?name * T<obj>?value ^-> T<unit>
            "setStart" => DateInput?startInput * !?EventSetStart?options ^-> T<unit>            
            "setEnd" => !?DateInput?endInput * !?EventSetEnd?options ^-> T<unit>  
            "setDates" => DateInput?startInput * !?DateInput?endInput * !?EventSetDates?options ^-> T<unit>  
            "moveStart" => DurationInput?deltaInput ^-> T<unit>  
            "moveEnd" => DurationInput?deltaInput ^-> T<unit>  
            "moveDates" => DurationInput?deltaInput ^-> T<unit>  
            "setAllDay" => T<bool>?allDay * !?EventSetAllDay?options ^-> T<unit>  
            "formatRange" => FormatterInput?formatInput ^-> T<obj> 
            "remove" => T<unit> ^-> T<unit> 
            "toPlainObject" => EventPlainObjectSettings?settings ^-> DictionaryObj
            "toJSON" => T<unit>  ^-> DictionaryObj
        ]
        |> ignore

        let CalendarListenerRefinersOptionalFields = ResizeArray [
            "datesSet", (DatesSetArg?arg ^-> T<unit>)
            "eventsSet", (!| EventApi ^-> T<unit>)
            "eventAdd", (EventAddArg?arg ^-> T<unit>)
            "eventChange", (EventChangeArg?arg ^-> T<unit>)
            "eventRemove", (EventRemoveArg?arg ^-> T<unit>)
            "windowResize", (WindowResizeProps?arg ^-> T<unit>)
            "eventClick", (EventClickArg?arg ^-> T<unit>)
            "eventMouseEnter", (EventHoveringArg?arg ^-> T<unit>)
            "eventMouseLeave", (EventHoveringArg?arg ^-> T<unit>)
            "select", (DateSelectArg?arg ^-> T<unit>)
            "unselect", (DateUnselectArg?arg ^-> T<unit>)
            "loading", (T<bool>?isLoading ^-> T<unit>)
            "_unmount", (T<unit> ^-> T<unit>)
            "_beforeprint", (T<unit> ^-> T<unit>)
            "_afterprint", (T<unit> ^-> T<unit>)
            "_noEventDrop", (T<unit> ^-> T<unit>)
            "_noEventResize", (T<unit> ^-> T<unit>)
            "_resize", (T<bool>?forced ^-> T<unit>)
            "_scrollRequest", (T<obj>?arg ^-> T<unit>)
        ]

        CalendarListenerRefiners
        |+> Pattern.RequiredFields []
        |+> Pattern.OptionalFields (CalendarListenerRefinersOptionalFields |> List.ofSeq)
        |> ignore

        let PluginHooksOptionalFields = [
            "premiumReleaseDate", T<Date> + T<unit>
            "reducers", !| ReducerFunc
            "isLoadingFuncs", !| (DictionaryObj?state ^-> T<bool>)
            "contextInit", !| (CalendarContext?context ^-> T<unit>)
            "eventRefiners", GenericRefiners
            "eventDefMemberAdders", !|EventDefMemberAdder
            "eventSourceRefiners", GenericRefiners
            "isDraggableTransformers", !| EventIsDraggableTransformer
            "EventDragMutationMassagers", !| EventDragMutationMassager 
            "eventDefMutationAppliers", !| EventDefMutationApplier
            "dateSelectionTransformers", !| DateSelectionJoinTransformer
            "datePointTransforms", !| DatePointTransform
            "dateSpanTransforms", !| DateSpanTransform
            "views", ViewConfigInputHash
            "viewPropsTransformers", !| ViewPropsTransformerClass
            "isPropsValid", IsPropsValidTester
            "externalDefTransforms", !| ExternalDefTransform
            "viewContainerAppends", !| ViewContainerAppend
            "eventDropTransformers", !| EventDropTransformers
            "componentInteractions", !| InteractionClass
            "calendarInteractions", !| CalendarInteractionClass
            "themeClasses", DictionaryObj
            "eventSourceDefs", !| EventSourceDef.[T<obj>]              
            "recurringTypes", !| RecurringType.[T<obj>]              
            "initialView", T<string>                
            "optionChangeHandlers", OptionChangeHandlerMap
            "scrollGridImpl", ScrollGridImpl.Type
            "listenerRefiners", GenericListenerRefiners
            "optionRefiners", GenericRefiners
            "propSetHandlers", DictionaryObj
            "cmdFormatter", CmdFormatterFunc
            "namedTimeZonedImpl", NamedTimeZoneImplClass.Type
            "elementDraggingImpl", ElementDraggingClass.Type
        ]

        PluginDef
        |+> Pattern.RequiredFields [
            "id", T<string>
            "name", T<string>
            "deps", !|TSelf
        ]
        |+> Pattern.OptionalFields PluginHooksOptionalFields // Inherits PluginHooks
        |> ignore

        DateSpanApi
        |+> Pattern.RequiredFields [
            "allDay", T<bool>
        ]
        |+> Pattern.OptionalFields [
            // Inherits RangeApi
            "start", T<Date>
            "end", T<Date>
            "startStr", T<string>
            "endStr", T<string>
        ]
        |> ignore 

        ViewContext 
        |+> Pattern.RequiredFields []
        |+> Pattern.OptionalFields ([
            "theme", Theme.Type
            "isRtl", T<bool>
            "dateProfileGenerator", DateProfileGenerator.Type
            "viewSpec", ViewSpec.Type
            "viewApi", ViewImpl.Type
            "addResizeHandler", ResizeHandler?handler ^-> T<unit>
            "removeResizeHandler", ResizeHandler?handler ^-> T<unit>
            "createScrollResponder", ScrollRequestHandler?execFunc ^-> ScrollResponder
            "registerInteractiveComponent", DateComponent.[T<obj>, T<obj>]?``component`` * InteractionSettingsInput?settingsInput ^-> T<unit>
            "unregisterInteractiveComponent", DateComponent.[T<obj>, T<obj>]?``component`` ^-> T<unit>
        ] @ CalendarContextOptionalFields // Inherits CalendarContext
        )
        |> ignore

        DateProfileGeneratorProps
        |+> Pattern.RequiredFields [
            "dateProfileGeneratorClass", DateProfileGeneratorClass.Type
            "duration", Duration.Type
            "durationUnit", T<string>
            "usesMinMaxTime", T<bool>
            "dateEnv", DateEnv.Type
            "calendarApi", CalendarImpl.Type
        ]
        |+> Pattern.OptionalFields DateProfileOptionsOptionalFields // Inherits DateProfileOptions
        |> ignore

        PluginHooks 
        |+> Pattern.RequiredFields []
        |+> Pattern.OptionalFields PluginHooksOptionalFields
        |> ignore

        CalendarData
        |+> Pattern.RequiredFields []
        |+> Pattern.OptionalFields (
            CalendarDataFields @
            //Inherits CalendarDataBase
            CalendarOptionsDataFields @
            CalendarCurrentViewDataFields @
            CalendarDataManagerStateFields
        )
        |> ignore

        EventStore
        |+> Pattern.RequiredFields [
            "defs", EventDefHash
            "instances", EventInstanceHash
        ]
        |> ignore

        ViewOptions
        |+> Pattern.RequiredFields []
        |+> Pattern.OptionalFields (
            (CalendarListenerRefinersOptionalFields |> List.ofSeq) @
            BaseOptionsOptionalFields
        )            
        |> ignore

        let CustomContentGeneratorParameter (renderProps:CodeModel.TypeParameter) = CustomContent + (renderProps?renderProps * T<obj>?createElement ^-> CustomContent + T<bool>)

        let ContentGeneratorProps =
            Generic - fun renderProps ->
                Pattern.Config "ContentGeneratorProps" {
                    Required = [
                        "renderProps", renderProps.Type
                        "generatorName", T<string>
                    ]
                    Optional = [
                        "customGenerator", CustomContentGeneratorParameter renderProps
                        "defaultGenerator", renderProps.Type?renderProps ^-> T<obj>
                    ]
                }

        let InnerContainerFunc renderProps = T<obj>?InnerContainer * renderProps?renderProps * ElAttrs?elAttrs ^-> ComponentChildren

        let InnerContainerFuncTypeParam renderProps = T<obj>?InnerContainer * renderProps?renderProps * ElAttrs?elAttrs ^-> ComponentChildren

        let Parameter (renderProps:CodeModel.TypeParameter) = ClassNamesInput + (renderProps?renderProps ^-> ClassNamesInput)

        let ContentContainerPropsOptionalFields renderProps = [
            "elTag", T<string>
            "children", !?(InnerContainerFuncTypeParam renderProps)
        ]

        let ContentContainerProps = 
            Generic - fun renderProps ->
            Pattern.Config "ContentContainerProps" {
                Required = []
                Optional = (
                    [
                        "classNameGenerator", !?(Parameter renderProps)
                        "didMount", !?((renderProps.Type + T<obj>)?renderProps ^-> T<unit>)
                        "willUnmount", !?((renderProps.Type + T<obj>)?renderProps ^-> T<unit>)

                        // inherits ContentGeneratorProps
                        "renderProps", renderProps.Type
                        "generatorName", T<string>
                    ] @
                    ContentContainerPropsOptionalFields renderProps @
                    [
                        // inherits ContentGeneratorProps
                        "customGenerator", CustomContentGeneratorParameter renderProps
                        "defaultGenerator", renderProps.Type?renderProps ^-> T<obj>
                    ] @
                    ElAttrsPropsOptionalFields // inherits ElAttrsProps
                )
            }

        let MoreLinkContainerState =
            Pattern.Config "MoreLinkContainerState" {
                Required = [
                    "isPopoverOpen", T<bool>
                    "popoverId", T<string>
                ]
                Optional = []
            }

        let EventTuple = 
            Pattern.Config "EventTuple" {
                Required = [
                    "def", EventDef.Type
                    "instance", !?EventInstance
                ]
                Optional = []
            }
            |> Import "EventTuple" "@fullcalendar/core"

        let EventRenderRange = 
            Pattern.Config "EventRenderRange" {
                Required = []
                Optional = [
                    "ui", EventUi.Type
                    "range", DateRange.Type
                    "isStart", T<bool>
                    "isEnd", T<bool>

                    // Inherits EventTuple
                    "def", EventDef.Type
                    "instance", !?EventInstance
                ]
            }
            |> Import "EventRenderRange" "@fullcalendar/core"

        let SegFields = [
            "component", DateComponent.[T<obj>, T<obj>]
            "eventRange", EventRenderRange.Type
            "el", T<obj>
            "isStart", T<bool>
            "isEnd", T<bool>
        ]

        let Seg =
            Pattern.Config "Seg" {
                Required = []
                Optional = SegFields
            }
            |> Import "Seg" "@fullcalendar/core"

        let MoreLinkContainerProps = 
            Pattern.Config "MoreLinkContainerProps" {
                Required = []
                Optional = (
                    [                        
                        "dateProfile", DateProfile.Type
                        "todayRange", DateRange.Type
                        "allDayDate", !?DateMarker 
                        "moreCnt", T<int>
                        "allSegs", !| Seg
                        "hiddenSegs", !| Seg
                        "popoverContent", T<unit> ^-> T<obj>
                        "extraDateSpan", DictionaryObj
                        "alignmentElRef", !?T<HTMLElement>
                        "alignGridTop", T<bool>
                        "forceTimed", T<bool>
                        "defaultGenerator", MoreLinkContentArg?renderProps ^-> T<obj>
                        "children", InnerContainerFunc MoreLinkContentArg
                        // Inherits ElProps
                        "elTag", T<string>
                    ] @ 
                    ElAttrsPropsOptionalFields // Inherits ElProps
                )
            }

        let DayCellContainerProps = 
            Pattern.Config "DayCellContainerProps" {
                Required = []
                Optional = (
                    [
                        "isMonthStart", T<bool>
                        "showDayNumber", T<bool>
                        "extraRenderProps", DictionaryObj
                        "defaultGenerator", (DayCellContentArg?renderProps ^-> ComponentChild)
                        "children", InnerContainerFunc DayCellContentArg
                        "date", DateMarker
                        "dateProfile", DateProfile.Type
                        "todayRange", DateRange.Type
                        // Inherits ElProps
                        "elTag", T<string>
                    ] @ 
                    ElAttrsPropsOptionalFields // Inherits ElProps
                )
            }

        let DayCellRenderPropsInput =
            Pattern.Config "DayCellRenderPropsInput" {
                Required = [
                    "date", DateMarker
                    "dateProfile", DateProfile.Type
                    "todayRange", DateRange.Type
                    "dateEnv", DateEnv.Type
                    "viewApi", ViewApi.Type
                    "monthStartFormat", DateFormatter.Type
                    "isMonthStart", T<bool>
                ]
                Optional = [
                    "showDayNumber", T<bool>
                    "extraRenderProps", DictionaryObj
                ]
            }

        let ViewContainerProps = 
            Pattern.Config "ViewContainerProps" {
                Required = [
                    // Inherits ElProps
                    "elTag", T<string>
                ]
                Optional = (
                    [
                        "viewSpec", ViewSpec.Type
                        "children", ComponentChildren
                    ] @
                    ElAttrsPropsOptionalFields // Inherits ElProps
                )
            }
            |> Import "ViewContainerProps" "@fullcalendar/core"

        let SegSpan =
            Pattern.Config "SegSpan" {
                Required = [
                    "start", T<int>
                    "end", T<int>
                ]
                Optional = []
            }
            |> Import "SegSpan" "@fullcalendar/core"

        let SegEntry =
            Pattern.Config "SegEntry" {
                Required = [
                    "index", T<int>
                    "span", SegSpan.Type
                ]
                Optional = [
                    "thickness", T<float>
                ]
            }
            |> Import "SegEntry" "@fullcalendar/core"

        let SegInsertion =
            Pattern.Config "SegInsertion" {
                Required = [
                    "level", T<int>
                    "levelCoord", T<float>
                    "lateral", T<int>
                    "touchingLevel", T<int>
                    "touchingLateral", T<int>
                    "touchingEntry", SegEntry.Type
                    "stackCnt", T<int>
                ]
                Optional = []
            }
            |> Import "SegInsertion" "@fullcalendar/core"

        let SegRect =
            Pattern.Config "SegRect" {
                Required = [
                    "index", T<int>
                    "span", SegSpan.Type
                    "thickness", T<float>
                    "levelCoord", T<float>
                ]
                Optional = []
            }
            |> Import "SegRect" "@fullcalendar/core"

        let SegEntryGroup =
            Pattern.Config "SegEntryGroup" {
                Required = [
                    "entries", !| SegEntry
                    "span", SegSpan.Type
                ]
                Optional = []
            }
            |> Import "SegEntryGroup" "@fullcalendar/core"

        let CalendarRootProps =
            Pattern.Config "CalendarRootProps" {
                Required = [
                    "options", CalendarOptions.Type
                    "theme", Theme.Type
                    "emitter", Emitter.[CalendarListeners]
                    "children", (!| T<string> * CssDimValue * T<bool> * T<bool>) ^-> ComponentChildren
                ]
                Optional = []
            }
            |> WithSourceName "I.CalendarRootProps"

        let CalendarRootState =
            Pattern.Config "CalendarRootState" {
                Required = [
                    "forPrint", T<bool>
                ]
                Optional = []
            }

        let DayHeaderProps =
            Pattern.Config "DayHeaderProps" {
                Required = [
                    "dateProfile", DateProfile.Type
                    "dates", !| DateMarker
                    "datesRepDistinctDays", T<bool>
                ]
                Optional = [
                    "renderIntro", (T<string>?rowKey ^-> VNode)
                ]
            }

        let TableDateCellProps =
            Pattern.Config "TableDateCellProps" {
                Required = [
                    "date", DateMarker
                    "dateProfile", DateProfile.Type
                    "todayRange", DateRange.Type
                    "colCnt", T<int>
                    "dayHeaderFormat", DateFormatter.Type
                ]
                Optional = [
                    "colSpan", T<int>
                    "isSticky", T<bool>
                    "extraDataAttrs", DictionaryObj
                    "extraRenderProps", DictionaryObj
                ]
            }

        let TableDowCellProps =
            Pattern.Config "TableDowCellProps" {
                Required = [
                    "dow", T<int>
                    "dayHeaderFormat", DateFormatter.Type
                ]
                Optional = [
                    "colSpan", T<int>
                    "isSticky", T<bool>
                    "extraRenderProps", DictionaryObj
                    "extraDataAttrs", DictionaryObj
                    "extraClassNames", !| T<string>
                ]
            }

        let DaySeriesSeg =
            Pattern.Config "DaySeriesSeg" {
                Required = [
                    "firstIndex", T<int>
                    "lastIndex", T<int>
                    "isStart", T<bool>
                    "isEnd", T<bool>
                ]
                Optional = []
            }

        let DayTableSeg = 
            Pattern.Config "DayTableSeg" {
                Required = []
                Optional = [
                    "row", T<int>
                    "firstCol", T<int>
                    "lastCol", T<int>
                ] @ SegFields // Inherits Seg
            }

        let DayTableCell =
            Pattern.Config "DayTableCell" {
                Required = [
                    "key", T<string>
                    "date", DateMarker
                ]
                Optional = [
                    "extraRenderProps", DictionaryObj
                    "extraDataAttrs", DictionaryObj
                    "extraClassNames", !| T<string>
                    "extraDateSpan", DictionaryObj
                ]
            }
            |> Import "DayTableCell" "@fullcalendar/core"

        let ScrollerProps =
            Pattern.Config "ScrollerProps" {
                Required = [
                    "overflowX", E.OverflowValue.Type
                    "overflowY", E.OverflowValue.Type
                ]
                Optional = [
                    "elRef", T<HTMLElement> + T<obj>
                    "overcomeLeft", T<int>
                    "overcomeRight", T<int>
                    "overcomeBottom", T<int>
                    "maxHeight", CssDimValue
                    "liquid", T<bool>
                    "liquidIsAbsolute", T<bool>
                    "children", ComponentChildren
                ]
            }

        let ScrollerLikeFields = [
            "needsYScrolling", T<unit> ^-> T<bool>
            "needsXScrolling", T<unit> ^-> T<bool>
        ]

        let ScrollerLike =
            Pattern.Config "ScrollerLike" {
                Required = []
                Optional = ScrollerLikeFields
            }
            |> Import "ScrollerLike" "@fullcalendar/core"

        let SimpleScrollGridSection = 
            Pattern.Config "SimpleScrollGridSection" {
                Required = [
                    "key", T<string>
                ]
                Optional = (
                    [
                        "chunk", ChunkConfig.Type
                    ] @
                    SectionConfigOptionalFields // Inherits SectionConfig
                )
            }
            |> Import "SimpleScrollGridSection" "@fullcalendar/core"

        let SimpleScrollGridProps =
            Pattern.Config "SimpleScrollGridProps" {
                Required = [
                    "cols", !| ColProps
                    "sections", !| SimpleScrollGridSection
                    "liquid", T<bool>
                    "collapsibleWidth", T<bool>
                ]
                Optional = [
                    "height", CssDimValue
                ]
            }

        let SimpleScrollGridState =
            Pattern.Config "SimpleScrollGridState" {
                Required = [
                    "shrinkWidth", !?T<int>
                    "forceYScrollbars", T<bool>
                    "scrollerClientWidths", Dictionary T<string> T<int>
                    "scrollerClientHeights", Dictionary T<string> T<int>
                ]
                Optional = []
            }

        let ScrollerDims =
            Pattern.Config "ScrollerDims" {
                Required = [
                    "forceYScrollbars", T<bool>
                    "scrollerClientWidths", Dictionary T<string> T<int>
                    "scrollerClientHeights", Dictionary T<string> T<int>
                ]
                Optional = []
            }

        let NowTimerProps =
            Pattern.Config "NowTimerProps" {
                Required = [
                    "unit", T<string>
                    "children", DateMarker?now * DateRange?todayRange ^-> ComponentChildren
                ]
                Optional = []
            }

        let NowTimerStateFields = [
            "nowDate", DateMarker
            "todayRange", DateRange.Type
        ]

        let NowTimerState =
            Pattern.Config "NowTimerState" {
                Required = []
                Optional = NowTimerStateFields
            }

        let StandardEventProps =
            Pattern.Config "StandardEventProps" {
                Required = [
                    "seg", Seg.Type
                    "isDragging", T<bool>
                    "isResizing", T<bool>
                    "isDateSelecting", T<bool>
                    "isSelected", T<bool>
                    "isPast", T<bool>
                    "isFuture", T<bool>
                    "isToday", T<bool>
                    "defaultTimeFormat", DateFormatter.Type
                ]
                Optional = [
                    "elRef", ElRef
                    "elClasses", !| T<string>
                    "disableDragging", T<bool>
                    "disableResizing", T<bool>
                    "defaultDisplayEventTime", T<bool>
                    "defaultDisplayEventEnd", T<bool>
                ]
            }

        let MinimalEventProps =
            Pattern.Config "MinimalEventProps" {
                Required = [
                    "seg", Seg.Type
                    "isDragging", T<bool>
                    "isResizing", T<bool>
                    "isDateSelecting", T<bool>
                    "isSelected", T<bool>
                    "isPast", T<bool>
                    "isFuture", T<bool>
                    "isToday", T<bool>
                ]
                Optional = []
            }

        let EventContainerProps = 
            Pattern.Config "EventContainerProps" {
                Required = []
                Optional = [
                    "timeText", T<string>
                    "defaultGenerator", EventContentArg?renderProps ^-> ComponentChild
                    "disableDragging", T<bool>
                    "disableResizing", T<bool>
                    "children", InnerContainerFunc EventContentArg
                ]
            }

        let BgEventProps =
            Pattern.Config "BgEventProps" {
                Required = [
                    "seg", Seg.Type
                    "isPast", T<bool>
                    "isFuture", T<bool>
                    "isToday", T<bool>
                ]
                Optional = []
            }

        let SliceableProps =
            Pattern.Config "SliceableProps" {
                Required = [
                    "dateSelection", DateSpan.Type
                    "businessHours", EventStore.Type
                    "eventStore", EventStore.Type
                    "eventDrag", !?EventInteractionState
                    "eventResize", !?EventInteractionState
                    "eventSelection", T<string>
                    "eventUiBases", EventUiHash
                ]
                Optional = []
            }

        let EventSegUiInteractionState =
            Pattern.Config "EventSegUiInteractionState" {
                Required = [
                    "affectedInstances", EventInstanceHash
                    "segs", !|Seg
                    "isEvent", T<bool>
                ]
                Optional = []
            }
            |> Import "EventSegUiInteractionState" "@fullcalendar/core"

        let SlicedProps =
            Generic - fun segType ->
            Pattern.Config "SlicedProps" {
                Required = [
                    "dateSelectionSegs", !|segType
                    "businessHourSegs", !|segType
                    "fgEventSegs", !|segType
                    "bgEventSegs", !|segType
                    "eventDrag", !?EventSegUiInteractionState.Type
                    "eventResize", !?EventSegUiInteractionState.Type
                    "eventSelection", T<string>
                ]
                Optional = []
            }
            |> Import "SlicedProps" "@fullcalendar/core"

        // Interface

    // Core
    module E = Enums
    module I = Interfaces

    let CalendarDataManager =
        Class "CalendarDataManager"
        |+> Static [
            Constructor (I.CalendarDataManagerProps?props)
        ]
        |+> Instance [
            "emitter" =@ Emitter.[I.CalendarListenerRefiners]
            "currentCalendarOptionsInput" =@ I.CalendarOptions
            "currentCalendarOptionsRefiners" =@ T<obj>

            "getCurrentData" => T<unit> ^-> I.CalendarData
            "dispatch" => T<obj>?action ^-> T<unit>
            "resetOptions" => I.CalendarOptions?optionOverrides * !? (!| T<string>)?changedOptionNames ^-> T<unit>
            "_handleAction" => T<obj>?action ^-> T<unit>
            "updateData" => T<unit> ^-> T<unit>                
            "computeOptionsData" => I.CalendarOptions?optionOverrides * I.CalendarOptions?dynamicOptionOverrides * I.CalendarApi?calendarApi ^-> I.CalendarOptionsData
            "processRawCalendarOptions" => I.CalendarOptions?optionOverrides * I.CalendarOptions?dynamicOptionOverrides ^-> I.ProcessRawCalendarOptionsResult 
            "_computeCurrentViewData" => T<string>?viewType * I.CalendarOptionsData?optionsData * I.CalendarOptions?optionOverrides * I.CalendarOptions?dynamicOptionOverrides ^-> I.CalendarCurrentViewData
            "processRawViewOptions" =>
                I.ViewSpec?viewSpec * I.PluginHooks?pluginHooks * I.CalendarOptions?localeDefaults * I.CalendarOptions?optionOverrides * I.CalendarOptions?dynamicOptionOverrides
                ^-> I.ProcessRawViewOptionsResult
        ]

    DateEnv
        |+> Static [
            Constructor (I.DateEnvSettings?settings)
        ]
        |+> Instance [
            "timeZone" =@ T<string>
            "namedTimeZoneImpl" =@ NamedTimeZoneImpl.Type
            "canComputeOffset" =@ T<bool>
            "calendarSystem" =@ I.CalendarSystem
            "locale" =@ I.Locale
            "weekDow" =@ T<int>
            "weekDoy" =@ T<int>
            "weekNumberFunc" =@ T<obj>
            "weekText" =@ T<string>
            "weekTextLong" =@ T<string>
            "cmdFormatter" =@ !?I.CmdFormatterFunc
            "defaultSeparator" =@ T<string>

            "createMarker" => DateInput?input ^-> DateMarker
            "createNowMarker" => T<unit> ^-> DateMarker
            "createMarkerMeta" => DateInput?input ^-> I.DateMarkerMeta
            "parse" => T<string>?s ^-> I.ParsedMarkerResult

            "getYear" => DateMarker?marker ^-> T<int>
            "getMonth" => DateMarker?marker ^-> T<int>
            "getDay" => DateMarker?marker ^-> T<int>
            "add" => DateMarker?marker * I.Duration?dur ^-> DateMarker
            "subtract" => DateMarker?marker * I.Duration?dur ^-> DateMarker
            "addYears" => DateMarker?marker * T<int>?n ^-> T<Date>
            "addMonths" => DateMarker?marker * T<int>?n ^-> T<Date>
            "diffWholeYears" => DateMarker?m0 * DateMarker?m1 ^-> T<int>
            "diffWholeMonths" => DateMarker?m0 * DateMarker?m1 ^-> T<int>
            "greatestWholeUnit" => DateMarker?m0 * DateMarker?m1 ^-> I.GreatestUnit
            "countDurationsBetween" => DateMarker?m0 * DateMarker?m1 * I.Duration?d ^-> T<int>
            "startOf" => DateMarker?m * T<string>?unit ^-> T<Date>
            "startOfYear" => DateMarker?m ^-> DateMarker
            "startOfMonth" => DateMarker?m ^-> DateMarker
            "startOfWeek" => DateMarker?m ^-> DateMarker
            "computeWeekNumber" => DateMarker?marker ^-> T<int>
            "format" => DateMarker?marker * I.DateFormatter?formatter * !?I.DateOptions?dateOptions ^-> T<string>
            "formatRange" => DateMarker?start * DateMarker?``end`` * I.DateFormatter?formatter * !?I.DateRangeFormatOptions?dateOptions ^-> T<string>
            "formatIso" => DateMarker?marker * !?T<obj>?extraOptions ^-> T<string>
            "timestampToMarker" => T<float>?ms ^-> T<Date>
            "offsetForMarker" => DateMarker?m ^-> T<int>
            "toDate" => DateMarker?m * !?T<int>?forcedTzo ^-> T<Date>
        ]
        |> ignore
            
    CalendarImpl
        |=> Inherits I.CalendarApi
        |+> Instance [
            "currentDataManager" =@ !?CalendarDataManager

            "getCurrentData" => T<unit> ^-> I.CalendarData
            "dispatch" => T<obj>?action ^-> T<unit>
            "batchRendering" => (T<unit> ^-> T<unit>)?callback ^-> T<unit>
            "unselect" => !?I.PointerDragEvent?pev ^-> T<unit>
            "addEvent" => I.EventInput?eventInput * !?(EventSourceImpl + T<string> + T<bool>)?sourceInput ^-> !?EventImpl
            "getEventById" => T<string>?id ^-> !?EventImpl
            "getEvents" => T<unit> ^-> !| EventImpl
            "getEventSources" => T<unit> ^-> !| EventSourceImpl
            "getEventSourceById" => T<string>?id ^-> !?EventSourceImpl
            "addEventSource" => I.EventSourceInput?sourceInput ^-> EventSourceImpl
        ]
        |> ignore

    EventSourceImpl 
        |=> Inherits I.EventSourceApi
        |+> Static [
            Constructor (
                I.CalendarContext?context * I.EventSource.[T<obj>]?internalEventSource ^-> T<unit>
            )
        ]
        |+> Instance [
            "internalEventSource" =@ I.EventSource.[T<obj>]
        ]
        |> ignore

    EventImpl
        |=> Inherits I.EventApi
        |+> Static [
            Constructor (I.CalendarContext?context * I.EventDef?``_def`` * !?I.EventInstance?instance)
        ]
        |+> Instance [
            "_context" =@ I.CalendarContext
            "_def" =@ I.EventDef
            "_instance" =@ !?I.EventInstance

            "source" =@ !?EventSourceImpl
            "constraint" =@ (T<string> + I.EventStore)
            "allow" =@ I.AllowFunc

            "mutate" => I.EventMutation?mutation ^-> T<unit>
        ]
        |> ignore

    DateProfileGenerator
        |+> Static [
            Constructor (I.DateProfileGeneratorProps?props)
        ]
        |+> Instance [
            "props" =@ I.DateProfileGeneratorProps.Type
            "nowDate" =@ DateMarker
            "isHiddenDayHash" =@ !| T<bool>

            "buildPrev" => I.DateProfile?currentDateProfile * DateMarker?currentDate * !?T<bool>?forceToValid ^-> I.DateProfile
            "buildNext" => I.DateProfile?currentDateProfile * DateMarker?currentDate * !?T<bool>?forceToValid ^-> I.DateProfile
            "build" => DateMarker?currentDate * !?T<obj>?direction * !?T<bool>?forceToValid ^-> I.DateProfile
            "buildValidRange" => T<unit> ^-> I.OpenDateRange
            "buildCurrentRangeInfo" => DateMarker?date * T<obj>?direction ^-> I.CurrentRangeInfo
            "getFallbackDuration" => T<unit> ^-> I.Duration
            "adjustActiveRange" => I.DateRange?range ^-> I.AdjustedRange
            "buildRangeFromDuration" => DateMarker?date * T<obj>?direction * I.Duration?duration * T<obj>?unit ^-> T<obj>
            "buildRangeFromDayCount" => DateMarker?date * T<obj>?direction * T<obj>?dayCount ^-> I.RangeFromDayCount
            "buildCustomVisibleRange" => DateMarker?date ^-> I.DateRange
            "buildRenderRange" => I.DateRange?currentRange * T<obj>?currentRangeUnit * T<obj>?isRangeAllDay ^-> I.DateRange
            "buildDateIncrement" => T<obj>?fallback ^-> I.Duration
            "refineRange" => !?I.DateRangeInput?rangeInput ^-> I.DateRange
            "initHiddenDays" => T<unit> ^-> T<unit>
            "trimHiddenDays" => I.DateRange?range ^-> I.DateRange
            "isHiddenDay" => T<obj>?day ^-> T<bool>
            "skipHiddenDays" => DateMarker?date * !?T<int>?inc * !?T<bool>?isExclusive ^-> T<Date>
        ]
        |> ignore

    Emitter
        |+> Instance [
            "setThisContext" => T<obj>?thisContext ^-> T<unit>
            "trigger" => T<string>?``type`` * T<obj[]>?args ^-> T<unit>
            "hasHandlers" => T<string>?``type`` ^-> T<bool>
        ]
        |> ignore

    ViewImpl
        |=> Inherits I.ViewApi
        |+> Static [
            Constructor (
                T<string>?``type`` * (T<unit> ^-> I.CalendarData)?getCurrentData * DateEnv?dateEnv
            )
        ]
        |+> Instance [
            "type" =@ T<string>
        ]
        |> ignore

    Theme 
        |+> Static [
            Constructor (I.CalendarOptionsRefined?calendarOptions)
        ]
        |+> Instance [
            "classes" =@ T<obj>
            "iconClasses" =@ T<obj>
            "rtlIconClasses" =@ T<obj>
            "baseIconClass" =@ T<string>
            "iconOverrideOption" =@ T<obj>
            "iconOverrideCustomButtonOption" =@ T<obj>
            "iconOverridePrefix" =@ T<string>

            "setIconOverride" => T<obj>?iconOverrideHash ^-> T<unit>
            "applyIconOverridePrefix" => T<obj>?className ^-> T<obj>
            "getClass" => T<obj>?key ^-> T<obj>
            "getIconClass" => T<obj>?buttonName * !?T<bool>?isRtl ^-> T<string>
            "getCustomButtonIconClass" => T<obj>?customButtonProps ^-> T<string>
        ]
        |> ignore

    ScrollResponder
        |+> Static [
            Constructor (I.ScrollRequestHandler?execFunc * Emitter.[I.CalendarListeners]?emitter * I.Duration?scrollTime * T<bool>?scrollTimeReset)
        ]
        |+> Instance [
            "queuedRequest" =@ I.ScrollRequest

            "detach" => T<unit> ^-> T<unit>
            "update" => T<bool>?isDatesNew ^-> T<unit>
        ]
        |> ignore

    let Calendar =
        Class "Calendar"
        |=> Inherits CalendarImpl
        |+> Instance [
            "el" =@ T<HTMLElement>

            "render" => T<unit> ^-> T<unit>
            "destroy" => T<unit> ^-> T<unit>
            "updateSize" => T<unit> ^-> T<unit>
            "batchRendering" => T<obj>?func ^-> T<unit>
            "pauseRendering" => T<unit> ^-> T<unit>
            "resumeRendering" => T<unit> ^-> T<unit>
            "resetOptions" => T<obj>?optionOverrides * !?T<string[]>?changedOptionNames ^-> T<unit>
        ]
        |+> Static [
            Constructor (T<HTMLElement>?el * !?I.CalendarOptions?optionOverrides)
        ]
        |> Import "Calendar" "@fullcalendar/core"

    let DelayedRunner =
        Class "DelayedRunner"
        |+> Instance [
            "request" => !?T<int>?delay ^-> T<unit>
            "pause" => !?T<string>?scope ^-> T<unit>
            "resume" => !?T<string>?scope * !?T<bool>?force ^-> T<unit>
            "isPaused" => T<unit> ^-> T<int>
            "tryDrain" => T<unit> ^-> T<unit>
            "clear" => T<unit> ^-> T<unit>
        ]
        |+> Static [
            Constructor (!?(T<unit> ^-> T<unit>)?drainedOption)
        ]
        |> Import "DelayedRunner" "@fullcalendar/core"

    let ContentContainer =
        Generic - fun renderProps ->
        Class "ContentContainer"
        |=> Inherits (I.ContentContainerProps.[renderProps])
        |+> Static [
            "contextType" =@ T<obj>
        ]
        |+> Instance [
            "didMountMisfire" =@ !?T<bool>
            "context" =@ T<int>
            "el" =@ T<HTMLElement>
            "InnerContent" =@ T<obj>

            "render" => T<unit> ^-> T<obj>
            "handleEl" => T<HTMLElement>?el ^-> T<unit>
            "componentDidMount" => T<unit> ^-> T<unit>
            "componentWillUnmount" => T<unit> ^-> T<unit>
        ]
        |> Import "ContentContainer" "@fullcalendar/core"

    let MoreLinkContainer =
        Class "MoreLinkContainer"
        |=> Inherits I.BaseComponent.[I.MoreLinkContainerProps, I.MoreLinkContainerState]
        |+> Instance [
            "state" =@ I.MoreLinkContainerState

            "render" => T<unit> ^-> T<obj>
            "componentDidMount" => T<unit> ^-> T<unit>
            "componentDidUpdate" => T<unit> ^-> T<unit>
            "handleLinkEl" => (!?T<HTMLElement>)?linkEl ^-> T<unit>
            "updateParentEl" => T<unit> ^-> T<unit>
            "handleClick" => T<Dom.MouseEvent>?ev ^-> T<unit>
            "handlePopoverClose" => T<unit> ^-> T<unit>
        ]
        |> Import "MoreLinkContainer" "@fullcalendar/core"

    let DayCellContainer =
        Class "DayCellContainer"
        |=> Inherits I.BaseComponent.[I.DayCellContainerProps, DictionaryObj]
        |+> Instance [
            "refineRenderProps" => I.DayCellRenderPropsInput?arg ^-> I.DayCellContentArg
            "render" => T<unit> ^-> ComponentChild
        ]
        |> Import "DayCellContainer" "@fullcalendar/core"

    let ViewContainer =
        Class "ViewContainer"
        |=> Inherits I.BaseComponent.[I.ViewContainerProps, DictionaryObj]
        |+> Instance [
            "render" => T<unit> ^-> T<obj>
        ]
        |> Import "ViewContainer" "@fullcalendar/core"

    let Store =
        Generic - fun t ->
            Class "Store"
            |+> Instance [
                "set" => t?value ^-> T<unit>
                "subscribe" => (t?value ^-> T<unit>)?handler ^-> T<unit>
            ]

    let CustomRenderingStore =
        Generic - fun t ->
        Class "CustomRenderingStore"
        |=> Inherits (Store.[Dictionary T<string> I.CustomRendering.[t]])
        |+> Instance [
            "handle" => I.CustomRendering.[t]?customRendering ^-> T<unit>
        ]
        |> Import "CustomRenderingStore" "@fullcalendar/core"

    let JsonRequestError =
        Class "JsonRequestError"
        |=> Inherits T<Error>
        |+> Instance [
            "response" =@ T<Response>
        ]
        |+> Static [
            Constructor (T<string>?message * T<Response>?response)
        ]
        |> Import "JsonRequestError" "@fullcalendar/core"

    let PositionCache =
        Class "PositionCache"
        |+> Instance [
            "els" =@ !| T<HTMLElement>
            "originClientRect" =@ T<obj>
            "lefts" =@ T<obj>
            "rights" =@ T<obj>
            "tops" =@ T<obj>
            "bottoms" =@ T<obj>

            "buildElHorizontals" => T<float>?originClientLeft ^-> T<unit>
            "buildElVerticals" => T<float>?originClientTop ^-> T<unit>
            "leftToIndex" => T<float>?leftPosition ^-> T<obj>
            "topToIndex" => T<float>?topPosition ^-> T<obj>
            "getWidth" => T<int>?leftIndex ^-> T<float>
            "getHeight" => T<int>?topIndex ^-> T<float>
            "similarTo" => TSelf?otherCache ^-> T<bool>
        ]
        |+> Static [
            Constructor (
                T<HTMLElement>?originEl *
                T<HTMLElement[]>?els *
                T<bool>?isHorizontal *
                T<bool>?isVertical
            )
        ]
        |> Import "PositionCache" "@fullcalendar/core"

    let ScrollController =
        Class "ScrollController"
        |+> Instance [
            "getScrollTop" => T<unit> ^-> T<float>
            "getScrollLeft" => T<unit> ^-> T<float>
            "setScrollTop" => T<float>?top ^-> T<unit>
            "setScrollLeft" => T<float>?left ^-> T<unit>
            "getClientWidth" => T<unit> ^-> T<float>
            "getClientHeight" => T<unit> ^-> T<float>
            "getScrollWidth" => T<unit> ^-> T<float>
            "getScrollHeight" => T<unit> ^-> T<float>
            "getMaxScrollTop" => T<unit> ^-> T<float>
            "getMaxScrollLeft" => T<unit> ^-> T<float>
            "canScrollVertically" => T<unit> ^-> T<bool>
            "canScrollHorizontally" => T<unit> ^-> T<bool>
            "canScrollUp" => T<unit> ^-> T<bool>
            "canScrollDown" => T<unit> ^-> T<bool>
            "canScrollLeft" => T<unit> ^-> T<bool>
            "canScrollRight" => T<unit> ^-> T<bool>
        ]
        |> Import "ScrollController" "@fullcalendar/core"

    let ElementScrollController =
        Class "ElementScrollController"
        |=> Inherits ScrollController
        |+> Instance [
            "el" =@ T<HTMLElement>
            "getScrollTop" => T<unit> ^-> T<float>
            "getScrollLeft" => T<unit> ^-> T<float>
            "setScrollTop" => T<float>?top ^-> T<unit>
            "setScrollLeft" => T<float>?left ^-> T<unit>
            "getScrollWidth" => T<unit> ^-> T<float>
            "getScrollHeight" => T<unit> ^-> T<float>
            "getClientHeight" => T<unit> ^-> T<float>
            "getClientWidth" => T<unit> ^-> T<float>
        ]
        |+> Static [
            Constructor (T<HTMLElement>?el)
        ]
        |> Import "ElementScrollController" "@fullcalendar/core"

    let WindowScrollController =
        Class "WindowScrollController"
        |=> Inherits ScrollController
        |+> Instance [
            "getScrollTop" => T<unit> ^-> T<float>
            "getScrollLeft" => T<unit> ^-> T<float>
            "setScrollTop" => T<float>?n ^-> T<unit>
            "setScrollLeft" => T<float>?n ^-> T<unit>
            "getScrollWidth" => T<unit> ^-> T<float>
            "getScrollHeight" => T<unit> ^-> T<float>
            "getClientHeight" => T<unit> ^-> T<float>
            "getClientWidth" => T<unit> ^-> T<float>
        ]
        |> Import "WindowScrollController" "@fullcalendar/core"

    let SegHierarchy =
        Class "SegHierarchy"
        |+> Instance [
            "strictOrder" =@ T<bool>
            "allowReslicing" =@ T<bool>
            "maxCoord" =@ T<float>
            "maxStackCnt" =@ T<int>
            "levelCoords" =@ !| T<float>
            "entriesByLevel" =@ !| (!| I.SegEntry)
            "stackCnts" =@ Dictionary T<string> T<int>

            "addSegs" => (!| I.SegEntry)?inputs ^-> !| I.SegEntry
            "insertEntry" => I.SegEntry?entry * (!| I.SegEntry)?hiddenEntries ^-> T<unit>
            "isInsertionValid" => I.SegInsertion?insertion * I.SegEntry?entry ^-> T<bool>
            "handleInvalidInsertion" => I.SegInsertion?insertion * I.SegEntry?entry * (!| I.SegEntry)?hiddenEntries ^-> T<unit>
            "splitEntry" => I.SegEntry?entry * I.SegEntry?barrier * (!| I.SegEntry)?hiddenEntries ^-> T<unit>
            "insertEntryAt" => I.SegEntry?entry * I.SegInsertion?insertion ^-> T<unit>
            "findInsertion" => I.SegEntry?newEntry ^-> I.SegInsertion
            "toRects" => T<unit> ^-> !| I.SegRect
        ]
        |+> Static [
            Constructor (!? (I.SegEntry ^-> T<float>)?getEntryThickness)
        ]
        |> Import "SegHierarchy" "@fullcalendar/core"

    let CalendarRoot =
        Class "CalendarRoot"
        |=> Inherits I.BaseComponent.[I.CalendarRootProps, I.CalendarRootState]
        |+> Instance [
            "state" =@ I.CalendarRootState
            "render" => T<unit> ^-> ComponentChildren
            "componentDidMount" => T<unit> ^-> T<unit>
            "componentWillUnmount" => T<unit> ^-> T<unit>
            "handleBeforePrint" => T<unit> ^-> T<unit>
            "handleAfterPrint" => T<unit> ^-> T<unit>
        ]
        |> Import "CalendarRoot" "@fullcalendar/core"

    let DayHeader =
        Class "DayHeader"
        |=> Inherits I.BaseComponent.[I.DayHeaderProps, DictionaryObj]
        |+> Instance [
            "createDayHeaderFormatter" => 
                I.DateFormatter?explicitFormat * T<obj>?datesRepDistinctDays * T<obj>?dateCnt ^-> I.DateFormatter
            "render" => T<unit> ^-> T<obj>
        ]
        |> Import "DayHeader" "@fullcalendar/core"

    let TableDateCell =
        Class "TableDateCell"
        |=> Inherits I.BaseComponent.[I.TableDateCellProps, DictionaryObj]
        |+> Instance [
            "render" => T<unit> ^-> T<obj>
        ]
        |> Import "TableDateCell" "@fullcalendar/core"

    let TableDowCell =
        Class "TableDowCell"
        |=> Inherits I.BaseComponent.[I.TableDowCellProps, T<obj>]
        |+> Instance [
            "render" => T<unit> ^-> T<obj>
        ]
        |> Import "TableDowCell" "@fullcalendar/core"

    let DaySeriesModel =
        Class "DaySeriesModel"
        |+> Instance [
            "cnt" =@ T<int>
            "dates" =@ !| DateMarker
            "indices" =@ !| T<int>
            "sliceRange" => I.DateRange.Type?range ^-> !?I.DaySeriesSeg
        ]
        |+> Static [
            Constructor (I.DateRange.Type?range * DateProfileGenerator?dateProfileGenerator)
        ]
        |> Import "DaySeriesModel" "@fullcalendar/core"

    let DayTableModel =
        Class "DayTableModel"
        |+> Instance [
            "rowCnt" =@ T<int>
            "colCnt" =@ T<int>
            "cells" =@ !| (!| I.DayTableCell)
            "headerDates" =@ !| DateMarker
            "sliceRange" => I.DateRange?range ^-> !| I.DayTableSeg
        ]
        |+> Static [
            Constructor (DaySeriesModel?daySeries * T<bool>?breakOnWeeks)
        ]
        |> Import "DayTableModel" "@fullcalendar/core"

    let Scroller =
        Class "Scroller"
        |=> Inherits I.BaseComponent.[I.ScrollerProps, DictionaryObj]
        |+> Instance [
            "el" =@ T<HTMLElement>
            "render" => T<unit> ^-> VNode
            "handleEl" => T<HTMLElement>?el ^-> T<unit>
            "getXScrollbarWidth" => T<unit> ^-> T<int>
            "getYScrollbarWidth" => T<unit> ^-> T<int>
        ]
        |+> Pattern.OptionalFields I.ScrollerLikeFields
        |> Import "Scroller" "@fullcalendar/core"

    let RefMap =
        Generic - fun refType ->
        Class "RefMap"
        |+> Instance [
            "masterCallback" =@ (refType?``val`` * T<string>?key ^-> T<unit>)
            "currentMap" =@ Dictionary T<string> refType

            "createRef" => (T<string> + T<int>)?key ^-> (refType?``val`` ^-> T<unit>)
            "handleValue" => refType?``val`` * T<string>?key ^-> T<unit>
            "collect" => !?T<int>?startIndex * !?T<int>?endIndex * !?T<int>?step ^-> !| refType
            "getAll" => T<unit> ^-> !| refType
        ]
        |+> Static [
            Constructor (!?(refType?``val`` * T<string>?key ^-> T<unit>)?masterCallback)
        ]
        |> Import "RefMap" "@fullcalendar/core"

    let RenderMicroColGroup = (!| I.ColProps)?cols * !?T<int>?shrinkWidth ^-> VNode

    let SimpleScrollGrid =
        Class "SimpleScrollGrid"
        |=> Inherits I.BaseComponent.[I.SimpleScrollGridProps, I.SimpleScrollGridState]
        |+> Instance [
            "processCols" =@ (T<obj>?a ^-> T<obj>)
            "renderMicroColGroup" =@ RenderMicroColGroup
            "scrollerRefs" =@ RefMap.[Scroller]
            "scrollerElRefs" =@ RefMap.[T<HTMLElement>]
            "state" =@ I.SimpleScrollGridState
            "render" => T<unit> ^-> VNode
            "renderSection" => I.SimpleScrollGridSection?sectionConfig * VNode?microColGroupNode * T<bool>?isHeader ^-> VNode
            "renderChunkTd" => I.SimpleScrollGridSection?sectionConfig * VNode?microColGroupNode * I.ChunkConfig?chunkConfig * T<bool>?isHeader ^-> VNode
            "_handleScrollerEl" => T<HTMLElement>?scrollerEl * T<string>?key ^-> T<unit>
            "handleSizing" => T<unit> ^-> T<unit>
            "componentDidMount" => T<unit> ^-> T<unit>
            "componentDidUpdate" => T<unit> ^-> T<unit>
            "componentWillUnmount" => T<unit> ^-> T<unit>
            "computeShrinkWidth" => T<unit> ^-> T<int>
            "computeScrollerDims" => T<unit> ^-> I.ScrollerDims
        ]
        |> Import "SimpleScrollGrid" "@fullcalendar/core"

    let NowTimer =
        Class "NowTimer"
        |=> Inherits I.NowTimerProps
        |+> Static [
            Constructor (I.NowTimerProps?props * I.ViewContext?context)
        ]
        |+> Instance [
            "context" =@ I.ViewContext
            "initialNowDate" =@ DateMarker
            "initialNowQueriedMs" =@ T<int>
            "timeoutId" =@ T<obj>

            "render" => T<unit> ^-> ComponentChildren
            "componentDidMount" => T<unit> ^-> T<unit>
            "componentDidUpdate" => I.NowTimerProps?prevProps ^-> T<unit>
            "componentWillUnmount" => T<unit> ^-> T<unit>
        ]
        |+> Pattern.OptionalFields I.NowTimerStateFields
        |> Import "NowTimer" "@fullcalendar/core"

    let StandardEvent =
        Class "StandardEvent"
        |=> Inherits I.BaseComponent.[I.StandardEventProps, DictionaryObj]
        |+> Instance [
            "render" => T<unit> ^-> T<obj>
        ]
        |> Import "StandardEvent" "@fullcalendar/core"

    let EventContainer =
        Class "EventContainer"
        |=> Inherits I.BaseComponent.[I.EventContainerProps, DictionaryObj]
        |+> Instance [
            "el" =@ T<HTMLElement>

            "render" => T<unit> ^-> T<obj>
            "handleEl" => (T<HTMLElement>)?el ^-> T<unit>
            "componentDidUpdate" => I.EventContainerProps?prevProps ^-> T<unit>
        ]
        |> Import "EventContainer" "@fullcalendar/core"

    let BgEvent =
        Class "BgEvent"
        |=> Inherits I.BaseComponent.[I.BgEventProps, DictionaryObj]
        |+> Instance [
            "render" => T<unit> ^-> T<obj>
        ]
        |> Import "BgEvent" "@fullcalendar/core"

    let Slicer =
        Generic -- fun segType extraArgs ->
        Class "Slicer"
        |+> Instance [
            "forceDayIfListItem" =@ T<bool>

            "sliceRange" => I.DateRange?dateRange * (!|extraArgs)?extra ^-> !|segType
            "sliceProps" => I.SliceableProps?props * I.DateProfile?dateProfile * !?I.Duration?nextDayThreshold * I.CalendarContext?context * (!|extraArgs)?extraArgs ^-> I.SlicedProps.[segType]
            "sliceNowDate" => DateMarker?date * I.DateProfile?dateProfile * !?I.Duration?nextDayThreshold * I.CalendarContext?context * (!|extraArgs)?extraArgs^-> !|segType
        ]
        |> Import "Slicer" "@fullcalendar/core"

    let Splitter =
        Generic - fun propsType ->
        Class "Splitter"
        |+> Instance [
            "getKeyInfo" => propsType?props ^-> T<obj[]>
            "getKeysForDateSpan" => I.DateSpan?dateSpan ^-> !| T<string>
            "getKeysForEventDef" => I.EventDef?eventDef ^-> !| T<string>
            "splitProps" => propsType?props ^-> Dictionary T<string> I.SplittableProps
        ]
        |> Import "Splitter" "@fullcalendar/core"
    // Core