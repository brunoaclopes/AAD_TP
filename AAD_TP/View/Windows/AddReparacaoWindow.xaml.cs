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

namespace AAD_TP.View.Windows
{
    /// <summary>
    /// Interaction logic for AddReparacaoWindow.xaml
    /// </summary>
    public partial class AddReparacaoWindow : Window
    {
        public AddReparacaoWindow()
        {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DatePicker.Text == "")
            {
                MessageBox.Show("Por favor preencha a data de entrega!");

                return;
            }

            if (DatePicker.SelectedDate.Value <= DateTime.Today)
            {
                MessageBox.Show("A data de entrega do dispositivo tem de ser posterior à data atual!");

                return;
            }

            ServiceLocator.Current.GetInstance<DatabaseService>().InsertReparacao(ServiceLocator.Current.GetInstance<MainViewModel>().SelectedRecurso.Id, DatePicker.SelectedDate);


            ServiceLocator.Current.GetInstance<MainViewModel>().Recursos = new ObservableCollection<Recurso>(ServiceLocator.Current.GetInstance<DatabaseService>().GetRecursos().OrderBy(recurso => recurso.Id_Seccao));

            ServiceLocator.Current.GetInstance<MainViewModel>().SelectedRecurso = ServiceLocator.Current
                .GetInstance<MainViewModel>().Recursos.First(recurso =>
                    recurso.Id == ServiceLocator.Current.GetInstance<MainViewModel>().SelectedRecurso.Id);

            ServiceLocator.Current.GetInstance<MainViewModel>().Estados = new ObservableCollection<Estado>(ServiceLocator.Current.GetInstance<DatabaseService>().GetEstados(ServiceLocator.Current.GetInstance<MainViewModel>().SelectedRecurso.Id).OrderByDescending(estado => estado.Entrega));

            ServiceLocator.Current.GetInstance<RecursoInfoUserControl>().AddReparacaoButton.IsEnabled = true;

            Close();
        }

        private void OtherButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<DatabaseService>().InsertReparacao(ServiceLocator.Current.GetInstance<MainViewModel>().SelectedRecurso.Id);

            ServiceLocator.Current.GetInstance<MainViewModel>().Recursos = new ObservableCollection<Recurso>(ServiceLocator.Current.GetInstance<DatabaseService>().GetRecursos().OrderBy(recurso => recurso.Id_Seccao));

            ServiceLocator.Current.GetInstance<MainViewModel>().SelectedRecurso = ServiceLocator.Current
                .GetInstance<MainViewModel>().Recursos.First(recurso =>
                    recurso.Id == ServiceLocator.Current.GetInstance<MainViewModel>().SelectedRecurso.Id);

            ServiceLocator.Current.GetInstance<RecursoInfoUserControl>().EstadoListView.IsEnabled = false;

            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<RecursoInfoUserControl>().AddReparacaoButton.IsEnabled = true;

            Close();
        }
    }
}
