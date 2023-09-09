using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.matrizSintactica
{
    class Columna:Diccionario_columna
    {
        public int convertir(int token)
        {
            int columna = coltoken[token];//Convierte el string encontrado en su valor dentro de la matriz
            return columna;
        }
    }
}
