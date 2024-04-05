using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace SEA
{
    public class Game1 : Game
    {
        public enum Etats { Menu, Controls, Play, Quit, GameOver };
        public Etats _etat;


        public static GraphicsDeviceManager _graphics { get; set; }

        public static SpriteBatch _spriteBatch { get; set; }

        private readonly ScreenManager _screenManager;
        private ScreenMenu _screenMenu;
        private ScreenOptions _screenOptions;
        private MyScreen2 _ScreenPlay;
        private ScreenGameOver _screenGameOver;

        public float deltaTime;

        public KeyboardState _keyboardState;

        internal static Game1 _myGame;

        public bool _mort;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            // Par défaut, le 1er état flèche l'écran de menu
            _etat = Etats.Menu;

            // on charge les 2 écrans 
            _screenMenu = new ScreenMenu(this);
            _ScreenPlay = new MyScreen2(this);
            _screenOptions = new ScreenOptions(this);
            _screenGameOver = new ScreenGameOver(this);

        }

        // Méthode appelée une seule fois au démarrage du jeu
        protected override void Initialize()
        {
            SetWindowSize(1000, 1000);
            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // on charge l'écran de menu par défaut 
            _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));

            _screenMenu = new ScreenMenu(this); // en leur donnant une référence au Game
            _screenOptions = new ScreenOptions(this);
            _ScreenPlay = new MyScreen2(this);
            _screenGameOver = new ScreenGameOver(this);
        }


        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // On teste le clic de souris et l'état pour savoir quelle action faire 
            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {

                // Attention, l'état a été mis à jour directement par l'écran en question
                if (_etat == Etats.Quit)
                {
                    Exit();
                }


                else if (_etat == Etats.Play)
                {
                    _screenManager.LoadScreen(_ScreenPlay, new FadeTransition(GraphicsDevice, Color.Black));
                }


                else if (_etat == Etats.Controls)
                {
                    _screenManager.LoadScreen(_screenOptions, new FadeTransition(GraphicsDevice, Color.Black));
                }
            }


          
            if (_etat == Etats.GameOver && _mort)
            {
                _screenManager.LoadScreen(_screenGameOver, new FadeTransition(GraphicsDevice, Color.Black));
                _mort = false;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                if (_etat == Etats.Menu)
                    _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
            }

            if (_keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }

        public void SetWindowSize(int w, int h)
        {
            //Change la taille de fenêtre pour chaque scène
            _graphics.PreferredBackBufferWidth = w;
            _graphics.PreferredBackBufferHeight = h;
            _graphics.ApplyChanges();
        }
    }
}