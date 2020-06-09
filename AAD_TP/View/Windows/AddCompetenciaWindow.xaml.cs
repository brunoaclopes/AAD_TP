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
    /// Interaction logic for AddCompetenciaWindow.xaml
    /// </summary>
    public partial class AddCompetenciaWindow : Window
    {
        public AddCompetenciaWindow()
        {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ServiceLocator.Current.GetInstance<MainViewModel>().Competencias.Any(competencia => ((Tipo_Recurso)CompetenciaComboBox.SelectedItem).Nome == competencia))
            {
                ServiceLocator.Current.GetInstance<ProfessionalInfoUserControl>().AddButton.IsEnabled = true;

                MessageBox.Show("Competência já existente!");

                return;
            }

            ServiceLocator.Current.GetInstance<DatabaseService>().InsertCompetencia(ServiceLocator.Current.GetInstance<MainViewModel>().SelectedProfessional.Id, ((Tipo_Recurso)CompetenciaComboBox.SelectedItem).Id);

            ServiceLocator.Current.GetInstance<ProfessionalInfoUserControl>().AddButton.IsEnabled = true;

            ServiceLocator.Current.GetInstance<MainViewModel>().Competencias = new ObservableCollection<string>(
                ServiceLocator.Current.GetInstance<DatabaseService>().GetCompetencias(ServiceLocator.Current.GetInstance<MainViewModel>().SelectedProfessional.Id));

            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<ProfessionalInfoUserControl>().AddButton.IsEnabled = true;

            Close();
        }
    }
}
