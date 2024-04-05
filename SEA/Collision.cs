
namespace SEA
{
    internal class Collision
    {
        private bool _accepte_collision;
        private int _time_collision;
        
        public void Initialize()
        {
            _accepte_collision = false;
            _time_collision = 0;
        }
        public void Update(Ennemis ennemis, Joueur joueur)
        {
            if(joueur._code_triche == false)
            {
                if (joueur._collision_hero.Intersects(ennemis._collision) && _accepte_collision)
                {

                    joueur._vie--;
                    _accepte_collision = false;
                }
            }
           

            if (_accepte_collision == false)
            {
                _time_collision++;
                if (_time_collision == 180 * 24)
                {
                    _time_collision = 0;
                    _accepte_collision = true;
                }
            }
        }
    }
}
