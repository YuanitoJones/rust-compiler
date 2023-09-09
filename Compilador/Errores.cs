using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    public enum TipoError
    {
        Lexico,
        Sintactico,
        Semantico
    }
    public class Errores
    {
        private TipoError tipoerror;
        private int valorerror;
        private int linea;
        private string mensaje;

        public TipoError Tipoerror { get => tipoerror; set => tipoerror = value; }
        public int Valorerror { get => valorerror; set => valorerror = value; }
        public int Linea { get => linea; set => linea = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
    }
}
