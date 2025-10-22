using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using System.Collections.Generic;

namespace pacman
{
    internal class Sprite
    {
        Texture2D _spriteTex;
        Microsoft.Xna.Framework.Vector2 _spritePos;
        Microsoft.Xna.Framework.Color _spriteCol;
        Microsoft.Xna.Framework.Rectangle _spriteBox;
        public Sprite()
        { }

        public Sprite(Texture2D texture, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color)
        {
            _spriteTex = texture;
            _spritePos = position;
            _spriteCol = color;
            _spriteBox = new Microsoft.Xna.Framework.Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public Texture2D Texture
        {
            get { return _spriteTex; }
            set { _spriteTex = value; }
        }

        public Microsoft.Xna.Framework.Vector2 Position
        { 
            get { return _spritePos; }
            set { _spritePos = value; } 
        }

        public Microsoft.Xna.Framework.Color Color
        {
            get { return _spriteCol; }
            set { _spriteCol = value; }
        }

        public Microsoft.Xna.Framework.Rectangle BoundingBox
        {
            get { return _spriteBox; }
            set { _spriteBox = value; }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteTex, _spritePos, _spriteCol);
        }
    }
}
