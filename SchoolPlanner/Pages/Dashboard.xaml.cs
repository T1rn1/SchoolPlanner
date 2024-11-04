using Microsoft.EntityFrameworkCore;
using SchoolPlanner;
using SchoolPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolPlanner.Pages
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;

        private List<(TimeOnly, TimeOnly)> timeSlots;
        private DateOnly _currentWeekStart;
        public Dashboard()
        {
            InitializeComponent();
            InitializeTimeSlots();
            LoadSchedule();
        }

        private void InitializeTimeSlots()
        {
            timeSlots = new List<(TimeOnly, TimeOnly)>
            {
                (new TimeOnly(8, 0), new TimeOnly(9, 40)),
                (new TimeOnly(9, 50), new TimeOnly(11, 30)),
                (new TimeOnly(11, 50), new TimeOnly(13, 30)),
                (new TimeOnly(13, 40), new TimeOnly(15, 20)),
                (new TimeOnly(15, 40), new TimeOnly(17, 20)),
                (new TimeOnly(17, 30), new TimeOnly(19, 10)),
                (new TimeOnly(19, 20), new TimeOnly(21, 0))
            };
        }

        private void LoadSchedule()
        {
            var scheduleData = _dbContext.Schedules
                .Include(s => s.IdSubjectNavigation)
                .Include(s => s.IdPassNavigation)
                .OrderBy(s => s.Date)
                .ThenBy(s => s.Time)
                .ToList();

            var today = DateOnly.FromDateTime(DateTime.Today.AddDays(-10));
            var weekDates = GetWeekDates(today);

            string[] daysOfWeek = { "", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                scheduleGrid.ColumnDefinitions.Add(new ColumnDefinition());
                string dayHeader = daysOfWeek[i];
                if (i > 0)
                {
                    dayHeader = $"{daysOfWeek[i]}\n({weekDates[i - 1].ToString("dd.MM")})";
                }

                var dayLabel = new Label
                {
                    Content = dayHeader,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(dayLabel, i);
                Grid.SetRow(dayLabel, 0);
                scheduleGrid.Children.Add(dayLabel);
            }

            for (int i = 0; i <= timeSlots.Count; i++)
            {
                scheduleGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < timeSlots.Count; i++)
            {
                var timeLabel = new Label
                {
                    Content = $"{timeSlots[i].Item1.ToString("hh\\:mm")}-{timeSlots[i].Item2.ToString("hh\\:mm")}",
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(timeLabel, 0);
                Grid.SetRow(timeLabel, i + 1);
                scheduleGrid.Children.Add(timeLabel);
            }

            // Заполнение расписания
            foreach (var schedule in scheduleData)
            {
                int dayColumn = (int)schedule.Date.DayOfWeek;
                int timeRow = FindTimeSlotIndex(schedule.Time) + 1;

                // Преобразование дня недели к правильному индексу (понедельник = 1, ..., воскресенье = 7)
                if (dayColumn == 0) dayColumn = 7;

                if (dayColumn >= 1 && dayColumn <= 7 && timeRow > 0)
                {
                    string passNote = schedule.IdPassNavigation?.Note ?? "No Pass";
                    var subjectLabel = new Label
                    {
                        Content = $"{schedule.IdSubjectNavigation.Name} \n({passNote})",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Background = new SolidColorBrush(Colors.LightGray),
                        Margin = new Thickness(2)
                    };
                    Grid.SetColumn(subjectLabel, dayColumn);
                    Grid.SetRow(subjectLabel, timeRow);
                    scheduleGrid.Children.Add(subjectLabel);
                }
            }
        }

        private void PreviousWeekButton_Click(object sender, RoutedEventArgs e)
        {
            scheduleGrid.Children.Clear();
            _currentWeekStart = _currentWeekStart.AddDays(-7);
            LoadSchedule();
        }

        private void NextWeekButton_Click(object sender, RoutedEventArgs e)
        {
            scheduleGrid.Children.Clear();
            _currentWeekStart = _currentWeekStart.AddDays(7);
            LoadSchedule();
        }

        private int FindTimeSlotIndex(TimeOnly time)
        {
            for (int i = 0; i < timeSlots.Count; i++)
            {
                if (time >= timeSlots[i].Item1 && time <= timeSlots[i].Item2)
                {
                    return i;
                }
            }
            return -1;
        }

        public static List<DateOnly> GetWeekDates(DateOnly date)
        {
            var startOfWeek = date;

            startOfWeek = startOfWeek.AddDays(-(int)startOfWeek.DayOfWeek + (int)DayOfWeek.Monday);

            var weekDates = new List<DateOnly>();
            for (int i = 0; i < 7; i++)
            {
                weekDates.Add(startOfWeek.AddDays(i));
            }

            return weekDates;
        }
    }
}