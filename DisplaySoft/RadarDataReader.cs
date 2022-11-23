using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace DisplaySoft
{
    internal class RadarDataReader
    {

        #region Fields:
        public Chart CH;
        public RichTextBox textBox;
        private int posX, posY, height, width;
        private string title;
        private DockStyle dc;
        private Color textColor, bgColor;
        #endregion

        #region Properties:
        public string Title
            { get { return title; } set { title = value; } }
        public DockStyle Dock
        { get { return dc; } set { dc = value; } }
        public int Height
        { get { return height; } set { height = value; } }
        public int Width
        { get { return width; } set { width = value; } }
        public int PosX
        { get { return posX; } set { posX = value; } }
        public int PosY
        { get { return posY; } set { posY = value; } }
        public Color BgColor
        { get { return bgColor; } set { bgColor = value; } }
        public Color TextColor
        { get { return textColor; } set { textColor = value; } }

        #endregion

        public RadarDataReader()
        {
            CH = new Chart();
            
            ChartArea ca = new ChartArea();
            CH.ChartAreas.Add(ca);

            textBox = new RichTextBox();

            CH.Anchor = ((AnchorStyles.Left | AnchorStyles.Right)
                | (AnchorStyles)((AnchorStyles.Top | AnchorStyles.Bottom)));

            textBox.Anchor = ((AnchorStyles.Left | AnchorStyles.Right)
                | (AnchorStyles)((AnchorStyles.Top | AnchorStyles.Bottom)));

            // Borders
            textBox.BorderStyle = BorderStyle.None;
     
            CH.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            CH.ChartAreas[0].BorderWidth = 3;
            CH.ChartAreas[0].BorderColor = Color.White;


        }
        public void Initialize()
        {
            
            CH.Size = new Size(Width, Height);
            CH.Location = new Point(PosX, PosY);
            CH.BackColor = bgColor;


            CH.ChartAreas[0].BackColor = BgColor;

            #region Border:
            /*CH.BorderlineDashStyle = ChartDashStyle.Solid;
            CH.BorderlineWidth = 3;
            CH.BorderlineColor = Color.White;*/
            #endregion

            #region Text Box:
            textBox.Location = new Point(PosX + 20, PosY + 110);
            textBox.Size = new Size(Width - 45, Height - 138);
            textBox.BackColor = BgColor;
            textBox.ForeColor = Color.White;
            textBox.Font = new Font("Arial", 12, FontStyle.Bold);
            #endregion

            Title title = CH.Titles.Add(Title);
            title.ForeColor = Color.White;
            title.Font = new Font("Arial", 16, FontStyle.Bold);
        }

    }
}


#region Initialization:
/* 
            RadarDataReader c3 = new RadarDataReader();
            c3.BgColor = Color.Black;

            //c3.Dock = DockStyle.Fill;

            c3.PosX = 1100;
            c3.PosY = 15;

            c3.Width = 350;
            c3.Height = 680;
            c3.Title = "Radar Data \nHeader";
            c3.TextColor = Color.White;
            c3.Initialize();

            // Sequence is Important while Adding Charts to Form:
            Controls.Add(c3.textBox);
            Controls.Add(c3.CH);
 */
#endregion