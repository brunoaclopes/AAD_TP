using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD_TP.Model
{
    public enum Tipo_Profissional
    {
        Medico,
        Enfermeiro
    }

    public class Professional
    {
        public Professional(int id, string nome, string tipo, string especialidade, int idHospital)
        {
            Id = id;
            Nome = nome;
            Especialidade = especialidade;
            Id_Hospital = idHospital;
            Competencias = new ObservableCollection<int>();
            Enum.TryParse(tipo, out _tipo);
        }

        public int Id { get; }

        public string Nome { get; }

        private Tipo_Profissional _tipo;
        public Tipo_Profissional Tipo => _tipo;

        public string Especialidade { get; }

        public int Id_Hospital { get; }

        public ObservableCollection<int> Competencias { get; set; }
    }
}
