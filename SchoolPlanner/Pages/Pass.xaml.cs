﻿using SchoolPlanner;
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
    /// Логика взаимодействия для Pass.xaml
    /// </summary>
    public partial class Pass : Page
    {
        private SchoolPlannerContext _dbContext = MainWindow.dbContext;
        public Pass()
        {
            InitializeComponent();
            
        }
    }
}
