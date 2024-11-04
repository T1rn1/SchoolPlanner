using SchoolPlanner.Models;
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
        private SchoolPlannerContext _dbContext;

        public AddEditTeacher(SchoolPlannerContext context, Models.Teacher teacher = null)
        {
            InitializeComponent();
            _dbContext = context;
            /*_currentTeacher = teacher;
            if (_currentTeacher != null)
            {
                // Если передан существующий преподаватель, заполняем поля
                PopulateFields();
            }*/
        }

        public static bool[] ValidateInput(Models.Teacher newTeacher)
        {
            bool[] arr = new bool[]
            {
                ValidateFullName(newTeacher.FullName),
                ValidatePhoneNumber(newTeacher.TelephoneNumber.ToString()),
                ValidateWorkingHours(newTeacher.WorkingHours)
            };

            return arr;
        }

        private static bool ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return false;

            string fullNamePattern = @"^[А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+$";
            return Regex.IsMatch(fullName, fullNamePattern);
        }

        private static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            string phoneNumberPattern = @"^\d{9}$";
            return Regex.IsMatch(phoneNumber, phoneNumberPattern);
        }

        private static bool ValidateWorkingHours(string workingHours)
        {
            if (string.IsNullOrWhiteSpace(workingHours))
                return false;

            string workingHoursPattern = @"^\d{2}\.\d{2}-\d{2}\.\d{2}$";
            return Regex.IsMatch(workingHours, workingHoursPattern);
        }

        private void AddNewTeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            Models.Teacher newTeacher = new Models.Teacher();

            var validationResults = ValidateInput(newTeacher);
            if (validationResults.Contains(false))
            {
                var errorMessage = "Ошибки в полях:\n";
                if (!validationResults[0]) errorMessage += "- ФИО\n";
                if (!validationResults[1]) errorMessage += "- Телефон\n";
                if (!validationResults[2]) errorMessage += "- Рабочее время\n";

                MessageBox.Show(errorMessage, "Сохранение изменений", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _dbContext.Teachers.Add(newTeacher);
                _dbContext.SaveChanges();
                Close();
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
