using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;

namespace SEA
{
    internal class Ennemis
    {
       
        private AnimatedSprite _animation;


        private string _nom_fichier; 

        private string _nom_repos;
        private string _nom_animation_gauche;
        private string _nom_animation_droite;
        private string _nom_animation_bas;
        private string _nom_animation_hauts;

        private Vector2 _position;

        private Random _position_rdm;

        private int _sens_deplacement_DG;
        private int _sens_deplacement_HB;
        private int _vitesse_monstre;

     
        private bool _mort;
        private bool _gagne_score;

        private int _longueur_sprite;
        private int _largeur_sprite;

        private int _score_monstre;

        public Rectangle _collision;

        private int _vie_monstre;
        private int _nombre_mort;



        public Ennemis(String nom_fichier,String nom_repos, String nom_animation_gauche,String nom_animation_droite, String nom_animation_bas, String nom_animation_hauts,
            Int32 vie_monstre, Int32 vitesse_monstre , int longeur_sprite , int latgeur_sprite, int score_monste)
        {
            this.Nom_fichier = nom_fichier; 
            this.Nom_repos = nom_repos;
            this.Nom_animation_gauche = nom_animation_gauche;
            this.Nom_animation_droite = nom_animation_droite;
            this.Nom_animation_bas = nom_animation_bas;
            this.Nom_animation_hauts = nom_animation_hauts;
            this.Vie_monstre = vie_monstre;
            this.Vitesse_monstre = vitesse_monstre;
            this.Longeur_sprite = longeur_sprite;
            this.Latgeur_sprite = latgeur_sprite;
            this.Score_monste = score_monste;

        }

       

        public string Nom_animation_gauche
        {
            get
            {
                return _nom_animation_gauche;
            }

            set
            {
                _nom_animation_gauche = value;
            }
        }

        public string Nom_animation_droite
        {
            get
            {
                return _nom_animation_droite;
            }

            set
            {
                _nom_animation_droite = value;
            }
        }

        public string Nom_animation_bas
        {
            get
            {
                return _nom_animation_bas;
            }

            set
            {
                _nom_animation_bas = value;
            }
        }

        public string Nom_animation_hauts
        {
            get
            {
                return _nom_animation_hauts;
            }

            set
            {
                _nom_animation_hauts = value;
            }
        }

        public int Vie_monstre
        {
            get
            {
                return _vie_monstre;
            }

            set
            {
                _vie_monstre = value;
            }
        }

        public int Vitesse_monstre
        {
            get
            {
                return _vitesse_monstre;
            }

            set
            {
                _vitesse_monstre = value;
            }
        }

        public Int32 Longeur_sprite
        {
            get
            {
                return this._longueur_sprite;
            }

            set
            {
                this._longueur_sprite = value;
            }
        }

        public Int32 Latgeur_sprite
        {
            get
            {
                return this._largeur_sprite;
            }

            set
            {
                this._largeur_sprite = value;
            }
        }

        public Int32 Score_monste
        {
            get
            {
                return this._score_monstre;
            }

            set
            {
                this._score_monstre = value;
            }
        }

        public String Nom_fichier
        {
            get
            {
                return this._nom_fichier;
            }

            set
            {
                this._nom_fichier = value;
            }
        }

        public String Nom_repos
        {
            get
            {
                return this._nom_repos;
            }

            set
            {
                this._nom_repos = value;
            }
        }

        public void Initialize(Game game)
        {
            _position_rdm = new Random();
            _position.X = _position_rdm.Next(407, 1833);
            _position.Y = _position_rdm.Next(387, 1803);

            _collision = new Rectangle((int)_position.X, (int)_position.Y, _longueur_sprite / 2, _largeur_sprite / 2);


            _mort = false;
            _gagne_score = false;


        }
    
