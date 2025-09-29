using System;
using System.Windows;
using System.Windows.Controls;

namespace ModernCalendarLib
{
    public partial class CustomDatePicker : UserControl
    {
        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register(nameof(SelectedDate), typeof(DateTime?), typeof(CustomDatePicker),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public DateTime? SelectedDate
        {
            get => (DateTime?)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        public CustomDatePicker()
        {
            InitializeComponent();

            // Load styles

            // Set today by default
            var now = DateTime.Now;
            SelectedDate = now;
            DateBox.Text = now.ToString("dd MMM yyyy");

            CalendarControl.DateSelected += OnDateSelected;
        }

        private void OpenCalendar_Click(object sender, RoutedEventArgs e)
        {
            CalendarPopup.IsOpen = true;
        }

        private void OnDateSelected(DateTime date)
        {
            DateTime now = DateTime.Now;
            SelectedDate = new DateTime(date.Year, date.Month, date.Day,
                                        now.Hour, now.Minute, now.Second);
            DateBox.Text = SelectedDate.Value.ToString("dd MMM yyyy");
            CalendarPopup.IsOpen = false;
        }

        private void DateBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
