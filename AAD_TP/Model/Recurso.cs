using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD_TP.Model
{
    public enum Estado_Recurso
    {
        Funcional,
        NaoFuncional,
        EmReparacao
    }

    public class Recurso
    {
        public Recurso(string id, string nome, string tipo, int idSeccao, string estado)
        {
            Id = id;
            Nome = nome;
            Tipo = tipo;
            Id_Seccao = idSeccao;
            Enum.TryParse(estado, out _estado);
        }

        public string Id { get; }

        public string Nome { get; }

        public string Tipo { get; }

        public int Id_Seccao { get; set; }

        private Estado_Recurso _estado;
        public Estado_Recurso Estado => _estado;
    }
}
