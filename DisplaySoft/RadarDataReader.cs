﻿using System;
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

        public Chart CH;

        public RichTextBox textBox;
        private int posX, posY, height, width;
        private DockStyle dc;
        private Color textColor, bgColor;


        #region Properties:

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
            //CH.Dock = DockStyle.Right;
            textBox = new RichTextBox();

            CH.Anchor = ((AnchorStyles.Left | AnchorStyles.Right)
                | (AnchorStyles)((AnchorStyles.Top | AnchorStyles.Bottom)));

            textBox.Anchor = ((AnchorStyles.Left | AnchorStyles.Right)
                | (AnchorStyles)((AnchorStyles.Top | AnchorStyles.Bottom)));

            //textBox.Dock = DockStyle.Fill;

        }
        public void Initialize()
        {
            CH.Size = new Size(Width, Height);
            CH.Location = new Point(PosX, 0);
            CH.BackColor = BgColor;
            //CH.Dock = Dock;

            textBox.Location = new Point(PosX + 35, 15);
            textBox.Size = new Size(Width - 70, 420);
        }

    }
}
