using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameSpaceInvaders
{
    class Shields
    {
        Texture2D image;
        Vector2 position;
        Color color;
        public int timesHit;

        public Rectangle Hitbox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height); }
        }

        public Shields(Texture2D image, Vector2 position)
        {
            this.image = image;
            this.position = position;
            color = Color.White;
            timesHit = 0;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, color);
        }

        public bool update(Rectangle bulletHitbox)
        {
            if(Hitbox.Intersects(bulletHitbox))
            {
                timesHit++;
            }
            if (timesHit >= 10)
            {
                return true;
            }
            return false;
        }
    }
}
