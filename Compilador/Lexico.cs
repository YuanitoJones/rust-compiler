using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    public class Lexico
    {
        PalabrasReservadas.Traductor palabrasReservadas;
        TipoErrorLexico.Traductor errortypel;

        private int[,] matrizTransicion =
        {
            //   0         1       2      3      4      5      6      7      8      9      10     11     12     13     14     15     16     17     18     19     20     21     22     23     24     25     26     27     28     29     30      31         32       33        34     35
            // digito ||palabra||  "  ||  '  ||  +  ||  -  ||  *  ||  /  ||  %  ||  &  ||  |  ||  ^  ||  !  ||  =  ||  <  ||  >  ||  {  ||  }  ||  [  ||  ]  ||  (  ||  )  ||  ;  ||  .  ||  ,  ||  ?  ||  @  ||  _  ||  :  ||  #  ||  $  ||  enter  ||  tab  || espacio || Desc || eof
         /*0*/{   2    ,   1    ,  5   ,  6   ,  8   ,  9   ,  10  ,  11  ,  2   ,  13  ,  14  ,  15  ,  16  ,  17  ,  18  ,  19  , -43  , -44  , -45  , -46  , -47  , -48  , -50  ,  22  , -52  , -53  , -54  , -55  ,  28  , -62  , -63  ,    0     ,   0    ,    0     , -506  ,  0  },
         /*1*/{   1    ,   1    , -500 , -500 , -1   ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  , -500 , -500 ,   1  ,  -1  , -500 , -500 ,   -1     ,  -1    ,   -1     , -506  ,  -1 },
         /*2*/{   2    , -501   , -501 , -501 ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  -2  ,  3   ,  -2  , -501 , -501 , -501 ,  -2  , -501 , -501 ,   -2     ,  -2    ,   -2     , -506  ,  -2 },
         /*3*/{   4    , -502   , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 ,   -502   ,  -502  ,  -502    , -506  , -502},
         /*4*/{   4    , -502   , -502 , -502 ,  -3  ,  -3  ,  -3  ,  -3  ,  -3  , -502 , -502 , -502 , -502 ,  -3  ,  -3  ,  -3  ,  -3  ,  -3  ,  -3  ,  -3  ,  -3  ,  -3  ,  -3  , -502 , -502 , -502 , -502 , -502 , -502 , -502 , -502 ,   -3     ,  -3    ,   -3     , -506  ,  -3 },
         /*5*/{   5    ,   5    ,  -4  ,  5   ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   5  ,   -503   ,   5    ,    5     ,   5   , -503},
         /*6*/{   7    ,   7    ,   7  , -504 ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   7  ,   -505   ,   7    ,    7     , -506  , -505},
         /*7*/{ -505   , -505   , -505 ,  -5  , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 , -505 ,   -505   ,  -505  ,  -505    , -506  , -505},
         /*8*/{  -13   ,  -13   ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -28 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,  -13 ,   -13    ,  -13   ,   -13    , -506  , -13 },
         /*9*/{  -14   ,  -14   ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -29 ,  -14 ,  -49 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,  -14 ,   -14    ,  -14   ,   -14    , -506  , -14 },
        /*10*/{  -15   ,  -15   ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -30 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,  -15 ,   -15    ,  -15   ,   -15    , -506  , -15 },
        /*11*/{  -16   ,  -16   ,  -16 ,  -16 ,  -16 ,  -16 ,  25  ,  24  ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -31 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,  -16 ,   -16    ,  -16   ,   -16    , -506  , -16 },
        /*12*/{  -17   ,  -17   ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -32 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,  -17 ,   -17    ,  -17   ,   -17    , -506  , -17 },
        /*13*/{  -18   ,  -18   ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -42 ,  -18 ,  -18 ,  -18 ,  -33 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,  -18 ,   -18    ,  -18   ,   -18    , -506  , -18 },
        /*14*/{  -19   ,  -19   ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -41 ,  -19 ,  -19 ,  -34 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,  -19 ,   -19    ,  -19   ,   -19    , -506  , -19 },
        /*15*/{  -20   ,  -20   ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -35 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,  -20 ,   -20    ,  -20   ,   -20    , -506  , -20 },
        /*16*/{  -21   ,  -21   ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -23 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,  -21 ,   -21    ,  -21   ,   -21    , -506  , -21 },
        /*17*/{  -40   ,  -40   ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -22 ,  -40 ,  -61 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,  -40 ,   -40    ,  -40   ,   -40    , -506  , -40 },
        /*18*/{  -24   ,  -24   ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -26 ,  20  ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,  -24 ,   -24    ,  -24   ,   -24    , -506  , -24 },
        /*19*/{  -25   ,  -25   ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -27 ,  -25 ,  21  ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,  -25 ,   -25    ,  -25   ,   -25    , -506  , -25 },
        /*20*/{  -37   ,  -37   ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -38 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,  -37 ,   -37    ,  -37   ,   -37    , -506  , -37 },
        /*21*/{  -36   ,  -36   ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -39 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,  -36 ,   -36    ,  -36   ,   -36    , -506  , -36 },
        /*22*/{  -51   ,  -51   ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  23  ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,  -51 ,   -51    ,  -51   ,   -51    , -506  , -51 },
        /*23*/{  -56   ,  -56   ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -58 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -57 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,  -56 ,   -56    ,  -56   ,   -56    , -506  , -56 },
        /*24*/{   24   ,   24   ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,  24  ,    0     ,   24   ,    24    ,  24   ,  0  },
        /*25*/{   25   ,   25   ,  25  ,  25  ,  25  ,  25  ,  26  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,   25     ,   25   ,    25    ,  25   , -507},
        /*26*/{   25   ,   25   ,  25  ,  25  ,  25  ,  25  ,  25  ,   0  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,  25  ,   25     ,   25   ,    25    ,  25   , -507},
        /*27*/{    1   ,    1   , -500 , -500 , -500 , -500 , -500 , -500 , -500 , -500 , -500 , -500 , -500 , -500 , -500 , -500 ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  ,  -1  , -500 , -500 , -500 , -500 ,   1  , -500 , -500 , -500 , -507     ,  -1    ,   -1     , -506  , -506},
        /*28*/{  -59   ,  -59   , -59  , -59  ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -59 ,  -60 ,  -59 ,  -59 ,   -59    ,  -59   ,   -59    ,  -59  , -59 }
        };

        public List<Token> tokens;
        public List<Errores> errores;
        private string fuente;
        private int j=0;
        private int lineaActiva = 1;

        public Lexico(string fuente)
        {
            this.fuente = fuente + " ";
            tokens = new List<Token>();
            errores = new List<Errores>();
        }
        private int columna(char i)//Retorna columna de la matriz
        {
            if (char.IsLetter(i))
                return 1;
            if (char.IsDigit(i))
                return 0;
            switch (i)
            {
                case '"':
                    return 2;
                case '\'':
                    return 3;
                case '+':
                    return 4;
                case '-':
                    return 5;
                case '*':
                    return 6;
                case '/':
                    return 7;
                case '%':
                    return 8;
                case '&':
                    return 9;
                case '|':
                    return 10;
                case '^':
                    return 11;
                case '!':
                    return 12;
                case '=':
                    return 13;
                case '<':
                    return 14;
                case '>':
                    return 15;
                case '{':
                    return 16;
                case '}':
                    return 17;
                case '[':
                    return 18;
                case ']':
                    return 19;
                case '(':
                    return 20;
                case ')':
                    return 21;
                case ';':
                    return 22;
                case '.':
                    return 23;
                case ',':
                    return 24;
                case '?':
                    return 25;
                case '@':
                    return 26;
                case '_':
                    return 27;
                case ':':
                    return 28;
                case '#':
                    return 29;
                case '$':
                    return 30;
                case '\r':
                    return 31;
                case '\n':
                    return 31;
                case '\t':
                    return 32;
                case ' ':
                    return 33;
                default:
                    return 34;
            }
        }
        private Tipotokens tokentype(int estado)//Tipos de tokens
        {
            switch (estado)
            {
                case -1:
                    return Tipotokens.id;
                case -2:
                    return Tipotokens.numentero;
                case -3:
                    return Tipotokens.numdecimal;
                case -4:
                    return Tipotokens.cadena;
                case -5:
                    return Tipotokens.caracter;
                case -6:
                    return Tipotokens.boolean;
                case -13:
                    return Tipotokens.oparitmetico;
                case -14:
                    return Tipotokens.oparitmetico;
                case -15:
                    return Tipotokens.oparitmetico;
                case -16:
                    return Tipotokens.oparitmetico;
                case -17:
                    return Tipotokens.oparitmetico;
                case -18:
                    return Tipotokens.oplogico;
                case -19:
                    return Tipotokens.oplogico;
                case -20:
                    return Tipotokens.oplogico;
                case -21:
                    return Tipotokens.oplogico;
                case -22:
                    return Tipotokens.opcomparacion;
                case -23:
                    return Tipotokens.opcomparacion;
                case -24:
                    return Tipotokens.opcomparacion;
                case -25:
                    return Tipotokens.opcomparacion;
                case -26:
                    return Tipotokens.opcomparacion;
                case -27:
                    return Tipotokens.opcomparacion;
                case -28:
                    return Tipotokens.expasignacioncomp;
                case -29:
                    return Tipotokens.expasignacioncomp;
                case -30:
                    return Tipotokens.expasignacioncomp;
                case -31:
                    return Tipotokens.expasignacioncomp;
                case -32:
                    return Tipotokens.expasignacioncomp;
                case -33:
                    return Tipotokens.expasignacioncomp;
                case -34:
                    return Tipotokens.expasignacioncomp;
                case -35:
                    return Tipotokens.expasignacioncomp;
                case -36:
                    return Tipotokens.expasignacioncomp;
                case -37:
                    return Tipotokens.expasignacioncomp;
                case -38:
                    return Tipotokens.expasignacioncomp;
                case -39:
                    return Tipotokens.expasignacioncomp;
                case -40:
                    return Tipotokens.expasignacion;
                case -41:
                    return Tipotokens.booleanoflojo;
                case -42:
                    return Tipotokens.booleanoflojo;
                case -43:
                    return Tipotokens.delimitadores;
                case -44:
                    return Tipotokens.delimitadores;
                case -45:
                    return Tipotokens.delimitadores;
                case -46:
                    return Tipotokens.delimitadores;
                case -47:
                    return Tipotokens.delimitadores;
                case -48:
                    return Tipotokens.delimitadores;
                case -49:
                    return Tipotokens.otro;
                case -50:
                    return Tipotokens.otro;
                case -51:
                    return Tipotokens.otro;
                case -52:
                    return Tipotokens.otro;
                case -53:
                    return Tipotokens.otro;
                case -54:
                    return Tipotokens.otro;
                case -55:
                    return Tipotokens.otro;
                case -56:
                    return Tipotokens.otro;
                case -57:
                    return Tipotokens.otro;
                case -58:
                    return Tipotokens.otro;
                case -59:
                    return Tipotokens.otro;
                case -60:
                    return Tipotokens.otro;
                case -61:
                    return Tipotokens.otro;
                case -62:
                    return Tipotokens.otro;
                case -63:
                    return Tipotokens.palabraReservada;
                case -64:
                    return Tipotokens.palabraReservada;
                case -65:
                    return Tipotokens.palabraReservada;
                case -66:
                    return Tipotokens.palabraReservada;
                case -67:
                    return Tipotokens.palabraReservada;
                case -68:
                    return Tipotokens.palabraReservada;
                case -69:
                    return Tipotokens.palabraReservada;
                case -70:
                    return Tipotokens.palabraReservada;
                case -71:
                    return Tipotokens.palabraReservada;
                case -72:
                    return Tipotokens.palabraReservada;
                case -73:
                    return Tipotokens.palabraReservada;
                case -74:
                    return Tipotokens.palabraReservada;
                case -75:
                    return Tipotokens.palabraReservada;
                case -76:
                    return Tipotokens.palabraReservada;
                case -77:
                    return Tipotokens.palabraReservada;
                case -78:
                    return Tipotokens.palabraReservada;
                case -79:
                    return Tipotokens.palabraReservada;
                case -80:
                    return Tipotokens.palabraReservada;
                case -81:
                    return Tipotokens.palabraReservada;
                case -82:
                    return Tipotokens.palabraReservada;
                case -83:
                    return Tipotokens.palabraReservada;
                case -84:
                    return Tipotokens.palabraReservada;
                case -85:
                    return Tipotokens.palabraReservada;
                case -86:
                    return Tipotokens.palabraReservada;
                case -87:
                    return Tipotokens.palabraReservada;
                case -88:
                    return Tipotokens.palabraReservada;
                case -89:
                    return Tipotokens.palabraReservada;
                case -90:
                    return Tipotokens.palabraReservada;
                case -91:
                    return Tipotokens.palabraReservada;
                case -92:
                    return Tipotokens.palabraReservada;
                case -93:
                    return Tipotokens.palabraReservada;
                case -94:
                    return Tipotokens.palabraReservada;
                case -95:
                    return Tipotokens.palabraReservada;
                case -96:
                    return Tipotokens.palabraReservada;
                case -97:
                    return Tipotokens.palabraReservada;
                case -98:
                    return Tipotokens.palabraReservada;
                case -99:
                    return Tipotokens.palabraReservada;
                case -100:
                    return Tipotokens.palabraReservada;
                case -101:
                    return Tipotokens.palabraReservada;
                case -102:
                    return Tipotokens.palabraReservada;
                case -103:
                    return Tipotokens.palabraReservada;
                case -104:
                    return Tipotokens.palabraReservada;
                case -105:
                    return Tipotokens.palabraReservada;
                case -106:
                    return Tipotokens.palabraReservada;
                case -107:
                    return Tipotokens.palabraReservada;
                case -108:
                    return Tipotokens.palabraReservada;
                case -109:
                    return Tipotokens.palabraReservada;
                case -110:
                    return Tipotokens.palabraReservada;
                case -111:
                    return Tipotokens.palabraReservada;
                case -112:
                    return Tipotokens.palabraReservada;
                case -113:
                    return Tipotokens.palabraReservada;
                case -114:
                    return Tipotokens.palabraReservada;
                case -115:
                    return Tipotokens.palabraReservada;
                case -116:
                    return Tipotokens.palabraReservada;
                case -117:
                    return Tipotokens.palabraReservada;
                default:
                    return Tipotokens.desc;
            }
        }
        private bool matchinitial(char i)//Match que verifica que se trate de un operador
        {
            switch (i)
            {
                case '+':
                    return true;
                case '-':
                    return true;
                case '*':
                    return true;
                case '/':
                    return true;
                case '%':
                    return true;
                case '&':
                    return true;
                case '.':
                    return true;
                case '=':
                    return true;
                case '<':
                    return true;
                case '>':
                    return true;
                case '^':
                    return true;
                case '|':
                    return true;
                case '!':
                    return true;
                case ':':
                    return true;
                default:
                    return false;
            }
        }
        private bool matchfinal(char i)//Match que verifica que se trate de un delimitador
        {
            switch (i)
            {
                case '{':
                    return true;
                case '}':
                    return true;
                case '[':
                    return true;
                case ']':
                    return true;
                case '(':
                    return true;
                case ')':
                    return true;
                case ';':
                    return true;
                case ',':
                    return true;
                case '\n':
                    return true;
                case '\r':
                    return true;
                case '\t':
                    return true;
                default:
                    return false;
            }
        }

        private bool matchotro(string fuente)//Match que verifica que sea una palabra reservada
        {
            palabrasReservadas = new PalabrasReservadas.Traductor();
            int estado=palabrasReservadas.convertir(fuente);
            palabrasReservadas = null;
            if(estado<=-64 && estado >= -118)
            {
                if (estado == -72 || estado == -93)
                    return false;
                return true;
            }
            return false;
        }
        private bool borrado(int i, string fuente)//Realiza validaciones para borrar ultimo caracter de string
        {
            bool flag = false;
            if (this.fuente.Substring(i, 1) == " " || matchfinal(Convert.ToChar(this.fuente.Substring(i, 1))))
            {
                flag = true;
            }
            else if (matchinitial(Convert.ToChar(this.fuente.Substring(i, 1))))
            {
                if (matchinitial(Convert.ToChar(this.fuente.Substring((i + 1), 1))))
                {
                    flag = true;
                }
                else
                {
                    if (!matchotro(fuente))
                    {
                        if (!matchinitial(Convert.ToChar(this.fuente.Substring((i - 1), 1))))
                        {
                            flag = true;
                        }
                    }
                }
            }
            else if (!matchinitial(Convert.ToChar(this.fuente.Substring(i, 1))))
            {
                if(this.fuente.Substring(i,1)=="\"" || this.fuente.Substring(i, 1) == "\'")
                {

                }
                else if (matchinitial(Convert.ToChar(this.fuente.Substring((i - 1), 1))))
                {
                    flag = true;
                }
            }
            return flag;
        }

        public List<Token> Execute(int fila, int columna, char cactual, string fuente, int primfila, int i)
        {
            if (i < this.fuente.ToCharArray().Length)
            {
                primfila = fila;
                cactual = Convert.ToChar(this.fuente.Substring(i, 1));
                if (cactual.Equals('\n'))
                {
                    lineaActiva++;
                    if (primfila!=25)
                        j +=2;

                }
                fuente += cactual;
                columna = this.columna(cactual);
                fila = matrizTransicion[fila, columna];
                if (cactual == ' ')//Se verifica que el espacio detectado es un eof
                {
                    if (j != 0)
                    {
                        int total = this.fuente.Substring(0, (j + 1)).Length;
                        if ((this.fuente.Length - total) == fuente.Length)
                        {
                            primfila = matrizTransicion[primfila, 35];
                            fila = primfila;
                        }
                    }
                    else
                    {
                        if (this.fuente.Length == fuente.Length)
                        {
                            primfila = matrizTransicion[primfila, 35];
                            fila = primfila;
                        }
                    }
                    
                    if (fila != 5 && fila!=25 && fila != 24)
                        j++; 
                }
                if (fila <= -1 && fila >= -117)
                {
                    if (fuente.Length > 1)
                    {
                        if(fila!=-38 && fila!=-39 && fila!=-57 && fila != -58)
                        {
                            if (borrado(i, fuente))
                            {
                                fuente = fuente.Remove(fuente.Length - 1);
                                i--;
                            }
                        }   
                    }
                    Token newToken = new Token()
                    {
                        Valor = fila,
                        Caracteres = fuente,
                        Linea = lineaActiva
                    };
                    if (fila == -1)
                    {
                        palabrasReservadas = new PalabrasReservadas.Traductor();
                        newToken.Valor = palabrasReservadas.convertir(newToken.Caracteres);
                        palabrasReservadas = null;
                    }
                    newToken.Tipotoken = tokentype(newToken.Valor);
                    tokens.Add(newToken);

                    fila = 0;
                    columna = 0;
                    fuente = string.Empty;
                    j = i;
                }
                else if (fila <= -500)
                {
                    errortypel = new TipoErrorLexico.Traductor();
                    Errores newerror = new Errores()
                    {
                        Tipoerror = TipoError.Lexico,
                        Linea = lineaActiva,
                        Valorerror = fila
                    };
                    newerror.Mensaje = errortypel.convertir(fila);
                    errores.Add(newerror);
                    errortypel = null;
                    fila = 0;
                    columna = 0;
                    fuente = string.Empty;
                }
                else if (fila == 0)
                {
                    columna = 0;
                    fuente = string.Empty;
                }
                Execute(fila, columna, cactual, fuente, primfila, (i + 1));
            }
            return tokens;
        }
    }
}