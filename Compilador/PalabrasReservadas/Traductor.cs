using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.PalabrasReservadas
{
    class Traductor : Diccionario
    {
        public int convertir(string cadena)
        {
            try
            {
                int resultado = palabras[cadena];//Convierte el string encontrado en su valor dentro de la matriz
                return resultado;
            }catch(Exception)
            {
                return -1;
            }
        }
    }
}
