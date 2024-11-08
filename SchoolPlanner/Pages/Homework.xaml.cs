using Microsoft.EntityFrameworkCore;
using SchoolPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SchoolPlanner.Pages
{
    public partial class Homework : Page
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;

        private SolidColorBrush PrimaryBackgroundColor;
        private Style placeholderTextBoxStyle;
        private Style TextBlockStyle;
        private Style RoundedButtonStyle;
        private PathGeometry EditPathGeometry;
        private PathGeometry DeletePathGeometry;

        public Homework()
        {
            InitializeComponent();

            PrimaryBackgroundColor = (SolidColorBrush)FindResource("PrimaryBackgroundColor");
            placeholderTextBoxStyle = (Style)FindResource("PlaceholderTextBoxStyle");
            TextBlockStyle = (Style)FindResource("TextBlockStyle");
            RoundedButtonStyle = (Style)FindResource("RoundedButtonStyle");
            EditPathGeometry = (PathGeometry)FindResource("edit");
            DeletePathGeometry = (PathGeometry)FindResource("delete");

            FillStackPanel();
        }

        public void CreateStackPanel(Models.Homework homework)
        {
            DockPanel ItemDockPanel = new DockPanel
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Tag = homework.Id,
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

            TextBlock SubjectTextBlock = new TextBlock
            {
                Text = "Предмет",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox SubjectTextBox = new TextBox
            {
                Text = homework.IdSubjectNavigation?.Name ?? "Не задано",
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                MinWidth = 100,
                MaxWidth = 150,
            };

            StackPanel innerStackPanel2 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            TextBlock TaskTextBlock = new TextBlock
            {
                Text = "Задание",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox TaskTextBox = new TextBox
            {
                Text = homework.Tusk,
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                MinWidth = 100,
                MaxWidth = 150,
            };

            StackPanel innerStackPanel3 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            TextBlock NoteTextBlock = new TextBlock
            {
                Text = "Заметка",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox NoteTextBox = new TextBox
            {
                Text = string.IsNullOrWhiteSpace(homework.Note) ? "Нет заметки" : homework.Note,
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                MinWidth = 100,
                MaxWidth = 150,
            };

            innerStackPanel1.Children.Add(SubjectTextBlock);
            innerStackPanel1.Children.Add(SubjectTextBox);
            innerStackPanel2.Children.Add(TaskTextBlock);
            innerStackPanel2.Children.Add(TaskTextBox);
            innerStackPanel3.Children.Add(NoteTextBlock);
            innerStackPanel3.Children.Add(NoteTextBox);

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
                Data = EditPathGeometry,
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
                Models.Homework entityToEdit = _dbContext.Homeworks.Find(ItemDockPanel.Tag);
                if (entityToEdit != null)
                {
                    AddEditHomework addEditHomework = new AddEditHomework(entityToEdit);
                    addEditHomework.Closed += AddHomework_Closed;
                    addEditHomework.ShowDialog();
                }
            };

            DeleteBtn.Click += (s, e) =>
            {
                Models.Homework entityToDelete = _dbContext.Homeworks.Find(ItemDockPanel.Tag);
                if (_dbContext != null && entityToDelete != null)
                {
                    _dbContext.Homeworks.Remove(entityToDelete);
                    _dbContext.SaveChanges();
                    MessageBox.Show($"Домашнее задание \"{entityToDelete.Tusk}\" успешно удалено", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
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
            List<Models.Homework> homeworks = _dbContext.Homeworks.Include(h => h.IdSubjectNavigation).ToList();

            foreach (var homework in homeworks)
            {
                CreateStackPanel(homework);
            }
        }

        private void AddNewHomeworkBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditHomework addEditHomeworkPage = new AddEditHomework();
            addEditHomeworkPage.Closed += AddHomework_Closed;
            addEditHomeworkPage.ShowDialog();
        }

        private void AddHomework_Closed(object? sender, EventArgs e)
        {
            FillStackPanel();
        }
    }
}
