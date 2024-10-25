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
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private dbContext _dbContext;

        public Home(dbContext context)
        {
            InitializeComponent();
            _dbContext = context;

            Cycliccommission c1 = new Cycliccommission { Id = 1, Name = "test1" };
            Cycliccommission c2 = new Cycliccommission { Id = 2, Name = "test2"};
            _dbContext.Cycliccommissions.Add(c1);
            _dbContext.Cycliccommissions.Add(c2);

            _dbContext.SaveChanges();

            var obj = _dbContext.Cycliccommissions.ToList();

            Title.Text = obj.Count.ToString();
        }
    }
}
