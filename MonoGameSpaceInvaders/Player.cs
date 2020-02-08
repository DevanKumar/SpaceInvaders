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
    class Player
    {
        Texture2D image;
        public Vector2 position;
        int speed;
        Color color;
        public int lives;

        public Rectangle Hitbox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height); }
        }

        public Player(Texture2D image, Vector2 position, int speed)
        {
            this.image = image;
            this.position = position;
            this.speed = speed;
            color = Color.White;
            lives = 3;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, color);
        }

        public void move(Viewport viewport, KeyboardState ks)
        {
            
            if(ks.IsKeyDown(Keys.Left))
            {
                position.X -= speed;
            }
            else if(ks.IsKeyDown(Keys.Right))
            {
                position.X += speed;
            }
        }

        public bool gameover(List<EnemyBullet> bulletHitbox)
        {
            for (int i = 0; i < bulletHitbox.Count; i++)
            {
                if (Hitbox.Intersects(bulletHitbox[i].Hitbox))
                {
                    lives--;
                    bulletHitbox.Remove(bulletHitbox[i]);
                }
                if(lives <= 0)
                {
                    return true;
                }
            }
            return false;
            
        }

    }
}
