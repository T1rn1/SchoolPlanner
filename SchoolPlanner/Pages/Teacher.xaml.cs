using SchoolPlanner;
using System.DirectoryServices;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SchoolPlanner.Pages
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Teacher : Page
    {
        //private SchoolPlannerContext _dbContext= MainWindow.dbContext;

        private SolidColorBrush PrimaryBackgroundColor;
        private Style placeholderTextBoxStyle;
        private Style TextBlockStyle;
        private Style RoundedButtonStyle;
        private PathGeometry EditPathGeomerty;
        private PathGeometry DeletePathGeometry;

        public Teacher()
        {
            InitializeComponent();

            PrimaryBackgroundColor = (SolidColorBrush)FindResource("PrimaryBackgroundColor");
            placeholderTextBoxStyle = (Style)FindResource("PlaceholderTextBoxStyle");
            TextBlockStyle = (Style)FindResource("TextBlockStyle");
            RoundedButtonStyle = (Style)FindResource("RoundedButtonStyle");
            EditPathGeomerty = (PathGeometry)FindResource("edit");
            DeletePathGeometry = (PathGeometry)FindResource("delete");

            //FillStackPanel();
        }

        /*public void CreateStackPanel(Models.Teacher item)
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

            TextBlock FullNameTextBlock = new TextBlock
            {
                Text = "ФИО",
                Style = TextBlockStyle,
                MinWidth = 100,
                Margin = new Thickness(10),
            };

            TextBox FullNameTextBox = new TextBox
            {
                Text = item.FullName,
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                MaxWidth = 250,
            };

            innerStackPanel1.Children.Add(FullNameTextBlock);
            innerStackPanel1.Children.Add(FullNameTextBox);

            StackPanel innerStackPanel2 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            TextBlock TelephoneNumberTextBlock = new TextBlock
            {
                Text = "Телефон",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox TelephoneNumberTextBox = new TextBox
            {
                Text = item.TelephoneNumber.ToString(),
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                Width = 100,
            };

            innerStackPanel2.Children.Add(TelephoneNumberTextBlock);
            innerStackPanel2.Children.Add(TelephoneNumberTextBox);

            StackPanel innerStackPanel3 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            TextBlock WorkingHoursTextBlock = new TextBlock
            {
                Text = "Рабочее время",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox WorkingHoursTextBox = new TextBox
            {
                Text = item.WorkingHours,
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                Width = 100,
            };

            innerStackPanel3.Children.Add(WorkingHoursTextBlock);
            innerStackPanel3.Children.Add(WorkingHoursTextBox);

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
                Models.Teacher entityToEdit = _dbContext.Teachers.Find(ItemDockPanel.Tag);
                if (entityToEdit != null)
                {
                    AddEditTeacher addEditTeacher = new AddEditTeacher(entityToEdit);
                    addEditTeacher.Closed += AddTeacher_Closed;
                    addEditTeacher.ShowDialog();
                }
            };

            DeleteBtn.Click += (s, e) =>
            {
                DeleteBtn.Click += (s, e) =>
                {
                    Models.Teacher entityToDelete = _dbContext.Teachers.Find(ItemDockPanel.Tag);
                    if (_dbContext != null && entityToDelete != null)
                    {
                        _dbContext.Teachers.Remove(entityToDelete);
                        _dbContext.SaveChanges();
                        MessageBox.Show($"Данные {entityToDelete.FullName} успешно удален", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                        FillStackPanel();
                    }   
                };
            };

            RightStackPanel.Children.Add(EditBtn);
            RightStackPanel.Children.Add(DeleteBtn);
            ItemDockPanel.Children.Add(RightStackPanel);
            MainStackPanel.Children.Add(border);
        }

        public void FillStackPanel()
        {
            MainStackPanel.Children.Clear();
            List<Models.Teacher> teachers = _dbContext.Teachers.ToList();

            foreach (var item in teachers)
            {
                CreateStackPanel(item);
            }
        }

        private void AddNewTeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditTeacher addTeacher = new AddEditTeacher();
            addTeacher.Closed += AddTeacher_Closed;
            addTeacher.ShowDialog();
        }

        private void AddTeacher_Closed(object? sender, EventArgs e)
        {
            FillStackPanel();
        }*/
    }
}