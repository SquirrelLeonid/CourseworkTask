using System;
using System.Drawing;
using System.Windows.Forms;

namespace CourseworkTask
{
    public class MapModel
    {
        private MapVisualizer _visualizer;
        private MapOrganiser _organiser;
        private Panel _map;
        private Timer _mapUpdateTimer;

        private bool _isMouseHold;
        private Point _prevMousePos;

        public MapModel(Panel map)
        {
            _map = map;
            InitFieldsState();
            RegisterEvents();
        }

        private void InitFieldsState()
        {
            _visualizer = new MapVisualizer();
            _organiser = new MapOrganiser();
            _mapUpdateTimer = new Timer();
            _mapUpdateTimer.Interval = 10;
        }

        private void RegisterEvents()
        {
            _map.MouseWheel += OnResizeEvent;
            _map.MouseDown += OnMouseDownEvent;
            _map.MouseMove += OnMouseMoveEvent;
            _map.MouseUp += OnMouseUpEvent;
            _mapUpdateTimer.Tick += OnRedrawEvent;
        }

        private void OnRedrawEvent(object sender, EventArgs e)
        {
            _visualizer.Redraw(_map);
        }

        private void OnMouseDownEvent(object sender, MouseEventArgs e)
        {
            _prevMousePos = Cursor.Position;
            Cursor.Current = Cursors.Hand;
            _isMouseHold = true;
            _mapUpdateTimer.Start();
        }

        private void OnMouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (_isMouseHold)
            {
                int mousePosX = Cursor.Position.X;
                int mousePosY = Cursor.Position.Y;

                int diffX = mousePosX - _prevMousePos.X;
                int diffY = mousePosY - _prevMousePos.Y;

                _map.Location = new Point(_map.Location.X + diffX, _map.Location.Y + diffY);

                _prevMousePos.X = mousePosX;
                _prevMousePos.Y = mousePosY;
            }

        }

        private void OnMouseUpEvent(object sender, MouseEventArgs e)
        {
            _isMouseHold = false;
            _mapUpdateTimer.Stop();
        }

        private void OnResizeEvent(object sender, MouseEventArgs e)
        {
            bool increase;
            if (e.Delta > 0)
                increase = true;
            else if (e.Delta < 0)
                increase = false;
            else
                return;
            _visualizer.Resize(_map, increase);
        }
    }
}