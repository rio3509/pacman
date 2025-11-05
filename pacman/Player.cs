//using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Drawing.Text;

namespace pacman
{
    internal class Player : Sprite
    {
        //declare variables
        Texture2D _PspriteTex;
        Microsoft.Xna.Framework.Vector2 _PspritePos;
        Color _PspriteCol;
        Rectangle _PspriteBox;
        private Point _grid;
        private int _tileWidth;
        private int _tileHeight;
        private KeyboardState _prevBoard;
        private Tile[,] _privateTiles;
        public Player()
        { }

        public Player(Texture2D texture, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color)
            :base (texture, position, color)
        {
            _PspriteTex = texture;
            _PspritePos = position;
            _PspriteCol = color;
            _PspriteBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        //set player location to a particular tile ("home" tile)
        public void HomeOnGrid(Point grid, int tileWidth, int tileHeight, Tile[,] tiles)
        {
            _privateTiles = tiles;
            _grid = grid;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;

            //convert grid location ("home" location) to pixel location
            _PspritePos = new Vector2(_grid.X * _tileWidth, _grid.Y * _tileHeight);

            //set bounding box to home too
            _PspriteBox = new Rectangle((int)_PspritePos.X, (int)_PspritePos.Y, _PspriteTex.Width, _PspriteTex.Height);
        }

        //method to check if a key has been pressed (will only return true when the key is released)
        private bool WasKeyPressed(KeyboardState current, Keys key)
        {
            if (current.IsKeyDown(key) && !_prevBoard.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //passable or not method
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
            else if (tiles[target.Y, target.X].Type == "Pill_L")
            {
                _privateTiles[target.Y, target.X].Type = "Empty";
                return "true";
            }
            else
            {
                return "false";
            }

        }


        //movement method
        public void Update(GameTime gameTime, Tile[,] tiles)
        {
            //_privateTiles = tiles;
            KeyboardState currentBoard = Keyboard.GetState();

            Point delta = Point.Zero;

            if (WasKeyPressed(currentBoard, Keys.Up))
            {
                delta = new Point(0, -1);
                //_PspriteCol = Color.Red;
            }

            else if (WasKeyPressed(currentBoard, Keys.Down))
            {
                delta = new Point(0, 1);
                //_PspriteCol = Color.Red;
            }

            else if (WasKeyPressed(currentBoard, Keys.Left))
            {
                delta = new Point(-1, 0);
            }

            else if (WasKeyPressed(currentBoard, Keys.Right))
            {
                delta = new Point(1, 0);
            }

            if (delta != Point.Zero)
            {
                //set target to tile that will be moved to, and check if it's walkable
                Point target = new Point(_grid.X + delta.X, _grid.Y + delta.Y);

                if (IsPassable(target, tiles) == "true")
                {
                    //update grid/position/bounding box
                    _grid = target;
                    _PspritePos = new Vector2(_grid.X * _tileWidth, _grid.Y * _tileHeight);
                    _PspriteBox = new Rectangle((int)_PspritePos.X, (int)_PspritePos.Y, _PspriteTex.Width, _PspriteTex.Height);

                }

                //if (IsPassable(target, tiles) == "bigPillTrue")
                //{
                //    //update grid/position/bounding box
                //    _grid = target;
                //    _PspritePos = new Vector2(_grid.X * _tileWidth, _grid.Y * _tileHeight);
                //    _PspriteBox = new Rectangle((int)_PspritePos.X, (int)_PspritePos.Y, _PspriteTex.Width, _PspriteTex.Height);

                //}
            }

            _prevBoard = currentBoard;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_PspriteTex, _PspritePos, _PspriteCol);
        }


        //test from here
        public override Rectangle BoundingBox
        {
            get { return _PspriteBox; }
            set { _PspriteBox = value; }
        }
    }
}
