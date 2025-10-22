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

namespace pacman
{
    internal class Player : Sprite
    {
        Texture2D _spriteTex;
        Microsoft.Xna.Framework.Vector2 _spritePos;
        Color _spriteCol;
        Rectangle _spriteBox;
        public Player()
        { }

        public Player(Texture2D texture, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color)
            :base (texture, position, color)
        {
            _spriteTex = texture;
            _spritePos = position;
            _spriteCol = color;
            _spriteBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Movement(Microsoft.Xna.Framework.Vector2 velocity)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (_spritePos.Y > 0)
                {
                    _spritePos = new Microsoft.Xna.Framework.Vector2(_spritePos.X, _spritePos.Y - velocity.Y);
                }
                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (_spritePos.Y < (1400 - _spriteTex.Height))
                {
                    _spritePos = new Microsoft.Xna.Framework.Vector2(_spritePos.X, _spritePos.Y + velocity.Y);
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (_spritePos.X > 0)
                {
                    _spritePos = new Microsoft.Xna.Framework.Vector2(_spritePos.X - velocity.X, _spritePos.Y);
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (_spritePos.X < (1000 - _spriteTex.Width))
                {
                    _spritePos = new Microsoft.Xna.Framework.Vector2(_spritePos.X + velocity.X, _spritePos.Y);
                }

            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteTex, _spritePos, _spriteCol);
        }
    }
}
