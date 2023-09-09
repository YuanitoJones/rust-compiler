using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    public enum Tipotokens
    {
        id,
        numentero,
        numdecimal,
        cadena,
        caracter,
        boolean,
        oparitmetico,
        oplogico,
        opcomparacion,
        expasignacion,
        expasignacioncomp,
        booleanoflojo,
        delimitadores,
        otro,
        palabraReservada,
        desc
    }
    public class Token
    {
        private Tipotokens tipotoken;
        private int valor;
        private string caracteres;
        private int linea;

        public Tipotokens Tipotoken { get => tipotoken; set => tipotoken = value; }
        public int Valor { get => valor; set => valor = value; }
        public string Caracteres { get => caracteres; set => caracteres = value; }
        public int Linea { get => linea; set => linea = value; }
    }
}