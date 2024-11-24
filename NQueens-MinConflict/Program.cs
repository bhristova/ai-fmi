using System;

namespace NQueens_MinConflict
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string count = Console.ReadLine();
            bool isParsedSuccessfully = int.TryParse(count, out int n);

            if (isParsedSuccessfully)
            {
                var nqueensMinConflict = new NQueensMinConflict(n);
                nqueensMinConflict.solve();
            } else
            {
                Console.WriteLine("Invalid input");
            }
        }
    }
}