        public void LoadContent(Game game)
        {
            SpriteSheet spriteSheet_gauche = game.Content.Load<SpriteSheet>(Nom_fichier, new JsonContentLoader());
            _animation = new AnimatedSprite(spriteSheet_gauche);
     
        }
        public void Update(float deltatime, Joueur joueur , Score score , Maps _map)
        {
            for (int y = 0; y < MyScreen2.attaque_list.Count; y++)
            {
                if (MyScreen2.attaque_list[y]._collision_balle.Intersects(this._collision))
                {
                    this._vie_monstre--;
                    MyScreen2.attaque_list.RemoveAt(y);
                }
            }
            

            if (!_mort)
            {
                _sens_deplacement_DG = 0;
                _sens_deplacement_HB = 0;

                if (_nom_fichier == "CactusSheet.sf")
                {
                    _collision.X = (int)_position.X - 10;
                    _collision.Y = (int)_position.Y - 5;
                }
                else if (_nom_fichier == "Coyote_Sheet.sf")
                {
                    _collision.X = (int)_position.X - 25;
                    _collision.Y = (int)_position.Y - 18;
                }
                else 
                {
                    _collision.X = (int)_position.X - 25;
                    _collision.Y = (int)_position.Y - 18;
                }

               



                if (joueur._position_hero.Y == _position.Y && joueur._position_hero.X == _position.X)
                {
                    _animation.Play(Nom_repos);

                }

                if (joueur._position_hero.X == _position.X)
                {
                    _sens_deplacement_DG = 0;
                }
                else if (joueur._position_hero.X > _position.X)
                {
                    _sens_deplacement_DG = 1;
                    _animation.Play(Nom_animation_droite);

                    ushort tx = (ushort)(_position.X / _map._tiledMap.TileWidth + 0.5);
                    ushort ty = (ushort)((_position.Y + _longueur_sprite / 4) / _map._tiledMap.TileHeight);
                    if (IsCollision(tx, ty, _map))
                        _sens_deplacement_DG = 0;
                }
                else if (joueur._position_hero.X < _position.X)
                {
                    _sens_deplacement_DG = -1;
                    _animation.Play(Nom_animation_gauche);

                    ushort tx = (ushort)(_position.X / _map._tiledMap.TileWidth - 0.5);
                    ushort ty = (ushort)((_position.Y + _longueur_sprite / 4) / _map._tiledMap.TileHeight);
                    if (IsCollision(tx, ty, _map))
                        _sens_deplacement_DG = 0;
                }

                if (joueur._position_hero.Y == _position.Y)
                {
                    _sens_deplacement_HB = 0;
                }
                else if (joueur._position_hero.Y > _position.Y)
                {
                    _sens_deplacement_HB = 1;
                    _animation.Play(Nom_animation_bas);

                    ushort tx = (ushort)(_position.X / _map._tiledMap.TileWidth);
                    ushort ty = (ushort)((_position.Y + _largeur_sprite / 4) / _map._tiledMap.TileHeight + 0.3);
                    if (IsCollision(tx, ty, _map))
                        _sens_deplacement_HB = 0;
                }
                else if (joueur._position_hero.Y < _position.Y)
                {
                    _sens_deplacement_HB = -1;
                    _animation.Play(Nom_animation_hauts);

                    ushort tx = (ushort)(_position.X / _map._tiledMap.TileWidth);
                    ushort ty = (ushort)((_position.Y + _largeur_sprite / 4) / _map._tiledMap.TileHeight - 0.5);
                    if (IsCollision(tx, ty, _map))
                        _sens_deplacement_HB = 0;
                }

                _position.X += _sens_deplacement_DG * Vitesse_monstre * deltatime;
                _position.Y += _sens_deplacement_HB * Vitesse_monstre * deltatime;
                _position.X = (Single)Math.Round(_position.X);
                _position.Y = (Single)Math.Round(_position.Y);

                

                if (_vie_monstre == 0)
                {
                    _gagne_score = true;
                    _mort = true;
                }

                _animation.Update(deltatime);

            }
            else if (_mort && _gagne_score)
            {
                score._score = score._score + _score_monstre;
                _gagne_score=false;
                _nombre_mort++;

                if(_nom_fichier == "CactusSheet.sf")
                    _vie_monstre = 1;

                else if(_nom_fichier == "Coyote_Sheet.sf")
                    _vie_monstre = 3;

                else _vie_monstre=2;

                _position.X = _position_rdm.Next(407, 1833);
                _position.Y = _position_rdm.Next(387, 1803);
                _mort = false;
            }       
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            
            _spriteBatch.Draw(_animation, _position);
            
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

