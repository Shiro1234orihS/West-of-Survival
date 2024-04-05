using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace SEA
{
    public class ScreenOptions : GameScreen
    {
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
        // défini dans Game1
        private Game1 _myGame;
        private Texture2D _fond_option;

        private Musique _musique;

        public ScreenOptions(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void Initialize()
        {
            _myGame.SetWindowSize(600, 500);

            _musique = new Musique("Option");

            base.Initialize();
        }
        public override void LoadContent()
        {
            _fond_option = Content.Load<Texture2D>("Optionsecran");

            _musique.LoadContent(_myGame);

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
                _myGame._etat = Game1.Etats.Menu;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            Game1._spriteBatch.Begin();
            Game1._spriteBatch.Draw(_fond_option, new Vector2(0, 0), Color.White);
            Game1._spriteBatch.End();
        }
    }
}