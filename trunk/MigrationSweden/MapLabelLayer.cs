using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Gav.Data;
using Gav.Graphics;

using Microsoft.DirectX;
using Direct3D = Microsoft.DirectX.Direct3D;

namespace MigrationSweden
{
    class MapLabelLayer : MapLayer
    {
        private List<string> iLabels;
        private bool iInited;

        private Direct3D.Font iFont;
        private System.Drawing.Font iWindowsFont;

        private Graphics iGraphics;
        private Control iControl;

        public List<string> Labels
        {
            get
            {
                return iLabels;
            }
        }

        public Control Control
        {
            set
            {
                iControl = value;
                iGraphics = iControl.CreateGraphics();
            }
            get
            {
                return iControl;
            }
        }

        public MapLabelLayer()
        {
            iLabels = new List<string>();
            iWindowsFont = new System.Drawing.Font("Courier New", 9.0f);
        }

        public MapLabelLayer(Control aControl)
        {
            iControl = aControl;
            iLabels = new List<string>();
            iWindowsFont = new System.Drawing.Font("Courier New", 9.0f);
        }

        protected override void InternalRender()
        {
            // If the glyph is not inited, call InternalInit. 
            if (!iInited)
            {
                InternalInit(this._device);
                if (!iInited)
                {
                    return;
                }
            }

            int incrementedHeight = 0;
            SizeF size;
            Rectangle rect = new Rectangle();
            for (int i =0; i< iLabels.Count; i++)
            {
                // calculate size of string in pixels
                size = iGraphics.MeasureString(iLabels[i].Trim(), iWindowsFont);

                // setup bounding box of that size
                rect.Width = size.ToSize().Width;
                rect.Height = size.ToSize().Height;
                rect.X = iControl.Width - (size.ToSize().Width+3);
                rect.Y = incrementedHeight;

                iFont.DrawText(null, iLabels[i].Trim(), rect, Direct3D.DrawTextFormat.Right | Direct3D.DrawTextFormat.VerticalCenter, Color.Black);
                incrementedHeight += size.ToSize().Height;
            }
        }

        protected override void InternalInit(Direct3D.Device device)
        {
            if (iLabels == null)
            {
                return;
            }

            if (iControl == null)
            {
                return;
            }

            iGraphics = iControl.CreateGraphics();

            iFont = new Direct3D.Font(_device, iWindowsFont);
            iInited = true;
        }

        protected override void InternalInvalidate()
        {

        }
    }
}
