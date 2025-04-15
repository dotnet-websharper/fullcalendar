# WebSharper FullCalendar API Binding

This repository provides an F# [WebSharper](https://websharper.com/) binding for the [FullCalendar](https://fullcalendar.io/) JavaScript library, allowing you to build interactive and dynamic calendars in F# WebSharper applications.

## Repository Structure

The repository consists of two main projects:

1. **Binding Project**:

   - Contains the F# WebSharper binding for FullCalendar and its plugins.

2. **Sample Project**:
   - Demonstrates how to use FullCalendar with WebSharper syntax.
   - Includes example usage with dynamic event creation and rendering.

## Installation

To use this package in your WebSharper project, install it via NuGet:

```bash
   dotnet add package WebSharper.FullCalendar
```

## Building

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
- Node.js and npm (for building web assets).

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/dotnet-websharper/FullCalendar.git
   cd FullCalendar
   ```

2. Build the Binding Project:

   ```bash
   dotnet build WebSharper.FullCalendar/WebSharper.FullCalendar.fsproj
   ```

3. Build and Run the Sample Project:

   ```bash
   cd WebSharper.FullCalendar.Sample
   dotnet build
   npx vite build
   npx vite
   ```

4. Open the hosted sample in your browser to see FullCalendar in action.

## Example Usage

Below is a sample code that initializes a calendar with static events and allows the user to add new events via date click:

```fsharp
namespace WebSharper.FullCalendar.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.FullCalendar

[<JavaScript>]
module Client =
    // Define the index template loaded from index.html
    type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

    [<SPAEntryPoint>]
    let Main () =
        let renderCalendar() =
            // Get the container element
            let calendarEl = JS.Document.GetElementById("calendar") |> As<HTMLElement>

            // Define the calendar options
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
                    // Prompt user to enter event title
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

            // Create and render the calendar
            let calendar = Calendar(calendarEl, calendarOptions)
            calendar.Render()

        // Initialize template and bind to DOM
        IndexTemplate.Main()
            .PageInit(fun () ->
                renderCalendar()
            )
            .Doc()
        |> Doc.RunById "main"
```

## Features

- Display month views using FullCalendar's day grid.
- Dynamically add events using date clicks.
- Editable and selectable events.
- Integration with FullCalendar plugins.
