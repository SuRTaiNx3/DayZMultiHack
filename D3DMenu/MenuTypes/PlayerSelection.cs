using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3DMenu.MenuTypes
{
    public class PlayerSelection : MenuItemBase
    {
        private List<Player> _players = new List<Player>();
        public List<Player> Players
        {
            get { return _players; }
            set
            {
                for (int i = 0; i < value.Count(); i++)
                    value[i].MenuIndex = i;

                if (SelectedEnemy == null && value.Count > 0)
                    SelectedEnemy = value[0];

                _players = value;
            }
        }

        public Player SelectedEnemy { get; set; }

        public PlayerSelection(string title, bool enabled = true)
        {
            this.Title = title;
            this.Enabled = enabled;
        }

        public void UpdateSelectedEnemy()
        {
            if (SelectedEnemy != null && Players.Count(p => p.Pointer == SelectedEnemy.Pointer) < 1)
            {
                if (Players[SelectedEnemy.MenuIndex + 1] != null)
                    SelectedEnemy = Players[SelectedEnemy.MenuIndex + 1];
                else if (Players[SelectedEnemy.MenuIndex - 1] != null)
                    SelectedEnemy = Players[SelectedEnemy.MenuIndex - 1];
                else
                    SelectedEnemy = null;
            }
        }

        public override void Next()
        {
            if (Players.Count <= 1)
                return;

            if ((Players.Count - 1) >= (SelectedEnemy.MenuIndex + 1))
                SelectedEnemy = Players[SelectedEnemy.MenuIndex + 1];
            else
                SelectedEnemy = Players[0];
        }

        public override void Previous()
        {
            if (Players.Count <= 1)
                return;

            if ((SelectedEnemy.MenuIndex - 1) > -1 && Players.Count - 1 > (SelectedEnemy.MenuIndex - 1))
                SelectedEnemy = Players[SelectedEnemy.MenuIndex - 1];
            else
                SelectedEnemy = Players[Players.Count - 1];
        }

        public class Player
        {
            public IntPtr Pointer;
            public string Name;
            public int MenuIndex;
        }
    }
}
