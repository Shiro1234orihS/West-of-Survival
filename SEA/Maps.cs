using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SEA
{
    internal class Maps
    {
        private string _map;
        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;
        public TiledMapTileLayer _mapLayer;

        public Matrix _tileMapMatrix;
        public const float SCALE = 2;
       

        public Maps(String map)
        {
            this.Map = map;
        }

        public string Map
        {
            get
            {
                return _map;
            }

            set
            {
                _map = value;
            }
        }

        public void Initialize(Game game)
        {
            game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
        }
        public void LoadContent(Game game)
        {
            _tiledMap = game.Content.Load<TiledMap>(Map);
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);

            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Maison");

            _tileMapMatrix = Matrix.CreateScale(SCALE);

            
        }
        public void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);

        }
        public void Draw()
        {
            
            _tiledMapRenderer.Draw(viewMatrix:_tileMapMatrix);

        }
    }
}
