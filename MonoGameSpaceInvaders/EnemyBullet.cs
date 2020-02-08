using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameSpaceInvaders
{
    class EnemyBullet
    {
        Texture2D image;
        public Vector2 position;
        public int speed;
        Color color;
        Random rand;
        int index;

        public Rectangle Hitbox
        {
            get { return new Rectangle((int)position.X - 10, (int)position.Y, image.Width - 10, image.Height); }
        }

        public EnemyBullet(Texture2D image)
        {
            this.image = image;
            position.X = 0;
            position.Y = 0;
            speed = 0;
            color = Color.White;
            rand = new Random();
            index = 0;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, color);
        }

        public void location(List<Enemies> enemie)
        {
            index = rand.Next(0, enemie.Count);
            position.X = enemie[index].position.X;
            position.Y = enemie[index].position.Y;
            speed = 0;
        }

        public void move(Viewport viewport)
        {
            speed = 4;
            position.Y += speed;
        }
    }
}
