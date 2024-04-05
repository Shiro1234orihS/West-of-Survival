using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace SEA
{
    internal class ScreenGameOver : GameScreen
    {
        private Game1 _myGame;
        private Rectangle[] _lesBoutons;
        private Texture2D _textBoutons;
        

        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
        // défini dans Game1

        private Musique _musique;

        public ScreenGameOver(Game1 game) : base(game)
        {
           
            _myGame = game;
            _lesBoutons = new Rectangle[3];
            _lesBoutons[0] = new Rectangle(10, 400, 325, 200);
            _lesBoutons[1] = new Rectangle(400, 400, 300, 200);
           
            
        }
        public override void Initialize()
        {
            _myGame.SetWindowSize(700, 700);

            _musique = new Musique("Game_Over");

            base.Initialize();
        }
        public override void LoadContent()
        {
            _textBoutons = Content.Load<Texture2D>("gameover");

            _musique.LoadContent(_myGame);

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < _lesBoutons.Length; i++)
                {
                    // si le clic correspond à un des 3 boutons
                    if (_lesBoutons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        // on change l'état défini dans Game1 en fonction du bouton cliqué
                        if (i == 0)
                            _myGame._etat = Game1.Etats.Play;
                        else if (i == 1)
                            _myGame._etat = Game1.Etats.Quit;
                        break;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            Game1._spriteBatch.Begin();
            Game1._spriteBatch.Draw(_textBoutons, new Vector2(0, 0), Color.White);
            Game1._spriteBatch.End();
        }
    }
}
