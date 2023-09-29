using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameProject2.StateManagement;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Audio;


namespace GameProject2.Screens
{
    // This screen implements the actual game logic. It is just a
    // placeholder to get the idea across: you'll probably want to
    // put some more interesting gameplay in here!
    public class GameplayScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;

        private Warrior warrior;
        private Skeleton[] skeletons;

        private readonly Random _random = new Random();

        private SoundEffect skeletonHurt;

        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        // Load graphics content for the game
        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _gameFont = _content.Load<SpriteFont>("BlackOpsOne-Regular");

            System.Random rand = new System.Random();
            skeletons = new Skeleton[]
            {
                new Skeleton(new Vector2((float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Height)),
                new Skeleton(new Vector2((float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Height)),
                new Skeleton(new Vector2((float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Height)),
                new Skeleton(new Vector2((float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Height)),
                new Skeleton(new Vector2((float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Height)),
                new Skeleton(new Vector2((float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Height))
            };
            warrior = new Warrior();

            foreach (var skeleton in skeletons) skeleton.LoadContent(_content);
            warrior.LoadContent(_content);

            skeletonHurt = _content.Load<SoundEffect>("Hit_Hurt");

            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            _content.Unload();
        }

        // This method checks the GameScreen.IsActive property, so the game will
        // stop updating when the pause menu is active, or if you tab away to a different application.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            if (IsActive)
            {
                // TODO: Add your update logic here
                warrior.Update(gameTime);
                foreach (var skeleton in skeletons)
                {
                    if (skeleton.Alive && skeleton.Bounds.CollidesWith(warrior.Bounds) && warrior.action == Action.Slash)
                    {
                        skeleton.Alive = false;
                        skeletonHurt.Play();
                    }
                }
            }
        }

        // Unlike the Update method, this will only be called when the gameplay screen is active.
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            var keyboardState = input.CurrentKeyboardStates[playerIndex];
            var gamePadState = input.CurrentGamePadStates[playerIndex];

        }

        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 0, 0);

            // Our player and enemy are both actually just text strings.
            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            foreach (var skeleton in skeletons) skeleton.Draw(gameTime, spriteBatch);
            warrior.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
