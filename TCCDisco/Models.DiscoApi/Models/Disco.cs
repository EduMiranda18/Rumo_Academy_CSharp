using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DiscoApi.Models
{
    public class Disco
    {
        public string Nome { get; set; }
        
        public decimal Preco { get; set; }
       
        public decimal PrecoAntigo { get; set; }  
       
        public string Link { get; set; }  
        
        public DateTime DataBusca { get; set; } = DateTime.Now;

    }
}
