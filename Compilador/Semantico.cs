﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    public enum Alcance
    {
        Public,
        Private
    }
    public enum TipoDato
    {
        i32,
        f32,
        CHAR,
        BOOL,
        STRING,
        NADA
    }
    public enum Regreso
    {
        INT,
        STRING,
        DOUBLE,
        CHAR,
        BOOL,
        OBJECT,
        BYTE,
        VOID
    }
    public enum TipoVariable
    {
        variableLocal,
        parametro
    }


    /// <summary>
    /// Es el nodo donde se guardan los registros de clases en la TSClases
    /// </summary>
    public class NodoClase
    {
        private string lexema;
        private Alcance miAlcance;
        private string herencia; // lexema de clase a heredar
        private int renglonDeclaracion;
        private int[] referencias;
        private int count=0;

        private Dictionary<string, NodoAtributo> tablaSimbolosAtributos =
            new Dictionary<string, NodoAtributo>();

        private Dictionary<string, NodoMetodo> tablaSimbolosMetodos =
            new Dictionary<string, NodoMetodo>();

        public string Lexema
        {
            get
            {
                return lexema;
            }

            set
            {
                lexema = value;
            }
        }
        public Alcance MiAlcance
        {
            get
            {
                return miAlcance;
            }

            set
            {
                miAlcance = value;
            }
        }
        public string Herencia
        {
            get
            {
                return herencia;
            }

            set
            {
                herencia = value;
            }
        }
        public int RenglonDeclaracion
        {
            get
            {
                return renglonDeclaracion;
            }

            set
            {
                renglonDeclaracion = value;
            }
        }
        public int[] Referencias
        {
            get
            {
                return referencias;
            }

            set
            {
                referencias = value;
            }
        }
        public Dictionary<string, NodoAtributo> TSA
        {
            get
            {
                return tablaSimbolosAtributos;
            }

            set
            {
                tablaSimbolosAtributos = value;
            }
        }
        public Dictionary<string, NodoMetodo> TSM
        {
            get
            {
                return tablaSimbolosMetodos;
            }

            set
            {
                tablaSimbolosMetodos = value;
            }
        }

        public int Count { get => count; set => count = value; }
    }
    public class NodoAtributo
    {
        public string lexema;
        public Alcance miAlcance;
        public TipoDato miTipo;
        public string valor;
        public string direccionMemoria;
        public int reglonDec;
        public int[] referencias;
    }
    public class NodoVariable
    {
        public string lexema;
        public TipoDato miTipoDato;
        public string valor;
        public TipoVariable miTipoVariable;
        public string direccionMemoria;
        public int reglonDec;
        public int[] referencias;
    }
    public class NodoMetodo
    {
        public string lexema;
        public Alcance miAlcance;
        public Regreso miRegreso;
        public int renglonDeclaracion;
        public int[] referencias;

        public Dictionary<object, NodoVariable> TablaSimbolosVariables =
            new Dictionary<object, NodoVariable>();
    }

    static public class TablaSimbolos
    {
        static public List<Errores> listaErroresSemantico;

        //dicionario de datos como una tabla de simbolos
        //tabla de simbolos raiz (principal)
        private static Dictionary<object, NodoClase> tablaSimbolosClase =
            new Dictionary<object, NodoClase>();

        /// <summary>
        /// propiedades del diccionario de nodo clases
        /// </summary>
        public static Dictionary<object, NodoClase> TablaSimbolosClase
        {
            get
            {
                return tablaSimbolosClase;
            }

            set
            {
                tablaSimbolosClase = value;
            }
        }

        #region METODOS para TS de CLASES

        /// <summary>
        /// Metodo para insertar los nodos de clases en la TS
        /// </summary>
        /// <param name="miNodoClase">nodo clase</param>
        static public void InsertarNodoClase(NodoClase miNodoClase)
        {
            if (!TablaSimbolosClase.ContainsKey(miNodoClase.Lexema)) // verificar una colision
            {
                TablaSimbolosClase.Add(miNodoClase.Lexema, miNodoClase);
            }
            else
            {
                var error = new Errores() { Valorerror = 601, Linea = miNodoClase.RenglonDeclaracion, Mensaje = "Ya existe una implementacion con ese identificador", Tipoerror = TipoError.Semantico };
                TablaSimbolos.listaErroresSemantico.Add(error);
            }
        }

        static public NodoClase BusquedaNodoClasePorLexema(string lexema)
        {
            if (TablaSimbolosClase.ContainsKey(lexema))
            {
                return TablaSimbolosClase.FirstOrDefault(x => x.Key.ToString() == lexema).Value;
            }
            else
            {
                var error = new Errores() { Valorerror = 602, Linea = 0, Mensaje = "No existe en el contexto una clase con ese nombre", Tipoerror = TipoError.Semantico };
                TablaSimbolos.listaErroresSemantico.Add(error);
                return null;
            }

        }

        static public void ExisteClaseHeredada(string lexema)
        {
            if (!TablaSimbolosClase.ContainsKey(lexema))
            {
                var error = new Errores() { Valorerror = 603, Linea = 0, Mensaje = "No se encontro la clase a heredar", Tipoerror = TipoError.Semantico };
                TablaSimbolos.listaErroresSemantico.Add(error);
            }
        }

        #endregion
        #region METODOS para TS de Atributos
        static public void InsertarNodoAtributo
            (NodoAtributo miNodoAtributo, NodoClase nodoClaseActual)
        {
            if (!nodoClaseActual.TSA.ContainsKey(miNodoAtributo.lexema))
            {
                nodoClaseActual.TSA.Add(miNodoAtributo.lexema, miNodoAtributo);
            }
            else
            {
                var error = new Errores() { Valorerror = 601, Linea = miNodoAtributo.reglonDec, Mensaje = "Ya existe un miembro con ese identificador", Tipoerror = TipoError.Semantico };
                TablaSimbolos.listaErroresSemantico.Add(error);
            }

        }
        static public NodoAtributo ObtenerNodoAtributo(string lexema, NodoClase nodoClaseActiva, int linea)
        {
            if (nodoClaseActiva.TSA
               .ContainsKey(lexema))
                return nodoClaseActiva
                    .TSA
                    .SingleOrDefault(x => x.Key == lexema).Value;
            else
            {
                var error = new Errores() { Valorerror = 601, Linea = 0, Mensaje = "No se Encontro el atributo en el contexto actual", Tipoerror = TipoError.Semantico };
                TablaSimbolos.listaErroresSemantico.Add(error);
                return null;
            }
        }

        static public TipoDato ObtenerTipoDatoAtributo(string lexema, NodoClase nodoClaseActiva)
        {
            if (nodoClaseActiva.TSA.ContainsKey(lexema))
            {
                return (nodoClaseActiva.TSA.FirstOrDefault(x => x.Key == lexema).Value).miTipo;
            }
            else
            {
                var error = new Errores() { Valorerror = 601, Linea = 0, Mensaje = "No se Encontro el atributo en el contexto actual", Tipoerror = TipoError.Semantico };
                TablaSimbolos.listaErroresSemantico.Add(error);
                return TipoDato.NADA;
            }
        }

        public static string ObtenerDireccionMemoriaRam(string lexemaAtributo, NodoClase nodoClaseActiva)
        {
            var resultado =
                nodoClaseActiva.TSA.FirstOrDefault(x => x.Key == lexemaAtributo).Value.direccionMemoria;
            return resultado;
        }


        #endregion
        #region METODOS PARA TS de METODOS
        public static void InsertarNodoMetodo(NodoMetodo miNodoMetodo, List<NodoVariable> misParametros, NodoClase nodoClaseActiva)
        {
            if (nodoClaseActiva.Lexema != miNodoMetodo.lexema) // verificar que el nombre de la clase no se uso
            {
                if (!nodoClaseActiva.TSM.ContainsKey(miNodoMetodo.lexema))
                {
                    foreach (var item in misParametros)
                    {
                        miNodoMetodo.TablaSimbolosVariables.Add(item.lexema, item);
                    }
                    nodoClaseActiva.TSM.Add(miNodoMetodo.lexema, miNodoMetodo);

                }
                else
                {
                    /*nodoClaseActiva.Count++;
                    nodoClaseActiva.Lexema += Convert.ToString(nodoClaseActiva.Count);
                    if (!nodoClaseActiva.TSM.ContainsKey(miNodoMetodo.lexema))
                    {
                        foreach (var item in misParametros)
                        {
                            miNodoMetodo.TablaSimbolosVariables.Add(item.lexema, item);
                        }
                        nodoClaseActiva.TSM.Add(miNodoMetodo.lexema, miNodoMetodo);
                    }*/



                    // configurar el diccionario de datos o tablash para que acepte colisiones y haga
                    //listas anidadas
                    // algoritmo que soporte
                    // metodo de sobrecarga 
                    //for anidados

                    //    nodoClaseActiva.TSM.Add(miNodoMetodo.lexema, miNodoMetodo);
                }
            }
            else
            {
                var error = new Errores() { Valorerror = 604, Linea = miNodoMetodo.renglonDeclaracion, Mensaje = "Tu nombre de metodo no puede llamarse como el nombre de la clase", Tipoerror = TipoError.Semantico };
                TablaSimbolos.listaErroresSemantico.Add(error);
            }
        }

        public static NodoMetodo BuscarMetodoPorLexema(string lexema, NodoClase nodoClaseActiva)
        {
            var nodoMetodo = nodoClaseActiva.TSM.FirstOrDefault(x => x.Key == lexema).Value;
            return nodoMetodo;
        }

        public static Regreso ObtenerRegresoMetodo(string lexema, NodoClase nodoClaseActiva)
        {

            var nodo = nodoClaseActiva.TSM.FirstOrDefault(x => x.Key == lexema);

            return nodo.Value.miRegreso;

        }
        public static List<NodoVariable> ObtenerParametrosMetodo(string lexemaMetodo, NodoClase nodoClaseActiva)
        {
            var nodo = nodoClaseActiva.TSM.FirstOrDefault(x => x.Key == lexemaMetodo);
            var nodoVariables = nodo.Value.TablaSimbolosVariables;
            return nodoVariables.Values.ToList();
        }

        static public void ComprobarInvocacion(string lexema, NodoClase nodoClaseActiva, int linea)
        {
            if (!nodoClaseActiva.TSM.ContainsKey(lexema))
            {
                var error = new Errores() { Valorerror = 605, Linea = linea, Mensaje = "Nombre de metodo a invocar inexistente", Tipoerror = TipoError.Semantico };
                TablaSimbolos.listaErroresSemantico.Add(error);
            }
            else
            {
                List<NodoVariable> parametros = ObtenerParametrosMetodo(lexema, nodoClaseActiva);
                
                //traeme todos los parametros de el metodo invocado
                //comparacion con los valores, comparar por cantidad de argumentos y tipos, y ademas 
                //si existen variables en los argumentos comprobar que exista y que tengan el mismo tipo
            }
        }

        #endregion
        #region METODOS PARA TS de VARIABLES y PARAMETROS

        public static void InsertarNodoVariable(NodoVariable miNodoVariable, NodoClase nodoClaseActiva, string nombreMetodoActivo)
        {

            if (!nodoClaseActiva.TSM.ContainsKey(miNodoVariable.lexema) || TablaSimbolos.TablaSimbolosClase.ContainsKey(miNodoVariable.lexema))
            {
                var nodoMetodo = nodoClaseActiva.TSM.SingleOrDefault(x => x.Key == nombreMetodoActivo).Value;
                if (!nodoMetodo.TablaSimbolosVariables.ContainsKey(miNodoVariable.lexema))
                {
                    nodoMetodo.TablaSimbolosVariables
                        .Add(miNodoVariable.lexema, miNodoVariable);

                }
                else
                {
                    var error = new Errores() { Valorerror = 605, Linea = miNodoVariable.reglonDec, Mensaje = "nombre de variable local ya existe en el contexto", Tipoerror = TipoError.Semantico };
                    TablaSimbolos.listaErroresSemantico.Add(error);
                }
            }
            else
            {
                var error = new Errores() { Valorerror = 604, Linea = miNodoVariable.reglonDec, Mensaje = "Tu nombre de metodo no puede llamarse como el nombre de la clase", Tipoerror = TipoError.Semantico };
                TablaSimbolos.listaErroresSemantico.Add(error);
            }
        }

        public static TipoDato ObtenerTipoDato(string lexamaVariable, NodoClase nodoClaseActiva, string nombreMetodoActivo)
        {
            try
            {

                //return nodoClaseActiva.TSM.SingleOrDefault(x => x.Key == nombreMetodoActivo).Value.TablaSimbolosVariables.Values.SingleOrDefault(x => x.lexema == lexamaVariable).miTipoDato;
                return TipoDato.INT;
            }
            catch (Exception)
            {
                return TipoDato.INT;
            }
        }
        public static string ObtenerDireccionMemoriaRam(string lexemaVariable, NodoClase nodoClaseActiva, string nombreMetodoActivo)
        {
            var resultado =
                nodoClaseActiva.TSM.FirstOrDefault(x => x.Key == nombreMetodoActivo).Value.TablaSimbolosVariables.FirstOrDefault(y => y.Key.ToString() == lexemaVariable).Value.direccionMemoria;
            return resultado;

        }

        public static NodoVariable obtenerNodoVariable(string lexemaVariable, NodoClase nodoClaseActiva, string nombreMetodoActivo)
        {
            var resultado =
                nodoClaseActiva.TSM.FirstOrDefault(x => x.Key == nombreMetodoActivo).Value.TablaSimbolosVariables.FirstOrDefault(y => y.Key.ToString() == lexemaVariable).Value;
            return resultado;
        }
        


        #endregion


    }
    
}
