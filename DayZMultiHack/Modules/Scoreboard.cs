using ExternalD3D11;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Modules
{
    public class Scoreboard : BaseModule
    {
        private float width = 190;
        private float right = 8;
        private float y = 55;

        private const int titleHeight = 23;
        private const float lineHeight = 17.5f;

        public Scoreboard(Main main, UI ui, Menu menu)
            : base(main, ui, menu) { }


        public void Draw()
        {
            if (!Menu.UIShowScoreboardOption.State)
                return;

            float x = DirectXUI.Width - width - right;

            float playerTextHeight = MainModule.ScoreboardTable.Count() * lineHeight;
            float totalHeight = playerTextHeight + titleHeight + 23;

            //Content box
            DirectXUI.DrawFilledBox(x, y, width, totalHeight, new Color(216, 216, 216));
            DirectXUI.DrawBox(x, y, width, totalHeight, 1, new Color(60, 60, 60));

            //Title
            DirectXUI.DrawTransparentBox(x, y, width, titleHeight, new Color(40, 40, 40), 248);
            DirectXUI.DrawBox(x, y, width, titleHeight, 1, Color.Black);
            DirectXUI.DrawShadowText(DirectXUI.HeadlineFont, "Players on Server", x + 5, y + 3, Color.White);

            string text = "";
            foreach (KeyValuePair<int, string> player in MainModule.ScoreboardTable)
                text += player.Value + "\n";

            DirectXUI.DrawBaseText(DirectXUI.UIFont, text, x + 3, y + 27, new Color(10, 10, 10));

            int tnPlayers = MainModule.ScoreboardTable.Where(p => p.Value.Contains("[TN]")).Count();

            DirectXUI.DrawBox(x, y + playerTextHeight + 28, width, 20, 1, Color.Black);
            DirectXUI.DrawFilledBox(x, y + playerTextHeight + 28, width, 20, new Color(60, 60, 60));

            DirectXUI.DrawCenterText(DirectXUI.CreditText, string.Format("Online / TN: {0} / {1}", MainModule.ScoreboardTable.Count.ToString(), tnPlayers.ToString()),
                new RectangleF(x, y + playerTextHeight + 28, width, 0), Color.White);
        }
    }
}
