using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace DisplaySoft
{
    internal class LinePlot
    {
        #region Private Fields:
        private float startX, startY, endX, endY, stepsX, stepsY;
        private string unitX, unitY, axisLabelX, axisLabelY, title;
        private int noOfGraphs,posX, posY, height, width;
        private Color textColor, gridColor, lineColor, bgColor;
        private DockStyle dock;

        #endregion


        #region Properties:
        public float StartX
        {
            get { return startX; }
            set { startX = value; }
        }
        public float StartY
        { get { return startY; } set { startY = value; } }
        //startY = float.Parse(Convert.ToString(value));
        public float StepsX
        { get { return stepsX; } set { stepsX = value; } }
        public float StepsY
        { get { return stepsY; } set { stepsY = value; } }
        public float EndX
        { get { return endX; } set { endX = value; } }
        public float EndY
        { get { return endY; } set { endY = value; } }
        public int NoOfGraphs
        { get { return noOfGraphs; } set { noOfGraphs = value; } }
        public int Height
        { get { return height; } set { height = value; } }
        public int Width
        { get { return width; } set { width = value; } }
        public int PosX
        { get { return posX; } set { posX = value; } }
        public int PosY
        { get { return posY; } set { posY = value; } }
        public string UnitX
        { get { return unitX; } set { unitX = value; } }
        public string UnitY
        { get { return unitY; } set { unitY = value; } }
        public string AxisLabelX
        { get { return axisLabelX; } set { axisLabelX = value + " ( " + UnitX + " ) "; } }
        public string AxisLabelY
        { get { return axisLabelY; } set { axisLabelY = value + " ( " + UnitY + " ) "; } }
        public string Title
        { get { return title; } set { title = value; } }
        public Color TextColor
        { get { return textColor; } set { textColor = value; } }
        public Color GridColor
        { get { return gridColor; } set { gridColor = value; } }
        public Color LineColor
        { get { return lineColor; } set { lineColor = value; } }
        public Color BgColor
        { get { return bgColor; } set { bgColor = value; } }
        public DockStyle Dock
        { get { return dock; } set { dock = value; } }

        #endregion

        public Chart CH;

        public LinePlot()
        {
            CH = new Chart();
            // Declaring Chart Area.
            ChartArea ca = new ChartArea();

            // Adding Chart Area to Chart.
            CH.ChartAreas.Add(ca);

            CH.Anchor = ((AnchorStyles)
                ((AnchorStyles.Top | AnchorStyles.Bottom)));

            CH.BackColor = Color.Transparent;

        }

        public void InitializeChart()
        {
            // Setting Position Of Chart.

            CH.Location = new Point(PosX, PosY);
            CH.Size = new Size(Width, Height);
            CH.BackColor = BgColor;


            // Chart.Titles.Add(title);
            Title title = CH.Titles.Add(Title);
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.ForeColor = TextColor;

            // Border of ChartArea
            CH.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            CH.ChartAreas[0].BorderWidth = 2;
            CH.ChartAreas[0].BorderColor = Color.White;
            CH.ChartAreas[0].BackColor = BgColor;

            #region Axises:

            //Ranges
            CH.ChartAreas[0].AxisX.Minimum = StartX;
            CH.ChartAreas[0].AxisX.Interval = StepsX;

            CH.ChartAreas[0].AxisY.Minimum = StartY;
            CH.ChartAreas[0].AxisY.Interval = StepsY;

            //Title
            CH.ChartAreas[0].AxisX.Title = AxisLabelX;
            CH.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            CH.ChartAreas[0].AxisX.TitleForeColor = TextColor;

            CH.ChartAreas[0].AxisY.Title = AxisLabelY;
            CH.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            CH.ChartAreas[0].AxisY.TitleForeColor = TextColor;
                
            //Lables
            CH.ChartAreas[0].AxisX.LabelStyle.Format = "0.00";
            CH.ChartAreas[0].AxisY.LabelStyle.Format = "0.00";

            CH.ChartAreas[0].AxisX.LabelStyle.ForeColor = TextColor;
            CH.ChartAreas[0].AxisY.LabelStyle.ForeColor = TextColor;

            //Base Line
            CH.ChartAreas[0].AxisX.LineWidth = 2;
            CH.ChartAreas[0].AxisX.LineColor = Color.White;


            #endregion

            #region MajorGrid:

            CH.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            CH.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            CH.ChartAreas[0].AxisY.MajorGrid.Interval = StepsY;
            CH.ChartAreas[0].AxisY.MajorGrid.LineColor = GridColor;
            #endregion

            #region Major Tick:
            //MajorTickMark
            CH.ChartAreas[0].AxisY.MajorTickMark.Interval = StepsY;
            CH.ChartAreas[0].AxisY.MajorTickMark.Size = 2;
            CH.ChartAreas[0].AxisY.MajorTickMark.LineColor = Color.White;
            CH.ChartAreas[0].AxisY.MajorTickMark.IntervalOffset = StepsY / 2;
            CH.ChartAreas[0].AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
            #endregion

            #region Zooming:
            CH.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            CH.ChartAreas[0].CursorX.AutoScroll = true;
            CH.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

            CH.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            CH.ChartAreas[0].CursorY.AutoScroll = true;
            CH.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            #endregion


        }


    }
}
