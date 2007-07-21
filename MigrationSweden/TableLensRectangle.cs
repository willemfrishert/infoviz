using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GAVExtra
{
    public class TableLensRectangle
    {
        private RectangleF iRectangle;
        private bool iSelectedRectangle;
        private int iSelectedRowNumber;
        private int iMinRowNumber;
        private int iMaxRowNumber;

        public RectangleF Rect
        {
            get
            {
                return iRectangle;
            }
            set
            {
                iRectangle = value;
            }
        }

        public bool FocusRectangle
        {
            get
            {
                return iSelectedRectangle;
            }
            set
            {
                iSelectedRectangle = value;
            }
        }

        public int SelectedRow
        {
            get
            {
                return iSelectedRowNumber;
            }
            set
            {
                iSelectedRowNumber = value;
            }
        }

        public int MinimumRowNumber
        {
            get
            {
                return iMinRowNumber;
            }
            set
            {
                iMinRowNumber = value;
            }
        }

        public int MaximumRowNumber
        {
            get
            {
                return iMaxRowNumber;
            }
            set
            {
                iMaxRowNumber = value;
            }
        }

        public TableLensRectangle()
        {
            iSelectedRectangle = false;
            iSelectedRowNumber = -1;
            iMaxRowNumber = -1;
            iMinRowNumber = -1;

            iRectangle = new RectangleF();
        }


    }
}
