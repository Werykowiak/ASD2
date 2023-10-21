﻿namespace ASD2
{
    internal class Program
    {
        static int n;
        static int m;
        static int d;
        static int count = 0;
        static List<Layer> cave;
        static Dictionary<string, bool> prawoMarcina;
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
            Console.WriteLine($"{n}, {m}, {d}");
            /*foreach (Layer layer in cave)
            {
                foreach (Line line in layer.lines)
                {
                    foreach (char block in line.blocks)
                    {
                        Console.Write(block);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }*/
            //Console.WriteLine(cave[3].lines[4].blocks[4]);
            Console.WriteLine(chief());
        }
        static int chief()
        {
            int temp = 0;
            for (int t = 0; t < m; t++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (cave[0].lines[t].blocks[x] == 'o')
                    {
                        prawoMarcina = new Dictionary<string, bool>();
                        int temp2 = glebiej(1, t, x);
                        if (temp2 > temp) { temp = temp2; }
                    }
                }
            }
            //temp = glebiej(1, 3, 2);
            return temp + 1;
        }

        static int glebiej(int i, int j, int k)
        {
            prawoMarcina.Add($"{i}{j}{k}", true);
            int temp = i;
            int temp2 = 0;
            if (temp == d - 1){ return temp; }
            //Console.WriteLine($"Warstwa: {i}, X: {k}, Y: {j}, temp: {temp} , {cave[i].lines[j].blocks[k]}");
            if (i < d - 1 && !prawoMarcina.ContainsKey($"{i + 1}{j}{k}") && cave[i + 1].lines[j].blocks[k] == 'o')
            {
                temp = glebiej(i + 1, j, k);
            }
            if(j<m -1 && !prawoMarcina.ContainsKey($"{i}{j+1}{k}") && cave[i].lines[j+1].blocks[k] == 'o') 
            { 
                temp2 = glebiej(i, j+1, k);
                if (temp2 > temp) { temp = temp2; }
            }
            
            if(j>0 && !prawoMarcina.ContainsKey($"{i}{j-1}{k}") && cave[i].lines[j-1].blocks[k] == 'o') 
            {
                temp2 = glebiej(i, j-1, k);
                if (temp2 > temp) { temp = temp2; }
            }
            
            if (k < n -1 && !prawoMarcina.ContainsKey($"{i}{j}{k+1}") && cave[i].lines[j].blocks[k+1] == 'o') 
            { 
                temp2 = glebiej(i, j, k+1);
                if (temp2 > temp) { temp = temp2; }
            }
            
            if (k > 0 && !prawoMarcina.ContainsKey($"{i}{j}{k-1}") && cave[i].lines[j].blocks[k-1] == 'o') 
            { 
                temp2 = glebiej(i, j, k-1);
                if (temp2 > temp) { temp = temp2; }
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