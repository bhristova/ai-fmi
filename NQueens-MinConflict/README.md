Place N queens on a chess board NxN so that no two queens attack each other. Use the algorithm MinConflicts to solve the problem.

Input: an integer N - number of queens
Output:
- if N is less than 4, print -1 (solutions do not exist for such N)
- if N >=4 && N <=100, print the board of the solution
- otherwise, print the time it took to find the solution