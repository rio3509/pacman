//using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace pacman
{
    //create player class inheriting from sprites
    internal class Player : Sprite
    {
        //re-declare variables
        Texture2D _spriteTex;
        Microsoft.Xna.Framework.Vector2 _spritePos;
        Color _spriteCol;
        Rectangle _spriteBox;
        KeyboardState _prevBoard;
        private Point _grid;

        //allow for an empty instance to be created (constructor)
        public Player()
        { }

        //define method to create a player
        public Player(Texture2D texture, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color)
            :base (texture, position, color)
        {
            _spriteTex = texture;
            _spritePos = position;
            _spriteCol = color;
            _spriteBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        //define movement method
        public void Movement(Microsoft.Xna.Framework.Vector2 velocity, Tile[,] tileMap)
        {
            Point _delta = Point.Zero;
            KeyboardState currentBoard = Keyboard.GetState();

            if (WasKeyPressed(_prevBoard, Keys.Up))
            {
                _delta = new Point(0, -1);
                //if (IsTileWalkable(tileMap, _delta, _spritePos, _spriteTex))
                //{
                //    //_spritePos = new Microsoft.Xna.Framework.Vector2(_spritePos.X, _spritePos.Y - velocity.Y);
                //    _delta = new Point();
                //}
                
            }

            if (WasKeyPressed(_prevBoard, Keys.Down))
            {
                _delta = new Point(0, 1);
                //if (_spritePos.Y < (1400 - _spriteTex.Height))
                //{
                //    _spritePos = new Microsoft.Xna.Framework.Vector2(_spritePos.X, _spritePos.Y + velocity.Y);
                //}

            }

            if (WasKeyPressed(_prevBoard, Keys.Left))
            {
                _delta = new Point(-1, 0);
                //if (_spritePos.X > 0)
                //{
                //    _spritePos = new Microsoft.Xna.Framework.Vector2(_spritePos.X - velocity.X, _spritePos.Y);
                //}

            }

            if (WasKeyPressed(_prevBoard, Keys.Right))
            {
                _delta = new Point(1, 0);
                //if (_spritePos.X < (1000 - _spriteTex.Width))
                //{
                //    _spritePos = new Microsoft.Xna.Framework.Vector2(_spritePos.X + velocity.X, _spritePos.Y);
                //}

            }

            Point _target = new Point(_grid.X + _delta.X, _grid.Y + _delta.Y);
            
            if (IsTileWalkable(tileMap, _target))
            {
                //if tile is walkable, update position and collision box (done when drawing)
                _grid = _target;
                _spritePos = new Microsoft.Xna.Framework.Vector2(_grid.X * _spriteTex.Width, _grid.Y * _spriteTex.Height);
            }

            _prevBoard = currentBoard;
        }

        //define a method to check if a key has been held down
        private bool WasKeyPressed(KeyboardState current, Keys key)
        {
            //if the currently held down key and previously held down key match, return true
            if (current.IsKeyDown(key) && !_prevBoard.IsKeyDown(key))
            {
                return true;
            }
            //else false
            else
            {
                return false;
            }
        }

        //create a function to check if a given tile is walkable
        private bool IsTileWalkable(Tile[,] tileMap, Point target)
        {
            //if the position of the given tile is outside of bounds, then it is not walkable
            if (_spritePos.X >= (tileMap.GetLength(1)) || (_spritePos.X < 0) || (_spritePos.Y >= tileMap.GetLength(0)) || (_spritePos.Y < 0))
            {
                return false;
            }
            else
            {
                //if a given tile is "Empty" then return true
                return (tileMap[target.Y, target.X].Type == "Empty");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteTex, _spritePos, _spriteCol);
            _spriteBox.X = (int)_spritePos.X;
            _spriteBox.Y = (int)_spritePos.Y;
        }
    }
}
