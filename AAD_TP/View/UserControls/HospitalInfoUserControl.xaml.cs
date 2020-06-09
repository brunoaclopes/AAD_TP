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
using AAD_TP.Services;
using AAD_TP.ViewModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace AAD_TP.View
{
    /// <summary>
    /// Interaction logic for HospitalInfoUserControl.xaml
    /// </summary>
    public partial class HospitalInfoUserControl : UserControl
    {
        public HospitalInfoUserControl()
        {
            InitializeComponent();
            SimpleIoc.Default.Register<HospitalInfoUserControl>(() => this);
        }

        private void ReturnButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<MainViewModel>().Change_UserControl(UserControls.ListaHospitais);
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddSecçãoWindow();
            addWindow.Show();
            AddButton.IsEnabled = false;
        }
    }
}
