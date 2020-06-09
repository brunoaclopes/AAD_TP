using System;
using System.Collections.Generic;
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
using AAD_TP.ViewModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace AAD_TP.View
{
    /// <summary>
    /// Interaction logic for ProfessionalInfoUserControl.xaml
    /// </summary>
    public partial class ProfessionalInfoUserControl : UserControl
    {
        public ProfessionalInfoUserControl()
        {
            InitializeComponent();
            SimpleIoc.Default.Register<ProfessionalInfoUserControl>(() => this);
        }

        private void ReturnButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<MainViewModel>().Change_UserControl(UserControls.ListaProfissionais);
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddCompetenciaWindow();
            AddButton.IsEnabled = false;
            addWindow.Show();
        }
    }
}
