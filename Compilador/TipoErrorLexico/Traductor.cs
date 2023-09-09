using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.TipoErrorLexico
{
    class Traductor : Diccionario
    {
        public string convertir(int token)
        {
            try
            {
                string mensaje = errores[token];//Convierte el string encontrado en su valor dentro de la matriz
                return mensaje;
            }
            catch (Exception)
            {
                return "Error desconocido";
            }
        }
    }
}
