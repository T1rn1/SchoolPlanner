using Microsoft.EntityFrameworkCore;
using SchoolPlanner.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SchoolPlanner.Pages
{
    public partial class AddEditPass : Window
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;
        private Models.Pass _currentPass;

        public AddEditPass()
        {
            InitializeComponent();
            LoadReasons();
        }

        public AddEditPass(Models.Pass pass) : this()
        {
            _currentPass = pass;
            LoadPassData();
        }

        private void LoadReasons()
        {
            var reasons = _dbContext.Reasons.ToList();
            ReasonComboBox.ItemsSource = reasons;
            ReasonComboBox.DisplayMemberPath = "Name";
            ReasonComboBox.SelectedValuePath = "Id";
        }

        private void LoadPassData()
        {
            if (_currentPass != null)
            {
                DatePicker.SelectedDate = _currentPass.Date.ToDateTime(TimeOnly.MinValue);
                TimeComboBox.SelectedItem = TimeComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString().StartsWith(_currentPass.Time.ToString("HH:mm")));
                ReasonComboBox.SelectedValue = _currentPass.IdReason;
                NoteTextBox.Text = _currentPass.Note;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, выберите дату.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ReasonComboBox.SelectedValue == null || (int)ReasonComboBox.SelectedValue == 0)
            {
                MessageBox.Show("Пожалуйста, выберите причину.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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

            // Проверка, если текст в NoteTextBox пустой или состоит только из пробелов, присваиваем null
            string note = string.IsNullOrWhiteSpace(NoteTextBox.Text) ? null : NoteTextBox.Text;

            if (_currentPass != null)
            {
                _currentPass.Date = DateOnly.FromDateTime(DatePicker.SelectedDate.Value);
                _currentPass.Time = time;
                _currentPass.IdReason = (int)ReasonComboBox.SelectedValue;
                _currentPass.Note = note;

                _dbContext.SaveChanges();
                MessageBox.Show("Пропуск успешно обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Models.Pass newPass = new Models.Pass
                {
                    Date = DateOnly.FromDateTime(DatePicker.SelectedDate.Value),
                    Time = time,
                    IdReason = (int)ReasonComboBox.SelectedValue,
                    Note = note
                };

                _dbContext.Passes.Add(newPass);
                _dbContext.SaveChanges();
                MessageBox.Show("Новый пропуск успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Close();
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
