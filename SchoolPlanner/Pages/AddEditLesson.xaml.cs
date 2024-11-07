using SchoolPlanner.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SchoolPlanner.Pages
{
    public partial class AddEditLesson : Window
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;
        private Models.Schedule? _currentSchedule;

        public AddEditLesson(Models.Schedule? schedule = null)
        {
            InitializeComponent();
            FillComboBoxes();
            _currentSchedule = schedule;
            if (_currentSchedule != null)
            {
                PopulateFields();
            }
        }

        private void FillComboBoxes()
        {
            var subjects = _dbContext.Subjects.ToList();
            SubjectComboBox.ItemsSource = subjects;
            SubjectComboBox.DisplayMemberPath = "Name";
            SubjectComboBox.SelectedValuePath = "Id";
        }

        private void PopulateFields()
        {
            TitleTextBlock.Text = "Редактирование занятия";

            if (_currentSchedule != null)
            {
                DateDatePicker.SelectedDate = _currentSchedule.Date.ToDateTime(new TimeOnly(0, 0));
                TimeComboBox.SelectedItem = TimeComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString().StartsWith(_currentSchedule.Time.ToString("HH:mm")));
                SubjectComboBox.SelectedValue = _currentSchedule.IdSubject;
                ClassroomTextBox.Text = _currentSchedule.Class.ToString();
            }
        }

        private void SaveLessonBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DateDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, выберите дату.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SubjectComboBox.SelectedValue == null || (int)SubjectComboBox.SelectedValue == 0)
            {
                MessageBox.Show("Пожалуйста, выберите предмет.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(ClassroomTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, выберите аудиторию.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime selectedDate = DateDatePicker.SelectedDate.Value;
            int subjectId = (int)SubjectComboBox.SelectedValue;
            string classId = ClassroomTextBox.Text;

            if (TimeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите время.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string timeSlot = (TimeComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            string startTime = timeSlot.Split('-')[0].Trim();

            TimeOnly time;
            try
            {
                time = TimeOnly.Parse(startTime);
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат времени.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_currentSchedule != null)
            {
                _currentSchedule.Date = DateOnly.FromDateTime(selectedDate);
                _currentSchedule.IdSubject = subjectId;
                _currentSchedule.Class = Convert.ToInt32(classId);
                _currentSchedule.Time = time;
                _dbContext.SaveChanges();
                MessageBox.Show("Занятие успешно обновлено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Models.Schedule newSchedule = new Models.Schedule
                {
                    Date = DateOnly.FromDateTime(selectedDate),
                    IdSubject = subjectId,
                    Class = Convert.ToInt32(classId),
                    Time = time
                };

                _dbContext.Schedules.Add(newSchedule);
                _dbContext.SaveChanges();
                MessageBox.Show("Новое занятие успешно добавлено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
