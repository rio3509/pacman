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

namespace pacman
{
    internal class Sprite
    {
        Texture2D _spriteTex;
        Vector2 _spritePos;
        Microsoft.Xna.Framework.Color _spriteCol;
        Rectangle _spriteBox;
        public Sprite()
        { }

        public Sprite(Texture2D texture, Vector2 position, Microsoft.Xna.Framework.Color color)
        {
            _spriteTex = texture;
            _spritePos = position;
            _spriteCol = color;
            _spriteBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public Texture2D Texture
        {
            get { return _spriteTex; }
            set { _spriteTex = value; }
        }

        public Vector2 Position
        { 
            get { return _spritePos; }
            set { _spritePos = value; } 
        }

        public Microsoft.Xna.Framework.Color Color
        {
            get { return _spriteCol; }
            set { _spriteCol = value; }
        }

        public Rectangle BoundingBox
        {
            get { return _spriteBox; }
            set { _spriteBox = value; }
        }
    }
}
