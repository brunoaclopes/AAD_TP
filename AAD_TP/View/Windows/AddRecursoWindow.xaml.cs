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
    /// Interaction logic for AddRecursoWindow.xaml
    /// </summary>
    public partial class AddRecursoWindow : Window
    {
        public AddRecursoWindow()
        {
            InitializeComponent();
            ServiceLocator.Current.GetInstance<MainViewModel>().Seccoes = new ObservableCollection<Seccao>(ServiceLocator.Current.GetInstance<DatabaseService>().GetSeccoes().OrderBy(seccao => seccao.Id));
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (IdTextBox.Text == "" || NomeTextBox.Text == "" || TipoComboBox.SelectedItem == null ||
                IdSeccaoComboBox.SelectedItem == null)
            {
                MessageBox.Show("Por favor preencha todos os campos!");
                return;
            }

            ServiceLocator.Current.GetInstance<DatabaseService>().InsertRecurso(IdTextBox.Text, NomeTextBox.Text, ((Seccao)IdSeccaoComboBox.SelectedItem).Id, ((Tipo_Recurso)TipoComboBox.SelectedItem).Id);

            ServiceLocator.Current.GetInstance<MainViewModel>().Recursos = new ObservableCollection<Recurso>(ServiceLocator.Current.GetInstance<DatabaseService>().GetRecursos().OrderBy(recurso => recurso.Id_Seccao));

            ServiceLocator.Current.GetInstance<ListaRecursosUserControl>().AddButton.IsEnabled = true;

            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<ListaRecursosUserControl>().AddButton.IsEnabled = true;

            Close();
        }
    }
}
