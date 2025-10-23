using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace pacman
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int SCREEN_TILES_WIDE = 20;
        private const int SCREEN_TILES_HIGH = 28;

        private char[,] _tileValuesArray;
        private Tile[,] _tileArray;
        private List<Texture2D> _allFileTextures = new List<Texture2D>();

        private Vector2 testSpeed = new Vector2(5.0f, 5.0f);
        private Vector2 TestPos;
        private Texture2D TestTex;
        private Player _pacman;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = SCREEN_TILES_WIDE * 50;
            _graphics.PreferredBackBufferHeight = SCREEN_TILES_HIGH * 50;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //_tileValuesArray = TileManager.FileReader("MazeMap.txt", SCREEN_TILES_HIGH, SCREEN_TILES_WIDE);
            //int testInt = 1 + 1;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _allFileTextures = TileManager.LoadContent(Content);

            int _tileSizeX = _allFileTextures[0].Width;
            int _tileSizeY = _allFileTextures[0].Height;

            _tileValuesArray = TileManager.FileReader("MazeMap.txt", SCREEN_TILES_WIDE, SCREEN_TILES_HIGH);
            _tileArray = TileManager.CreateMap(_tileValuesArray, _tileSizeX, _tileSizeY, _allFileTextures);

            TestPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            TestTex = Content.Load<Texture2D>("pacman");
            _pacman = new Player(TestTex, TestPos, Color.White);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _pacman.Movement(testSpeed, _tileArray);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            foreach (Tile t in _tileArray)
            {
                t.DrawTile(_spriteBatch);
            }

            _pacman.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
