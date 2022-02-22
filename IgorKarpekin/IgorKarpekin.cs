using System;
using System.Collections.Generic;

namespace IgorKarpekin
{
    public class IgorKarpekin
    {
        private static int _wayLength = 8;
        private static string[,] _matrix;
        private static int _waysCount = 0;

        private static HashSet<string> _vowels = new HashSet<string> { "A", "E", "I", "O", "U", "Y" };
        private static int[,] _directions =
        {
            {2, 1},
            {-2, 1},
            {2, -1},
            {-2, -1},
            {1, 2},
            {-1, 2},
            {1, -2},
            {-1, -2},
        };

        public static long SolveMatrix()
        {
            //string[,] matrix =
            //{
            //    {"A", "B", "C", string.Empty, "E"},
            //    {string.Empty, "G", "H", "I", "J"},
            //    {"K", "L", "M", "N", "O"},
            //    {"P", "Q", "R", "S", "T"},
            //    {"U", "V", string.Empty, string.Empty, "Y"},
            //};
            //_matrix = matrix;

            InputMatrix();
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    Dfs(i, j, new Stack<string>(), 0);
                }
            }

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            return _waysCount;
        }

        public static void Dfs(int i, int j, Stack<string> current, int vowelsCount)
        {
            if (i < 0 || j < 0
                      || i >= _matrix.GetLength(0) || j >= _matrix.GetLength(1)
                      || string.IsNullOrWhiteSpace(_matrix[i, j])
                      || (vowelsCount == 2 && _vowels.Contains(_matrix[i, j])))
                return;

            if (_vowels.Contains(_matrix[i, j]))
            {
                vowelsCount++;
            }

            current.Push(_matrix[i, j]);

            if (current.Count == _wayLength)
            {
                _waysCount += 1;
                current.Pop();
                return;
            }

            for (int k = 0; k < _directions.GetLength(0); k++)
            {
                int nextI = i + _directions[k, 0];
                int nextJ = j + _directions[k, 1];
                Dfs(nextI, nextJ, current, vowelsCount);
            }

            current.Pop();
        }

        public static void InputMatrix()
        {
            Console.WriteLine("Please, enter the length of rows");
            int rows = int.Parse(Console.ReadLine());
            Console.WriteLine("Please, enter the length of columns");
            int columns = int.Parse(Console.ReadLine());

            _matrix = new string[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _matrix[i, j] = Console.ReadLine();
                }
            }
        }
    }
}