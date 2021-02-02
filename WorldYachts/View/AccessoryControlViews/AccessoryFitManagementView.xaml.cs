using System;
using System.Collections.Generic;
using System.Linq;
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
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Model;
using WorldYachts.ViewModel.AccessoryControlViewModels;

namespace WorldYachts.View.AccessoryControlViews
{
    /// <summary>
    /// Логика взаимодействия для AccessoryFitManagementView.xaml
    /// </summary>
    public partial class AccessoryFitManagementView : UserControl
    {
        public AccessoryFitManagementView()
        {
            InitializeComponent();
        }

        private async void Chip_OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var boatName = ((Chip) sender).Content.ToString();
            var accessoryName = Expander.HeaderProperty.Name;
            var dataContext = (AccessoryFitManagementViewModel) DataContext;
            //var accessoryToBoatModel = new AccessoryToBoatModel();
            //var accessoryToBoatCollection = await accessoryToBoatModel.LoadAsync();
            //await accessoryToBoatModel.RemoveAsync(accessoryToBoatCollection.Where(i => i.BoatId == boat.Id));
        }
    }
}
