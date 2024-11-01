using SchoolPlanner.Models;
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
        private SchoolPlannerContext _dbContext;

        private SolidColorBrush PrimaryBackgroundColor;
        private Style placeholderTextBoxStyle;
        private Style TextBlockStyle;
        private Style RoundedButtonStyle;
        private PathGeometry OpenPathGeometry;
        private PathGeometry BlockPathGeometry;
        private PathGeometry SavePathGeometry;
        private PathGeometry DeletePathGeometry;

        public Teacher(SchoolPlannerContext context)
        {
            InitializeComponent();
            _dbContext = context;

            PrimaryBackgroundColor = (SolidColorBrush)FindResource("PrimaryBackgroundColor");
            placeholderTextBoxStyle = (Style)FindResource("PlaceholderTextBoxStyle");
            TextBlockStyle = (Style)FindResource("TextBlockStyle");
            RoundedButtonStyle = (Style)FindResource("RoundedButtonStyle");
            OpenPathGeometry = (PathGeometry)FindResource("open");
            BlockPathGeometry = (PathGeometry)FindResource("block");
            SavePathGeometry = (PathGeometry)FindResource("save");
            DeletePathGeometry = (PathGeometry)FindResource("delete");

            FillStackPanel();
        }

        public void CreateStackPanel(Models.Teacher item)
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

            System.Windows.Shapes.Path OpenPath = new System.Windows.Shapes.Path
            {
                Data = OpenPathGeometry,
                Fill = PrimaryBackgroundColor,
                StrokeThickness = 1
            };
            System.Windows.Shapes.Path BlockPath = new System.Windows.Shapes.Path
            {
                Data = BlockPathGeometry,
                Fill = PrimaryBackgroundColor,
                StrokeThickness = 1
            };
            System.Windows.Shapes.Path SavePath = new System.Windows.Shapes.Path
            {
                Data = SavePathGeometry,
                Fill = PrimaryBackgroundColor,
                StrokeThickness = 1
            };
            System.Windows.Shapes.Path DeletePath = new System.Windows.Shapes.Path
            {
                Data = DeletePathGeometry,
                Fill = PrimaryBackgroundColor,
                StrokeThickness = 1
            };

            Button SaveChangesBtn = new Button
            {
                Content = SavePath,
                Width = 50,
                Style = RoundedButtonStyle,
                Margin = new Thickness(5)
            };

            Button EditBtn = new Button
            {
                Content = BlockPath,
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

            SaveChangesBtn.Click += (s, e) =>
            {
                Models.Teacher entityToEdit = _dbContext.Teachers.Find(ItemDockPanel.Tag);
                if (_dbContext != null && entityToEdit != null)
                {
                    entityToEdit.FullName = FullNameTextBox.Text;
                    entityToEdit.TelephoneNumber = Convert.ToInt32(TelephoneNumberTextBox.Text);
                    entityToEdit.WorkingHours = WorkingHoursTextBox.Text;

                    _dbContext.SaveChanges();

                    MessageBox.Show($"Данные {entityToEdit.FullName} успешно сохранены", "Сохранение изменений", MessageBoxButton.OK, MessageBoxImage.Information);

                    FillStackPanel();
                }
            };

            EditBtn.Click += (s, e) =>
            {
                if (FullNameTextBox.IsReadOnly)
                {
                    EditBtn.Content = OpenPath;
                    FullNameTextBox.IsReadOnly = false;
                    TelephoneNumberTextBox.IsReadOnly = false;
                    WorkingHoursTextBox.IsReadOnly = false;
                }
                else
                {
                    EditBtn.Content = BlockPath;
                    FullNameTextBox.IsReadOnly = true;
                    TelephoneNumberTextBox.IsReadOnly = true;
                    WorkingHoursTextBox.IsReadOnly = true;
                }
            };

            DeleteBtn.Click += (s, e) =>
            {
                Models.Teacher entityToDelete = _dbContext.Teachers.Find(ItemDockPanel.Tag);
                if (_dbContext != null && entityToDelete != null)
                {
                    _dbContext.Teachers.Remove(entityToDelete);
                    _dbContext.SaveChanges();
                }
                MessageBox.Show($"Данные {entityToDelete.FullName} успешно удален", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                FillStackPanel();
            };

            RightStackPanel.Children.Add(SaveChangesBtn);
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
            
        }
    }
}