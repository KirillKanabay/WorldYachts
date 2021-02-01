using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldYachts.Data;

namespace WorldYachts.View.AccessoryControlViews
{
    /// <summary>
    /// Логика взаимодействия для FitExpanderView.xaml
    /// </summary>
    public partial class FitExpanderView : UserControl
    {
        public string Name { get; set; }
        public IEnumerable<Boat> Boats { get; set; }
        public FitExpanderView()
        {
            InitializeComponent();
        }
    }
}
