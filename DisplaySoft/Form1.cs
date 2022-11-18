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
            LinePlot p1 = new LinePlot();
            p1.PosX = 10;
            p1.PosY = 10;

            p1.Height = 500;
            p1.Width = 300;
            p1.Title = "TimeSeriesPlot\nI-Data (S10-W10)";

            //Axis
            p1.StartX = 0;
            p1.StartY = 0;
            p1.EndX = 360;
            p1.EndY = 50;
            p1.StepsX = 10;
            p1.StepsY = 2;

            p1.UnitX = "ms";
            p1.UnitY = "Kms";
            p1.AxisLabelX = "Time";
            p1.AxisLabelY = "Range";

            p1.NoOfGraphs = 20;
            p1.GridColor = Color.Gray;
            p1.TextColor = Color.White;
            p1.LineColor = Color.ForestGreen;
            p1.BgColor = Color.IndianRed;

            //p1.Dock = DockStyle.Left;
            p1.InitializeChart();
            #endregion

            #region Line Plot2:
            //Chart
            LinePlot p2 = new LinePlot();
            p2.PosX = 310;
            p2.PosY = 10;

            p2.Height = 500;
            p2.Width = 300;
            p2.Title = "TimeSeriesPlot\nI-Data (S10-W10)";

            //Axis
            p2.StartX = 0;
            p2.StartY = 0;
            p2.EndX = 360;
            p2.EndY = 50;
            p2.StepsX = 10;
            p2.StepsY = 2;

            p2.UnitX = "ms";
            p2.UnitY = "Kms";
            p2.AxisLabelX = "Time";
            p2.AxisLabelY = "Range";

            p2.NoOfGraphs = 20;
            p2.GridColor = Color.Gray;
            p2.TextColor = Color.White;
            p2.LineColor = Color.ForestGreen;

            //p2.Dock = DockStyle.Right;
            p2.BgColor = Color.IndianRed;
            p2.InitializeChart(); 

            #endregion

            Controls.Add(p1.CH);
            Controls.Add(p2.CH);
        }
    }
}
