using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MigrationSweden
{
    class MapTabPage : TabPage
    {
        private bool iSelectable;
        
        public bool Selectable
        {
            set
            {
                iSelectable = value;
            }
            get
            {
                return iSelectable;
            }
        }

        public MapTabPage()
        {
            iSelectable = true;
            this.ForeColor = Color.DarkBlue;
        }

        public MapTabPage(string aTabString) : base(aTabString)
        {
            iSelectable = true;
        }
    }
}
