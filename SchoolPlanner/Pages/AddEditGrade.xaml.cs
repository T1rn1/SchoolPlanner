using SchoolPlanner.Models;
using System;
using System.Linq;
using System.Windows;

namespace SchoolPlanner.Pages
{
    public partial class AddEditGrade : Window
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;
        private Models.Grade? _currentGrade;

        public AddEditGrade(Models.Grade? grade = null)
        {
            InitializeComponent();
            FillComboBoxes();
            _currentGrade = grade;
            if (_currentGrade != null)
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

            var lessonTypes = _dbContext.Lessontypes.ToList();

            LessonTypeComboBox.ItemsSource = lessonTypes;
            LessonTypeComboBox.DisplayMemberPath = "Type";
            LessonTypeComboBox.SelectedValuePath = "Id";
        }

        private void PopulateFields()
        {
            TitleTextBlock.Text = "Редактирование оценки";

            GradeTextBox.Text = _currentGrade.Grade1.ToString();
            DateDatePicker.SelectedDate = new DateTime(_currentGrade.Date.Year, _currentGrade.Date.Month, _currentGrade.Date.Day);
            SubjectComboBox.SelectedValue = _currentGrade.IdSubject;
            LessonTypeComboBox.SelectedValue = _currentGrade.IdLessonType;
        }

        private void SaveGradeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GradeTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите оценку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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

            if (LessonTypeComboBox.SelectedValue == null || (int)LessonTypeComboBox.SelectedValue == 0)
            {
                MessageBox.Show("Пожалуйста, выберите тип урока.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int subjectId = (int)SubjectComboBox.SelectedValue;
            int lessonTypeId = (int)LessonTypeComboBox.SelectedValue;
            DateTime selectedDate = DateDatePicker.SelectedDate.Value;
            int gradeValue;

            if (!int.TryParse(GradeTextBox.Text, out gradeValue))
            {
                MessageBox.Show("Введите правильное значение для оценки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_currentGrade != null)
            {
                _currentGrade.Grade1 = gradeValue;
                _currentGrade.Date = DateOnly.FromDateTime(selectedDate);
                _currentGrade.IdSubject = subjectId == 0 ? null : subjectId;
                _currentGrade.IdLessonType = lessonTypeId;
                _dbContext.SaveChanges();
                MessageBox.Show("Оценка успешно обновлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Models.Grade newGrade = new Models.Grade
                {
                    Grade1 = gradeValue,
                    Date = DateOnly.FromDateTime(selectedDate),
                    IdSubject = subjectId == 0 ? null : subjectId,
                    IdLessonType = lessonTypeId
                };

                _dbContext.Grades.Add(newGrade);
                _dbContext.SaveChanges();
                MessageBox.Show("Новая оценка успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}