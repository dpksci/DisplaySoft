using DisplaySoft;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DisplaySoft
{

    internal class DataReader
    {
        #region Feilds:
        private Graphics graphic;
        private Pen pen;
        private string title, xmlPath;
        private Color penColor, txtColor;
        private int posX, posY, height, width, penSize;
        private Font titleFont, textFont; // = new Font(Font, TitleFontSize, FontStyle.Bold);
        #endregion

        #region Properties:
        public string Title { get { return title; } set { title = value; } }
        
        public int PosX { get { return posX; } set { posX = value; } }
        public int PosY { get { return posY; } set { posY = value; } }
        public int Height { get { return height; } set { height = value; } }
        public int Width { get { return width; } set { width = value; } }
        public int PenSize { get { return penSize; } set { penSize = value; } }
        public Graphics SetGraphic { set { graphic = value; } }
        public Color PenColor { set { penColor = value; } get { return penColor; } }
        public Color TextColor { set { txtColor = value; } get { return txtColor; } }

        public void Position(int x, int y)
        {
            posX = x; posY = y;
        }
        public void SetTitleFont(string font, int size, FontStyle style)
        {
            titleFont = new Font(font, size, style);
        }
        public void SetTextFont(string font, int size, FontStyle style)
        {
            textFont = new Font(font, size, style);
        }
        public void SetPen(Color penColor, int penSize)
        {
            pen = new Pen(penColor, penSize);
        }
        public string XmlPath
            { get { return xmlPath; } set { xmlPath = value; } }
       

        //public Brush SetBrush { set { brush = value; } }

        //blic int TitleFontSize { get { return titleFontSize; } set { titleFontSize = value; } }
        //public Pen SetPen { set { pen = value; } }
        //blic string Font { get { return font; } set { font = value; } }
        #endregion


        public void RenderText(PaintEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.WordBreak | TextFormatFlags.Left;

            XElement xElements = XElement.Load(XmlPath);

            IEnumerable<XElement> Elements = xElements.Elements("Parameters");

            int nL = 0;   
            foreach (var item in Elements)
            {
               
                /*int fontSize = int.Parse((item.Elements("FontSize").ToString()));
                Font myFont = new Font(font, fontSize, FontStyle.Regular);*/
                TextRenderer.DrawText(e.Graphics, item.Value, textFont,
                new Rectangle(PosX + 10, PosY + 10 + nL, Width - 20, Height - 10), 
                SystemColors.ControlLightLight, flags);
                nL += 150;
            }

        }

        public void Initialise()
        {
            //pen = new Pen(PenColor, PenSize);
            graphic.DrawRectangle(pen, PosX, PosY, Width, Height);

            //Font myFont = new Font(Font, TitleFontSize, FontStyle.Bold);
            Brush myBrush = new SolidBrush(TextColor);
            graphic.DrawString(Title, titleFont, myBrush, PosX + 100, 30);
            graphic.Dispose();


        }

      /*  public XElement Data()
        {    
            XElement xElement = XElement.Load(XmlPath);
            return xElement;   
        }
        public string PrintData(XElement xelem)
        {
            XElement xElem = xelem;

            var Elements = from elemList in xElem.Elements("Elements")
                                 select elemList;

            foreach (var item in Elements)
            {
                return item.Value;
            }
        }*/

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