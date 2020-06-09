using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD_TP.Model
{
    public class Deslocamento
    {
        public Deslocamento(int id, int origem, int destino)
        {
            Id = id;
            Origem = origem;
            Destino = destino;
        } 

        public int Id { get; }

        public int Origem { get; }

        public int Destino { get; }

    }
}
