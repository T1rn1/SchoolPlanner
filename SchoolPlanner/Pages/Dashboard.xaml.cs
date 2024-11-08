using Microsoft.EntityFrameworkCore;
using SchoolPlanner.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SchoolPlanner.Pages
{
    public partial class Dashboard : Page
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;
        private Style TextBlockStyle;
        private Style RoundedButtonStyle;
        private Brush TextTertiaryColor;
        private Brush TextFourthColor;
        private Brush PrimaryBackgroundColor;
        private PathGeometry DeletePathGeometry;
        private DateTime currentWeekStartDate;

        public Dashboard()
        {
            InitializeComponent();
            InitializeResources();
            currentWeekStartDate = GetStartOfWeek(DateTime.Today);
            InitializeScheduleGrid();
        }

        private void InitializeResources()
        {
            TextBlockStyle = (Style)FindResource("TextBlockStyle");
            RoundedButtonStyle = (Style)FindResource("RoundedButtonStyle");
            TextTertiaryColor = (Brush)FindResource("TextTertiaryColor");
            TextFourthColor = (Brush)FindResource("TextFourthColor");
            PrimaryBackgroundColor = (Brush)FindResource("PrimaryBackgroundColor");
            DeletePathGeometry = (PathGeometry)FindResource("delete");
        }

        private void InitializeScheduleGrid()
        {
            scheduleGrid.ColumnDefinitions.Clear();
            scheduleGrid.RowDefinitions.Clear();
            scheduleGrid.Children.Clear();

            AddColumnsAndHeaders();
            AddTimeSlotsAndLessons();
        }

        private void AddColumnsAndHeaders()
        {
            string[] daysOfWeek = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };

            for (int i = 0; i < 8; i++)
            {
                scheduleGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            scheduleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                AddHeaderToGrid(daysOfWeek[i], i + 1);
            }

            scheduleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                DateTime date = currentWeekStartDate.AddDays(i);
                AddDateToGrid(date.ToString("dd.MM.yyyy"), i + 1);
            }
        }

        private void AddTimeSlotsAndLessons()
        {
            string[] timeSlots =
            {
                "8.00-9.40", "9.50-11.30", "11.50-13.30", "13.40-15.20",
                "15.40-17.20", "17.30-19.10", "19.20-21.00"
            };

            var scheduleData = _dbContext.Schedules
                .Include(s => s.IdSubjectNavigation)
                .ThenInclude(sub => sub.IdTeacherNavigation)
                .ToList();

            for (int row = 1; row <= timeSlots.Length; row++)
            {
                scheduleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                AddTimeSlotToGrid(timeSlots[row - 1], row + 1);

                for (int col = 1; col <= 7; col++)
                {
                    DateTime date = currentWeekStartDate.AddDays(col - 1);
                    var lesson = scheduleData.FirstOrDefault(s => s.Date.ToDateTime(new TimeOnly()) == date && GetTimeSlotIndex(s.Time) == row - 1);

                    AddLessonToGrid(lesson, row + 1, col);
                }
            }
        }

        private void AddHeaderToGrid(string header, int column)
        {
            var border = CreateBorder();
            Grid.SetRow(border, 0);
            Grid.SetColumn(border, column);
            scheduleGrid.Children.Add(border);

            var textBlock = CreateTextBlock(header);
            Grid.SetRow(textBlock, 0);
            Grid.SetColumn(textBlock, column);
            scheduleGrid.Children.Add(textBlock);
        }

        private void AddDateToGrid(string date, int column)
        {
            var border = CreateBorder();
            Grid.SetRow(border, 1);
            Grid.SetColumn(border, column);
            scheduleGrid.Children.Add(border);

            var textBlock = CreateTextBlock(date);
            Grid.SetRow(textBlock, 1);
            Grid.SetColumn(textBlock, column);
            scheduleGrid.Children.Add(textBlock);
        }

        private void AddTimeSlotToGrid(string timeSlot, int row)
        {
            var border = CreateBorder();
            Grid.SetRow(border, row);
            Grid.SetColumn(border, 0);
            scheduleGrid.Children.Add(border);

            var textBlock = CreateTextBlock(timeSlot);
            Grid.SetRow(textBlock, row);
            Grid.SetColumn(textBlock, 0);
            scheduleGrid.Children.Add(textBlock);
        }

        private void AddLessonToGrid(Schedule lesson, int row, int column)
        {
            string lessonName = lesson?.IdSubjectNavigation?.Name ?? string.Empty;
            string teacherName = lesson?.IdSubjectNavigation?.IdTeacherNavigation?.FullName ?? string.Empty;
            string room = lesson?.Class.ToString() ?? string.Empty;

            string displayTeacher = teacherName.Length > 20 ? GetTeacherInitials(teacherName) : teacherName;

            var border = CreateLessonBorder(column);
            Grid.SetRow(border, row);
            Grid.SetColumn(border, column);
            scheduleGrid.Children.Add(border);

            var textBlock = new TextBlock
            {
                Text = $"{lessonName}\n{displayTeacher}\n{room}",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Style = TextBlockStyle,
                Foreground = PrimaryBackgroundColor,
                TextWrapping = TextWrapping.Wrap
            };

            Grid.SetRow(textBlock, row);
            Grid.SetColumn(textBlock, column);
            scheduleGrid.Children.Add(textBlock);

            if (lesson != null)
            {

                Path DeletePath = new Path
                {
                    Data = DeletePathGeometry,
                    Fill = PrimaryBackgroundColor,
                    StrokeThickness = 1
                };

                var deleteButton = new Button
                {
                    Content = DeletePath,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Margin = new Thickness(3),
                    FontSize = 10,
                    Height = DeletePath.Height,
                    Style = RoundedButtonStyle,
                };

                deleteButton.Click += (sender, e) => DeleteLesson(lesson, row, column);

                Grid.SetRow(deleteButton, row);
                Grid.SetColumn(deleteButton, column);
                scheduleGrid.Children.Add(deleteButton);
            }
        }

        private void DeleteLesson(Schedule lesson, int row, int column)
        {
            if (lesson != null)
            {
                _dbContext.Schedules.Remove(lesson);
                _dbContext.SaveChanges();

                InitializeScheduleGrid();
            }
        }


        private Border CreateBorder()
        {
            return new Border
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                CornerRadius = new CornerRadius(5)
            };
        }

        private TextBlock CreateTextBlock(string text)
        {
            return new TextBlock
            {
                Text = text,
                FontWeight = FontWeights.Bold,
                Style = TextBlockStyle
            };
        }

        private Border CreateLessonBorder(int column)
        {
            return new Border
            {
                Background = (column % 2 == 0) ? TextTertiaryColor : TextFourthColor,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Padding = new Thickness(2),
                Margin = new Thickness(1),
                CornerRadius = new CornerRadius(3)
            };
        }

        private string GetTeacherInitials(string fullName)
        {
            var nameParts = fullName.Split(' ');
            if (nameParts.Length == 1) return nameParts[0];
            if (nameParts.Length == 2)
                return $"{nameParts[0]} {nameParts[1][0]}.";
            if (nameParts.Length == 3)
                return $"{nameParts[0]} {nameParts[1][0]}.{nameParts[2][0]}.";
            return string.Empty;
        }

        private int GetTimeSlotIndex(TimeOnly time)
        {
            string[] timeSlots = { "08:00", "09:50", "11:50", "13:40", "15:40", "17:30", "19:20" };
            for (int i = 0; i < timeSlots.Length; i++)
                if (TimeOnly.Parse(timeSlots[i]) == time) return i;
            return -1;
        }

        private DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (date.DayOfWeek - DayOfWeek.Monday + 7) % 7;
            return date.AddDays(-diff).Date;
        }

        private void PreviousWeekButton_Click(object sender, RoutedEventArgs e)
        {
            currentWeekStartDate = currentWeekStartDate.AddDays(-7);
            InitializeScheduleGrid();
        }

        private void NextWeekButton_Click(object sender, RoutedEventArgs e)
        {
            currentWeekStartDate = currentWeekStartDate.AddDays(7);
            InitializeScheduleGrid();
        }

        private void AddNewLessonButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditLesson addEditLesson = new AddEditLesson();

            addEditLesson.Closed += (s, args) =>
            {
                InitializeScheduleGrid();
            };

            addEditLesson.ShowDialog();
        }
    }
}
