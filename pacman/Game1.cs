using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

//using SharpDX.Direct2D1;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
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
        private List<Pill> _bigPills = new List<Pill>();

        private Texture2D _playerTexture;
        private Texture2D _pillTex;
        private Texture2D _bigPillTex;
        private Texture2D _ghostTex;
        private SpriteFont _scoreFont;
        private Player _pacman;
        private Pill _tempPill;
        private Ghost _ghost;
        private Color _pillColor = Color.White;
        private Color _bigPillColor = Color.White;
        private Vector2 _scorePos = new Vector2 (25, 5);

        private bool _powered = false;
        private bool _endGame = false;
        private int _powerTimer = 0;
        private int _timer = 0;
        private int _score = 0;
        private string _scoreString;

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

        private Point FindRandomTile()
        {
            Random random = new Random();
            int y = random.Next(1, 28);
            int x = random.Next(1, 20);

            if (_tileArray[y, x].Type == "Empty")
            {
                return new Point(x, y);
            }
            else
            {
                return new Point(5, 5);
            }
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

            _scoreFont = Content.Load<SpriteFont>("ScoreFont");

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

            //iterate through tile array to find the position of every "Pill_L" tile

            _bigPillTex = Content.Load<Texture2D>("Big Pill");

            foreach (Tile bigpilltile in _tileArray)
            {
                //if the tile type is "Pill_L" then create a new pill in a list with that tile's position
                if (bigpilltile.Type == "Pill_L")
                {
                    _tempPill = new Pill(_bigPillTex, _bigPillColor, bigpilltile.Position);
                    _bigPills.Add(_tempPill);
                }
            }

            _ghostTex = Content.Load<Texture2D>("Ghost");
            Point GhostStart = FindRandomTile();

            _ghost = new Ghost(_ghostTex, new Vector2(GhostStart.X * _tileSizeX, GhostStart.Y * _tileSizeY), Color.White);
            _ghost.PlaceGhostOnGrid(GhostStart, _tileSizeX, _tileSizeY, _tileArray);
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
            _timer += 1;
            _pacman.Update(gameTime, _tileArray);


            //make ghost movement update every 10 frames to prevent spamming
            if (_timer == 10)
            {
                _ghost.Update(gameTime, _tileArray);
                _timer = 0;
            }

            //check ghost collision with the player
            if (_ghost.BoundingBox.Intersects(_pacman.BoundingBox))
            {
                _endGame = true;
            }

            //check collision between each pill and the player
            foreach (Pill test in _allPills)
            {
                if (_pacman.BoundingBox.Intersects(test.BoundingBox))
                {
                    //if the player collides with a visible pill, update score and make the pill invisible
                    if (test.Visible == true)
                    {
                        test.Visible = false;
                        _score += 1;
                    }
                }
            }

            //repeat for every big pill
            foreach (Pill bigtest in _bigPills)
            {
                if (_pacman.BoundingBox.Intersects(bigtest.BoundingBox))
                {
                    //if the player collides with a visible big pill, update score and make it invisible
                    if (bigtest.Visible == true)
                    {
                        bigtest.Visible = false;
                        _score += 10;
                        _powered = true;
                    }
                }
            }

            //update score string
            _scoreString = "Score: " + _score + "pts";

            //if _endGame is false, continue to use base.Update() (otherwise stop updating to stop score from increasing)
            if (_endGame == false)
            {
                base.Update(gameTime);
            }
            //base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            // TODO: Add your drawing code here

            //draw normal game loop as long as _endGame is false
            if (_endGame == false)
            {
                //iterate through tiles to draw them
                foreach (Tile t in _tileArray)
                {
                    t.DrawTile(_spriteBatch);
                }

                //iterate through pills to draw them
                foreach (Pill p in _allPills)
                {
                    p.DrawPill(_spriteBatch);
                }

                //iterate through big pills to draw them
                foreach (Pill a in _bigPills)
                {
                    a.DrawPill(_spriteBatch);
                }

                //draw player
                _pacman.Draw(_spriteBatch);

                //draw ghost
                _ghost.Draw(_spriteBatch);

                //draw score last so it's above everything
                _spriteBatch.DrawString(_scoreFont, _scoreString, _scorePos, Color.Red);
            }

            //if _endGame is true then draw end screen
            if (_endGame == true)
            {
                //clear screen + draw black background
                GraphicsDevice.Clear(Color.Black);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
