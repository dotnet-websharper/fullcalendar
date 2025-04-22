namespace WebSharper.FullCalendar

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator
open WebSharper.TouchEvents

module InteractionPlugin = 
        
    module CoreInterfaces = Core.Interfaces

    let PointerDragging =
        Class "PointerDragging"
        |+> Static [
            Constructor (T<Dom.EventTarget>?containerEl)
        ]
        |+> Instance [
            "containerEl" =@ T<Dom.EventTarget>
            "subjectEl" =@ !?T<HTMLElement>
            "emitter" =@ Core.Emitter.[T<obj>]
            "selector" =@ T<string>
            "handleSelector" =@ T<string>
            "shouldIgnoreMove" =@ T<bool>
            "shouldWatchScroll" =@ T<bool>
            "isDragging" =@ T<bool>
            "isTouchDragging" =@ T<bool>
            "wasTouchScroll" =@ T<bool>
            "origPageX" =@ T<float>
            "origPageY" =@ T<float>
            "prevPageX" =@ T<float>
            "prevPageY" =@ T<float>
            "prevScrollX" =@ T<float>
            "prevScrollY" =@ T<float>

            "destroy" => T<unit> ^-> T<unit>
            "tryStart" => T<Dom.UIEvent>?ev ^-> T<bool>
            "cleanup" => T<unit> ^-> T<unit>
            "querySubjectEl" => T<Dom.UIEvent>?ev ^-> T<HTMLElement>
            "handleMouseDown" =@ (T<Dom.MouseEvent> ^-> T<unit>)
            "handleMouseMove" =@ (T<Dom.MouseEvent> ^-> T<unit>)
            "handleMouseUp" =@ (T<Dom.MouseEvent> ^-> T<unit>)
            "shouldIgnoreMouse" => T<unit> ^-> (T<int> + T<bool>)
            "handleTouchStart" =@ (T<TouchEvent> ^-> T<unit>)
            "handleTouchMove" =@ (T<TouchEvent> ^-> T<unit>)
            "handleTouchEnd" =@ (T<TouchEvent> ^-> T<unit>)
            "handleTouchScroll" =@ (T<unit> ^-> T<unit>)
            "cancelTouchScroll" => T<unit> ^-> T<unit>
            "initScrollWatch" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "recordCoords" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "handleScroll" =@ (T<Dom.UIEvent> ^-> T<unit>)
            "destroyScrollWatch" => T<unit> ^-> T<unit>
            "createEventFromMouse" => T<Dom.MouseEvent>?ev * !?T<bool>?isFirst ^-> CoreInterfaces.PointerDragEvent
            "createEventFromTouch" => T<TouchEvent>?ev * !?T<bool>?isFirst ^-> CoreInterfaces.PointerDragEvent
        ]

    let ElementMirror =
        Class "ElementMirror"
        |+> Instance [
            "isVisible" =@ T<bool>
            "origScreenX" =@ !?T<float>
            "origScreenY" =@ !?T<float>
            "deltaX" =@ !?T<float>
            "deltaY" =@ !?T<float>
            "sourceEl" =@ !?T<HTMLElement>
            "mirrorEl" =@ !?T<HTMLElement>
            "sourceElRect" =@ !?CoreInterfaces.Rect
            "parentNode" =@ T<HTMLElement>
            "zIndex" =@ T<int>
            "revertDuration" =@ T<int>
            "start" => T<HTMLElement>?sourceEl * T<float>?pageX * T<float>?pageY ^-> T<unit>
            "handleMove" => T<float>?pageX * T<float>?pageY ^-> T<unit>
            "setIsVisible" => T<bool>?bool ^-> T<unit>
            "stop" => T<bool>?needsRevertAnimation * (T<unit> ^-> T<unit>)?callback ^-> T<unit>
            "doRevertAnimation" => (T<unit> ^-> T<unit>)?callback * T<int>?revertDuration ^-> T<unit>
            "cleanup" => T<unit> ^-> T<unit>
            "updateElPosition" => T<unit> ^-> T<unit>
            "getMirrorEl" => T<unit> ^-> T<HTMLElement>
        ]

    let ScrollGeomCache =
        Class "ScrollGeomCache"
        |=> Inherits Core.ScrollController
        |+> Static [
            Constructor (Core.ScrollController?scrollController * T<bool>?doesListening)
        ]
        |+> Instance [
            "clientRect" =@ CoreInterfaces.Rect
            "origScrollTop" =@ T<float>
            "origScrollLeft" =@ T<float>
            "scrollController" =@ Core.ScrollController
            "doesListening" =@ T<bool>
            "scrollTop" =@ T<float>
            "scrollLeft" =@ T<float>
            "scrollWidth" =@ T<float>
            "scrollHeight" =@ T<float>
            "clientWidth" =@ T<float>
            "clientHeight" =@ T<float>
            "destroy" => T<unit> ^-> T<unit>
            "handleScroll" =@ (T<unit> ^-> T<unit>)
            "getScrollTop" => T<unit> ^-> T<float>
            "getScrollLeft" => T<unit> ^-> T<float>
            "setScrollTop" => T<float>?top ^-> T<unit>
            "setScrollLeft" => T<float>?left ^-> T<unit>
            "getClientWidth" => T<unit> ^-> T<float>
            "getClientHeight" => T<unit> ^-> T<float>
            "getScrollWidth" => T<unit> ^-> T<float>
            "getScrollHeight" => T<unit> ^-> T<float>
            "handleScrollChange" => T<unit> ^-> T<unit>
            "getEventTarget" => T<unit> ^-> T<Dom.EventTarget>
            "computeClientRect" => T<unit> ^-> CoreInterfaces.Rect
        ]

    let AutoScroller =
        Class "AutoScroller"
        |+> Instance [
            "isEnabled" =@ T<bool>
            "scrollQuery" =@ !| (T<Window> + T<string>)
            "edgeThreshold" =@ T<int>
            "maxVelocity" =@ T<int>
            "pointerScreenX" =@ !?T<float>
            "pointerScreenY" =@ !?T<float>
            "isAnimating" =@ T<bool>
            "scrollCaches" =@ !?(!| ScrollGeomCache)
            "msSinceRequest" =@ !?T<float>
            "everMovedUp" =@ T<bool>
            "everMovedDown" =@ T<bool>
            "everMovedLeft" =@ T<bool>
            "everMovedRight" =@ T<bool>
            "start" => T<float>?pageX * T<float>?pageY * T<HTMLElement>?scrollStartEl ^-> T<unit>
            "handleMove" => T<float>?pageX * T<float>?pageY ^-> T<unit>
            "stop" => T<unit> ^-> T<unit>
            "requestAnimation" => T<float>?now ^-> T<unit>
        ]

    let FeaturefulElementDragging =
        Class "FeaturefulElementDragging"
        |=> Inherits CoreInterfaces.ElementDragging
        |+> Static [
            Constructor (T<HTMLElement>?containerEl * !?T<string>?selector)
        ]
        |+> Instance [
            "pointer" =@ PointerDragging
            "mirror" =@ ElementMirror
            "autoScroller" =@ AutoScroller
            "delay" =@ T<int> + T<unit>
            "minDistance" =@ T<int>
            "touchScrollAllowed" =@ T<bool>
            "mirrorNeedsRevert" =@ T<bool>
            "isInteracting" =@ T<bool>
            "isDragging" =@ T<bool>
            "isDelayEnded" =@ T<bool>
            "isDistanceSurpassed" =@ T<bool>
            "delayTimeoutId" =@ !?T<int>

            "destroy" => T<unit> ^-> T<unit>
            "onPointerDown" =@ (CoreInterfaces.PointerDragEvent?ev ^-> T<unit>)
            "onPointerMove" =@ (CoreInterfaces.PointerDragEvent?ev ^-> T<unit>)
            "onPointerUp" =@ (CoreInterfaces.PointerDragEvent?ev ^-> T<unit>)
            "startDelay" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "handleDelayEnd" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "handleDistanceSurpassed" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "tryStartDrag" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "tryStopDrag" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "stopDrag" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "setIgnoreMove" => T<bool>?bool ^-> T<unit>
            "setMirrorIsVisible" => T<bool>?bool ^-> T<unit>
            "setMirrorNeedsRevert" => T<bool>?bool ^-> T<unit>
            "setAutoScrollEnabled" => T<bool>?bool ^-> T<unit>
        ]

    let DateClickArg =
        Pattern.Config "DateClickArg" {
            Required = []
            Optional = [
                "dayEl", T<HTMLElement>
                "jsEvent", T<Dom.MouseEvent>
                "view", CoreInterfaces.ViewApi.Type

                // Inherits CoreInterfaces.DatePointApi
                "date", T<Date>
                "dateStr", T<string>
                "allDay", T<bool>
            ]
        }
        |> Import "DateClickArg" "@fullcalendar/interaction"

    let EventDragArgFields = [
        "el", T<HTMLElement>
        "event", CoreInterfaces.EventApi.Type
        "jsEvent", T<Dom.MouseEvent>
        "view", CoreInterfaces.ViewApi.Type
    ]

    let EventDragStopArg = 
        Pattern.Config "EventDragStopArg" {
            Required = EventDragArgFields
            Optional = []
        }
        |> Import "EventDragStopArg" "@fullcalendar/interaction"

    let EventDragStartArg = 
        Pattern.Config "EventDragStartArg" {
            Required = EventDragArgFields
            Optional = []
        }
        |> Import "EventDragStartArg" "@fullcalendar/interaction"

    let EventResizeStartStopArgFields = [
        "el", T<HTMLElement>
        "event", CoreInterfaces.EventApi.Type
        "jsEvent", T<Dom.MouseEvent>
        "view", CoreInterfaces.ViewApi.Type
    ]

    let EventResizeStartArg = 
        Pattern.Config "EventResizeStartArg" {
            Required = EventResizeStartStopArgFields
            Optional = []
        }
        |> Import "EventResizeStartArg" "@fullcalendar/interaction"

    let EventResizeStopArg = 
        Pattern.Config "EventResizeStopArg" {
            Required = EventResizeStartStopArgFields
            Optional = []
        }
        |> Import "EventResizeStopArg" "@fullcalendar/interaction"

    let EventResizeDoneArg =
        Pattern.Config "EventResizeDoneArg" {
            Required = []
            Optional = [
                "el", T<HTMLElement>
                "startDelta", CoreInterfaces.Duration.Type
                "endDelta", CoreInterfaces.Duration.Type
                "jsEvent", T<Dom.MouseEvent>
                "view", CoreInterfaces.ViewApi.Type

                // Inherits CoreInterfaces.EventChangeArg
                "oldEvent", Core.EventImpl.Type
                "event", Core.EventImpl.Type
                "relatedEvents", !| Core.EventImpl
                "revert", T<unit> ^-> T<unit>
            ]
        }
        |> Import "EventResizeDoneArg" "@fullcalendar/interaction"

    let DropArg =
        Pattern.Config "DropArg" {
            Required = []
            Optional = [
                "draggedEl", T<HTMLElement>
                "jsEvent", T<Dom.MouseEvent>
                "view", CoreInterfaces.ViewApi.Type

                // Inherits CoreInterfaces.DatePointApi
                "date", T<Date>
                "dateStr", T<string>
                "allDay", T<bool>
            ]
        }
        |> Import "DropArg" "@fullcalendar/interaction"

    let EventReceiveLeaveArgFields = [
        "draggedEl", T<HTMLElement>
        "event", CoreInterfaces.EventApi.Type
        "relatedEvents", !| CoreInterfaces.EventApi
        "revert", T<unit> ^-> T<unit>
        "view", CoreInterfaces.ViewApi.Type
    ]

    let EventReceiveArg = 
        Pattern.Config "EventReceiveArg" {
            Required = EventReceiveLeaveArgFields
            Optional = []
        }
        |> Import "EventReceiveArg" "@fullcalendar/interaction"

    let EventLeaveArg = 
        Pattern.Config "EventLeaveArg" {
            Required = EventReceiveLeaveArgFields
            Optional = []
        }
        |> Import "EventLeaveArg" "@fullcalendar/interaction"

    let ListenerRefinersOptionalFields = [
        "dateClick", (DateClickArg?arg ^-> T<unit>)
        "eventDragStart", (EventDragStartArg?arg ^-> T<unit>)
        "eventDragStop", (EventDragStopArg?arg ^-> T<unit>)
        "eventDrop", (CoreInterfaces.EventDropArg?arg ^-> T<unit>)
        "eventResizeStart", (EventResizeStartArg?arg ^-> T<unit>)
        "eventResizeStop", (EventResizeStopArg?arg ^-> T<unit>)
        "eventResize", (EventResizeDoneArg?arg ^-> T<unit>)
        "drop", (DropArg?arg ^-> T<unit>)
        "eventReceive", (EventReceiveArg?arg ^-> T<unit>)
        "eventLeave", (EventLeaveArg?arg ^-> T<unit>)
    ]

    let ListenerRefiners =
        Pattern.Config "ListenerRefiners" {
            Required = []
            Optional = ListenerRefinersOptionalFields
        }

    let DragMetaGenerator = CoreInterfaces.DragMetaInput + (T<HTMLElement>?el ^-> CoreInterfaces.DragMetaInput)

    let ExternalDraggableSettings =
        Pattern.Config "ExternalDraggableSettings" {
            Required = []
            Optional = [
                "eventData", DragMetaGenerator
                "itemSelector", T<string>
                "minDistance", T<int>
                "longPressDelay", T<int>
                "appendTo", T<HTMLElement>
            ]
        }
        |> Import "ExternalDraggable" "@fullcalendar/interaction"

    let ExternalDraggable =
        Class "ExternalDraggable"
        |+> Static [
            Constructor (T<HTMLElement>?el * !?ExternalDraggableSettings?settings)
        ]
        |+> Instance [
            "dragging" =@ FeaturefulElementDragging
            "settings" =@ ExternalDraggableSettings.Type

            
            "handlePointerDown" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "handleDragStart" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "destroy" => T<unit> ^-> T<unit>
        ]

    let InferredElementDragging =
        Class "InferredElementDragging"
        |=> Inherits CoreInterfaces.ElementDragging
        |+> Static [
            Constructor (T<HTMLElement>?containerEl)
        ]
        |+> Instance [
            "pointer" =@ PointerDragging
            "shouldIgnoreMove" =@ T<bool>
            "mirrorSelector" =@ T<string>
            "currentMirrorEl" =@ T<HTMLElement>

            "destroy" => T<unit> ^-> T<unit>
            "handlePointerDown" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "handlePointerMove" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "handlePointerUp" => CoreInterfaces.PointerDragEvent?ev ^-> T<unit>
            "setIgnoreMove" => T<bool>?bool ^-> T<unit>
            "setMirrorIsVisible" => T<bool>?bool ^-> T<unit>
        ]

    let ThirdPartyDraggableSettings =
        Pattern.Config "ThirdPartyDraggableSettings" {
            Required = []
            Optional = [
                "eventData", DragMetaGenerator
                "itemSelector", T<string>
                "mirrorSelector", T<string>
            ]
        }

    let ThirdPartyDraggable =
        Class "ThirdPartyDraggable"
        |+> Static [
            Constructor (!?(T<Dom.EventTarget> + ThirdPartyDraggableSettings.Type)?containerOrSettings * !?ThirdPartyDraggableSettings?settings)
        ]
        |+> Instance [
            "dragging" =@ InferredElementDragging
            "destroy" => T<unit> ^-> T<unit>
        ]
        |> Import "ThirdPartyDraggable" "@fullcalendar/interaction"