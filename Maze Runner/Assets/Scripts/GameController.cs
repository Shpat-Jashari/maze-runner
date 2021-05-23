using UnityEngine;

/** GameControlles manages the game flow.*/
public class GameController : MonoBehaviour
{
    public Light playerLight; //the light which follows the player
    public MazeDrawer mazePrefab; //the MazeDrawer prefab
    MazeDrawer mazeInstance; //an instance of mazePrefab
    public GameObject finishLine; //the finish line
    public GameObject mainMenuView; //the main menu view
    public PlayerController playerController; //the player controller

    /* Update is called once per frame.*/
    void Update()
    {
        //the game is ended if the player presses the "escape" key
        if (Input.GetKey("escape") && this.gameObject.active)
        {
            //the level was ended manually
            EndGame();
        }
    }

    /* BeginGame loads the game view and starts a new game.*/
    public void BeginGame()
    {
        mainMenuView.SetActive(false);
        this.gameObject.SetActive(true);
        mazeInstance = Instantiate(mazePrefab) as MazeDrawer;

    }

     /* EndGame ends the game and loads the main menu.*/
    public void EndGame()
    {
        Destroy(mazeInstance.gameObject);
        playerController.ResetPlayer();
        this.gameObject.SetActive(false);
        FinishLineDisplay(false);
        mainMenuView.SetActive(true);
    }

    /* QuitGame quits the game and closes the application.*/
    public void QuitGame()
    {
        Application.Quit();
    }

    /* SetDifficulty sets the game difficulty.*/
    public void SetDifficulty(int i)
    {
        if (i == 0)
            playerLight.range = 25; //easy
        else if (i == 1)
            playerLight.range = 15; //medium
        else
            playerLight.range = 5; //hard
    }

    /* FinishLineDisplay displays the finish line.*/
    public void FinishLineDisplay(bool isActive)
    {
        finishLine.SetActive(isActive);
    }
}
