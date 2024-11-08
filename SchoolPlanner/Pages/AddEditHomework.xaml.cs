using SchoolPlanner.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SchoolPlanner.Pages
{
    public partial class AddEditHomework : Window
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;
        private Models.Homework? _currentHomework;

        public AddEditHomework(Models.Homework? homework = null)
        {
            InitializeComponent();
            FillComboBoxes();
            _currentHomework = homework;

            if (_currentHomework != null)
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
            TitleTextBlock.Text = "Редактирование домашнего задания";

            if (_currentHomework != null)
            {
                SubjectComboBox.SelectedValue = _currentHomework.IdSubject;
                TaskTextBox.Text = _currentHomework.Tusk;
                NoteTextBox.Text = _currentHomework.Note ?? string.Empty;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectComboBox.SelectedValue == null || (int)SubjectComboBox.SelectedValue == 0)
            {
                MessageBox.Show("Пожалуйста, выберите предмет.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(TaskTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите задание.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int subjectId = (int)SubjectComboBox.SelectedValue;
            string task = TaskTextBox.Text;
            string? note = string.IsNullOrWhiteSpace(NoteTextBox.Text) ? null : NoteTextBox.Text;

            if (_currentHomework != null)
            {
                _currentHomework.IdSubject = subjectId;
                _currentHomework.Tusk = task;
                _currentHomework.Note = note;

                _dbContext.SaveChanges();
                MessageBox.Show("Домашнее задание успешно обновлено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Models.Homework newHomework = new Models.Homework
                {
                    IdSubject = subjectId,
                    Tusk = task,
                    Note = note
                };

                _dbContext.Homeworks.Add(newHomework);
                _dbContext.SaveChanges();
                MessageBox.Show("Новое домашнее задание успешно добавлено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
