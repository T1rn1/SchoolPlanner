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
    public partial class Grade : Page
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;

        private SolidColorBrush PrimaryBackgroundColor;
        private Style placeholderTextBoxStyle;
        private Style TextBlockStyle;
        private Style RoundedButtonStyle;
        private PathGeometry EditPathGeomerty;
        private PathGeometry DeletePathGeometry;

        public Grade()
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

        public void CreateStackPanel(Models.Grade grade)
        {
            DockPanel ItemDockPanel = new DockPanel
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Tag = grade.Id,
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

            TextBlock GradeTextBlock = new TextBlock
            {
                Text = "Оценка",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox GradeTextBox = new TextBox
            {
                Text = grade.Grade1.ToString(),
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                MinWidth = 50,
                MaxWidth = 100,
            };

            StackPanel innerStackPanel2 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            TextBlock DateTextBlock = new TextBlock
            {
                Text = "Дата",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox DateTextBox = new TextBox
            {
                Text = grade.Date.ToString(),
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

            TextBlock SubjectTextBlock = new TextBlock
            {
                Text = "Предмет",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox SubjectTextBox = new TextBox
            {
                Text = grade.IdSubjectNavigation?.Name ?? "не задано",
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                MinWidth = 100,
                MaxWidth = 150,
            };

            StackPanel innerStackPanel4 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            TextBlock LessonTypeTextBlock = new TextBlock
            {
                Text = "Тип урока",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox LessonTypeTextBox = new TextBox
            {
                Text = grade.IdLessonTypeNavigation.Type,
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                MinWidth = 100,
                MaxWidth = 150,
            };

            innerStackPanel1.Children.Add(GradeTextBlock);
            innerStackPanel1.Children.Add(GradeTextBox);
            innerStackPanel2.Children.Add(DateTextBlock);
            innerStackPanel2.Children.Add(DateTextBox);
            innerStackPanel3.Children.Add(SubjectTextBlock);
            innerStackPanel3.Children.Add(SubjectTextBox);
            innerStackPanel4.Children.Add(LessonTypeTextBlock);
            innerStackPanel4.Children.Add(LessonTypeTextBox);

            LeftStackPanel.Children.Add(innerStackPanel1);
            LeftStackPanel.Children.Add(innerStackPanel2);
            LeftStackPanel.Children.Add(innerStackPanel3);
            LeftStackPanel.Children.Add(innerStackPanel4);

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
                SchoolPlanner.Models.Grade entityToEdit = _dbContext.Grades.Find(ItemDockPanel.Tag);
                if (entityToEdit != null)
                {
                    AddEditGrade addEditGrade = new AddEditGrade(entityToEdit);
                    addEditGrade.Closed += AddGrade_Closed;
                    addEditGrade.ShowDialog();
                }
            };

            DeleteBtn.Click += (s, e) =>
            {
                SchoolPlanner.Models.Grade entityToDelete = _dbContext.Grades.Find(ItemDockPanel.Tag);
                if (_dbContext != null && entityToDelete != null)
                {
                    _dbContext.Grades.Remove(entityToDelete);
                    _dbContext.SaveChanges();
                    MessageBox.Show($"Оценка {entityToDelete.Grade1} успешно удалена", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
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
            List<SchoolPlanner.Models.Grade> grades = _dbContext.Grades.Include(g => g.IdSubjectNavigation)
                                                                        .Include(g => g.IdLessonTypeNavigation)
                                                                        .ToList();

            foreach (var grade in grades)
            {
                CreateStackPanel(grade);
            }
        }

        private void AddNewGradeBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditGrade addGrade = new AddEditGrade();
            addGrade.Closed += AddGrade_Closed;
            addGrade.ShowDialog();
        }

        private void AddGrade_Closed(object? sender, EventArgs e)
        {
            FillStackPanel();
        }
    }
}
