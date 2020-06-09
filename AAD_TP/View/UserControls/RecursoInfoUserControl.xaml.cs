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
using AAD_TP.Model;
using AAD_TP.View.Windows;
using AAD_TP.ViewModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace AAD_TP.View
{
    /// <summary>
    /// Interaction logic for RecursoInfoUserControl.xaml
    /// </summary>
    public partial class RecursoInfoUserControl : UserControl
    {
        public RecursoInfoUserControl()
        {
            InitializeComponent();
            SimpleIoc.Default.Register<RecursoInfoUserControl>(() => this);
        }

        private void ReturnButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<MainViewModel>().Change_UserControl(UserControls.ListaRecursos);
        }

        private void AddDeslocamentoButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddDeslocamentoWindow();
            AddDeslocamentoButton.IsEnabled = false;
            addWindow.Show();
        }

        private void AddReparacaoButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddReparacaoWindow();
            AddReparacaoButton.IsEnabled = false;
            addWindow.Show();
        }
    }
}
