using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for AddDeslocamentoWindow.xaml
    /// </summary>
    public partial class AddDeslocamentoWindow : Window
    {
        public AddDeslocamentoWindow()
        {
            InitializeComponent();
            ServiceLocator.Current.GetInstance<MainViewModel>().Seccoes = new ObservableCollection<Seccao>(ServiceLocator.Current.GetInstance<DatabaseService>().GetSeccoes().OrderBy(seccao => seccao.Id));
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ProfissionalComboBox.SelectedItem == null ||
                DestinoComboBox.SelectedItem == null)
            {
                MessageBox.Show("Por favor preencha todos os campos!");

                return;
            }

            ServiceLocator.Current.GetInstance<DatabaseService>().InsertDeslocamneto(
                ServiceLocator.Current.GetInstance<MainViewModel>().SelectedRecurso.Id,
                ((Professional) ProfissionalComboBox.SelectedItem).Id, ((Seccao) DestinoComboBox.SelectedItem).Id);

            ServiceLocator.Current.GetInstance<MainViewModel>().Deslocamentos = new ObservableCollection<Deslocamento>(
                ServiceLocator.Current.GetInstance<DatabaseService>()
                    .GetDeslocamentos(ServiceLocator.Current.GetInstance<MainViewModel>().SelectedRecurso.Id) );

            ServiceLocator.Current.GetInstance<MainViewModel>().Recursos = new ObservableCollection<Recurso>(
                ServiceLocator.Current.GetInstance<DatabaseService>().GetRecursos()
                    .OrderBy(recurso => recurso.Id_Seccao));

            ServiceLocator.Current.GetInstance<RecursoInfoUserControl>().AddDeslocamentoButton.IsEnabled = true;

            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<RecursoInfoUserControl>().AddDeslocamentoButton.IsEnabled = true;

            Close();
        }
    }
}
