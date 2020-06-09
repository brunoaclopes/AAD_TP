using System;
using System.Collections.ObjectModel;

namespace AAD_TP.Model
{
    public enum Tipo_Hospital
    {
        publico,
        privado
    }
    public class Hospital
    {
        public Hospital(int id, string nome, string tipo, string codigoPostal, string arruamento, string localidade, string concelho, string distrito)
        {
            Id = id;
            Nome = nome;
            Codigo_Postal = codigoPostal;
            Arruamento = arruamento;
            Localidade = localidade;
            Concelho = concelho;
            Distrito = distrito;
            Seccoes = new ObservableCollection<Seccao>();
            Enum.TryParse(tipo, out _tipo);
        }

        public int Id { get; }

        public string Nome { get; }

        private Tipo_Hospital _tipo;
        public Tipo_Hospital Tipo => _tipo;

        public string Codigo_Postal { get; set; }

        public string Arruamento { get; set; }

        public string Localidade { get; }

        public string Concelho { get; }

        public string Distrito { get; }

        public ObservableCollection<Seccao> Seccoes { get; set; }
    }
}
