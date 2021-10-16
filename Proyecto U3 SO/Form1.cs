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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonclick(object sender, EventArgs e)
        {
            if ((Button)sender == button1)
            {
                Form2 f2 = new Form2();
                f2.ShowDialog();
                f2.Dispose();
                return;
            }
            if ((Button)sender == button2)
            {
                Form3 f3 = new Form3();
                f3.ShowDialog();
                f3.Dispose();
                return;
            }
            if ((Button)sender == button3)
            {
                Form4 f4 = new Form4();
                f4.ShowDialog();
                f4.Dispose();
                return;
            }
        }
        }
    }

