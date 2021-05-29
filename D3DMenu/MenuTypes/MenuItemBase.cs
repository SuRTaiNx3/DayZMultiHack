using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3DMenu.MenuTypes
{
    public class MenuItemBase
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public bool Save { get; set; }

        private int _height = 15;
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }


        private bool _drawMe = true;
        public bool DrawMe
        {
            get { return _drawMe; }
            set { _drawMe = value; }
        }


        public MenuItemBase()
        {
            this.Enabled = true;
        }

        public virtual void Previous(){}

        public virtual void Next() { }
    }
}
