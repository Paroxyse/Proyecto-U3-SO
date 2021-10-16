using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

using System.Windows.Forms;

namespace Proyecto_U3_SO
{
    public partial class Form2 : Form
    {
        const int ProcessTypeCount = 4;
        Queue<Proceso>[] qar = new Queue<Proceso>[ProcessTypeCount];
        Proceso[] PAr = new Proceso[ProcessTypeCount];
        System.Random r;
        Proceso Empty;
        DataGridView[] dgvar;
        TextBox[] tbar;
        public Form2()
        {
            InitializeComponent();
            utilityinit();
        }
        private void utilityinit()
        {
            //Inicializar colas de procesos
            for(int i=0;i<qar.Length;i++)
            {
                qar[i] = new Queue<Proceso>();
            }
            comboBox1.SelectedIndex = 0;
             r = new Random();
            //Declarar arreglo auxiliar de datagridviews
            dgvar = new DataGridView[]{
                dataGridView1,dataGridView2,dataGridView3,dataGridView4};
            //Inicializar Proceso Empty y asignarlo a posiciones de arreglo
            Empty = new Proceso("Vacío", 1);
            for(int i = 0; i < ProcessTypeCount; i++) { PAr[i] = Empty; }
            //Inicializar arreglo de tb
            tbar = new TextBox[] { 
            EXA, EXB, EXC, EXD        
            };
        }
        private void updateExec()
        {
           
            //Decrementar tiempo de activos y mover a ejecución si está vacío
            //El trabajo se hace paso a paso, cambiando entre el trabajo de cada tipo de proceso rápidamente
            for(int i = 0; i < ProcessTypeCount; i++)
            {
                //Revisa si hay un proceso activo, si es así, decrementa el tiempo
                MoveToExec(i);
                if (PAr[i]!=Empty)
                {
                    PAr[i].Tiempo--;
                }
                //Si el tiempo llega a 0, saca el proceso
                if (PAr[i] != Empty && PAr[i].Tiempo <= 0)
                {
                    PAr[i] = Empty;
                }
                //Actualizar texto de ejecución 
                tbUpdate(i);
                    
                
            }
            utilidad.ActualizarFilas(dgvar, qar);

        }
        private void tbUpdate(int i)
        {
            string s;
            s = PAr[i].Nombre + " " + PAr[i].Tiempo;
            if (tbar[i].Text != s)
            {
                tbar[i].Text = s;
            }
        }
        private void MoveToExec(int i) {
            if(PAr[i]==Empty && qar[i].Count > 0)
            {
                PAr[i] = qar[i].Dequeue();
                
            }
            
        }
        private void AddToQ(Proceso p, int i) {
            qar[i].Enqueue(p);


            utilidad.ActualizarFilas(dgvar,qar);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            utilidad.PauseButton((Button) sender, t);
        }

        private void t_Tick(object sender, EventArgs e)
        {
            tickActions();
        }
        private void tickActions()
        {
            //Generación automática de procesos
            if (checkBox1.Checked)
            {
                AddToQ(new Proceso(utilidad.generarNP(), r.Next(20) + 1), r.Next(ProcessTypeCount));

            }
            //Actualización de listas y de temporizador
            updateExec();
            Time.Text = "" + (int.Parse(Time.Text) + 1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddToQ(new Proceso(TBNombre.Text,(int) SPTiempo.Value), comboBox1.SelectedIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            t.Stop();
            button3.BackColor = System.Drawing.Color.Red;
            for(int i = 0; i < ProcessTypeCount; i++)
            {
                dgvar[i].Rows.Clear();
                qar[i].Clear();
                PAr[i] = Empty;
                tbar[i].Text = "";
            }
            Time.Text = "0";
            
        }
        private void ForceEnd(int i)
        {
            if (PAr[i] == Empty)
            {
                return;
            }
            PAr[i]= Empty;
            tbUpdate(i);
            
        }

        private void XB4_Click(object sender, EventArgs e)
        {
            ForceEnd(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ForceEnd(1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ForceEnd(2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ForceEnd(3);
        }
    }
}
