using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SEA
{
    internal class Vies
    {
        private Texture2D[] _vie;
        private Vector2[] _position;

        public void Initialize(Joueur joueur)
        {
            _vie = new Texture2D[5];
            _position = new Vector2[5];

            for (int i = 0; i < _position.Length; i++)
            {
                _position[i] = new Vector2(joueur._position_hero.X + Game1._graphics.PreferredBackBufferHeight / 4 - (30*i), joueur._position_hero.Y - Game1._graphics.PreferredBackBufferWidth / 10 ) ;
            }
        }
        public void LoadContent(Game game )
        {
            for (int i = 0; i < 5; i++)
            {
                _vie[i] = game.Content.Load<Texture2D>("heart");
            }
        }
        public void Update(Joueur joueur)
        {
            for (int i = 0; i < _position.Length; i++)
            {
                _position[i] = new Vector2(joueur._position_hero.X + Game1._graphics.PreferredBackBufferHeight / 4 - (30 * i), joueur._position_hero.Y - Game1._graphics.PreferredBackBufferWidth / 10);
            }
        }

        public void draw( SpriteBatch _spriteBatch , Joueur joueur)
        {
            for(int i = 0; i < joueur._vie ; i++)
                _spriteBatch.Draw(_vie[i], _position[i], Color.White);
        }
    }
}
