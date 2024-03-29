﻿using System;
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
        public ObservableCollection<HistoryDataType> HistoryData { get; set; }
        public History(ObservableCollection<HistoryDataType> history_data)
        {
            HistoryData = history_data;
            DataContext = this;
            InitializeComponent();
        }

        
    }

    public class HistoryDataType
    {
        public String Number_Type { get; set; }
        public String Operand_1 { get; set; }
        public String Operation { get; set; }
        public String Operand_2 { get; set; }
        public String Result { get; set; }
    }
}
