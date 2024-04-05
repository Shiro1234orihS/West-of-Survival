using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace SEA
{
    public class ScreenMenu : GameScreen
    {
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
        // défini dans Game1
        private Game1 _myGame;

        // texture du menu avec 3 boutons
        private Texture2D _fond_menu;

        // contient les rectangles : position et taille des 3 boutons présents dans la texture 
        private Rectangle[] _lesBoutons;

        private Musique _musique;

        public ScreenMenu(Game1 game) : base(game)
        {
            _myGame = game;
            _lesBoutons = new Rectangle[3];
            _lesBoutons[0] = new Rectangle(350, 270, 110, 75);
            _lesBoutons[1] = new Rectangle(350, 380, 110, 75);
            _lesBoutons[2] = new Rectangle(350, 500, 110, 75);

        }
        public override void Initialize()
        {
            //change la taille de la fenetre pour chaque scene
            _myGame.SetWindowSize(790, 735);

            _musique = new Musique("Menu");

            base.Initialize();
        }
        public override void LoadContent()
        {
            _fond_menu = Content.Load<Texture2D>("ecranMenu");

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
                            _myGame._etat = Game1.Etats.Controls;
                        else
                            _myGame._etat = Game1.Etats.Quit;
                        break;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Orange);
            Game1._spriteBatch.Begin();
            Game1._spriteBatch.Draw(_fond_menu, new Vector2(0, 0), Color.White);
            Game1._spriteBatch.End();
        }
    }
}
