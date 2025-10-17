using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace pacman
{
    internal class Player : Sprite
    {
        public Player()
        { }

        public Player(Texture2D texture, Vector2 position, Microsoft.Xna.Framework.Color color)
            :base (texture, position, color)
        {

        }

        public void Movement(Vector2 velocity)
        {
        }
    }
}
