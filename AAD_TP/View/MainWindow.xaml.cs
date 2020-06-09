using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AAD_TP.ViewModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace AAD_TP.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SimpleIoc.Default.Register<MainWindow>(() => this);
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TopBar_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ListViewMenu_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ListViewMenu.SelectedIndex)
            {
                case 0:
                    ServiceLocator.Current.GetInstance<MainViewModel>().Change_UserControl(UserControls.ListaHospitais);
                    break;
                case 1:
                    ServiceLocator.Current.GetInstance<MainViewModel>().Change_UserControl(UserControls.ListaProfissionais);
                    break;
                default:
                    ServiceLocator.Current.GetInstance<MainViewModel>().Change_UserControl(UserControls.ListaRecursos);
                    break;
            }
        }
    }
}
