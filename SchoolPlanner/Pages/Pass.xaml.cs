﻿using Microsoft.EntityFrameworkCore;
using SchoolPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SchoolPlanner.Pages
{
    public partial class Pass : Page
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;

        private SolidColorBrush PrimaryBackgroundColor;
        private Style placeholderTextBoxStyle;
        private Style TextBlockStyle;
        private Style RoundedButtonStyle;
        private PathGeometry EditPathGeometry;
        private PathGeometry DeletePathGeometry;

        public Pass()
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

        public void CreateStackPanel(Models.Pass pass)
        {
            DockPanel ItemDockPanel = new DockPanel
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Tag = pass.Id,
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

            TextBlock DateTextBlock = new TextBlock
            {
                Text = "Дата",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox DateTextBox = new TextBox
            {
                Text = pass.Date.ToString(),
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

            TextBlock TimeTextBlock = new TextBlock
            {
                Text = "Время",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox TimeTextBox = new TextBox
            {
                Text = pass.Time.ToString(),
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

            TextBlock ReasonTextBlock = new TextBlock
            {
                Text = "Причина",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox ReasonTextBox = new TextBox
            {
                Text = pass.IdReasonNavigation?.Name ?? "не задано",
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

            TextBlock NoteTextBlock = new TextBlock
            {
                Text = "Заметка",
                Style = TextBlockStyle,
                Margin = new Thickness(10),
            };

            TextBox NoteTextBox = new TextBox
            {
                Text = pass.Note ?? "нет заметок",
                Margin = new Thickness(10),
                Style = placeholderTextBoxStyle,
                IsReadOnly = true,
                MinWidth = 100,
                MaxWidth = 150,
            };

            innerStackPanel1.Children.Add(DateTextBlock);
            innerStackPanel1.Children.Add(DateTextBox);
            innerStackPanel2.Children.Add(TimeTextBlock);
            innerStackPanel2.Children.Add(TimeTextBox);
            innerStackPanel3.Children.Add(ReasonTextBlock);
            innerStackPanel3.Children.Add(ReasonTextBox);
            innerStackPanel4.Children.Add(NoteTextBlock);
            innerStackPanel4.Children.Add(NoteTextBox);

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
                Models.Pass entityToEdit = _dbContext.Passes.Find(ItemDockPanel.Tag);
                if (entityToEdit != null)
                {
                    AddEditPass addEditPass = new AddEditPass(entityToEdit);
                    addEditPass.Closed += AddPass_Closed;
                    addEditPass.ShowDialog();
                }
            };

            DeleteBtn.Click += (s, e) =>
            {
                Models.Pass entityToDelete = _dbContext.Passes.Find(ItemDockPanel.Tag);
                if (_dbContext != null && entityToDelete != null)
                {
                    _dbContext.Passes.Remove(entityToDelete);
                    _dbContext.SaveChanges();
                    MessageBox.Show($"Пропуск от {entityToDelete.Date} успешно удален", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
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
            List<Models.Pass> passes = _dbContext.Passes.Include(p => p.IdReasonNavigation).ToList();

            foreach (var pass in passes)
            {
                CreateStackPanel(pass);
            }
        }

        private void AddNewPassBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditPass addEditPass = new AddEditPass();
            addEditPass.Closed += AddPass_Closed;
            addEditPass.ShowDialog();
        }

        private void AddPass_Closed(object? sender, EventArgs e)
        {
            FillStackPanel();
        }
    }
}