using System;

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
        }
    }
}
