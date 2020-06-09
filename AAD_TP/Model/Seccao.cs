using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD_TP.Model
{
    public class Seccao
    {
        public Seccao(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id { get; }

        public string Nome { get; set; }
    }
}
