using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для Subject.xaml
    /// </summary>
    public partial class Subject : Page
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;

        private SolidColorBrush PrimaryBackgroundColor;
        private Style placeholderTextBoxStyle;
        private Style TextBlockStyle;
        private Style RoundedButtonStyle;
        private PathGeometry EditPathGeomerty;
        private PathGeometry DeletePathGeometry;

        public Subject()
        {
            InitializeComponent();

            PrimaryBackgroundColor = (SolidColorBrush)FindResource("PrimaryBackgroundColor");
            placeholderTextBoxStyle = (Style)FindResource("PlaceholderTextBoxStyle");
            TextBlockStyle = (Style)FindResource("TextBlockStyle");
            RoundedButtonStyle = (Style)FindResource("RoundedButtonStyle");
            EditPathGeomerty = (PathGeometry)FindResource("edit");
            DeletePathGeometry = (PathGeometry)FindResource("delete");

            FillStackPanel();
        }

        public void CreateStackPanel(Models.Subject item)
        {
            DockPanel ItemDockPanel = new DockPanel
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Tag = item.Id,
            };

            Border border = new Border
            {
                CornerRadius = new CornerRadius(10),
                Background = PrimaryBackgroundColor,
                Child = ItemDockPanel,
                Margin = new Thickness(10),
                Padding = new Thickness(3)
            };

            StackPanel LeftStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
            };

            StackPanel innerStackPanel1 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            TextBlock SubjectNameTextBlock = new TextBlock
            {
                Text = "Название предмета",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox SubjectNameTextBox = new TextBox
            {
                Text = item.Name,
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                MinWidth = 150,
                MaxWidth = 250,
            };

            var teacherFullName = _dbContext.Teachers
                            .Where(t => t.Id == item.IdTeacher)
                            .Select(t => t.FullName)
                            .FirstOrDefault() ?? "не задано";

            TextBlock TeacherNameTextBlock = new TextBlock
            {
                Text = "Преподаватель",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox TeacherNameTextBox = new TextBox
            {
                Text = teacherFullName,
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                MinWidth = 150,
                MaxWidth = 200,
            };

            var cyclicCommissionName = _dbContext.Cycliccommissions
                            .Where(t => t.Id == item.IdCyclicCommission)
                            .Select(t => t.Name)
                            .FirstOrDefault() ?? "не задано";

            TextBlock CyclicCommissionTextBlock = new TextBlock
            {
                Text = "Цикловая комиссия",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox CyclicCommissionTextBox = new TextBox
            {
                Text = cyclicCommissionName,
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                MinWidth = 150,
                MaxWidth = 200,
            };

            innerStackPanel1.Children.Add(SubjectNameTextBlock);
            innerStackPanel1.Children.Add(SubjectNameTextBox);

            StackPanel innerStackPanel2 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            innerStackPanel2.Children.Add(TeacherNameTextBlock);
            innerStackPanel2.Children.Add(TeacherNameTextBox);

            StackPanel innerStackPanel3 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            innerStackPanel3.Children.Add(CyclicCommissionTextBlock);
            innerStackPanel3.Children.Add(CyclicCommissionTextBox);

            LeftStackPanel.Children.Add(innerStackPanel1);
            LeftStackPanel.Children.Add(innerStackPanel2);
            LeftStackPanel.Children.Add(innerStackPanel3);

            ItemDockPanel.Children.Add(LeftStackPanel);

            StackPanel RightStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,

                Margin = new Thickness(10)
            };

            System.Windows.Shapes.Path EditPath = new System.Windows.Shapes.Path
            {
                Data = EditPathGeomerty,
                Fill = PrimaryBackgroundColor,
                StrokeThickness = 1
            };
            System.Windows.Shapes.Path DeletePath = new System.Windows.Shapes.Path
            {
                Data = DeletePathGeometry,
                Fill = PrimaryBackgroundColor,
                StrokeThickness = 1
            };

            Button EditBtn = new Button
            {
                Content = EditPath,
                Width = 50,
                Style = RoundedButtonStyle,
                Margin = new Thickness(5)
            };

            Button DeleteBtn = new Button
            {
                Content = DeletePath,
                Width = 50,
                Style = RoundedButtonStyle,
                Margin = new Thickness(5)
            };

            EditBtn.Click += (s, e) =>
            {
                /*Models.Subject entityToEdit = _dbContext.Subjects.Find(ItemDockPanel.Tag);
                if (entityToEdit != null)
                {
                    AddEditSubject addEditSubject = new AddEditSubject(entityToEdit);
                    addEditSubject.Closed += AddSubject_Closed;
                    addEditSubject.ShowDialog();
                }*/
            };

            DeleteBtn.Click += (s, e) =>
            {
                Models.Subject entityToDelete = _dbContext.Subjects.Find(ItemDockPanel.Tag);
                if (_dbContext != null && entityToDelete != null)
                {
                    _dbContext.Subjects.Remove(entityToDelete);
                    _dbContext.SaveChanges();
                    MessageBox.Show($"Данные {entityToDelete.Name} успешно удалены", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    FillStackPanel();
                }
            };

            RightStackPanel.Children.Add(EditBtn);
            RightStackPanel.Children.Add(DeleteBtn);
            ItemDockPanel.Children.Add(RightStackPanel);
            MainStackPanel.Children.Add(border);
        }

        public void FillStackPanel()
        {
            MainStackPanel.Children.Clear();
            List<Models.Subject> subjects = _dbContext.Subjects.ToList();

            foreach (var item in subjects)
            {
                CreateStackPanel(item);
            }
        }

        private void AddNewSubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditSubject addSubject = new AddEditSubject();
            addSubject.Closed += AddSubject_Closed;
            addSubject.ShowDialog();
        }

        private void AddSubject_Closed(object? sender, EventArgs e)
        {
            FillStackPanel();
        }
    }
}
