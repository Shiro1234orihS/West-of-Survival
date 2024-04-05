using MonoGame.Extended.Sprites;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;


namespace SEA
{
    internal class Boule_Feu
    {
        private const int VITESSE = 200;
        private const int TAILLE_SPRITE = 50;
        public static AnimatedSprite _balle;

        private String _animation;

        public Vector2 _position;

        public Rectangle _collision_balle;

        private int _sens_deplacement_DG;
        private int _sens_deplacement_HB;
        private static int _vitesse;

        public Boule_Feu(String animation, Vector2 position, Int32 sens_deplacement_DG, Int32 sens_deplacement_HB)
        {
            this.Animation = animation;
            this.Position = position;
            this.Sens_deplacement_DG = sens_deplacement_DG;
            this.Sens_deplacement_HB = sens_deplacement_HB;

        }

        public String Animation
        {
            get
            {
                return this._animation;
            }

            set
            {
                this._animation = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this._position;
            }

            set
            {
                this._position = value;
            }
        }

        public Int32 Sens_deplacement_DG
        {
            get
            {
                return this._sens_deplacement_DG;
            }

            set
            {
                this._sens_deplacement_DG = value;
            }
        }

        public Int32 Sens_deplacement_HB
        {
            get
            {
                return this._sens_deplacement_HB;
            }

            set
            {
                this._sens_deplacement_HB = value;
            }
        }

        public void In()
        {
            
            _collision_balle = new Rectangle((int)_position.X, (int)_position.Y, TAILLE_SPRITE / 2, TAILLE_SPRITE / 2);

        }

        public  static void LoadContent(Game game)
        {
            SpriteSheet spriteSheet = game.Content.Load<SpriteSheet>("balle_doite.sf", new JsonContentLoader());
            _balle = new AnimatedSprite(spriteSheet);
           
        }
        public void Uptade(float deltaTime)
        {
            _collision_balle.X = (Int32)_position.X;
            _collision_balle.Y = (Int32)_position.Y;

            _balle.Play(Animation);
            _balle.Update(deltaTime);

            _position.X += Sens_deplacement_DG * VITESSE * deltaTime;
            _position.Y += Sens_deplacement_HB * VITESSE * deltaTime;
        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_balle, _position);
        }
    }
}
