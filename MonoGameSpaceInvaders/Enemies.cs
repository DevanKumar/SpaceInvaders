using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameSpaceInvaders
{
    class Enemies
    {
        Texture2D image;
        public Vector2 position;
        Color color;
        public int direction = 1;

        public Rectangle Hitbox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height); }
        }

        public Enemies(Texture2D image, Vector2 position)
        {
            this.image = image;
            this.position = position;
            color = Color.White;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, color);
        }

        public bool checkWall(Viewport viewport)
        {
            if (position.X + (image.Width) >= viewport.Width)
            {
                return true;
            }
            if (position.X <= 0)
            {
                return true;
            }
            return false;
        }

        public void update(Viewport viewport)
        {
            position.X += (image.Width) * direction;
        }
    }
}
