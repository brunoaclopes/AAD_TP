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
    /// Interaction logic for ListaRecursosUserControl.xaml
    /// </summary>
    public partial class ListaRecursosUserControl : UserControl
    {
        public ListaRecursosUserControl()
        {
            InitializeComponent();
            SimpleIoc.Default.Register<ListaRecursosUserControl>(() => this);
        }

        private void ListaRecursos_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListaRecursos.SelectedItem == null) return;

            ServiceLocator.Current.GetInstance<MainViewModel>().Deslocamentos = new ObservableCollection<Deslocamento>(
                ServiceLocator.Current.GetInstance<DatabaseService>()
                    .GetDeslocamentos( ((Recurso)ListaRecursos.SelectedItem).Id) );

            ServiceLocator.Current.GetInstance<MainViewModel>().Estados = new ObservableCollection<Estado>(
                ServiceLocator.Current.GetInstance<DatabaseService>()
                    .GetEstados( ((Recurso)ListaRecursos.SelectedItem).Id)
                    .OrderByDescending(estado => estado.Entrega));

            ServiceLocator.Current.GetInstance<MainViewModel>().SelectedRecurso =
                (Recurso)ListaRecursos.SelectedItem;


            if (ServiceLocator.Current.GetInstance<MainViewModel>().SelectedRecurso.Estado ==
                Estado_Recurso.NaoFuncional)
            {
                ServiceLocator.Current.GetInstance<RecursoInfoUserControl>().EstadoListView.IsEnabled = false;
                ServiceLocator.Current.GetInstance<RecursoInfoUserControl>().AddReparacaoButton.IsEnabled = false;
            }
            else
            {
                ServiceLocator.Current.GetInstance<RecursoInfoUserControl>().EstadoListView.IsEnabled = true;
                ServiceLocator.Current.GetInstance<RecursoInfoUserControl>().AddReparacaoButton.IsEnabled = true;

            }

            ServiceLocator.Current.GetInstance<MainViewModel>().Change_UserControl(UserControls.InfoRecurso);
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddRecursoWindow();
            AddButton.IsEnabled = false;
            addWindow.Show();
        }
    }
}
