using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_U3_SO
{
    public class Proceso
    {
        private string nombre;
        private int tiempo, memoria;

        public Proceso(string nombre, int tiempo)
        {
            this.nombre = nombre;
            this.tiempo = tiempo;
        }

        public Proceso(string nombre, int tiempo, int memoria)
        {
            this.nombre = nombre;
            this.tiempo = tiempo;
            this.memoria = memoria;
        }

        public int Tiempo { get => tiempo; set => tiempo = value; }
        public int Memoria { get => memoria; set => memoria = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
    public static class utilidad
    {
        static System.Random r=new Random(System.DateTime.Now.Millisecond);
        public static string generarNP()
        {
            
            string aux = "";
            int Local_random;
            for (int i = 0; i < 3; i++)
            {
                Local_random = r.Next(15);
                switch (Local_random)
                {
                    case 10:
                        aux += "A";
                        break;
                    case 11:
                        aux += "B";
                        break;
                    case 12:
                        aux += "C";
                        break;
                    case 13:
                        aux += "D";
                        break;
                    case 14:
                        aux += "E";
                        break;
                    case 15:
                        aux += "F";
                        break;
                    default:
                        aux += "" + Local_random;
                        break;
                }
            }
            return aux;
        }
        public static void ActualizarFilas(DataGridView[] aux,Queue<Proceso>[] arp)
        {

            for (int i = 0; i < aux.Length; i++)
            {
                aux[i].Rows.Clear();
                foreach (Proceso x in arp[i])
                {
                    aux[i].Rows.Add(x.Nombre, x.Tiempo);
                }
            }
        } 
       public static void PauseButton(Button p, Timer t)
        {
            if (p.BackColor == System.Drawing.Color.Red)
            {
                p.BackColor = System.Drawing.Color.LimeGreen;
                t.Start();
                return;
            }
            p.BackColor = System.Drawing.Color.Red;
            t.Stop();
        }
        
    }
}
           
          
            
        
    
 

