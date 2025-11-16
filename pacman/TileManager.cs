using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectWrite;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman
{
    internal static class TileManager
    {
        public static char[,] FileReader(string file, int tilesX, int tilesY)
        {

            //create array that will be returned using dimensions taken
            char[,] charArray = new char[tilesY, tilesX];

            //deine universal path to map file + open reader
            string path = Path.Combine("..", "..", "..", "Content", file);
            using StreamReader reader = new StreamReader(path);


            //define iteration variables and line
            #nullable enable
            int row = 0;
            string? line;

            //iterate through every row, checking that they are not null
            while ((row < tilesY) && ((line = reader.ReadLine()) != null))
            {
                //iterate through every column in said row
                for (int col = 0; col < tilesX && col < line.Length; col++)
                {
                    //assign the character at pos[row, col] to the character array
                    charArray[row, col] = line[col];
                }
                row++;
            }

            //return character array
            return charArray;
        }


        public static List<Texture2D> LoadContent(ContentManager content)
        {
            Texture2D topLeft, top, bottom, topRight, left, right, bottomLeft, bottomRight, empty, solid;
            List<Texture2D> TextureList = new List<Texture2D>();

            topLeft = content.Load<Texture2D>("Wall TL");
            top = content.Load<Texture2D>("Wall T");
            bottom = content.Load<Texture2D>("Wall B");
            topRight = content.Load<Texture2D>("Wall TR");
            left = content.Load<Texture2D>("Wall L");
            right = content.Load<Texture2D>("Wall R");
            bottomLeft = content.Load<Texture2D>("Wall BL");
            bottomRight = content.Load<Texture2D>("Wall BR");
            empty = content.Load<Texture2D>("Empty");
            solid = content.Load<Texture2D>("Solid");
            //bigPill = content.Load<Texture2D>("Big Pill");

            TextureList.Add(topLeft);
            TextureList.Add(top);
            TextureList.Add(bottom);
            TextureList.Add(topRight);
            TextureList.Add(left);
            TextureList.Add(right);
            TextureList.Add(bottomLeft);
            TextureList.Add(bottomRight);
            TextureList.Add(empty);
            TextureList.Add(solid);
            //TextureList.Add(bigPill);

            return TextureList;
        }


        public static Tile[,] CreateMap(char[,] mapArray, int tileSizeX, int tileSizeY, List<Texture2D> textures)
        {
            // mapArray is [rows, cols]
            int rows = mapArray.GetLength(0);
            int cols = mapArray.GetLength(1);
            Tile[,] tileArray = new Tile[rows, cols];
            Microsoft.Xna.Framework.Color tileColour = Microsoft.Xna.Framework.Color.LightBlue;

            for (int y = 0; y < rows; y++)
            {   
                for (int x = 0; x < cols; x++)
                {
                    // Screen position uses x for horizontal (cols) and y for vertical (rows)
                    Microsoft.Xna.Framework.Vector2 tilePosition = new Microsoft.Xna.Framework.Vector2(tileSizeX * x, tileSizeY * y);

                    switch (mapArray[y, x])
                    {
                        case '0':
                            tileArray[y, x] = new Tile(textures[0], tilePosition, tileColour, "TL");
                            break;
                        case '1':
                            tileArray[y, x] = new Tile(textures[1], tilePosition, tileColour, "T");
                            break;
                        case '2':
                            tileArray[y, x] = new Tile(textures[2], tilePosition, tileColour, "B");
                            break;
                        case '3':
                            tileArray[y, x] = new Tile(textures[3], tilePosition, tileColour, "TR");
                            break;
                        case '4':
                            tileArray[y, x] = new Tile(textures[4], tilePosition, tileColour, "L");
                            break;
                        case '5':
                            tileArray[y, x] = new Tile(textures[5], tilePosition, tileColour, "R");
                            break;
                        case '6':
                            tileArray[y, x] = new Tile(textures[6], tilePosition, tileColour, "BL");
                            break;
                        case '7':
                            tileArray[y, x] = new Tile(textures[7], tilePosition, tileColour, "BR");
                            break;
                        case '8':
                            tileArray[y, x] = new Tile(textures[8], tilePosition, tileColour, "Empty");
                            break;
                        case 'A':
                            //draw an empty tile but set the type to "Pill_L"
                            tileArray[y, x] = new Tile(textures[8], tilePosition, tileColour, "Pill_L");
                            break;
                        case 'G':
                            //draw an empty tile but set the type to "Spawn"
                            tileArray[y, x] = new Tile(textures[8], tilePosition, tileColour, "Spawn");
                            break;
                        default:
                            // Fallback to a safe default to avoid nulls if the map contains unexpected chars or missing data
                            tileArray[y, x] = new Tile(textures[9], tilePosition, tileColour, "Solid");
                            break;
                    }
                }
            }
            //initial commit
            return tileArray;
        }
    }
}
