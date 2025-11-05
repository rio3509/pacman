using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace pacman
{
    internal class Pill
    {
        private Texture2D _texture;
        private Color _color;
        private Rectangle _boundingBox;
        private Vector2 _position;
        private bool _visible;

        public Pill()
        {
        }

        public Pill(Texture2D texture, Color color, Vector2 position)
        {
            _texture = texture;
            _color = color;
            _position = position;
            _boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            _visible = true;
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public Rectangle BoundingBox
        {
            get { return _boundingBox; }
            set { _boundingBox = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public void DrawPill(SpriteBatch spritebatch)
        {
            if (_visible)
            {
                spritebatch.Draw(_texture, Position, _color);
            }
        }

    }
}
