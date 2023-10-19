namespace ASD2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader streamReader = new StreamReader("input.txt");
            var fileLine = streamReader.ReadLine();

            string[] fLine = fileLine.Split(" ");
            int n = int.Parse(fLine[0]);
            int m = int.Parse(fLine[0]);
            int d = int.Parse(fLine[0]);

            List<Layer> cave = new List<Layer>();

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
                cave.Add(layer);
            }
            streamReader.Close();

            foreach (Layer layer in cave)
            {
                foreach(Line line in layer.lines)
                {
                    foreach(char block in line.blocks)
                    {
                        Console.Write(block);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
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