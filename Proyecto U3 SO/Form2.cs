using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Proyecto_U3_SO
{
    public partial class Form2 : Form
    {
        Queue<Proceso>[] qar = new Queue<Proceso>[4];
        public Form2()
        {
            InitializeComponent();
           
        }
        private void utilityinit()
        {
            //Inicializar colas de procesos
            for(int i=0;i<qar.Length;i++)
            {
                qar[i] = new Queue<Proceso>();
            }
        }
    }
}
