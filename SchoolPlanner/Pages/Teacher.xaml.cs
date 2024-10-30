using SchoolPlanner.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SchoolPlanner.Pages
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Teacher : Page
    {
        private SchoolPlannerContext _dbContext;
        private SolidColorBrush PrimaryBackgroundColor;
        private SolidColorBrush TextTertiaryColor;
        private Style placeholderTextBoxStyle;

        public Teacher(SchoolPlannerContext context)
        {
            InitializeComponent();
            _dbContext = context;

            PrimaryBackgroundColor = (SolidColorBrush)FindResource("PrimaryBackgroundColor");
            TextTertiaryColor = (SolidColorBrush)FindResource("TextTertiaryColor");
            placeholderTextBoxStyle = (Style)FindResource("PlaceholderTextBoxStyle");

            CreateStackPanelWithData();
        }

        public void CreateStackPanelWithData()
        {
            StackPanel MainStackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top,
            };
            MainGrid.Children.Add(MainStackPanel);

            List<Models.Teacher> teachers = _dbContext.Teachers.ToList();

            foreach (var item in teachers)
            {
                DockPanel ItemDockPanel = new DockPanel
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Center
                };

                Border border = new Border {
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

                TextBlock textBlock1a = new TextBlock
                {
                    Text = "ФИО",
                    Foreground = TextTertiaryColor,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(10),
                };

                TextBox textBox1b = new TextBox
                {
                    Text = item.FullName,
                    Margin = new Thickness(10),
                    Style = placeholderTextBoxStyle,
                    IsReadOnly = true,
                    MaxWidth = 250,
                };

                innerStackPanel1.Children.Add(textBlock1a);
                innerStackPanel1.Children.Add(textBox1b);

                StackPanel innerStackPanel2 = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Margin = new Thickness(10)
                };

                TextBlock textBlock2a = new TextBlock
                {
                    Text = "Телефон",
                    Foreground = TextTertiaryColor,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(10),
                };

                TextBox textBox2b = new TextBox
                {
                    Text = item.TelephoneNumber.ToString(),
                    Margin = new Thickness(10),
                    Style = placeholderTextBoxStyle,
                    IsReadOnly = true,
                    Width = 100,
                };

                innerStackPanel2.Children.Add(textBlock2a);
                innerStackPanel2.Children.Add(textBox2b);

                StackPanel innerStackPanel3 = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Margin = new Thickness(10)
                };

                TextBlock textBlock3a = new TextBlock
                {
                    Text = "Рабочее время",
                    Foreground = TextTertiaryColor,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(10),
                };

                TextBox textBox3b = new TextBox
                {
                    Text = item.WorkingHours,
                    Margin = new Thickness(10),
                    Style = placeholderTextBoxStyle,
                    IsReadOnly = true,
                    Width = 100,
                };

                innerStackPanel3.Children.Add(textBlock3a);
                innerStackPanel3.Children.Add(textBox3b);

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

                Button SaveChangesBtn = new Button
                {
                    Content = "Д",
                    Margin = new Thickness(5)
                };

                Button EditBtn = new Button {
                    Content = "Р",
                    Margin = new Thickness(5)
                };
                
                Button DeleteBtn = new Button {
                    Content = "У",
                    Margin = new Thickness(5)
                };

                SaveChangesBtn.Click += (s, e) =>
                {
                    
                };

                EditBtn.Click += (s, e) =>
                {
                    if (textBox1b.IsReadOnly)
                    {
                        textBox1b.IsReadOnly = false;
                        textBox2b.IsReadOnly = false;
                        textBox3b.IsReadOnly = false;
                    }
                    else
                    {
                        textBox1b.IsReadOnly = true;
                        textBox2b.IsReadOnly = true;
                        textBox3b.IsReadOnly = true;
                    }
                };
                
                RightStackPanel.Children.Add(SaveChangesBtn);
                RightStackPanel.Children.Add(EditBtn);
                RightStackPanel.Children.Add(DeleteBtn);
                ItemDockPanel.Children.Add(RightStackPanel);
                MainStackPanel.Children.Add(border);
            }
        }
    }
}