using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameSpaceInvaders
{
    class PlayerBullet
    {
        Texture2D image;
        public Vector2 position;
        public int speed;
        Color color;

        public Rectangle Hitbox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height); }
        }

        public PlayerBullet(Texture2D image, Vector2 position)
        {
            this.image = image;
            this.position = position;
            speed = 0;
            color = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, color);
        }

        public void update(List<Enemies> enemies)
        {
            speed = 4;
            for (int i = 0; i < enemies.Count; i++)
            {
                if(Hitbox.Intersects(enemies[i].Hitbox))
                {
                    enemies.Remove(enemies[i]);
                }
            }
            position.Y -= speed;
        }
        public void location(Vector2 playerPosition)
        {
            position.X = playerPosition.X + 10;
            position.Y = playerPosition.Y;
            speed = 0;
        }

       
    }
}
