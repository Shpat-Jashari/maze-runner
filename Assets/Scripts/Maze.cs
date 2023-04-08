using System;
/** Maze generates a maze using randomized Kruskal's algorithm.*/
public class Maze
{
    public int width = 20; //width of the maze matrix
    public int height = 16; //height of the maze matrix
    public int[,] maze; //a matrix that contains all the walls and cells
    public int[,] walls; //a matrix that contains the walls
    public int[,] GetMaze { get { return maze; } } //returns the ith and jth element of maze
    public int getWidth { get { return width; } } //returns the width of the maze
    public int getHeight { get { return height; } } //returns the height of the maze

    /* GenerateMaze generates a maze
     * @param n - width of the maze
     * @param m - height of the maze.*/
    public void GenerateMaze(int n, int m)
    {
        width = 2 * n + 1; 
        height = 2 * m + 1; 
        maze = new int[height, width];
        FillCellPositions();
        FillWallList();
        walls = ShuffleRows(walls, new System.Random());
        //for each wall, if the cells divided by this wall have different values,
        //assign the minimum of these values to both cells and remove the wall between them
        for (int i = 0; i < walls.GetLength(0); i++)
        {
            int x = walls[i, 0];
            int y = walls[i, 1];
            int x1, x2, y1, y2;
            //check the horizontal walls
            if (x % 2 == 0)
            {
                x1 = x - 1;
                y1 = y;
                x2 = x + 1;
                y2 = y;
            }
            //check the vertical walls
            else
            {
                x1 = x;
                y1 = y - 1;
                x2 = x;
                y2 = y + 1;
            }
            //check if the cells have different values
            if (maze[x1, y1] != maze[x2, y2])
            {
                int minimum = (int)Math.Min(maze[x1, y1], maze[x2, y2]);
                int maximum = (int)Math.Max(maze[x1, y1], maze[x2, y2]);
                //to remove the walls assign a non-zero value to them
                maze[walls[i, 0], walls[i, 1]] = minimum;
                //assign the minimum to both cell sets
                ReplaceInArray(maze, maximum, minimum);
            }
        }
    }

    /* ReplaceInArray assigns a new value to elements in a matrix that are already assigned with a value
     * @param matrix - the matrix
     * @param val - elements equal to this value will be assigned with the new value
     * @param newVal - the new value to be assigned.*/
    void ReplaceInArray(int[,] matrix, int val, int newVal)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0; j < matrix.GetLength(1); j++)
                if (matrix[i, j] == val)
                    matrix[i, j] = newVal;
    }

    /* FillWallList initializes matrix walls and fills it with the wall positions from matrix maze.*/
    void FillWallList()
    {
        int n = (width - 1) / 2;
        int m = (height - 1) / 2;
        walls = new int[(n - 1) * m + n * (m - 1), 2]; //the matrix that contains the walls of the maze
        int count = 0;
        for (int x = 1; x < height - 1; x++)
            for (int y = 1; y < width - 1; y++)
                if ((x + y) % 2 == 1) //element in position [x,y] from matrix maze where (x + y) % 2 = 1 are walls
                {
                    walls[count, 0] = x;
                    walls[count, 1] = y;
                    count++;
                }
    }

    /* FillCellPosition assigns a unique number to each cell in matrix maze.*/
    void FillCellPositions()
    {
        int uniqueIndex = 1;
        for (int i = 1; i < height; i += 2)
            for (int j = 1; j < width; j += 2)
            {
                maze[i, j] = uniqueIndex;
                uniqueIndex++;
            }
    }

    /* ShuffleRows shuffles the elements in a matrix and returns the new matrix
     * @param matrix - the matrix
     * @param rg - represents a pseudo-random number generator
     * @return - the new matrix.*/
    int[,] ShuffleRows(int[,] matrix, System.Random rg)
    {
        for (int i = 0; i < matrix.GetLength(0) - 1; i++)
        {
            //rg.Next(int minValue, int maxValue) returns a 32-bit signed integer greater than or equal
            //to minValue and less than maxValue
            int randomIndex = rg.Next(i, matrix.GetLength(0));

            int[] tempRow = GetRow(matrix, randomIndex);
            matrix = SetRow(matrix, GetRow(matrix, i), randomIndex);
            matrix = SetRow(matrix, tempRow, i);
        }

        return matrix;
    }

    /* GetRow returns a row of a matrix
     * @param matrix - the matrix 
     * @param rowNumber - the row of the matrix
     * @return - the row of the matrix at position rowNumber.*/
    int[] GetRow(int[,] matrix, int rowNumber)
    {
        int rowLength = matrix.GetLength(1);
        int[] row = new int[rowLength];
        for (int i = 0; i < rowLength; i++)
            row[i] = matrix[rowNumber, i];

        return row;
    }

    /* SetRow replaces a row in a matrix with a new row
     * @param matrix - the matrix
     * @param newRow - the new row
     * @param rowNumber - the position of the row that is going to be replaced
     * @return - the new matrix.*/
    int[,] SetRow(int[,] matrix, int[] newRow, int rowNumber)
    {
        for (int i = 0; i < newRow.GetLength(0); i++)
            matrix[rowNumber, i] = newRow[i];

        return matrix;
    }
}

