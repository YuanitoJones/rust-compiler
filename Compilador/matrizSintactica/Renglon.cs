using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.matrizSintactica
{
    class Renglon:Diccionario_renglon
    {
        public int convertir(int regla)
        {
            int renglon = renregla[regla];//Convierte el string encontrado en su valor dentro de la matriz
            return renglon;
        }
    }
}
