using SchoolPlanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SchoolPlanner.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddTeacher.xaml
    /// </summary>
    public partial class AddEditTeacher : Window
    {
        /*private SchoolPlannerContext _dbContext = MainWindow.dbContext;
        private Models.Teacher _currentTeacher;*/

        /*public AddEditTeacher(Models.Teacher teacher = null)
        {
            InitializeComponent();
            _currentTeacher = teacher;
            if (_currentTeacher != null)
            {
                PopulateFields();
            }
        }
        private void PopulateFields()
        {
            FullNameTextBox.Text = _currentTeacher.FullName;
            TelephoneNumberTextBox.Text = _currentTeacher.TelephoneNumber.ToString();
            WorkingHoursTextBox.Text = _currentTeacher.WorkingHours;
        }

        public static (bool isValid, List<string> errors) ValidateInput(Models.Teacher newTeacher)
        {
            var errors = new List<string>();

            if (!ValidateFullName(newTeacher.FullName.Trim()))
                errors.Add("Поле ФИО должно содержать либо полное ФИО (фамилия, имя, отчество), либо имя и отчество.");

            if (!ValidatePhoneNumber(newTeacher.TelephoneNumber.ToString().Trim()))
                errors.Add("Телефонный номер должен содержать 9 цифр.");

            if (!ValidateWorkingHours(newTeacher.WorkingHours.Trim()))
                errors.Add("Рабочие часы должны быть в формате HH:mm-HH:mm.");

            return (errors.Count == 0, errors);
        }

        private static bool ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return false;

            string fullNamePattern = @"^([А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+|[А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+)$";
            return Regex.IsMatch(fullName, fullNamePattern);
        }

        private static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return true;

            string phoneNumberPattern = @"^\d{9}$";
            return Regex.IsMatch(phoneNumber, phoneNumberPattern);
        }

        private static bool ValidateWorkingHours(string workingHours)
        {
            if (string.IsNullOrWhiteSpace(workingHours))
                return true;

            string workingHoursPattern = @"^(0[0-9]|1[0-9]|2[0-3])([:.])[0-5][0-9]-((0[0-9]|1[0-9]|2[0-3])([:.])[0-5][0-9])$";
            return Regex.IsMatch(workingHours, workingHoursPattern);
        }

        private void AddNewTeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            Models.Teacher newTeacher = new Models.Teacher
            {
                FullName = FullNameTextBox.Text,
                TelephoneNumber = string.IsNullOrWhiteSpace(TelephoneNumberTextBox.Text) ? (int?)null : Convert.ToInt32(TelephoneNumberTextBox.Text),
                WorkingHours = WorkingHoursTextBox.Text,
            };

            var(isValid, errors) = ValidateInput(newTeacher);

            if (!isValid)
            {
                var errorMessage = "Ошибки в полях:\n" + string.Join("\n", errors);
                MessageBox.Show(errorMessage, "Сохранение изменений", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if(_currentTeacher != null)
                {
                    _currentTeacher.FullName = newTeacher.FullName;
                    _currentTeacher.TelephoneNumber = newTeacher.TelephoneNumber;
                    _currentTeacher.WorkingHours = newTeacher.WorkingHours;
                    _dbContext.SaveChanges();
                    MessageBox.Show("Данные учителя успешно обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    _dbContext.Teachers.Add(newTeacher);
                    _dbContext.SaveChanges();
                    Close();
                } 
            }           
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }*/
    }
}
