using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD_TP.Model
{
    public class Estado
    {
        public Estado(DateTime pedido, DateTime entrega)
        {
            Pedido = pedido;
            Entrega = entrega;
        }

        public DateTime Pedido { get; set; }

        public DateTime Entrega { get; set; } 
    }
}
