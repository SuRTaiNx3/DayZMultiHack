using DayZMultiHack.Models;
using ExternalD3D11;
using ExternalD3D11.Controls;
using ExternalD3D11.ScrollMenu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilsLibrary.MathObjects;

namespace DayZMultiHack.Modules
{
    public class WaypointESP : BaseModule
    {
        #region Globals

        private const string xmlFile = "waypoints.xml";

        private float DeleteWaypointRadius = 50;

        private bool waitingForEnter = false;
        private List<Waypoint> waypoints = new List<Waypoint>();
        private Waypoint newWaypoint;

        #endregion

        #region Constructor

        public WaypointESP(Main main, UI ui, Menu menu)
            : base(main, ui, menu)
        {
            ui.TextBox.OnEnterPressed += TextBox_OnEnterPressed;
            ui.OnKeyPressed += Ui_OnKeyPressed;

            LoadFromFile();

            // Add to quick menu
            //ScrollMenuCategory waypointCategory = new ScrollMenuCategory();
            //waypointCategory.Title = "Waypoints";
            //DirectXUI.ScrollMenu.Categories.Add(waypointCategory);
            //waypointCategory.Items.Add(new ScrollMenuFunctionItem("Delete Waypoint", DeleteWaypoint, DeleteVisibilityCheck));
            //waypointCategory.Items.Add(new ScrollMenuFunctionItem("Add Waypoint", AddWaypoint));
        }

        #endregion

        #region Methods

        public void Draw()
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                Waypoint w = waypoints[i];
                if (!w.IsVisible)
                    continue;

                Vector3 screenPosition = Geometry.W2SN(w.Position, MainModule.TransformationData);
                //Vector3 screenPosition = new Vector3(300, 300, 1);
                Vector3 d3 = MainModule.LocalEntity.Position - w.Position;
                float distance = (float)Math.Sqrt((d3.X * d3.X) + (d3.Y * d3.Y) + (d3.Z * d3.Z)); ;

                if (screenPosition.Z < 0.01f || screenPosition.X <= 0 ||
                screenPosition.X > DirectXUI.Width || screenPosition.Y > DirectXUI.Height)
                    continue;


                w.ScreenPosition = screenPosition;
                w.Distance = distance;

                DirectXUI.DrawShadowText(DirectXUI.UIFont, string.Format("{0}[{1}]", w.Name, distance.ToString("00")), screenPosition.X, screenPosition.Y, Menu.AirfieldsColorOption.GetColor());
            }
        }

        private void DeleteVisibilityCheck(ScrollMenuBaseItem item)
        {
            int x = DirectXUI.Width / 2;
            int y = DirectXUI.Height / 2;

            ScrollMenuFunctionItem deleteWaypointItem = item as ScrollMenuFunctionItem;
            Waypoint nearestWaypoint = waypoints.Aggregate((a, b) => DistanceToPoint(a, x, y) > DistanceToPoint(b, x, y) ? b : a);

            double distance = DistanceToPoint(nearestWaypoint, x, y);
            if (distance > 0 && distance <= DeleteWaypointRadius)
                item.IsVisible = true;
            else
                item.IsVisible = false;
        }

        public void AddWaypoint()
        {
            // New Waypoint
            newWaypoint = new Waypoint();
            newWaypoint.Position = MainModule.InGameCrosshairPosition;
            newWaypoint.IsVisible = false;

            // Show textbox
            waitingForEnter = true;
            DirectXUI.TextBox.Width = 200;
            DirectXUI.TextBox.PositionX = (DirectXUI.Width / 2) - (DirectXUI.TextBox.Width / 2);
            DirectXUI.TextBox.PositionY = (DirectXUI.Height / 2) - (DirectXUI.TextBox.Height / 2);
            DirectXUI.TextBox.Title = "Waypoint name:";
            DirectXUI.TextBox.IsVisible = true;
        }

        private void DeleteWaypoint()
        {
            int x = DirectXUI.Width / 2;
            int y = DirectXUI.Height / 2;

            if (waypoints.Count > 0)
            {
                Waypoint nearestWaypoint = waypoints.Aggregate((a, b) => DistanceToPoint(a, x, y) > DistanceToPoint(b, x, y) ? b : a);

                if (!string.IsNullOrEmpty(nearestWaypoint.Name))
                    waypoints.Remove(nearestWaypoint);

                SaveToFile();
            }
        }

        private void Ui_OnKeyPressed(object sender, int code)
        {
            if (code == Keys.Escape.GetHashCode())
            {
                DirectXUI.TextBox.IsVisible = false;
                DirectXUI.TextBox.ResetText();
            }
        }

        private void TextBox_OnEnterPressed(object sender, string text)
        {
            if (!waitingForEnter)
                return;

            waitingForEnter = false;

            // Waypoint text
            DirectXUI.TextBox.IsVisible = false;
            DirectXUI.TextBox.ResetText();
            newWaypoint.Name = text;
            newWaypoint.IsVisible = true;
            waypoints.Add(newWaypoint);

            SaveToFile();
        }

        private double DistanceToPoint(Waypoint waypoint, float x, float y)
        {
            // Current Distance
            float dx = (waypoint.ScreenPosition.X - x);
            float dy = (waypoint.ScreenPosition.Y - y);
            double distance = Math.Sqrt((dx * dx) + (dy * dy));

            return distance;
        }

        private void SaveToFile()
        {
            IOHelper.Serialize<List<Waypoint>>(waypoints, xmlFile);
        }

        private void LoadFromFile()
        {
            if (!File.Exists(xmlFile))
                return;

            waypoints = IOHelper.Deserialize<List<Waypoint>>(xmlFile);
        }

        #endregion
    }
}
