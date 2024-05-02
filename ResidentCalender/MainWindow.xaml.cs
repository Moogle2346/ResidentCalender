using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;

namespace ResidentCalender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DateTime ViewDate;
        private Label[,] day = new Label[7,6];

        public MainWindow()
        {
            InitializeComponent();
            CreateCalenderFrame();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime StartUpDate = DateTime.Now;
            lbl_month.Content = ConvertToYearMonth(StartUpDate);

            SetDays(StartUpDate);

            ViewDate = StartUpDate.AddDays(1 - StartUpDate.Day);
        }

        private void CreateCalenderFrame()
        {
            int rowOffset = 1;
            for (int row = 0; row < day.GetLength(1); row++)
            {
                for(int col = 0; col < day.GetLength(0); col++)
                {
                    day[col, row] = new Label();
                    Grid.SetRow(day[col, row], row + rowOffset);
                    Grid.SetColumn(day[col, row], col);
                    grid_days.Children.Add(day[col, row]);
                    day[col, row].HorizontalContentAlignment = HorizontalAlignment.Left;
                    day[col, row].VerticalContentAlignment = VerticalAlignment.Top;
                    day[col, row].FontFamily = new FontFamily("Meiryo UI");
                    day[col, row].FontSize = 12;
                    day[col, row].Padding = new Thickness(10, 0, 0, 0);
                    day[col, row].MouseEnter += lbl_date_MouseEnter;
                    day[col, row].MouseLeave += lbl_date_MouseLeave;

                    day[col, row].Content = row * 7 + col;
                }
            }
        }

        private void SetDays(DateTime dateTime)
        {
            DateTime firstDay = dateTime.AddDays(1 - dateTime.Day);
            firstDay = firstDay.AddDays(-(int)firstDay.DayOfWeek);

            for(int row = 0;row < day.GetLength(1); row++)
            {
                for (int col = 0;col < day.GetLength(0); col++)
                {
                    day[col, row].Content = firstDay.Day;
                    if (firstDay.Month != dateTime.Month)
                    {
                        day[col, row].Foreground = new SolidColorBrush(Colors.Gray);
                    }
                    else if (firstDay.DayOfWeek == DayOfWeek.Sunday || firstDay.DayOfWeek == DayOfWeek.Saturday)
                    {
                        day[col, row].Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (false)
                    {
                        // 祝日処理
                    }
                    else
                    {
                        day[col, row].Foreground = new SolidColorBrush(Colors.Black);
                    }

                    if (firstDay.Date == DateTime.Now.Date)
                    {
                        day[col, row].Background = new SolidColorBrush(Color.FromArgb(0xA0, 0xC6, 0xE5, 0xFA));
                    }
                    else
                    {
                        day[col, row].Background = new SolidColorBrush(Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF));
                    }

                    firstDay = firstDay.AddDays(1);
                }
            }
        }

        private string ConvertToYearMonth(DateTime dt)
        {
            return dt.Year.ToString() + "年" + dt.Month.ToString() + "月";
        }

        #region MonthEvent
        private void lbl_previous_next_MouseEnter(object sender, MouseEventArgs e)
        {
            Label? lbl = sender as Label;
            if (lbl == null) return;
            lbl.BorderThickness = new Thickness(1);
            lbl.BorderBrush = new SolidColorBrush(Colors.Black);

        }

        private void lbl_previous_next_MouseLeave(object sender, MouseEventArgs e)
        {
            Label? lbl = sender as Label;
            if (lbl == null) return;
            lbl.BorderThickness = new Thickness(0);
            lbl.BorderBrush = null;
        }

        private void lbl_previous_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewDate = ViewDate.AddMonths(-1);
            lbl_month.Content = ConvertToYearMonth(ViewDate);
            SetDays(ViewDate);
        }

        private void lbl_next_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewDate = ViewDate.AddMonths(1);
            lbl_month.Content = ConvertToYearMonth(ViewDate);
            SetDays(ViewDate);
        }

        private void lbl_date_MouseEnter(object sender, MouseEventArgs e)
        {
            Label? lbl = sender as Label;
            if (lbl == null) return;
            lbl.BorderThickness = new Thickness(2);
            lbl.BorderBrush = new SolidColorBrush(Colors.Gray);
            //lbl.BorderBrush = new SolidColorBrush(Color.FromArgb(0x80, 0x00, 0x00, 0xFF));
        }

        private void lbl_date_MouseLeave(object sender, MouseEventArgs e)
        {
            Label? lbl = sender as Label;
            if (lbl == null) return;
            lbl.BorderThickness = new Thickness(0);
            lbl.BorderBrush = null;
        }

        #endregion
    }
}