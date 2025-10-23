using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pacman
{
    internal class Tile
    {
        private Vector2 _tilePos;
        private Color _tileColor;
        private Texture2D _tileTexture;
        private string _tileType;
        private Rectangle _tileBox;
        public Tile()
        { }

        public Tile(Texture2D texture, Vector2 position, Color color, string type)
        {
            _tilePos = position;
            _tileColor = color;
            _tileTexture = texture;
            _tileType = type;
            _tileBox = new Microsoft.Xna.Framework.Rectangle((int)_tilePos.X, (int)_tilePos.Y, _tileTexture.Width, _tileTexture.Height);
        }

        public Texture2D Texture
        {
            get { return _tileTexture; }
            set { _tileTexture = value; }
        }

        public string Type
        {
            get { return _tileType; }
            set { _tileType = value; }
        }


        public Vector2 Position
        {
            get { return _tilePos; }
            set { _tilePos = value; }
        }

        public Color Color
        {
            get { return _tileColor; }
            set { _tileColor = value; }
        }

        public Rectangle TileBox
        {
            get { return _tileBox; }
            set { _tileBox = value; }
        }

        public void DrawTile(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color);
        }


    }
}
