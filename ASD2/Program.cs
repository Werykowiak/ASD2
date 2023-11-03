using System.Diagnostics;
namespace ASD2
{
    internal class Program
    {
        static int n;
        static int m;
        static int d;
        static bool isSurface;
        static int count = 0;
        static List<Layer> cave;
        static Dictionary<string, bool> visitedCaves;

        static void Main(string[] args)
        {

            StreamReader streamReader = new StreamReader("in1.txt");
            var fileLine = streamReader.ReadLine();

            string[] fLine = fileLine.Split(' ');
            n = int.Parse(fLine[0]);
            m = int.Parse(fLine[1]);
            d = int.Parse(fLine[2]);

            cave = new List<Layer>();

            for (int i = 0; i < d; i++)
            {
                Layer layer = new Layer();
                for (int j = 0; j < m; j++)
                {
                    fileLine = streamReader.ReadLine();
                    Line line = new Line();
                    line.blocks = fileLine.ToCharArray().ToList();
                    layer.lines.Add(line);
                }
                streamReader.ReadLine();
                cave.Add(layer);
            }
            streamReader.Close();

            Thread thread = new Thread(caveAnalysis, 8 * 1024 * 1024);
            thread.Start();
        }

        static void caveAnalysis()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int isolated = 0;
            int maxDepth = 0;
            visitedCaves = new Dictionary<string, bool>();
            long volume = 0;
            for (int a = d - 1; a > 0; a--)
            {
                for (int t = 0; t < m; t++)
                {
                    for (int x = 0; x < n; x++)
                    {
                        if (cave[a].lines[t].blocks[x] == 'o' && !visitedCaves.ContainsKey($"{a},{t},{x}"))
                        {
                            isSurface = false;
                            long temp = Up(a, t, x);
                            if (temp > volume) { volume = temp; }
                            if (!isSurface) { isolated++; } else if (a + 1 > maxDepth) { maxDepth = a + 1; }
                        }
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Największa głębokość: {maxDepth}, Największa objętość: {volume}, Jaskinie izolowane: {isolated} Czas: {stopwatch.Elapsed}");
        }

        static long Up(int i, int j, int k)
        {
            visitedCaves.Add($"{i},{j},{k}", true);
            long temp = 1;
            if (i > 0 && cave[i - 1].lines[j].blocks[k] == 'o' && !visitedCaves.ContainsKey($"{i - 1},{j},{k}"))
            {
                temp += Up(i - 1, j, k);
            }
            else if (i == 0) { isSurface = true; }
            if (i < d - 1 && cave[i + 1].lines[j].blocks[k] == 'o' && !visitedCaves.ContainsKey($"{i + 1},{j},{k}"))
            {
                temp += Up(i + 1, j, k);
            }
            if (j < m - 1 && cave[i].lines[j + 1].blocks[k] == 'o' && !visitedCaves.ContainsKey($"{i},{j + 1},{k}"))
            {
                temp += Up(i, j + 1, k);
            }

            if (j > 0 && cave[i].lines[j - 1].blocks[k] == 'o' && !visitedCaves.ContainsKey($"{i},{j - 1},{k}"))
            {
                temp += Up(i, j - 1, k);
            }

            if (k < n - 1 && cave[i].lines[j].blocks[k + 1] == 'o' && !visitedCaves.ContainsKey($"{i},{j},{k + 1}"))
            {
                temp += Up(i, j, k + 1);
            }

            if (k > 0 && cave[i].lines[j].blocks[k - 1] == 'o' && !visitedCaves.ContainsKey($"{i},{j},{k - 1}"))
            {
                temp += Up(i, j, k - 1);
            }
            return temp;
        }
    }


    public class Layer
    {
        public List<Line> lines;
        public Layer() { lines = new List<Line>(); }
    }
    public class Line
    {
        public List<char> blocks;
        public Line() { blocks = new List<char>(); }
    }
}