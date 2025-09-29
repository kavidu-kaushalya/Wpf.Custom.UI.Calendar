using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ModernCalendarLib
{
    public partial class CustomCalendar : UserControl
    {
        private DateTime _currentMonth;
        private bool _isYearPicker = false;
        private DateTime? SelectedDate;

        public event Action<DateTime>? DateSelected;

        // Static constructor runs only once per type
        static CustomCalendar()
        {
            // Override Calendar.SelectedDateProperty metadata safely
            System.Windows.Controls.Calendar.SelectedDateProperty.OverrideMetadata(
                typeof(CustomCalendar),
                new FrameworkPropertyMetadata(null, OnSelectedDateChangedStatic));
        }

        public CustomCalendar()
        {
            InitializeComponent();

            // Initialize current month
            _currentMonth = DateTime.Now;
            DisplayMonth(_currentMonth);
        }

        // Static handler for SelectedDate changes
        private static void OnSelectedDateChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomCalendar calendar)
            {
                calendar.SelectedDate = (DateTime?)e.NewValue;
                if (calendar.SelectedDate.HasValue)
                    calendar.DateSelected?.Invoke(calendar.SelectedDate.Value);
                calendar.DisplayMonth(calendar._currentMonth);
            }
        }

        private void DisplayMonth(DateTime month)
        {
            _isYearPicker = false;
            YearGrid.Visibility = Visibility.Collapsed;
            DayHeadersPanel.Visibility = Visibility.Visible;
            DaysGrid.Visibility = Visibility.Visible;

            DaysGrid.Children.Clear();
            MonthText.Text = month.ToString("MMMM yyyy", CultureInfo.InvariantCulture);

            DateTime firstDay = new DateTime(month.Year, month.Month, 1);
            int startOffset = ((int)firstDay.DayOfWeek + 6) % 7;
            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
            DateTime today = DateTime.Today;

            for (int i = 0; i < startOffset; i++)
                DaysGrid.Children.Add(new TextBlock());

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime date = new DateTime(month.Year, month.Month, day);
                Button btn = new Button
                {
                    Content = day.ToString(),
                    Style = (Style)FindResource("DayButtonStyle"),
                    Tag = date
                };

                // Today highlight
                if (date == today)
                {
                    btn.BorderBrush = Brushes.DodgerBlue;
                    btn.BorderThickness = new Thickness(2);
                }

                // Selected highlight
                if (SelectedDate.HasValue && date == SelectedDate.Value)
                {
                    btn.Background = Brushes.DodgerBlue;
                    btn.Foreground = Brushes.White;
                }

                btn.Click += Day_Click;
                DaysGrid.Children.Add(btn);
            }
        }

        private void DisplayYearPicker()
        {
            _isYearPicker = true;
            DaysGrid.Visibility = Visibility.Collapsed;
            DayHeadersPanel.Visibility = Visibility.Collapsed;
            YearGrid.Visibility = Visibility.Visible;

            YearGrid.Children.Clear();
            int startYear = _currentMonth.Year - 6;
            int todayYear = DateTime.Today.Year;

            for (int y = startYear; y < startYear + 12; y++)
            {
                Button btn = new Button
                {
                    Content = y.ToString(),
                    Style = (Style)FindResource("DayButtonStyle"),
                    Tag = y
                };

                // Today year border
                if (y == todayYear)
                {
                    btn.BorderBrush = Brushes.DodgerBlue;
                    btn.BorderThickness = new Thickness(2);
                }

                // Selected year background
                if (SelectedDate.HasValue && y == SelectedDate.Value.Year)
                {
                    btn.Background = Brushes.DodgerBlue;
                    btn.Foreground = Brushes.White;
                }

                btn.Click += Year_Click;
                YearGrid.Children.Add(btn);
            }

            MonthText.Text = $"{startYear} - {startYear + 11}";
        }

        private void Year_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && int.TryParse(btn.Content.ToString(), out int year))
            {
                _currentMonth = new DateTime(year, _currentMonth.Month, 1);
                DisplayMonth(_currentMonth);
            }
        }

        private void Day_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is DateTime date)
            {
                SelectedDate = date;
                DateSelected?.Invoke(date);
                DisplayMonth(_currentMonth);
            }
        }

        private void PrevMonth_Click(object sender, RoutedEventArgs e)
        {
            if (_isYearPicker)
                _currentMonth = _currentMonth.AddYears(-12);
            else
                _currentMonth = _currentMonth.AddMonths(-1);

            if (_isYearPicker) DisplayYearPicker();
            else DisplayMonth(_currentMonth);
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            if (_isYearPicker)
                _currentMonth = _currentMonth.AddYears(12);
            else
                _currentMonth = _currentMonth.AddMonths(1);

            if (_isYearPicker) DisplayYearPicker();
            else DisplayMonth(_currentMonth);
        }

        private void MonthText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_isYearPicker)
                DisplayMonth(_currentMonth);
            else
                DisplayYearPicker();
        }
    }
}
