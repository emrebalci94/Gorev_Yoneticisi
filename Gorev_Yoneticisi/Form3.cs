using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;

namespace Gorev_Yoneticisi
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void ram()
        {
            ManagementObjectSearcher ramara = new ManagementObjectSearcher("Select * From Win32_ComputerSystem");//Ram Bilgileri        
            PerformanceCounter kullanilanram = new PerformanceCounter("Memory", "Available MBytes");//Kullanılabilir Ram miktarı
            foreach (ManagementObject ram in ramara.Get())
            {
                double Ram_Bytes = (Convert.ToDouble(ram["TotalPhysicalMemory"]));
                double mbram = (Ram_Bytes / 1024) / 1024;
                double islem = (mbram - kullanilanram.NextValue()) / 1024;//Görev Yöneticisindeki Bellek Kısmı...                            
                chart1.Series[0].Points.Add(islem , islem);
                label1.Text = "Toplam Ram:" +Convert.ToInt32( mbram).ToString();
                label2.Text = "Kullanılabilir:  " + kullanilanram.NextValue().ToString();
                label3.Text = ":" + islem.ToString("#.##");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ram();
            
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void görevYöneticisineDönToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void durdurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
