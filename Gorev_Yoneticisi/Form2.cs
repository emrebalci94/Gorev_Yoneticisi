using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Gorev_Yoneticisi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpu.NextValue();
            System.Threading.Thread.Sleep(1000);
            chart1.Series[0].Points.Add(Math.Round(cpu.NextValue()), cpu.NextValue());
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void durdurToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}
