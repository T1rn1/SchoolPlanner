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
            StackPanel MainStackPanel = new StackPanel {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Name = "MainStackPanel"
            };
            MainGrid.Children.Add(MainStackPanel);

            List<Models.Teacher> teachers = _dbContext.Teachers.ToList();

            foreach (var item in teachers)
            {
               
                StackPanel outerStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Background = (SolidColorBrush)FindResource("PrimaryBackgroundColor"),
                    Margin = new Thickness(10),
                };

                StackPanel innerStackPanel1 = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Margin = new Thickness(10)
                };
                TextBlock textBlock1a = new TextBlock
                {
                    Text = "ФИО",
                    Margin = new Thickness(10)
                };
                TextBlock textBlock1b = new TextBlock
                {
                    Text = item.FullName,
                    Margin = new Thickness(10)
                };
                innerStackPanel1.Children.Add(textBlock1a);
                innerStackPanel1.Children.Add(textBlock1b);

                StackPanel innerStackPanel2 = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Margin = new Thickness(10)
                };
                TextBlock textBlock2a = new TextBlock
                {
                    Text = "Телефон",
                    Margin = new Thickness(10)
                };
                TextBlock textBlock2b = new TextBlock
                {
                    Text = item.TelephoneNumber.ToString(),
                    Margin = new Thickness(10)
                };
                innerStackPanel2.Children.Add(textBlock2a);
                innerStackPanel2.Children.Add(textBlock2b);

                StackPanel innerStackPanel3 = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Margin = new Thickness(10)
                };
                TextBlock textBlock3a = new TextBlock
                {
                    Text = "Рабочее время",
                    Margin = new Thickness(10)
                };
                TextBlock textBlock3b = new TextBlock
                {
                    Text = item.WorkingHours,
                    Margin = new Thickness(10)
                };
                innerStackPanel3.Children.Add(textBlock3a);
                innerStackPanel3.Children.Add(textBlock3b);

                outerStackPanel.Children.Add(innerStackPanel1);
                outerStackPanel.Children.Add(innerStackPanel2);
                outerStackPanel.Children.Add(innerStackPanel3);

                MainStackPanel.Children.Add(outerStackPanel);
            }

        }
    }
}