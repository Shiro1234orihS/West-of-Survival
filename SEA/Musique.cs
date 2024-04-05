using Microsoft.Xna.Framework.Media;

namespace SEA
{
    internal class Musique
    {
        private Song _musique;
        private string _titre;

        public Musique(string titre)
        {
            this.Titre = titre;
        }

        public string Titre
        {
            get
            {
                return this._titre;
            }

            set
            {
                this._titre = value;
            }
        }

        public void LoadContent(Game1 game)
        {
            _musique = game.Content.Load<Song>(Titre);

            MediaPlayer.Play(_musique);
            MediaPlayer.IsRepeating = true;
        }

    }
}
