using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplaySoft
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            LinePlot p1 = new LinePlot();
            p1.PosX = 10;
            p1.PosY = 10;
            p1.Height = 100;
            p1.Width = 100;
            p1.Dock = DockStyle.Left;
            p1.BgColor = Color.IndianRed;
            p1.InitializeChart();

            LinePlot p2 = new LinePlot();
            p2.PosX = 100;
            p2.PosY = 100;
            p2.Height = 100;
            p2.Width = 100;
            p2.Dock = DockStyle.Right;
            p2.BgColor = Color.Gainsboro;
            p2.InitializeChart();

            Controls.Add(p1.CH);
            Controls.Add(p2.CH);
        }
    }
}
