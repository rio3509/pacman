using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using SharpDX.Direct2D1;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
//using System.Drawing;
//using System.Drawing.Text;

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
        private List<Pill> _allPills = new List<Pill>();

        private Texture2D _playerTexture;
        private Texture2D _pillTex;
        private Player _pacman;
        private Pill _tempPill;
        private Color _pillColor = Color.Black;

        //iterate through tile array to find the first empty tile
        private Point FindFirstEmptyTile()
        {
            for (int y = 0; y < _tileArray.GetLength(0); y++)
            {
                for (int x = 0; x < _tileArray.GetLength(1); x++)
                {
                    if (_tileArray[y, x].Type == "Empty")
                    {
                        return new Point(x, y);
                    }
                }
            }
            return new Point(0, 0);
        }


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

            _playerTexture = Content.Load<Texture2D>("pacman");
            Microsoft.Xna.Framework.Point startPosition = FindFirstEmptyTile();

            _pacman = new Player(_playerTexture, new Vector2(startPosition.X * _tileSizeX, startPosition.Y * _tileSizeY), Color.White);
            _pacman.HomeOnGrid(startPosition, _tileSizeX, _tileSizeY, _tileArray);

            //iterate through tile array to find the position of every empty tile

            _pillTex = Content.Load<Texture2D>("Pill");

            foreach (Tile tile in _tileArray)
            {
                //if the tile type is "empty" then create a new pill in a list with that tile's position
                if (tile.Type == "Empty")
                {
                    _tempPill = new Pill(_pillTex, _pillColor, tile.Position);
                    _allPills.Add(_tempPill);
                }
            }

        }

        private Tile[,] RenewTileMap(Tile[,] tileMap)
        {
            Tile[,] newMap = tileMap;
            return newMap;
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _pacman.Update(gameTime, _tileArray);

            //check collision between each pill and the player
            foreach (Pill test in _allPills)
            {
                if (_pacman.BoundingBox.Intersects(test.BoundingBox))
                {
                    test.Visible = false;
                }
            }

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

            foreach (Pill p in _allPills)
            {
                p.DrawPill(_spriteBatch);
            }

            _pacman.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
