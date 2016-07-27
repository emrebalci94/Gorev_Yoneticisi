using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;

namespace Gorev_Yoneticisi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ram()
        {
             ManagementObjectSearcher ramara = new ManagementObjectSearcher("Select * From Win32_ComputerSystem");//Ram Bilgileri
             PerformanceCounter kullanilanram = new PerformanceCounter("Memory", "Available MBytes");//Kullanılabilir Ram miktarı
            foreach (ManagementObject ram in ramara.Get())
             {
                 double Ram_Bytes = (Convert.ToDouble(ram["TotalPhysicalMemory"]));
                  double mbram= (Ram_Bytes/1024)/1024;
                double islem=(mbram-kullanilanram.NextValue())/1024;//Görev Yöneticisindeki Bellek Kısmı...                            
                 label3.Text = "Ram Kullanımı:" + islem .ToString("#.##");
             } 
        }

        private void listele()
        {
            listView1.Items.Clear();
            int i = 0;
           
            foreach (Process p in Process.GetProcesses("."))
            {
                listView1.Items.Add(p.ProcessName);
                listView1.Items[i].SubItems.Add(p.SessionId.ToString());
                listView1.Items[i].SubItems.Add(p.PeakWorkingSet64.ToString());
                if (p.Responding==true)
                {
                    listView1.Items[i].SubItems.Add("Çalışıyor");
                }
                else
                {
                    listView1.Items[i].SubItems.Add("Çalışmıyor");
                }
             
                listView1.Items[i].SubItems.Add(System.Environment.UserName.ToString());
                listView1.Items[i].SubItems.Add(p.MainWindowTitle.ToString());
                i++;
            }
            label1.Text = "Toplam Çalışan Uygulama Sayısı:" + listView1.Items.Count.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            timer1.Start();
        }
        string  programadi;
        private void listView1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                programadi = item.Text;
            }
        }

        private void göreviSonlandırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcesses("."))//Local Makinedeki Çalısan Programlar
            {
                if (p.ProcessName.ToString() == programadi)
                {
                    p.Kill();
                }
            }
            listele();
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listele();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            ram();
            PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpu.NextValue();
            System.Threading.Thread.Sleep(1000);
            label2.Text ="CPU Kullanımı:%"+ Math.Round( cpu.NextValue()).ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

    }
}
