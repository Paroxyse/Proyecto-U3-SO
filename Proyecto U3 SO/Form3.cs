using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Proyecto_U3_SO
{
    public partial class Form3 : Form
    {
        List<Proceso> LP;
        const int CantidadParticiones=5;
        Proceso[] procesos = new Proceso[CantidadParticiones];
        int EspaciosDisp;
        Label[] labels;
        System.Random r;
        public Form3()
        {
            InitializeComponent();
            UtilityInit();
        }
        public void UtilityInit()
        {
            LP = new List<Proceso>();
            r = new Random();
            for (int i = 0; i < procesos.Length; i++)
            {
                procesos[i] = utilidad.Empty;
            }
            labels = new Label[] {
                LBN1, LBT1, LbCR1,MEM1,
                LBN2, LBT2, LbCR2,MEM2,
                LBN3, LBT3, LbCR3,MEM3,
                LBN4, LBT4, LbCR4,MEM4,
                LBN5, LBT5, LbCR5,MEM5

            };
            EspaciosDisp = 5;
        }
        private void UpdateExec() {
            var first = lookforfirst();
          //  MessageBox.Show("bro" + first.Item2 + first.Item1);
            if (first.Item1)
            {              
                movetoexec(first.Item3,first.Item2);            
            }
            for (int i = 0; i < CantidadParticiones; i++)
            {
                if (procesos[i] != utilidad.Empty)
                {
                    procesos[i].Tiempo--;

                }
                freeExec(i);
                updateMemory(i);
                utilidad.ActualizarFilas(dataGridView2, LP, true);
            }

        }
        private void updateMemory(int i)
        {
           
                labels[i * 4].Text = procesos[i].Nombre;
                labels[i * 4+1].Text = procesos[i].Tiempo+"";
                labels[i * 4+2].Text = procesos[i].Memoria+"";
            
        }
        private (bool,int,Proceso) lookforfirst()
        {
            var first = (false, -1, utilidad.Empty);
            foreach(Proceso x in LP)
            {
                if (checkspace(x).Item1) { 
                    first = (checkspace(x).Item1,checkspace(x).Item2,x); return first; 
                }
                
            }
           // MessageBox.Show(first.Item1+"", first.Item2+"");
            return first;
        }
        private (bool, int) checkspace(Proceso p)
            {
            bool b=false;
            int bestindex = -1;
            int sizediff = 0;
            int auxmem = 0;
            if (EspaciosDisp == 0)
            {
               // MessageBox.Show("No hay espacio");
                return (b, bestindex);
            }
            for(int i=0;i<CantidadParticiones;i++)
            {
                auxmem = int.Parse(labels[i * 4 + 3].Text);
                

                if (p.Memoria <= auxmem && procesos[i] == utilidad.Empty) {
                    b = true;

                    if (bestindex == -1 || (auxmem-p.Memoria>=0 && (auxmem - p.Memoria) < sizediff)){
                        bestindex = i;
                        sizediff = auxmem - p.Memoria;
                    }
                  
                }

            }
            return (b, bestindex);
            }
        private void freeExec(int i)
        {
            if(procesos[i]!=utilidad.Empty && procesos[i].Tiempo <= 0)
            {
                procesos[i] = utilidad.Empty;
                EspaciosDisp++;
            }
        }
        private void movetoexec(Proceso p,int receiverindex)
        {
            if (procesos[receiverindex] == utilidad.Empty && EspaciosDisp>0)
            {
                procesos[receiverindex] = p;
                LP.Remove(p);
                EspaciosDisp--;
            }
        }
        private void AddProcess(Proceso p)
        {
            LP.Add(p);
            utilidad.ActualizarFilas(dataGridView2, LP, true);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            utilidad.PauseButton((Button)sender, t);
        }
        private void TickActions()
        {
            if (checkBox1.Checked)
            {
                AddProcess(new Proceso(utilidad.generarNP(), r.Next(20)+1, r.Next(1024) + 1));
            }
            UpdateExec();
            Time.Text = "" + (int.Parse(Time.Text) + 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProcess(new Proceso(TBNombre.Text, (int)SPNTiempo.Value,(int) SPNMemoria.Value));
        }

        private void t_Tick(object sender, EventArgs e)
        {
            TickActions();
        }

        //hay una forma más eficiente de hacer esto? eh, probablemente
        private void XB1_Click(object sender, EventArgs e)
        {
            int i=-1;
            if((Button)sender == XB1)
            {
                i = 0;
            }
            if ((Button)sender == XB2)
            {
                i = 1;
            }
            if ((Button)sender == XB3)
            {
                i = 2;
            }
            if ((Button)sender == XB4)
            {
                i = 3;
            }
            if ((Button)sender == XB5)
            {
                i = 4;
            }
            if (i == -1)
            {
                return;
            }
            if (procesos[i] != utilidad.Empty)
            {
                procesos[i] = utilidad.Empty;
                updateMemory(i);
                EspaciosDisp++;
            }
        }

        private void RB_Click(object sender, EventArgs e)
        {
            t.Stop();
            LP.Clear();
            dataGridView2.Rows.Clear();
            button3.BackColor = System.Drawing.Color.Red;
            Time.Text = 0;
            for(int i = 0; i < CantidadParticiones; i++)
            {
                procesos[i] = utilidad.Empty;
                updateMemory(i);
            }
        }
    }
}
