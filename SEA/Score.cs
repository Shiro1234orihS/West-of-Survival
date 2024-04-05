using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SEA
{
    internal class Score
    {
        public int _score;
        private int _temps; 
        public SpriteFont _police ;
        public Vector2 _position_score;


        public void Initialize()
        {
            _score = 0;
            _temps = 0; 
            _position_score = new Vector2(0,0);
        }
        public void LoadContent(Game game)
        {
            _police = game.Content.Load<SpriteFont>("Font");
        }
        public void Update(Joueur joueur , Game game)
        {
            _position_score.X = joueur._position_hero.X - 30 - Game1._graphics.PreferredBackBufferHeight / 4 ;
            _position_score.Y = joueur._position_hero.Y - Game1._graphics.PreferredBackBufferWidth / 10;

            _temps ++;

            if (_temps == 60)
            {
                _temps = 0;
                _score++;
            }

        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(_police, $"Score : {_score}", _position_score, Color.Black);
        }
    }
}
