using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace SEA
{
    internal class MyScreen2 : GameScreen
    {
        private Game1 _myGame;
        private Joueur _joueur;
        private Maps _maps_1;
        private Camera _camera;
        private Score _score;
        private Time _time;
        private Collision _collision;
        private Vies _vies;
        private Musique _musique;

        private KeyboardState _keyboardStateREF = Keyboard.GetState();

        public static List<Ennemis> ennemis_list;
        public static List<Boule_Feu> attaque_list;

        private int _chrono_mort;

        public MyScreen2(Game1 game) : base(game)
        {
            _myGame = game;
           
        }
        public override void Initialize()
        {
            _myGame.SetWindowSize(1440, 900);

            _joueur = new Joueur();
            _maps_1 = new Maps("Main_map_2");
            _camera = new Camera();
            _score = new Score();
            _time = new Time();
            ennemis_list = new List<Ennemis>();
            attaque_list = new List<Boule_Feu>();
            _musique = new Musique("Jeu");
            _collision = new Collision();
            _vies = new Vies();



            for (int i = 0; i < 8; i++)
            {
                ennemis_list.Add(new Ennemis("CactusSheet.sf", "repos", "deplacement_gauche", "deplacement_droite", "deplacement_bas",
                "deplacement_hauts", 1, 80,40,40, 100));
                ennemis_list.Add(new Ennemis("Coyote_Sheet.sf", "repos", "deplacement_gauche", "deplacement_droite", "deplacement_bas",
                "deplacement_hauts", 3, 50, 60, 60, 300));
                ennemis_list.Add(new Ennemis("Coffin_Sheet.sf", "repos", "deplacement_gauche", "deplacement_droite", "deplacement_bas",
                 "deplacement_hauts", 2, 65, 65, 60, 200));
            }

            _joueur.Initialize();
            _maps_1.Initialize(_myGame);
            _camera.Initialize(_myGame);
            _score.Initialize();
            _time.Initialize();
            _collision.Initialize();
            _vies.Initialize(_joueur);

            foreach(Ennemis ennemis in ennemis_list)
            {
                ennemis.Initialize(_myGame);
            }
        }
        public override void LoadContent()
        {
            _joueur.LoadContenent(_myGame);
            _maps_1.LoadContent(_myGame);
            _score.LoadContent(_myGame);
            _time.LoadContent(_myGame);
            _musique.LoadContent(_myGame);
            _vies.LoadContent(_myGame);

            foreach (Ennemis ennemis in ennemis_list)
            {
                ennemis.LoadContent(_myGame);
            }

             Boule_Feu.LoadContent(_myGame);

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
         
            if (_myGame._keyboardState.IsKeyDown(Keys.Right) && _keyboardStateREF.IsKeyUp(Keys.Right))
            {
                attaque_list.Add(new Boule_Feu("tir_droite", new Vector2(_joueur._position_hero.X + 20, _joueur._position_hero.Y), 1, 0));
            }
            if (_myGame._keyboardState.IsKeyDown(Keys.Left) && _keyboardStateREF.IsKeyUp(Keys.Left))
            {
                attaque_list.Add(new Boule_Feu("tir_gauche", new Vector2(_joueur._position_hero.X -20, _joueur._position_hero.Y), -1, 0));

            }
            if (_myGame._keyboardState.IsKeyDown(Keys.Up) && _keyboardStateREF.IsKeyUp(Keys.Up))
            {
                attaque_list.Add(new Boule_Feu("tir_hauts", new Vector2(_joueur._position_hero.X , _joueur._position_hero.Y - 20), 0, -1));

            }
            if (_myGame._keyboardState.IsKeyDown(Keys.Down) && _keyboardStateREF.IsKeyUp(Keys.Down))
            {
                attaque_list.Add(new Boule_Feu("tir_bas", new Vector2(_joueur._position_hero.X , _joueur._position_hero.Y+20), 0, 1));
            }
            _keyboardStateREF = _myGame._keyboardState;

            foreach (Ennemis ennemis in ennemis_list)
            {
                _collision.Update(ennemis, _joueur);
                ennemis.Update(_myGame.deltaTime, _joueur, _score, _maps_1);
                if (ennemis.Vie_monstre == 0)
                    Dispose();
            }

            foreach (Boule_Feu attaque in attaque_list)
            {
                attaque.Uptade(_myGame.deltaTime);
            }


            _joueur.Update(_myGame.deltaTime, _myGame._keyboardState, _maps_1);
            _maps_1.Update(gameTime);
            _camera.Update(gameTime, _joueur);
            _score.Update(_joueur , _myGame);
            _time.Update(_joueur,_myGame);
            _vies.Update(_joueur);

            
            if (_joueur._vie <=0 )
            {
                _chrono_mort++;
                if(_chrono_mort == 55)
                {
                    _myGame._etat = Game1.Etats.GameOver;
                    _myGame._mort = true;
                    _chrono_mort = 0;
                }  
            }
        }
        public override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            var transformMatrix = _camera._camera.GetViewMatrix();

            // Game1 pour chnager le graphisme
            Game1._spriteBatch.Begin(transformMatrix: transformMatrix);
            _camera.Draw(_maps_1, transformMatrix);
            _joueur.Draw(Game1._spriteBatch);
            _score.Draw(Game1._spriteBatch);
            _time.Draw(Game1._spriteBatch);
            if(!(_joueur._vie <= 0))
            {
                foreach (Ennemis ennemis in ennemis_list)
                {
                    ennemis.Draw(Game1._spriteBatch);
                }

            }
            foreach (Boule_Feu attaque in attaque_list)
                attaque.Draw(Game1._spriteBatch);

            _vies.draw(Game1._spriteBatch, _joueur);
            Game1._spriteBatch.End();
        }
    }
}
