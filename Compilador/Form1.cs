using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var lexico = new Lexico(textBox1.Text);
            int fila = 0;
            int columna = 0;
            char cactual='p';
            string fuente = string.Empty;
            int i = 0;
            int primfila = 0;
            lexico.Execute(fila, columna, cactual, fuente, primfila, i);

            var objsintactico = new Sintactico(lexico.tokens);
            objsintactico.EjecutarSintactico(objsintactico.listaToken);

            List<Errores> erroreslexicos = lexico.errores;
            List<Errores> erroressintacticos = objsintactico.listaError;

            List<Errores> ListaErrores = erroreslexicos.Union(erroressintacticos).ToList();

            var listatokens = new List<Token>(lexico.tokens);
            dataGridView1.DataSource = listatokens;
            dataGridView2.DataSource = ListaErrores;
        }
        private void TablaSimbolo(object sender, EventArgs e)
        {
            //TablaSimbolos.TablaSimbolosClase.Add(NodoVariable);
            //busqueda de metodos sobrecargados
            //var resultado=(from x in((TablaSimbolos.TablaSimbolosClase.FirstOrDefault(x => x.Key ==nodo.lexema).Value).TSM)Where x.Key.constains("metodo") select x).ToList();
        }
        
    }
}