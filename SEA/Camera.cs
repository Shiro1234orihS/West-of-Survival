using Microsoft.Xna.Framework;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended;

namespace SEA
{
    internal class Camera
    {
        public OrthographicCamera _camera;
        private Vector2 _positionCamera;
        private int _vitesseCamera;

        public void Initialize(Game game)
        {
            var viewportAdapter = new BoxingViewportAdapter(game.Window, game.GraphicsDevice, 1280, 720);
            _camera = new OrthographicCamera(viewportAdapter);
            _camera.Position = new Vector2(1280, 720);
            _camera.ZoomIn(1.5f);
        }

        public void Update(GameTime gameTime,Joueur joueur)
        {
            _vitesseCamera = joueur._vitesse;
            _positionCamera = joueur._position_hero;
            _camera.LookAt(_positionCamera);
        }

        public void Draw(Maps _maps, Matrix? transformMatrix)
        {
            _maps._tiledMapRenderer.Draw(transformMatrix);
        }
    }
}
