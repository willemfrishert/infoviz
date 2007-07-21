using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MigrationSweden
{
    class MapTabControl : TabControl
    {
        // list containing all tab panels
        private List<MapTabPage> iMapTabPage;

        public List<MapTabPage> AgeTabPage
        {
            get
            {
                return iMapTabPage;
            }
        }

        public MapTabControl(List<string> aTabPageNames)
        {
            this.Dock = DockStyle.Fill;
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.SizeMode = TabSizeMode.Normal;

            iMapTabPage = new List<MapTabPage>();

            for (int i=0; i < aTabPageNames.Count; i++)
            {
                MapTabPage tabPage = new MapTabPage(aTabPageNames[i]);
                tabPage.Tag = i;

                iMapTabPage.Add(tabPage);
                this.TabPages.Add(tabPage);
            }

            this.DrawItem += new DrawItemEventHandler(MapTabControl_DrawItem);
            this.Selecting += new TabControlCancelEventHandler(MapTabControl_Selecting);
        }

        void MapTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (iMapTabPage[e.TabPageIndex].Selectable == false)
            {
                e.Cancel = true;
            }
        }

        void MapTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            Font font = new Font("Times New Roman", 8.25f, FontStyle.Bold);
            SolidBrush brushBackground = new SolidBrush(Color.White);

            for (int i = 0; i < this.AgeTabPage.Count; i++)
            {
                SolidBrush brush = new SolidBrush(Color.Black);
                Rectangle tabArea = this.GetTabRect(i);

                if (this.SelectedIndex == i)
                {
                    brush = new SolidBrush(Color.Red);
                    g.FillRectangle(brushBackground, tabArea);
                }
                else
                {
                    if (!this.iMapTabPage[i].Selectable)
                    {
                        brush = new SolidBrush(iMapTabPage[i].BackColor);
                    }
                }

                g.DrawString(AgeTabPage[i].Text, font, brush, (RectangleF)tabArea, format);
            }
        }
    }
}
