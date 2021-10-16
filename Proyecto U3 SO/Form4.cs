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
   
    public partial class Form4 : Form
    {
        System.Random r;
        List<ProcesoPog> LP;
        List<ProcesoPog> ExecList;
        List<TextBox> tbar;
       
        public Form4()
        {
            InitializeComponent();
            UtilityInit();
        }
        private void UtilityInit()
        {
            r = new Random();
            ExecList = new List<ProcesoPog>();
            LP = new List<ProcesoPog>();
            tbar = new List<TextBox>();
            foreach(TextBox x in tableLayoutPanel1.Controls)
            {     
                //Esto es computación innecesaria, pero no voy a establecer readonly 20 veces por diversión
                x.ReadOnly = true;
                tbar.Add(x);
            }
        
        }
        //Debido a mala planeación, he tenido que copiar código que podría haber dejado en una clase para reutilizarlo :)
       private void UpdateExecution()
        {
            int auxindex;
            //update executing stuff

            for (int i = ExecList.Count - 1; i >= 0; i--)
            {
                ExecList[i].Tiempo--;
                tbar[ExecList[i].Inindex].Text = ExecList[i].Nombre + " " + ExecList[i].Tiempo;
                freeExec(i);
            }
            //load more stuff into memory
            for (int i = LP.Count - 1; i >= 0; i--)
            {
                auxindex = blanksearch(LP[i].Memoria);
                if (auxindex >= 0)
                {
                    LP[i].Inindex = auxindex;
                    movetoexec(LP[i]);
                }
            }
          
        }
        public void compactar()
        {
            int currentbot = 0;
            foreach(ProcesoPog x in ExecList)
            {
                x.Inindex = currentbot;
                currentbot = x.Inindex + x.Memoria;
                colorAndText(x);
            }
        }
        private void freeExec(int index)
        {
           
                if (ExecList[index].Tiempo <= 0)
                {
                    tbar[ExecList[index].Inindex].Text="";
                   for(int i = ExecList[index].Inindex; i < (ExecList[index].Inindex + ExecList[index].Memoria); i++)
                    {
                        tbar[i].BackColor = Color.White;
                    }
                ExecList.RemoveAt(index);
            }

              
            
        }
        private void movetoexec(ProcesoPog p) {
            int index = p.Inindex;
        if(index >= 0)
            {
                p.Colorawa = randomColor();
                colorAndText(p);
                ExecList.Add(p);
                LP.Remove(p);
            }
            utilidad.ActualizarFilas(dataGridView2, LP, true);
        }
        private void colorAndText(ProcesoPog p)
        {
            int index = p.Inindex;
            tbar[index].Text = p.Nombre + " " + p.Tiempo;
            for (int i = index; i < (index + p.Memoria); i++)
            {
                tbar[i].BackColor = p.Colorawa;
            }
        }
        //Revisa si hay un espacio en blanco de suficiente tamaño
        private int blanksearch(int reqspace)
        {
            int index = -1;
            int auxindex = 0, cslength=0;
            for(int i = 0; i < tbar.Count; i++)
            {
                if (tbar[i].BackColor == Color.White )
                {
                    if (cslength == 0)
                    {
                        auxindex = i;
                    }
                        cslength++;
                    
                    
                }
                if (cslength >= reqspace)
                {
                    index = auxindex;
                }
                if (tbar[i].BackColor != Color.White)
                {
                    cslength = 0;
                }
            }

            return index;
        }
        private Color randomColor()
        {
            int aux1, aux2, aux3;
            //255 en todos para que nunca quede 255 en los tres (blanco)
            aux1 = r.Next(255);
            Thread.Sleep(5);
            aux2 = r.Next(255);
            Thread.Sleep(5);
            aux3 = r.Next(255);
            Thread.Sleep(5);
            return Color.FromArgb(aux1, aux2, aux3);
        }
        private void AddProcess(ProcesoPog p)
        {
            LP.Add(p);
            utilidad.ActualizarFilas(dataGridView2, LP, true);
        }
        private void tickerActions()
        {
            if (checkBox1.Checked)
            {
                AddProcess(new ProcesoPog(utilidad.generarNP(), r.Next(20) + 1, r.Next(5) + 1));
            }
            UpdateExecution();
            Time.Text = "" + (int.Parse(Time.Text) + 1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddProcess(new ProcesoPog(TBName.Text, (int)SPNTime.Value, (int)SPNMem.Value));
        }

        private void t_Tick(object sender, EventArgs e)
        {
            tickerActions();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            utilidad.PauseButton((Button)sender, t);
        }

        private void RB_Click(object sender, EventArgs e)
        {
            LP.Clear();
            ExecList.Clear();
            Time.Text = 0+"";
            t.Stop();

            dataGridView2.Rows.Clear();
            for (int i = 0; i < tbar.Count; i++)
            {
                tbar[i].BackColor = Color.White;
                tbar[i].Text = "";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < tbar.Count; i++)
            {
                tbar[i].BackColor = Color.White;
                tbar[i].Text = "";
            }
            compactar();
        }
    }
}
