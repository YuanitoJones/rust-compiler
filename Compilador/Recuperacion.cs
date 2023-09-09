using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    public enum TipoRecuperacion
    {
        Ninguno,
        Falta,
        Sobra,
        Diferentes,
        Urgencia,
        Bai
    }
    public class Recuperacion
    {
        private TipoRecuperacion tipoRecuperacion;

        public TipoRecuperacion TipoRecuperacion { get => tipoRecuperacion; set => tipoRecuperacion = value; }
    }
}
