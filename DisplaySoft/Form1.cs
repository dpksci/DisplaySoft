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
        private TDP tdp;
        DataReader DR;

        int clock = 500;
        //public float[,] IData_TD, QData_TD;
        //private float[,,] TD_Data;// = new float[512, 1024];
        Timer Timer_Form;

        public Form1()
        {
            
            InitializeComponent();

            tdp = new TDP();
            DR = new DataReader();

            Timer_Form = new Timer();
            Timer_Form.Interval = (clock);
            Timer_Form.Tick += new EventHandler(Timer_Tick);
            Timer_Form.Start();


            #region Line Plot1:
            //Chart
            LinePlot c1 = tdp.LinePlot1_TD;
            c1.PosX = 0;
            c1.PosY = 0;

            c1.Width = 500;
            c1.Height = 800;
            //c1.Dock = DockStyle.Left;

            c1.Title = "TimeSeriesPlot\nI-Data (S10-W10)";

            //Axis

            c1.UnitX = "ms";
            c1.UnitY = "Kms";
            c1.AxisLabelX = "Time";
            c1.AxisLabelY = "Range";

            c1.GridColor = Color.Gray;
            c1.TextColor = Color.White;
            c1.LineColor = Color.Green;
            c1.BgColor = Color.Black;

            //c1.Dock = DockStyle.Left;
            #endregion
            c1.SetPlotParameters();

            #region Line Plot2:
            //Chart
            //LinePlot c2 = new LinePlot();
            LinePlot c2 =  tdp.LinePlot2_TD;
            c2.PosX = 560;
            c2.PosY = 0;

            c2.Width = 500;
            c2.Height = 800;
            //c2.Dock = DockStyle.Left;

            c2.Title = "TimeSeriesPlot\nQ-Data (S10-W10)";


            c2.AxisLabelX = "Time2";
            c2.UnitX = "ms";

            //c2.AxisLabelY = "Range";
            //c2.UnitY = "Kms";

            //c2.NoOfGraphs = 20;
            c2.GridColor = Color.Gray;
            c2.TextColor = Color.White;
            c2.LineColor = Color.Green;
            c2.BgColor = Color.Black;

            //c2.Dock = DockStyle.Left;
            #endregion
            c2.SetPlotParameters();

            Controls.Add(c1.GetChart);
            Controls.Add(c2.GetChart);
            
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (DR.IsUpdated)
            {
                //TD_Data = DR.Data_RB_TD;

                tdp.UpdateChart(DR.fiData_RB_TD, DR.NRGB, DR.NFFT);

                //tdp.InitializeChart();
                Controls.Add(tdp.LinePlot1_TD.GetChart);
                Controls.Add(tdp.LinePlot2_TD.GetChart);

                DR.IsUpdated = false;

            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            DataHeader r = new DataHeader();
            
            r.Position(1110, 110);
            r.Width = 350;
            r.Height = 670;

            r.SetGraphic = this.CreateGraphics();

            r.Title = "Radar Data\n    Reader";
            r.TextColor = Color.White;
            r.SetTitleFont("Arial", 17, FontStyle.Bold);

            r.SetTextFont("Arial", 12, FontStyle.Regular);
            r.SetPen(Color.White, 3);
            r.XmlPath = "D:\\RKS\\testXML.xml";

            r.RenderText(e);
            r.Initialise();

            
        }

        

    }
}
