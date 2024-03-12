using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MPT_UniversalCalculator
{
    /// <summary>
    /// Логика взаимодействия для Roots.xaml
    /// </summary>
    public partial class Roots : Window
    {
        public ObservableCollection<Root> RootsData { get; set; }
        public Roots(ObservableCollection<Root> rootsdata)
        {

            RootsData = rootsdata;
            DataContext = this;
            InitializeComponent();
            
        }
        
    }
    public class Root
    {
        public String Number { get; set; }
        public String RootValue { get; set; }
    }
}
