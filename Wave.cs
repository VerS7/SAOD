using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace SAOD
{
    internal class Map
    {
        public int[,] lab;
        public Map(string LabPath)
        {
            lab = GetMatrixFromImg(LabPath);
        }
        public void printLab()
        {
            for (int i = 0; i < lab.GetLength(0); i++)
            {
                for (int j = 0; j < lab.GetLength(0); j++)
                {
                    Console.Write(lab[i, j]);
                }
                Console.WriteLine("\n");
            }
        }
        public int[,] GetMatrixFromImg(string FilePath)
        {
            Bitmap img = new Bitmap(FilePath);

            int height = img.Height;
            int width = img.Width;

            int[,] imgMatrix = new int[height, width];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (img.GetPixel(i, j).R == 255)
                    {
                        imgMatrix[i, j] = -1;
                    }
                    else imgMatrix[i, j] = 99;
                }
            }
            return imgMatrix;
        }
        public void CreateLabPathImg(string FilePath, List<int[]> coords)
        {
            Bitmap img = new Bitmap(FilePath);
            int height = img.Height;
            int width = img.Width;

            foreach (int[] coord in coords)
            {
                img.SetPixel(coord[0], coord[1], Color.Blue);
            }
            img.Save("labsolved.png", ImageFormat.Png);
        }
    }
    class WaveAlg
    {
        int width;
        int height;
        int wall = 99;
        int[,] map;
        List<Point> wave = new List<Point>();

        public WaveAlg(int[,] map)
        {
            this.map = map;
            this.width = map.GetLength(0);
            this.height = map.GetLength(0);
        }
        public void findPath(int x, int y, int nx, int ny)
        {
            if (map[y, x] == wall || map[ny, nx] == wall)
            {
                Console.WriteLine("Вы выбрали препятствие");
                return;
            }
            int[,] cloneMap = (int[,])map.Clone();
            List<Point> oldWave = new List<Point>();
            oldWave.Add(new Point(nx, ny));
            int nstep = 0;
            map[ny, nx] = nstep;

            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { -1, 0, 1, 0 };

            while (oldWave.Count > 0)
            {
                nstep++;
                wave.Clear();
                foreach (Point i in oldWave)
                {
                    for (int d = 0; d < 4; d++)
                    {
                        nx = i.x + dx[d];
                        ny = i.y + dy[d];


                        if (map[ny, nx] == -1)
                        {
                            wave.Add(new Point(nx, ny));
                            map[ny, nx] = nstep;
                        }
                    }
                }
                oldWave = new List<Point>(wave);
            }
            bool flag = true;
            wave.Clear();
            wave.Add(new Point(x, y));
            while (map[y, x] != 0)
            {
                flag = true;
                for (int d = 0; d < 4; d++)
                {
                    nx = x + dx[d];
                    ny = y + dy[d];
                    if (map[y, x] - 1 == map[ny, nx])
                    {
                        x = nx;
                        y = ny;
                        wave.Add(new Point(x, y));
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    Console.WriteLine("Пути нет!");
                    break;
                }
            }

            map = cloneMap;

            wave.ForEach(delegate (Point i)
            {
                map[i.y, i.x] = 0;
            });
        }

        struct Point
        {
            public Point(int x, int y)
                : this()
            {
                this.x = x;
                this.y = y;
            }
            public int x;
            public int y;
        }

        public void waveOut() 
        {
            wave.ForEach(delegate (Point i)
            {
                Console.WriteLine("x = " + i.x + ", y = " + i.y);
            });
        }
        public List<int[]> waveOutCoords()
        {
            List<int[]> coords = new();
            wave.ForEach(delegate (Point i)
            {
                coords.Add(new int[2] { i.x, i.y });
            }
            );
            return coords;
        }
        public void traceOut() 
        {
            string m = null;
            Console.Write("   ");
            for (int i = 0; i < height; i++)
            {
                Console.Write(i > 9 ? i + " " : i + "  ");
            }
            Console.WriteLine();
            for (int i = 0; i < width; i++)
            {
                m = i > 9 ? i + " " : i + "  "; 
                for (int j = 0; j < height; j++)
                {
                    m += map[i, j] > 9 || map[i, j] < 0 ? map[i, j] + " " : map[i, j] + "  ";
                }
                Console.WriteLine(m);
            }
        }

    }
}
