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

            #region Line Plot1:
            //Chart
            LinePlot c1 = new LinePlot();
            c1.PosX = 0;
            c1.PosY = 0;

            c1.Width = 550;
            c1.Height = 500;
            c1.Dock = DockStyle.Left;

            c1.Title = "TimeSeriesPlot\nI-Data (S10-W10)";

            //Axis
            c1.StartX = 0;
            c1.StartY = 0;
            c1.EndX = 360;
            c1.EndY = 50;
            c1.StepsX = 100;
            c1.StepsY = 2;

            c1.UnitX = "ms";
            c1.UnitY = "Kms";
            c1.AxisLabelX = "Time";
            c1.AxisLabelY = "Range";

            c1.NoOfGraphs = 20;
            c1.GridColor = Color.Gray;
            c1.TextColor = Color.White;
            c1.LineColor = Color.Green;
            c1.BgColor = Color.Black;

            c1.Dock = DockStyle.Left;
            c1.InitializeChart();
            #endregion

            #region Line Plot2:
            //Chart
            LinePlot c2 = new LinePlot();
            c2.PosX = 0;
            c2.PosY = 0;

            c2.Width = 550;
            c2.Height = 500;
            c2.Dock = DockStyle.Left;

            c2.Title = "TimeSeriesPlot\nI-Data (S10-W10)";

            //Axis
            c2.StartX = 0;
            c2.StartY = 0;
            c2.EndX = 360;
            c2.EndY = 50;
            c2.StepsX = 50;
            c2.StepsY = 2;

            c2.UnitX = "ms";
            c2.UnitY = "Kms";
            c2.AxisLabelX = "Time2";
            c2.AxisLabelY = "Range2";

            c2.NoOfGraphs = 20;
            c2.GridColor = Color.Gray;
            c2.TextColor = Color.White;
            c2.LineColor = Color.Green;
            c2.BgColor = Color.Black;

            c2.Dock = DockStyle.Left;
            c2.InitializeChart();
            #endregion


            RadarDataReader c3 = new RadarDataReader();
            c3.BgColor = Color.Black;

            //c3.Dock = DockStyle.Fill;

            c3.PosX = 1100;
            c3.PosY = 0;

            c3.Width = 400;
            c3.Height = 800;
            c3.Title = "Radar Data Header";
            c3.TextColor = Color.White;
            c3.Initialize();

            // Sequence is Important while Adding Charts to Form:
            Controls.Add(c3.textBox);
            Controls.Add(c3.CH);
            Controls.Add(c2.CH);
            Controls.Add(c1.CH);
            
        }
    }
}
