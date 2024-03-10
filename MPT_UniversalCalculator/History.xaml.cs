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
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Window
    {
        public ObservableCollection<HistoryDataClass> HistoryDataCollection { get; set; }
        public History()
        {
            InitializeComponent();
            HistoryDataCollection = new ObservableCollection<HistoryDataClass>
            {
                new HistoryDataClass { Value = "Value1", Command = "Value2" },
                new HistoryDataClass { Value = "Value3", Command = "Value4" }
            };

            DataContext = this;
        }
        public class HistoryDataClass
        {
            public string Value { get; set; }
            public string Command { get; set; }
        }
    }
}
