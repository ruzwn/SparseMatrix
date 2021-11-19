using System;
using System.Collections.Generic;

namespace SparseMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SparseMatrix m1 = new SparseMatrix(5, 4);
            Console.WriteLine(m1.Size);
            m1.ChangeElem(1, 1, 8);
            Console.WriteLine(m1[1, 1]);
            Console.WriteLine(m1[5, 4]);
            Console.WriteLine(m1); 
            Dictionary<(int, int), int> temp1 = m1.GetNeighbors(2, 2);
            /*foreach (var (key, val) in temp1)
            {
                Console.WriteLine(key);
            }*/

            Console.WriteLine();
            temp1 = m1.GetNeighbors(1, 1);
            foreach (var (key, val) in temp1)
            {
                Console.WriteLine(key);
            }

            int[,] arr = new int[0, 0];
            SparseMatrix m2 = new SparseMatrix(arr);
        }
    }
}
