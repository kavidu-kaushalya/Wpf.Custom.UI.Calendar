# ModernCalendarLib
https://www.nuget.org/packages/Wpf.Custom.UI.Calendar/
A modern WPF calendar library for .NET 8 with custom date picker controls.

## Features

- **CustomCalendar**: A modern-looking calendar control with month/year navigation
- **CustomDatePicker**: A date picker control with popup calendar
- Clean, modern UI design
- Built for .NET 8.0 with WPF

## Controls

### CustomCalendar
A user control that provides a calendar interface with:
- Month and year navigation
- Year picker view
- Date selection with visual feedback
- Today highlight
- Customizable styling through dynamic resources

### CustomDatePicker
A date picker control that provides:
- Text box display of selected date
- Calendar button to open popup
- Integrated CustomCalendar for date selection
- Two-way data binding support

## Usage

Add the controls to your WPF application and reference the ModernCalendarLib namespace:

```xml
xmlns:modern="clr-namespace:ModernCalendarLib;assembly=ModernCalendarLib"
```

Then use the controls:

```xml
<modern:CustomDatePicker SelectedDate="{Binding MyDate}" />
<modern:CustomCalendar />
```

## Requirements

- .NET 8.0
- WPF (Windows Presentation Foundation)
- Visual Studio 2022 or compatible IDE

## Building

Build the project using Visual Studio 2022 or the .NET CLI:

```bash
dotnet build
```

## License

This project is open source. Feel free to use and modify as needed.
