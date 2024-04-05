using MonoGame.Extended.Sprites;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;

namespace SEA
{
    internal class Joueur
    {
        private const int TAILLE_SPRITE = 40;

        private AnimatedSprite _animation_perso;
        

        public Vector2 _position_hero;

        private int _sens_deplacement_DG;
        private int _sens_deplacement_HB;
        public int _vitesse;

        private string _animation;

        public Rectangle _collision_hero;
        public int _vie;

        public bool _code_triche;

        public bool _animation_mort;
       

        public void Initialize()
        {
            _position_hero = new Vector2(1075, 1125);
            _sens_deplacement_DG = 0;
            _sens_deplacement_HB = 0;
            _vitesse = 100;

            _collision_hero = new Rectangle((int)_position_hero.X, (int)_position_hero.Y, TAILLE_SPRITE / 2, TAILLE_SPRITE / 2);
            _vie = 5;
            _animation = "repos";

           
        }
        public void LoadContenent(Game game)
        {
            SpriteSheet spriteSheet_droite = game.Content.Load<SpriteSheet>("sprite.sf", new JsonContentLoader());

            _animation_perso = new AnimatedSprite(spriteSheet_droite);
  
        }
        public void Update(float deltaTime, KeyboardState _keyboardState, Maps _map )
        {
            _animation = "repos";

            _sens_deplacement_DG = 0;
            _sens_deplacement_HB = 0;

            _collision_hero.X = (int)_position_hero.X;
            _collision_hero.Y = (int)_position_hero.Y;

            if (_keyboardState.IsKeyDown(Keys.D) && !(_keyboardState.IsKeyDown(Keys.Q)))
            {
                _sens_deplacement_DG = 1;
                _animation = "deplacement_droite";

                ushort tx = (ushort)(_position_hero.X / _map._tiledMap.TileWidth + 0.5);
                ushort ty = (ushort)((_position_hero.Y + TAILLE_SPRITE / 2) / _map._tiledMap.TileHeight);
                if (IsCollision(tx, ty, _map))
                    _sens_deplacement_DG = 0;
            }
            if (_keyboardState.IsKeyDown(Keys.Q) && !(_keyboardState.IsKeyDown(Keys.D)))
            {
                _sens_deplacement_DG = -1;
                _animation = "deplacement_gauche";

                ushort tx = (ushort)(_position_hero.X / _map._tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)((_position_hero.Y + TAILLE_SPRITE / 2) / _map._tiledMap.TileHeight);
                if (IsCollision(tx, ty, _map))
                    _sens_deplacement_DG = 0;
            }
            if (_keyboardState.IsKeyDown(Keys.Z) && !(_keyboardState.IsKeyDown(Keys.S)))
            {
                _sens_deplacement_HB = -1;
                _animation = "deplacement_hauts";

                ushort tx = (ushort)(_position_hero.X / _map._tiledMap.TileWidth);
                ushort ty = (ushort)((_position_hero.Y + TAILLE_SPRITE / 2) / _map._tiledMap.TileHeight - 0.5);
                if (IsCollision(tx, ty, _map))
                    _sens_deplacement_HB = 0;
            }
            if (_keyboardState.IsKeyDown(Keys.S) && !(_keyboardState.IsKeyDown(Keys.Z)))
            {
                _sens_deplacement_HB = 1;
                _animation = "deplacement_bas";

                ushort tx = (ushort)(_position_hero.X / _map._tiledMap.TileWidth);
                ushort ty = (ushort)((_position_hero.Y + TAILLE_SPRITE / 2) / _map._tiledMap.TileHeight + 0.1);
                if (IsCollision(tx, ty, _map))
                    _sens_deplacement_HB = 0;
            }

           if(_keyboardState.IsKeyDown(Keys.S) && _keyboardState.IsKeyDown(Keys.Q))
           {
               _animation = "deplacement_diag_GB";
           }
           else if(_keyboardState.IsKeyDown(Keys.S) && _keyboardState.IsKeyDown(Keys.D))
           {
                _animation = "deplacement_diag_BD";
           }

           if (_keyboardState.IsKeyDown(Keys.Z) && _keyboardState.IsKeyDown(Keys.Q))
           {
               _animation = "deplacement_diag_GH";
           }
           else if (_keyboardState.IsKeyDown(Keys.Z) && _keyboardState.IsKeyDown(Keys.D))
           {
               _animation = "deplacement_diago_DH";
           }



            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                _animation = "tir_droite";
                _sens_deplacement_DG = 0;
                _sens_deplacement_HB = 0;
            }
            else if (_keyboardState.IsKeyDown(Keys.Left))
            {
                _animation = "tir_gauche";
                _sens_deplacement_DG = 0;
                _sens_deplacement_HB = 0;
            }

            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                _animation = "tir_hauts";
                _sens_deplacement_DG = 0;
                _sens_deplacement_HB = 0;
            }
            else if (_keyboardState.IsKeyDown(Keys.Down))
            {
                _animation = "tir_bas";
                _sens_deplacement_DG = 0;
                _sens_deplacement_HB = 0;
            }
            
            if(_vie == 0 )
            {
                _animation = "morts";
            }
            if (_keyboardState.IsKeyDown(Keys.Space))
                _code_triche = true;



           _position_hero.X += _sens_deplacement_DG * _vitesse * deltaTime;
           _position_hero.Y += _sens_deplacement_HB * _vitesse * deltaTime;
           _position_hero.X = (Single)Math.Round(_position_hero.X); // On arrondi pour eviter un bug sur la traque des ennemis 
           _position_hero.Y = (Single)Math.Round(_position_hero.Y); // On arrondi pour eviter un bug sur la traque des ennemis



            _animation_perso.Play(_animation);
           _animation_perso.Update(deltaTime);
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_animation_perso, _position_hero);
        }

        private bool IsCollision(ushort x, ushort y, Maps _map)
        {
            TiledMapTile? tile;
            if (!_map._mapLayer.TryGetTile(x, y, out tile))
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
    }
}
