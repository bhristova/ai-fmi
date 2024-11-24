using System;
using System.Collections.Generic;
using System.Linq;

namespace NQueens_MinConflict
{
    internal class NQueensMinConflict
    {
        int N;
        int[] queens;
        int[] queensCountRow;
        int[] queensCountDiagonalsLeftToRight;
        int[] queensCountDiagonalsRightToLeft;
        Random random;
        DateTime startTime;

        public NQueensMinConflict(int n)
        {
            startTime = DateTime.Now;
            N = n;
            queens = new int[N];
            random = new Random();
            queensCountRow = Enumerable.Repeat(0, N).ToArray();
            queensCountDiagonalsLeftToRight = Enumerable.Repeat(0, 2 * N - 1).ToArray();
            queensCountDiagonalsRightToLeft = Enumerable.Repeat(0, 2 * N - 1).ToArray();
        }

        public void solve()
        {
            if (N < 4)
            {
                Console.WriteLine(-1);
                return;
            }

            initializeQueens();

            while (true)
            {
                int column = getColWithQueenWithMaxConf();
                int newRow = getRowWithMinConflict(column);
                int oldRow = queens[column];

                if (!hasConflicts())
                {
                    printSolutionOrTime();
                    return;
                }

                queens[column] = newRow;

                addToRowConflictsCount(oldRow, -1);
                addToRowConflictsCount(newRow, 1);

                addToDiagonalsLeftToRightConflictsCount(oldRow, column, -1);
                addToDiagonalsLeftToRightConflictsCount(newRow, column, 1);

                addToDiagonalsRightToLeftConflictsCount(oldRow, column, -1);
                addToDiagonalsRightToLeftConflictsCount(newRow, column, 1);
            }
        }

        private void initializeQueens()
        {
            for (int column = 0; column < N; column++)
            {
                int oldRow = queens[column];
                int newRow = getRowWithMinConflict(column);
                queens[column] = newRow;

                addToRowConflictsCount(newRow, 1);
                addToDiagonalsLeftToRightConflictsCount(newRow, column, 1);
                addToDiagonalsRightToLeftConflictsCount(newRow, column, 1);
            }
        }

        private void addToRowConflictsCount(int row, int value)
        {
            int oldValue = queensCountRow[row];
            queensCountRow[row] = oldValue + value;
        }

        private void addToDiagonalsLeftToRightConflictsCount(int row, int column, int value)
        {
            int indexD1 = column - row;
            int indexD1Appended = indexD1 + N - 1;
            int oldValue = queensCountDiagonalsLeftToRight[indexD1Appended];
            queensCountDiagonalsLeftToRight[indexD1Appended] = oldValue + value;
        }

        private void addToDiagonalsRightToLeftConflictsCount(int row, int column, int value)
        {
            int indexD2 = row + column;
            int oldValue = queensCountDiagonalsRightToLeft[indexD2];
            queensCountDiagonalsRightToLeft[indexD2] = oldValue + value;
        }

        private int getRowWithMinConflict(int column)
        {
            int minConflictValue = int.MaxValue;
            List<int> minConflictRows = new List<int>();

            for (int i = 0; i < N; i++)
            {
                int rowQueensCount = queensCountRow[i];

                int indexD1 = column - i;
                int indexD1Appended = indexD1 + N - 1;
                int diagonalsLeftToRightQueensCount = queensCountDiagonalsLeftToRight[indexD1Appended];

                int indexD2 = i + column;
                int diagonalsRightToLeftQueensCount = queensCountDiagonalsRightToLeft[indexD2];

                int conflictsSum = rowQueensCount + diagonalsLeftToRightQueensCount + diagonalsRightToLeftQueensCount;

                if (conflictsSum == minConflictValue)
                {
                    minConflictRows.Add(i);
                }

                if (conflictsSum < minConflictValue)
                {
                    minConflictRows.Clear();
                    minConflictRows.Add(i);
                    minConflictValue = conflictsSum;
                }
            }

            int index = random.Next(minConflictRows.Count);
            return minConflictRows[index];
        }

        private int getColWithQueenWithMaxConf()
        {
            int maxConflictValue = int.MinValue;
            List<int> maxConflictColumns = new List<int>();

            for (int column = 0; column < N; column++)
            {
                int queenRow = queens[column];

                int rowQueensCount = queenRow >= 0 ? queensCountRow[queenRow] : 0;

                int indexD1 = column - queenRow;
                int indexD1Appended = indexD1 + N - 1; // because indexD1 may be negative, we need to add an offset of N - 1 so the indexes start from 0
                int diagonalsLeftToRightQueensCount = indexD1Appended >= 0 ? queensCountDiagonalsLeftToRight[indexD1Appended] : 0;

                int indexD2 = column + queenRow;
                int diagonalsRightToLeftQueensCount = queensCountDiagonalsRightToLeft[indexD2];

                int conflictsSum = rowQueensCount + diagonalsLeftToRightQueensCount + diagonalsRightToLeftQueensCount;

                if (conflictsSum == maxConflictValue)
                {
                    maxConflictColumns.Add(column);
                }

                if (conflictsSum > maxConflictValue)
                {
                    maxConflictColumns.Clear();
                    maxConflictColumns.Add(column);
                    maxConflictValue = conflictsSum;
                }
            }

            int index = random.Next(maxConflictColumns.Count);
            return maxConflictColumns[index];
        }

        private bool hasConflicts()
        {
            return queensCountRow.Any(x => x > 1) || queensCountDiagonalsLeftToRight.Any(x => x > 1) || queensCountDiagonalsRightToLeft.Any(x => x > 1);
        }

        public void printSolutionOrTime()
        {
            if (N > 100)
            {
                Console.WriteLine(DateTime.Now.Subtract(startTime).TotalSeconds);
                return;
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (queens[j] == i)
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write('_');
                    }
                }
                Console.Write('\n');
            }
            Console.Write('\n');
        }
    }
}
