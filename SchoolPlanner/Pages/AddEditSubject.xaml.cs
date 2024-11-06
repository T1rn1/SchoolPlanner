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
using System.Windows.Shapes;

namespace SchoolPlanner.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditSubject.xaml
    /// </summary>
    public partial class AddEditSubject : Window
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;
        private Models.Subject _currentSubject;

        public AddEditSubject(Models.Subject subject = null)
        {
            InitializeComponent();
            _currentSubject = subject;
            if (_currentSubject != null)
            {
                PopulateFields();
            }
            FillComboBoxes();
        }

        public void FillComboBoxes()
        {
            var cyclicCommissionNames = _dbContext.Cycliccommissions
                                    .Select(c => c.Name)
                                    .ToList();

            cyclicCommissionNames.Insert(0, null);

            CyclicCommissionComboBox.Items.Clear();

            foreach (var name in cyclicCommissionNames)
            {
                CyclicCommissionComboBox.Items.Add(name);
            }

            var teachersNames = _dbContext.Teachers
                                               .Select(t => t.FullName)
                                               .ToList();

            teachersNames.Insert(0, null);

            TeacherFullNameComboBox.Items.Clear();

            foreach (var name in teachersNames)
            {
                TeacherFullNameComboBox.Items.Add(name);
            }

        }

        private void PopulateFields()
        {
            var teacherFullName = _dbContext.Teachers
                            .Where(t => t.Id == _currentSubject.IdTeacher)
                            .Select(t => t.FullName)
                            .FirstOrDefault() ?? "не задано";

            var cyclicCommissionName = _dbContext.Cycliccommissions
                            .Where(t => t.Id == _currentSubject.IdCyclicCommission)
                            .Select(t => t.Name)
                            .FirstOrDefault() ?? "не задано";

            SubjectNameTextBox.Text = _currentSubject.Name;
            TeacherFullNameComboBox.Text = teacherFullName;
            CyclicCommissionComboBox.Text = cyclicCommissionName;
        }

        private void AddNewSubjectBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
