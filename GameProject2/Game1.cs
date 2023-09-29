using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameProject2.Screens;
using GameProject2.StateManagement;
using Microsoft.Xna.Framework.Media;


namespace GameProject2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;

        private Song backgroundMusic;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            AddInitialScreens();
        }

        private void AddInitialScreens()
        {
            _screenManager.AddScreen(new MainMenuScreen(), null);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent() 
        {
            backgroundMusic = Content.Load<Song>("Strobotone - Medieval Theme 02");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}