using UnityEngine;

/** MazeDrawer draws the maze.*/
public class MazeDrawer : MonoBehaviour
{
    public int mazeWidth = 20; //width of the maze
    public int mazeHeight = 16; //height of the maze
    //all the tiles that are going to be used to draw the maze
    public GameObject TopLeftWalls;
    public GameObject TopMiddleWalls;
    public GameObject TopRightWalls;
    public GameObject TopWalls;
    public GameObject MiddleLeftWalls;
    public GameObject MiddleWalls;
    public GameObject MiddleRightWalls;
    public GameObject VerticalWalls;
    public GameObject HorizontalWalls;
    public GameObject BottomLeftWalls;
    public GameObject BottomMiddleWalls;
    public GameObject BottomRightWalls;
    public GameObject BottomWalls;
    public GameObject LeftWalls;
    public GameObject RightWalls;

    public GameObject Key; //the keys that the player must collect to finish the game
    public int numberOfKeys = 5; //the number of keys

    Maze maze;

    //Start is called before the first frame update
    void Start()
    {
        //generate a maze
        maze = new Maze();
        maze.GenerateMaze(mazeWidth, mazeHeight);
        
        DrawWalls();
        DrawKeys();
    }

    /* DrawWalls draws the walls of the maze.*/
    void DrawWalls()
    {
        for (int x = 1; x < maze.getHeight; x += 2)
            for (int y = 1; y < maze.getWidth; y += 2)
            {
                GameObject tile = null;

                //check if the walls surrounding the cell are removed
                //the maze matrix elements that are equal to 0 are walls that have not been removed
                bool HasTopWall = maze.GetMaze[x - 1, y] == 0;
                bool HasLeftWall = maze.GetMaze[x, y - 1] == 0;
                bool HasBottomWall = maze.GetMaze[x + 1, y] == 0;
                bool HasRightWall = maze.GetMaze[x, y + 1] == 0;

                //check which walls surrounding the cell have been removed and based on that draw the tiles
                if (HasTopWall && HasLeftWall && !HasBottomWall && !HasRightWall)
                    tile = TopLeftWalls;
                else if (HasTopWall && !HasLeftWall && !HasBottomWall && !HasRightWall)
                    tile = TopMiddleWalls;
                else if (HasTopWall && !HasLeftWall && !HasBottomWall && HasRightWall)
                    tile = TopRightWalls;
                else if (HasTopWall && HasLeftWall && !HasBottomWall && HasRightWall)
                    tile = TopWalls;
                else if (!HasTopWall && HasLeftWall && !HasBottomWall && !HasRightWall)
                    tile = MiddleLeftWalls;
                else if (!HasTopWall && !HasLeftWall && !HasBottomWall && !HasRightWall)
                    tile = MiddleWalls;
                else if (!HasTopWall && !HasLeftWall && !HasBottomWall && HasRightWall)
                    tile = MiddleRightWalls;
                else if (!HasTopWall && HasLeftWall && !HasBottomWall && HasRightWall)
                    tile = VerticalWalls;
                else if (HasTopWall && !HasLeftWall && HasBottomWall && !HasRightWall)
                    tile = HorizontalWalls;
                else if (!HasTopWall && HasLeftWall && HasBottomWall && !HasRightWall)
                    tile = BottomLeftWalls;
                else if (!HasTopWall && !HasLeftWall && HasBottomWall && !HasRightWall)
                    tile = BottomMiddleWalls;
                else if (!HasTopWall && !HasLeftWall && HasBottomWall && HasRightWall)
                    tile = BottomRightWalls;
                else if (!HasTopWall && HasLeftWall && HasBottomWall && HasRightWall)
                    tile = BottomWalls;
                else if (HasTopWall && HasLeftWall && HasBottomWall && !HasRightWall)
                    tile = LeftWalls;
                else 
                    tile = RightWalls;

                Instantiate(tile, new Vector3((y + 1) / 2, -(x + 1) / 2, 0), Quaternion.identity, this.transform);
            }
    }

    /* DrawKeys draws the keys in the maze at random positions.*/
    void DrawKeys()
    {
        System.Random mazeRG = new System.Random();
        for(int i = 0; i < numberOfKeys; i++)
        {
            int x = mazeRG.Next(1, mazeHeight);
            int y = mazeRG.Next(1, mazeWidth);
            Instantiate(Key, new Vector3(y, -x, 0), Quaternion.identity, this.transform);
        }
    }
}