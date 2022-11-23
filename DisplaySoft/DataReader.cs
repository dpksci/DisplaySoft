using DisplaySoft;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplaySoft
{

    internal class DataReader
    {
        #region Brush brush:
        private Graphics graphic;
        private Pen pen;
        private string title;
        private Color penColor, txtColor;
        private int posX, posY, height, width, penSize;
        private Font titleFont, textFont; // = new Font(Font, TitleFontSize, FontStyle.Bold);
        #endregion

        #region Properties:
        public string Title { get { return title; } set { title = value; } }
        //blic string Font { get { return font; } set { font = value; } }
        public int PosX { get { return posX; } set { posX = value; } }
        public int PosY { get { return posY; } set { posY = value; } }
        public int Height { get { return height; } set { height = value; } }
        public int Width { get { return width; } set { width = value; } }
        //blic int TitleFontSize { get { return titleFontSize; } set { titleFontSize = value; } }
        public int PenSize { get { return penSize; } set { penSize = value; } }
        public Pen SetPen { set { pen = value; } }
        public Graphics SetGraphic { set { graphic = value; } }
        public Color PenColor { set { penColor = value; } get { return penColor; } }
        public Color TextColor { set { txtColor = value; } get { return txtColor; } }

        public void SetTitleFont(string font,int size, FontStyle style)
        {
            titleFont = new Font(font, size, style);
        }
        public void SetTextFont(string font, int size, FontStyle style)
        {
            textFont = new Font(font, size, style);
        }
        //public Brush SetBrush { set { brush = value; } }
        #endregion
        public void RenderText(PaintEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.WordBreak | TextFormatFlags.Left;
           
            TextRenderer.DrawText(e.Graphics, " is some text that will display on multiple lines.", textFont,
                new Rectangle(PosX + 10, PosY + 10 , Width-10, Height-10), SystemColors.ControlLightLight, flags);
        }

        public void Initialise()
        {
            pen = new Pen(PenColor, PenSize);
            graphic.DrawRectangle(pen, PosX, PosY, Width, Height);

            //Font myFont = new Font(Font, TitleFontSize, FontStyle.Bold);
            Brush myBrush = new SolidBrush(TextColor);
            graphic.DrawString(Title, titleFont, myBrush, PosX + 100, 30);
            graphic.Dispose();


        }
    }
}

#region Initialization:
/*
DataReader r = new DataReader();
r.PosX = 1110;
r.PosY = 111;
r.Width = 350;
r.Height = 680;
r.SetGraphic = this.CreateGraphics();

r.PenColor = Color.White;
r.PenSize = 3;

r.Title = "Radar Data\n    Reader";
r.TextColor = Color.White;
r.Font = "Arial";

r.TitleFontSize = 16;
r.Initialise();*/
#endregion