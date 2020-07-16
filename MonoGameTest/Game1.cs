﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        //need this for our sprites
        SpriteBatch spriteBatch;

        //Texture2D objects to hold our images in
        private Texture2D background;
        private Texture2D boat;
        private Texture2D seagull;
        private Texture2D spider;
        //current angle of the spider
        private float spiderAngle;

        //add a font to our game
        private SpriteFont pixelfont;

        //let's count how long our seagull has been on the island
        private float timer = 0;
        private int secondsOnIsland = 0;

        //let's animate the seagull
        private AnimateSprite animateSprite;
        //time the animation
        private float animationTimer;

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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //this contains methods for drawing sprites onto the screen
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //load our images from contents
            //refer specifically to the name of the file in contents
            background = Content.Load<Texture2D>("simple-island");
            boat = Content.Load<Texture2D>("small-boat");
            spider = Content.Load<Texture2D>("spider");

            //load our font
            pixelfont = Content.Load<SpriteFont>("pixel");

            //load the seagull and animate it
            seagull = Content.Load<Texture2D>("seagullatlas");
            animateSprite = new AnimateSprite(seagull, 1, 2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

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

            //timer that shows us how long the seagull has been on the island for
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //I only want to show the seconds
            secondsOnIsland = (int)timer % 60;

            //make the seagull move and delay his animation
            animationTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (animationTimer > 200)
            {
                animateSprite.Update();
                animationTimer = 0;
            }

            //change the angle of our spider
            spiderAngle += 0.01f;
    
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
         
            // TODO: Add your drawing code here

            //let's start using out spritebatch
            spriteBatch.Begin();

            //let's draw from out spritebatch

            //draw our background as a rectangle at specified position, width, and height
            //last argument is tint color where white won't tint at all
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 500), Color.White);

            //sprites stack when drawn
            //can draw off the screen, so keep coordinates in mind
            spriteBatch.Draw(boat, new Vector2(0, 50), Color.White);

            //let's draw the rotating spider
            //where we want the spider to render on screen
            Vector2 location = new Vector2(-20, 100);
            //the part of the image we want to render
            Rectangle sourceRectangle = new Rectangle(0, 0, spider.Width, spider.Height);
            //the point we want the image to rotate on
            Vector2 origin = new Vector2(0, 0);

            //the texture we want to draw, where we want to draw it, what we want to draw, tint color, current rotation angle, point of rotation, scale factor 
            //(in this case we don't want it to change, so it is set to 1), possible effects on sprite, depth of sprite
            spriteBatch.Draw(spider, location, sourceRectangle, Color.White, spiderAngle, origin, 1f, SpriteEffects.None, 1);

            //draw the animated seagull
            animateSprite.Draw(spriteBatch, new Vector2(450, 250));

            //let's draw some text on our screen with pixelfont
            spriteBatch.DrawString(pixelfont, "This is a seagull alone on an island. ", new Vector2(390, 180), Color.Black);
            spriteBatch.DrawString(pixelfont, "Seconds he has been alone for: " + secondsOnIsland, new Vector2(390, 200), Color.Black);

            //finished using our spritebatch
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
