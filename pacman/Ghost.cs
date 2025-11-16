using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pacman
{
    internal class Ghost : Sprite
    {
        private Texture2D _ghostTex;
        private Vector2 _ghostPos;
        private Color _ghostColor;
        private Rectangle _ghostBox;
        private Tile[,] _privateTiles;
        private Point _grid;
        private int _tileWidth;
        private int _tileHeight;
        private bool _ghostVisible = true;

        //constructor
        public Ghost()
        {
        }

        public Ghost(Texture2D texture, Vector2 position, Color color)
            :base (texture, position, color)
        {
            _ghostColor = color;
            _ghostTex = texture;
            _ghostPos = position;
            _ghostBox = new Rectangle((int)_ghostPos.X, (int)_ghostPos.Y, _ghostTex.Width, _ghostTex.Height);
        }

        //draw function
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_ghostVisible)
            {
                spriteBatch.Draw(_ghostTex, _ghostPos, _ghostColor);
            }
        }

        //bounding box property for collision
        public override Rectangle BoundingBox
        {
            get { return _ghostBox; }
            set { _ghostBox = value; }
        }

        //visibility property
        public bool Visible
        {
            get { return _ghostVisible; }
            set { _ghostVisible = value; }
        }

        //position property for spawn resetting
        public override Vector2 Position
        {
            get { return _ghostPos; }
            set { _ghostPos = value; }
        }

        //colour property
        public override Color Color
        {
            get { return _ghostColor; }
            set { _ghostColor = value; }
        }

        //homing function - set ghost on a given point on the grid
        public void PlaceGhostOnGrid(Point grid, int tileWidth, int tileHeight, Tile[,] tiles)
        {
            _privateTiles = tiles;
            _grid = grid;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;

            //convert grid location ("home" location) to pixel location
            _ghostPos = new Vector2(_grid.X * _tileWidth, _grid.Y * _tileHeight);

            //set bounding box to given location too
            _ghostBox = new Rectangle((int)_ghostPos.X, (int)_ghostPos.Y, _ghostTex.Width, _ghostTex.Height);
        }

        //movement method
        public void Update(GameTime gameTime, Tile[,] tiles)
        {
            //update grid to prevent teleportation
            _grid = new Point ((int)(_ghostPos.X / _tileWidth), (int)(_ghostPos.Y / _tileHeight));

            Point delta = Point.Zero;
            Random rand = new Random();

            int number = (int) rand.Next(0, 4);

            switch (number)
            {
                case 0:
                    //go up
                    delta = new Point(0, -1);
                    break;
                case 1:
                    //go down
                    delta = new Point(0, 1);
                    break;
                case 2:
                    //go left
                    delta = new Point(-1, 0);

                    break;
                case 3:
                    //go right
                    delta = new Point(1, 0);
                    break;
                default:
                    //go up
                    delta = new Point(0, -1);
                    break;
            }


            if (delta != Point.Zero)
            {
                //set target to tile that will be moved to, and check if it's walkable
                Point target = new Point(_grid.X + delta.X, _grid.Y + delta.Y);

                if (IsPassable(target, tiles) == "true")
                {
                    //update grid/position/bounding box
                    _grid = target;
                    _ghostPos = new Vector2(_grid.X * _tileWidth, _grid.Y * _tileHeight);
                    _ghostBox = new Rectangle((int)_ghostPos.X, (int)_ghostPos.Y, _ghostTex.Width, _ghostTex.Height);
                }
            }
        }

        private string IsPassable(Point target, Tile[,] tiles)
        {
            if (target.Y < 0 || target.Y >= tiles.GetLength(0) || target.X < 0 || target.X >= tiles.GetLength(1))
            {
                return "false";
            }
            else if (tiles[target.Y, target.X].Type == "Empty")
            {
                return "true";
            }
            else if (tiles[target.Y, target.X].Type == "Pill_L" || tiles[target.Y, target.X].Type == "Spawn")
            {
                _privateTiles[target.Y, target.X].Type = "Empty";
                return "true";
            }
            else
            {
                return "false";
            }

        }
    }
}
