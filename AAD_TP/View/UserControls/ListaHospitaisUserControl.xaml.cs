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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AAD_TP.Model;
using AAD_TP.Services;
using AAD_TP.ViewModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace AAD_TP.View
{
    /// <summary>
    /// Interaction logic for ListaHospitaisUserControl.xaml
    /// </summary>
    public partial class ListaHospitaisUserControl : UserControl
    {
        public ListaHospitaisUserControl()
        {
            InitializeComponent();
        }

        private void ListaHospitais_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<MainViewModel>().Seccoes = new ObservableCollection<Seccao>(
                ServiceLocator.Current.GetInstance<DatabaseService>()
                    .GetSeccoes(((Hospital) ListaHospitais.SelectedItem).Id)
                    .OrderBy(seccao => seccao.Id));

            ServiceLocator.Current.GetInstance<MainViewModel>().SelectedHospital =
                (Hospital) ListaHospitais.SelectedItem;

            ServiceLocator.Current.GetInstance<MainViewModel>().Change_UserControl(UserControls.InfoHospital);
        }
    }
}
