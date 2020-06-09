using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using AAD_TP.Model;
using AAD_TP.Services;
using AAD_TP.View;
using CommonServiceLocator;
using GalaSoft.MvvmLight;

namespace AAD_TP.ViewModel
{
    public enum UserControls
    {
        ListaHospitais,
        ListaProfissionais,
        ListaRecursos,
        InfoHospital,
        InfoProfissional,
        InfoRecurso
    }

    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Hospitais = new ObservableCollection<Hospital>(ServiceLocator.Current.GetInstance<DatabaseService>().GetHospitais().OrderBy(hospital => hospital.Id));
            Professionais = new ObservableCollection<Professional>(ServiceLocator.Current.GetInstance<DatabaseService>().GetProfissionais().OrderBy(professional => professional.Id));
            Recursos = new ObservableCollection<Recurso>(ServiceLocator.Current.GetInstance<DatabaseService>().GetRecursos().OrderBy(recurso => recurso.Id_Seccao));
            Tipos_Recursos = new ObservableCollection<Tipo_Recurso>(ServiceLocator.Current.GetInstance<DatabaseService>().GetTipoRecursos().OrderBy(recurso => recurso.Id));
        }

        public ObservableCollection<Hospital> Hospitais { get; set; }

        private ObservableCollection<Professional> _professionals;
        public ObservableCollection<Professional> Professionais
        {
            get => _professionals;
            set => Set(ref _professionals, value);
        }

        private ObservableCollection<Recurso> _recursos;
        public ObservableCollection<Recurso> Recursos
        {
            get => _recursos;
            set => Set(ref _recursos, value);
        }

        public ObservableCollection<Tipo_Recurso> Tipos_Recursos { get; set; }

        private Hospital _selectedHospital;
        public Hospital SelectedHospital
        {
            get => _selectedHospital;
            set => Set(ref _selectedHospital, value);
        }

        private Professional _selectedProfessional;
        public Professional SelectedProfessional
        {
            get => _selectedProfessional;
            set => Set(ref _selectedProfessional, value);
        }

        private Recurso _selectedRecurso;
        public Recurso SelectedRecurso
        {
            get => _selectedRecurso;
            set => Set(ref _selectedRecurso, value);
        }

        private ObservableCollection<Seccao> _seccoes;
        public ObservableCollection<Seccao> Seccoes
        {
            get => _seccoes;
            set => Set(ref _seccoes, value);
        }

        private ObservableCollection<string> _competencias;
        public ObservableCollection<string> Competencias 
        {
            get => _competencias;
            set => Set(ref _competencias, value);
        }

        private ObservableCollection<Deslocamento> _deslocamentos;
        public ObservableCollection<Deslocamento> Deslocamentos
        {
            get => _deslocamentos;
            set => Set(ref _deslocamentos, value);
        }

        private ObservableCollection<Estado> _estados;
        public ObservableCollection<Estado> Estados
        {
            get => _estados;
            set => Set(ref _estados, value);
        }


        public void Change_UserControl(UserControls userControls)
        {
            switch (userControls)
            {
                case UserControls.ListaHospitais:
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaHospitaisUserControl.Visibility =
                        Visibility.Visible; 
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaProfissionaisUserControl.Visibility =
                        Visibility.Collapsed; 
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaRecursosUserControl.Visibility =
                        Visibility.Collapsed; 
                    ServiceLocator.Current.GetInstance<MainWindow>().HospitalInfoUserControl.Visibility =
                        Visibility.Collapsed; 
                    ServiceLocator.Current.GetInstance<MainWindow>().ProfessionalInfoUserControl.Visibility =
                        Visibility.Collapsed; 
                    ServiceLocator.Current.GetInstance<MainWindow>().RecusroInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    break;
                case UserControls.ListaProfissionais:
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaHospitaisUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaProfissionaisUserControl.Visibility =
                        Visibility.Visible;
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaRecursosUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().HospitalInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ProfessionalInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().RecusroInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    break;
                case UserControls.ListaRecursos:
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaHospitaisUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaProfissionaisUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaRecursosUserControl.Visibility =
                        Visibility.Visible;
                    ServiceLocator.Current.GetInstance<MainWindow>().HospitalInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ProfessionalInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().RecusroInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    break;
                case UserControls.InfoHospital:
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaHospitaisUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaProfissionaisUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaRecursosUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().HospitalInfoUserControl.Visibility =
                        Visibility.Visible;
                    ServiceLocator.Current.GetInstance<MainWindow>().ProfessionalInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().RecusroInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    break;
                case UserControls.InfoProfissional:
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaHospitaisUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaProfissionaisUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaRecursosUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().HospitalInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ProfessionalInfoUserControl.Visibility =
                        Visibility.Visible;
                    ServiceLocator.Current.GetInstance<MainWindow>().RecusroInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    break;
                case UserControls.InfoRecurso:
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaHospitaisUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaProfissionaisUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ListaRecursosUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().HospitalInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().ProfessionalInfoUserControl.Visibility =
                        Visibility.Collapsed;
                    ServiceLocator.Current.GetInstance<MainWindow>().RecusroInfoUserControl.Visibility =
                        Visibility.Visible;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(userControls), userControls, null);
            }
        }

    }
}