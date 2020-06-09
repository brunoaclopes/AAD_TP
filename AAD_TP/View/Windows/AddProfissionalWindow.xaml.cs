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
    /// Interaction logic for AddProfissionalWindow.xaml
    /// </summary>
    public partial class AddProfissionalWindow : Window
    {
        public AddProfissionalWindow()
        {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if(NomeTextBox.Text == "" || TipoComboBox.Text == "" || EspecialidadeTextBox.Text == "" || HospitalComboBox.SelectedItem == null) return;
            ServiceLocator.Current.GetInstance<DatabaseService>().InsertProfessional(NomeTextBox.Text, TipoComboBox.Text, EspecialidadeTextBox.Text, ((Hospital)HospitalComboBox.SelectedItem).Id);

            ServiceLocator.Current.GetInstance<ListaProfissionaisUserControl>().AddButton.IsEnabled = true;

            ServiceLocator.Current.GetInstance<MainViewModel>().Professionais = new ObservableCollection<Professional>(ServiceLocator.Current.GetInstance<DatabaseService>().GetProfissionais().OrderBy(professional => professional.Id));

            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<ListaProfissionaisUserControl>().AddButton.IsEnabled = true;

            Close();
        }
    }
}
