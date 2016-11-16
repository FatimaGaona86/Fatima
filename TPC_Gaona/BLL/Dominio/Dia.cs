using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dominio
{
    public class Dia
    {
        private int idDia;
        public int IdDia
        {
            get { return idDia; }
            set { idDia = value; }
        }

        private string _dia;
        public string _Dia
        {
            get { return _dia; }
            set { _dia = value; }
        }
    }
}
