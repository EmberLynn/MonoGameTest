using Microsoft.Xna.Framework;
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

        //add a font to our game
        private SpriteFont pixelfont;

        //let's count how long our seagull has been on the island
        private float timer = 0;
        private int secondsOnIsland = 0;

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
            seagull = Content.Load<Texture2D>("seagull");

            //load our font
            pixelfont = Content.Load<SpriteFont>("pixel");

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
            spriteBatch.Draw(seagull, new Vector2(400, 100), Color.White);
            spriteBatch.Draw(boat, new Vector2(0, 50), Color.White);

            //let's draw some text on our screen with pixelfont
            spriteBatch.DrawString(pixelfont, "This is a seagull alone on an island. ", new Vector2(390, 180), Color.Black);
            spriteBatch.DrawString(pixelfont, "Seconds he has been alone for: " + secondsOnIsland, new Vector2(390, 200), Color.Black);

            //finished using our spritebatch
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
