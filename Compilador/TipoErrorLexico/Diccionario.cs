using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.TipoErrorLexico
{
    class Diccionario
    {
        public Dictionary<int, string> errores = new Dictionary<int, string>
        {
            {-500, "Identificador no valido"}, {-501, "Numero entero no valido"}, 
            {-502, "Numero decimal no valido"}, {-503, "Formato de cadena invalido"}, 
            {-504, "S esperaba un caracter"}, {-505, "Formato de caracter invalido"}, 
            {-506, "Palabra desconocida"}, {-507, "Se esperaba fin de comentario multilinea"}
        };
    }
}
