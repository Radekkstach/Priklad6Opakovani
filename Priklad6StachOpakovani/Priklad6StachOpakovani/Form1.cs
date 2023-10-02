using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Priklad6StachOpakovani
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            OpenFileDialog ofd= new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            FileStream fs = new FileStream("cisla.dat", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    listBox1.Items.Add(line);
                    string[] radek = line.Split(';');
                    int nejdelsi = 0;
                    foreach (string rade in radek)
                    {
                        if (rade.Length > nejdelsi)
                        {
                            nejdelsi = rade.Length;

                        }
                    }
                    bw.Write((double)nejdelsi/10);
                }
                sr.Close();
                bw.Close();
            }

            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Visible= true;
            listBox2.Items.Clear();
            FileStream fs = new FileStream("cisla.dat",FileMode.Open,FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            br.BaseStream.Position = 0;
            while(br.BaseStream.Position < br.BaseStream.Length)
            {
                listBox2.Items.Add(br.ReadDouble());
            }
            br.Close();
            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("cisla.dat", FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            List<double> zapis = new List<double>();
            

            while(br.BaseStream.Position<br.BaseStream.Length)
            {
                double cislo = br.ReadDouble();
                if(cislo < 1)
                {
                    zapis.Add(cislo * 10);
                }
                else zapis.Add(cislo);
                
            }
            fs.Close();
            br.Close();

            fs = new FileStream("cisla.dat", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            foreach(double x in zapis)
            {
                bw.Write(x);
            }
            fs.Close();
            bw.Close();

            button4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox3.Visible= true;
            FileStream fs = new FileStream("cisla.dat", FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            double prumer = 0;
            int pocet = 0;
            br.BaseStream.Position = 0;
            while(br.BaseStream.Position < br.BaseStream.Length)
            {
                double x = br.ReadDouble();
                if(x > 2)
                {
                    prumer += x;
                    pocet++;
                }
            }
            fs.Close();
            br.Close();

            prumer = prumer / pocet;

            FileStream fss = new FileStream("cisla.dat", FileMode.Append);
            BinaryWriter bw = new BinaryWriter(fss);
            bw.Write(prumer);
            fs.Close();
            bw.Close();

            FileStream fsss = new FileStream("cisla.dat", FileMode.Open);
            BinaryReader brr = new BinaryReader(fsss);
            brr.BaseStream.Position = 0;
            while(brr.BaseStream.Position < brr.BaseStream.Length)
            {
                double cisla = brr.ReadDouble();
                listBox3.Items.Add(cisla);
            }
            fsss.Close();
            brr.Close();
        }
    }
}
