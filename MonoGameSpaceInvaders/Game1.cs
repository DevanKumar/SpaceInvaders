using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoGameSpaceInvaders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            //this.Window.Title = $"Lives left: {player.lives}";
            graphics.PreferredBackBufferWidth = 450;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        Player player;
        Texture2D playerImage;
        List<Texture2D> enemies = new List<Texture2D>();
        Texture2D shieldImages;
        Texture2D invaderImage;
        Texture2D playerBulletImage;
        bool shot = false;
        List<Enemies> enemie = new List<Enemies>();
        List<PlayerBullet> playerBullet = new List<PlayerBullet>();
        List<Shields> shields = new List<Shields>();
        List<EnemyBullet> enemieBullets = new List<EnemyBullet>();
        bool didShoot = false;
        long time = 0;
        bool gamefinished = false;
        Texture2D enemieBulletImage;
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            playerImage = Content.Load<Texture2D>("player");

            shieldImages = Content.Load<Texture2D>("shield1");

            player = new Player(playerImage, new Vector2(10, 525), 3);

            shields.Add(new Shields(shieldImages, new Vector2(15, 450)));
            shields.Add(new Shields(shieldImages, new Vector2(125, 450)));
            shields.Add(new Shields(shieldImages, new Vector2(235, 450)));
            shields.Add(new Shields(shieldImages, new Vector2(345, 450)));

            invaderImage = Content.Load<Texture2D>("enemy2");

            playerBulletImage = Content.Load<Texture2D>("bullets");
            enemieBulletImage = Content.Load<Texture2D>("bullets");

            playerBullet.Add(new PlayerBullet(playerBulletImage, new Vector2(10000, 10000)));
            

            int xPos;
            int enemieWidth = GraphicsDevice.Viewport.Width / 10;
            int enemieHeight = 40;
            int enemieGap = 15;
            
            for (int x = 0; x < 10; x++)
            {
                xPos = x * enemieWidth;
                for (int y = 0; y < 5; y++)
                {
                     enemie.Add(new Enemies(invaderImage, new Vector2((xPos + enemieGap) / 2, y * (enemieHeight + enemieGap) / 2)));
                }
            }

            // TODO: use this.Content to load your game content here
        }

        

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        KeyboardState lastks = Keyboard.GetState();
        int delay = 1250;
        int delay2 = 1200;
        bool didEnemyShoot = false;
       
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState ks = Keyboard.GetState();
            player.move(GraphicsDevice.Viewport, ks);


            if (!gamefinished)
            {
                if (didShoot == false && shot == false)
                {
                    if (ks.IsKeyDown(Keys.Space) && !lastks.IsKeyDown(Keys.Space))
                    {
                        shot = true;
                    }
                }
                if (shot)
                {
                    shot = false;
                    didShoot = true;

                    playerBullet[0].location(player.position);
                }
                if (didShoot)
                {
                    playerBullet[playerBullet.Count - 1].update(enemie);
                    if (playerBullet[0].position.Y < 0)
                    {
                        playerBullet[0].location(player.position);
                        didShoot = false;
                    }
                    for (int i = 0; i < enemie.Count; i++)
                    {
                        if (playerBullet[0].Hitbox.Intersects(enemie[i].Hitbox))
                        {
                            playerBullet[0].position = player.position;
                            playerBullet[0].speed = 0;
                            enemie.Remove(enemie[i]);
                            didShoot = false;
                            break;
                        }
                    }
                    for (int i = 0; i < shields.Count; i++)
                    {
                        if (shields[i].update(playerBullet[0].Hitbox))
                        {
                            playerBullet[0].position = player.position;
                            playerBullet[0].speed = 0;
                            didShoot = false;
                            break;
                        }
                    }
                }
                time += gameTime.ElapsedGameTime.Milliseconds;



                if (time > delay)
                {
                    for (int i = 0; i < enemie.Count; i++)
                    {
                        enemie[i].update(GraphicsDevice.Viewport);
                    }
                    for (int i = 0; i < enemie.Count; i++)
                    {
                        if (enemie[i].checkWall(GraphicsDevice.Viewport))
                        {
                            for (int j = 0; j < enemie.Count; j++)
                            {
                                enemie[j].position.Y += 20;
                                if (enemie[i].position.X <= 0)
                                {
                                    enemie[j].direction = 1;
                                }
                                else
                                {
                                    enemie[j].direction = -1;
                                }
                            }
                            break;
                        }
                    }


                    time = 0;
                }
                if (time > delay2)
                {
                    enemieBullets.Add(new EnemyBullet(enemieBulletImage));
                    if (didEnemyShoot == false)
                    {
                        enemieBullets[enemieBullets.Count - 1].location(enemie);
                    }
                    didEnemyShoot = true;
                }

                for (int i = 0; i < enemieBullets.Count; i++)
                {
                    enemieBullets[i].move(GraphicsDevice.Viewport);
                }
                int[] shieldHitCounter = new int[shields.Count];
                for (int i = 0; i < shields.Count; i++)
                {
                    shieldHitCounter[i] = shields[i].timesHit;
                }

                for (int j = 0; j < enemieBullets.Count - 1; j++)
                {
                    for (int i = 0; i < shields.Count; i++)
                    {
                        if (shields[i].update(enemieBullets[j].Hitbox))
                        {
                            shields.Remove(shields[i]);
                            enemieBullets.Remove(enemieBullets[j]);
                            break;
                        }
                        if (shieldHitCounter[i] < shields[i].timesHit)
                        {
                            enemieBullets.Remove(enemieBullets[j]);
                        }

                    }
                    if (enemieBullets[j].position.Y > GraphicsDevice.Viewport.Y)
                    {
                        didEnemyShoot = false;
                    }
                }
                lastks = ks;
                base.Update(gameTime);
            }
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (!gamefinished)
            {
                if (didShoot)
                {
                    playerBullet[0].Draw(spriteBatch);
                }
                player.draw(spriteBatch);
                for (int i = 0; i < shields.Count; i++)
                {
                    shields[i].draw(spriteBatch);
                }
                for (int i = 0; i < enemie.Count; i++)
                {
                    enemie[i].draw(spriteBatch);
                }
                for (int i = 0; i < enemieBullets.Count; i++)
                {
                    enemieBullets[i].draw(spriteBatch);
                }
                this.Window.Title = $"Lives left: {player.lives}";
            }
            if (enemie.Count == 0)
            {
                spriteBatch.DrawString(font, "Winner!!", Vector2.Zero, Color.Black);
                gamefinished = true;
            }
            if (player.gameover(enemieBullets))
            {
                spriteBatch.DrawString(font, "Game Over", Vector2.Zero, Color.Black);
                gamefinished = true;
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
