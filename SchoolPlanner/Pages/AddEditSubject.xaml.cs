using SchoolPlanner.Models;
using System.Linq;
using System.Windows;

namespace SchoolPlanner.Pages
{
    public partial class AddEditSubject : Window
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;
        private Models.Subject _currentSubject;

        public AddEditSubject(Models.Subject subject = null)
        {
            InitializeComponent();
            FillComboBoxes();
            _currentSubject = subject;
            if (_currentSubject != null)
            {
                PopulateFields();
            }
        }

        public void FillComboBoxes()
        {
            var cyclicCommissions = _dbContext.Cycliccommissions.ToList();
            cyclicCommissions.Insert(0, new Models.Cycliccommission { Id = 0, Name = "не задано" });

            CyclicCommissionComboBox.ItemsSource = cyclicCommissions;
            CyclicCommissionComboBox.DisplayMemberPath = "Name";
            CyclicCommissionComboBox.SelectedValuePath = "Id";

            var teachers = _dbContext.Teachers.ToList();
            teachers.Insert(0, new Models.Teacher { Id = 0, FullName = "не задано" });

            TeacherFullNameComboBox.ItemsSource = teachers;
            TeacherFullNameComboBox.DisplayMemberPath = "FullName";
            TeacherFullNameComboBox.SelectedValuePath = "Id";
        }

        private void PopulateFields()
        {
            TitleTextBlock.Text = "Редактирование предмета";

            SubjectNameTextBox.Text = _currentSubject.Name;
            TeacherFullNameComboBox.SelectedValue = _currentSubject.IdTeacher;
            CyclicCommissionComboBox.SelectedValue = _currentSubject.IdCyclicCommission;
            NoteTextBox.Text = _currentSubject.Note;
        }

        private void AddNewSubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            int? teacherId = (int)TeacherFullNameComboBox.SelectedValue == 0 ? (int?)null : (int)TeacherFullNameComboBox.SelectedValue;
            int? cyclicCommissionId = (int)CyclicCommissionComboBox.SelectedValue == 0 ? (int?)null : (int)CyclicCommissionComboBox.SelectedValue;

            Models.Subject newSubject = new Models.Subject
            {
                Name = SubjectNameTextBox.Text,
                IdTeacher = teacherId,
                IdCyclicCommission = cyclicCommissionId,
                Note = NoteTextBox.Text
            };

            if (_currentSubject != null)
            {
                _currentSubject.Name = newSubject.Name;
                _currentSubject.IdTeacher = newSubject.IdTeacher;
                _currentSubject.IdCyclicCommission = newSubject.IdCyclicCommission;
                _currentSubject.Note = newSubject.Note;
                _dbContext.SaveChanges();
                MessageBox.Show("Данные предмета успешно обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _dbContext.Subjects.Add(newSubject);
                _dbContext.SaveChanges();
                MessageBox.Show("Новый предмет успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
