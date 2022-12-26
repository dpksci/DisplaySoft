using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace DisplaySoft
{
    internal class LinePlot
    {
        #region Private Fields:
        private float startX, startY, endX, endY, stepsX, stepsY, myFontSize;
        private string unitX, unitY, axisLabelX, axisLabelY, title, subTitle, myFontFamily;
        private int noOfGraphs, noOfPoints, posX, posY, height, width;
        private Color textColor, gridColor, lineColor, bgColor;
        
        private DockStyle dock;
        private AnchorStyles anchor;
        private Font myFont;
        private FontStyle myFontStyle;
        //private float[,] graphCoords;
        private Chart CH;
        #endregion

        #region Properties:
        public float StartX { get { return startX; } set { startX = value; }}
        public float StartY  { get { return startY; } set { startY = value; } } //startY = float.Parse(Convert.ToString(value));
        public float StepsX { get { return stepsX; } set { stepsX = value; } }
        public float StepsY { get { return stepsY; } set { stepsY = value; } }
        public float EndX { get { return endX; } set { endX = value; } }
        public float EndY { get { return endY; } set { endY = value; } }
        public float MyFontSize { get { return myFontSize; } set{myFontSize = value;}}
        public float[,] RB_Data { get; set; }
        
        public int NoOfGraphs { get { return noOfGraphs; } set { noOfGraphs = value; } }
        public int NoOfPoints { get { return noOfPoints; } set { noOfPoints = value; } }
        public int Height { get { return height; } set { height = value; } }
        public int Width { get { return width; } set { width = value; } }
        public int PosX { get { return posX; } set { posX = value; } }
        public int PosY { get { return posY; } set { posY = value; } }
        public string UnitX { get { return unitX; } set { unitX = value; } }
        public string UnitY { get { return unitY; } set { unitY = value; } }
        public string AxisLabelX{ get { return axisLabelX; } set { axisLabelX = value  + UnitX ; } }
        public string AxisLabelY { get { return axisLabelY; } set { axisLabelY = value + UnitY ; } }
        public string Title   { get { return title; } set { title = value; } }
        public string SubTitle { get { return subTitle; } set { subTitle = value; } }
        public string MyFontFamily { get { return myFontFamily; } set { myFontFamily = value; } }
        
        public Color TextColor   { get { return textColor; } set { textColor = value; } }
        public Color GridColor   { get { return gridColor; } set { gridColor = value; } }
        public Color LineColor  { get { return lineColor; } set { lineColor = value; } }
        public Color BgColor { get { return bgColor; } set { bgColor = value; } }
        
        public Font MyFont { get { return myFont; } set { myFont = value; } }
        public DockStyle Dock        { get { return dock; } set { dock = value; } }
        public AnchorStyles Anchor        { get { return anchor; } set { anchor = value; } }
        public FontStyle MyFontStyle { get { return myFontStyle; } set { myFontStyle = value; } }

        public Chart GetChart { get { return CH; } }
        #endregion

        public LinePlot()
        {
            CH = new Chart();

            ChartArea CA = new ChartArea();
            CH.ChartAreas.Add(CA);

  
            CH.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left)));
            //CH.Dock = DockStyle.Left;

            #region Default Values:
            Height = 900;
            Width = 500;
            PosX = 0;
            PosY = 0;
            MyFontSize = 17;
            
            Title = "";
            SubTitle = "";
            AxisLabelX = "";
            AxisLabelY = "";
            UnitX = "";
            UnitY = "";
            MyFontFamily = "Arial";
            MyFontStyle = FontStyle.Regular;
            MyFont = new Font(MyFontFamily, MyFontSize, MyFontStyle);

            TextColor = Color.White;
            BgColor = Color.Black;
            TextColor = Color.White;
            LineColor = Color.LightGreen;

            #endregion

        }

        public void SetPlotParameters()
        {
            // Setting Position Of Chart.

            CH.Location = new Point(PosX, PosY);
            CH.Size = new Size(Width, Height);

            CH.BackColor = BgColor;
     
            MyFont = new Font(MyFontFamily, MyFontSize, MyFontStyle);

            CH.Titles.Clear(); // IMP
            Title title = CH.Titles.Add(Title);
            title.Font = new Font(MyFontFamily, MyFontSize, FontStyle.Bold);
            title.ForeColor = TextColor;


            #region Chart Area:
            CH.ChartAreas[0].BackColor = BgColor;

            // Border of ChartArea
            CH.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            CH.ChartAreas[0].BorderWidth = 3;
            CH.ChartAreas[0].BorderColor = Color.White;
            #endregion

            #region Axises:
            // Axis Ranges
            CH.ChartAreas[0].AxisX.Minimum = StartX;
            CH.ChartAreas[0].AxisX.Interval = StepsX;

            CH.ChartAreas[0].AxisY.Minimum = StartY;
            CH.ChartAreas[0].AxisY.Interval = StepsY;

            // Axis Title
            CH.ChartAreas[0].AxisX.Title = AxisLabelX;
            CH.ChartAreas[0].AxisX.TitleFont = MyFont;
            CH.ChartAreas[0].AxisX.TitleForeColor = TextColor;

            CH.ChartAreas[0].AxisY.Title = AxisLabelY;
            CH.ChartAreas[0].AxisY.TitleFont = MyFont;
            CH.ChartAreas[0].AxisY.TitleForeColor = TextColor;

            //Axis Lables
            CH.ChartAreas[0].AxisX.LabelStyle.Format = "0.00";
            CH.ChartAreas[0].AxisY.LabelStyle.Format = "0.00";

            CH.ChartAreas[0].AxisX.LabelStyle.ForeColor = TextColor;
            CH.ChartAreas[0].AxisY.LabelStyle.ForeColor = TextColor;

            //Base Line
            CH.ChartAreas[0].AxisX.LineWidth = 3;
            CH.ChartAreas[0].AxisX.LineColor = Color.White;

            CH.ChartAreas[0].AxisY.LineWidth = 3;
            CH.ChartAreas[0].AxisY.LineColor = Color.White;

            #endregion

            #region MajorGrid:

            CH.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            CH.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            CH.ChartAreas[0].AxisY.MajorGrid.Interval = StepsY;
            CH.ChartAreas[0].AxisY.MajorGrid.LineColor = GridColor;

            #endregion

            #region Verticle Annotation:
            VerticalLineAnnotation VA = new VerticalLineAnnotation();
            VA.AxisX = CH.ChartAreas[0].AxisX;
            VA.AllowMoving = false;
            VA.IsInfinitive = true;
            VA.ClipToChartArea = CH.ChartAreas[0].Name;
            //VA.Name = "myLine";
            VA.LineColor = Color.Gray;
            VA.LineWidth = 2;         // use your numbers!
            VA.X = 0;
            CH.Annotations.Add(VA);
            #endregion // for PowerSpectra Plot

            #region Major Tick:
            //MajorTickMark
            //CH.ChartAreas[0].AxisY.MajorTickMark.Interval = StepsY;
            CH.ChartAreas[0].AxisY.MajorTickMark.Size = 1;
            CH.ChartAreas[0].AxisY.MajorTickMark.LineColor = LineColor;
            CH.ChartAreas[0].AxisY.MajorTickMark.IntervalOffset = StepsY / 2;
            CH.ChartAreas[0].AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;

            CH.ChartAreas[0].AxisX.MajorTickMark.Interval = StepsX;
            CH.ChartAreas[0].AxisX.MajorTickMark.Size = 1;
            CH.ChartAreas[0].AxisX.MajorTickMark.LineColor = LineColor;
            CH.ChartAreas[0].AxisX.MajorTickMark.IntervalOffset = StepsY;
            CH.ChartAreas[0].AxisX.MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
            #endregion

            #region Zooming:
            CH.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            CH.ChartAreas[0].CursorX.AutoScroll = true;
            CH.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            CH.ChartAreas[0].AxisX.ScrollBar.BackColor = Color.White;

            CH.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            CH.ChartAreas[0].CursorY.AutoScroll = true;
            CH.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            CH.ChartAreas[0].AxisY.ScrollBar.BackColor = Color.White;
            #endregion


        }
        public void UpdateChart(int[,] RB_Data, int nrgb, int nfft)
        {
            CH.Series.Clear();
            PlotData(RB_Data, nrgb, nfft);
        }
        public void UpdateChart(float[,] RB_Data, int nrgb, int nfft)
        {
            CH.Series.Clear();
            PlotData(RB_Data, nrgb, nfft);
        }
        public void PlotData(int[,] RB_Data, int nrgb, int nfft)
        {

            NoOfGraphs = nrgb; // RB_Data.GetLength(0); // returns numbres of rows;
            NoOfPoints = nfft; // RB_Data.GetLength(1); // returns numbers of column;


            //int height = CH.Height;
            int Steps = 0;    // height/ bins;
            StepsY = CH.Height / NoOfGraphs;

            int sno = CH.Series.Count;
            for (int j = 0; j < NoOfGraphs; j++)
            {

                string sName = "series" + (++sno).ToString();
                AddSeries(sName, CH);

                for (int i = 0; i < NoOfPoints; i++)
                {
                    //CH.Series[sName].Points.AddXY(i - (points / 2), (Scaled_RB_Data[j, i] * StepsY) + k);
                    //CH.Series[sName].Points.AddXY(i - (NoOfPoints / 2), (RB_Data[j, i] * StepsY) + Steps);
                    CH.Series[sName].Points.AddXY(i, (RB_Data[j, i] * StepsY) + Steps);
                }
                Steps += Convert.ToInt32(StepsY);

            }

        }
        public void PlotData(float[,] RB_Data, int nrgb, int nfft)
        {

            NoOfGraphs = nrgb; // RB_Data.GetLength(0); // returns numbres of rows;
            NoOfPoints = nfft; // RB_Data.GetLength(1); // returns numbers of column;


            //int height = CH.Height;
            float Steps = 0;    // height/ bins;
            StepsY = CH.Height / NoOfGraphs;

            int sno = CH.Series.Count;
            for (int j = 0; j < NoOfGraphs; j++)
            {

                string sName = "series" + (++sno).ToString();
                AddSeries(sName, CH);

                for (int i = 0; i < NoOfPoints; i++)
                {
                    //CH.Series[sName].Points.AddXY(i - (points / 2), (Scaled_RB_Data[j, i] * StepsY) + k);
                    //CH.Series[sName].Points.AddXY(i - (NoOfPoints / 2), (RB_Data[j, i] * StepsY) + Steps);
                    CH.Series[sName].Points.AddXY(i, (RB_Data[j, i] * StepsY) + Steps);
                }
                Steps += StepsY;

            }

        }
        public void AddSeries(string seriesName, Chart CH)
        {
            Series series = new Series(seriesName);
            series.ChartType = SeriesChartType.Line;
            series.ChartArea = "ChartArea1";

            series.Color = lineColor;
            CH.Series.Add(series);

            //series.Name = sName;
            //series.ChartArea = "ChartArea1";
            //series.Legend = "Legend1";
        }


    }
}
