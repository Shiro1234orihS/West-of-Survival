using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SEA
{
    internal class Time
    {
        public int _time;
        private int _chrono;
        public SpriteFont _police;
        private Vector2 _positionTimes;


        public void Initialize()
        {
            _time = 0;
            _chrono = 0;
            _positionTimes = new Vector2(0, 0);
        }
        public void LoadContent(Game game)
        {
            _police = game.Content.Load<SpriteFont>("Font");
        }
        public void Update(Joueur joueur, Game game)
        {
            _positionTimes.X = joueur._position_hero.X - 30 - Game1._graphics.PreferredBackBufferHeight / 4;
            _positionTimes.Y = joueur._position_hero.Y - Game1._graphics.PreferredBackBufferWidth / 10 +20;

            _chrono++;

            if (_chrono == 60)
            {
                _chrono = 0;
                _time++;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(_police, $"Temps : {_time}", _positionTimes, Color.Black);
        }
    }
}
