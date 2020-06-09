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
    /// Interaction logic for ListaProfissionaisUserControl.xaml
    /// </summary>
    public partial class ListaProfissionaisUserControl : UserControl
    {
        public ListaProfissionaisUserControl()
        {
            InitializeComponent();
            SimpleIoc.Default.Register<ListaProfissionaisUserControl>(() => this);
        }

        private void ListaProfissionais_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<MainViewModel>().Competencias = new ObservableCollection<string>(
                ServiceLocator.Current.GetInstance<DatabaseService>()
                    .GetCompetencias(((Professional)ListaProfissionais.SelectedItem).Id));

            ServiceLocator.Current.GetInstance<MainViewModel>().SelectedProfessional =
                (Professional)ListaProfissionais.SelectedItem;

            ServiceLocator.Current.GetInstance<MainViewModel>().Change_UserControl(UserControls.InfoProfissional);
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddProfissionalWindow();
            AddButton.IsEnabled = false;
            addWindow.Show();
        }
    }
}
