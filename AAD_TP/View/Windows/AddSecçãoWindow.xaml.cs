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
using AAD_TP.Model;
using AAD_TP.Services;
using AAD_TP.ViewModel;
using CommonServiceLocator;

namespace AAD_TP.View
{
    /// <summary>
    /// Interaction logic for AddSecçãoWindow.xaml
    /// </summary>
    public partial class AddSecçãoWindow : Window
    {
        public AddSecçãoWindow()
        {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (NomeTextBox.Text == "") return;
            ServiceLocator.Current.GetInstance<DatabaseService>().InsertSeccao(
                ServiceLocator.Current.GetInstance<MainViewModel>().SelectedHospital.Id,
                NomeTextBox.Text);
            
            ServiceLocator.Current.GetInstance<HospitalInfoUserControl>().AddButton.IsEnabled = true;

            ServiceLocator.Current.GetInstance<MainViewModel>().Seccoes = new ObservableCollection<Seccao>(
                ServiceLocator.Current.GetInstance<DatabaseService>()
                    .GetSeccoes(ServiceLocator.Current.GetInstance<MainViewModel>().SelectedHospital.Id)
                    .OrderBy(seccao => seccao.Id));

            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<HospitalInfoUserControl>().AddButton.IsEnabled = true;

            Close();
        }
    }
}
