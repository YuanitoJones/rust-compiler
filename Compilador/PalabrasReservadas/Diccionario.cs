using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.PalabrasReservadas
{
    class Diccionario
    {
        public Dictionary<string, int> palabras = new Dictionary<string, int>
        {
            {"as", -64}, {"break", -65}, {"const", -66}, {"continue", -67}, {"crate", -68}, {"else", -69}, {"enum", -70},
            {"extern", -71}, {"false", -72}, {"fn", -73}, {"for", -74}, {"if", -75}, {"impl", -76}, {"in", -77}, {"let", -78},
            {"loop", -79}, {"match", -80}, {"mod", -81}, {"move", -82}, {"mut", -83}, {"pub", -84}, {"ref", -85}, {"return", -86},
            {"self", -87}, {"Self", -88}, {"static", -89}, {"struct", -90}, {"super", -91}, {"trait", -92}, {"true", -93},
            {"type", -94}, {"unsafe", -95}, {"use", -96}, {"where", -97}, {"while", -98}, {"async", -99}, {"await", -100},
            {"dyn", -101}, {"abstract", -102}, {"become", -103}, {"box", -104}, {"do", -105}, {"final", -106}, {"macro", -107},
            {"override", -108}, {"priv", -109}, {"typeof", -110}, {"unsized", -111}, {"virtual", -112}, {"yield", -113},
            {"try", -114}, {"union", -115}, {"println!", -116}, {"dbg!", -117}, {"panic!", -118},

            {"i32", -151}, {"f32", -152}, {"String", -153}, {"&str", -154}, {"bool", -155}, {"main", -156}, 
            {"Tuple", -157}, {"io", -158}, {"stdin", -159}, {"read_line", -160}, {"ok", -161}, {"expect", -162}, {"char", -163}

        };
    }
}
