using UnityEngine;

/** PlayerController controls the player.*/
public class PlayerController : MonoBehaviour
{
    public int maxKeys = 5; //the number of keys in the maze
    int currentKeys; //the number of keys collected by the player

    float horizontal; //the player's current horizontal direction (-1 = left and 1 = right)
    float vertical; //the player's current vertical direction (-1 = down and 1 = up)
    public float playerSpeed; //the player's speed
    Rigidbody2D rigidbody2d; //Unity physics engine component that enables collision detection

    /* Start is called before the first frame update.*/
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); //initializing rigidbody2d 
        currentKeys = 0;
    }

    /* Update is called once per frame.*/
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal"); //taking the pressed state of AD and Left/Right arrow keys 
        vertical = Input.GetAxis("Vertical"); //taking the pressed state of WS and Up/Down arrow keys 
    }

    /* FixedUpdate is called every fixed time period.*/ 
    void FixedUpdate()
    {
        //moving the player
        rigidbody2d.MovePosition(new Vector2(
            rigidbody2d.position.x + playerSpeed * horizontal * Time.deltaTime,
            rigidbody2d.position.y + playerSpeed * vertical * Time.deltaTime));
    }

    /* OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only).*/
    void OnTriggerEnter2D(Collider2D other)
    {
        if (currentKeys < maxKeys)
        {
            AddKey();
            Destroy(other.gameObject); //removes the collected key
        }
        else
        {
            GameController gameController = GetComponentInParent(typeof(GameController)) as GameController;
            gameController.EndGame(); //ends the game after colliding with the finish line
        }
    }

    /* AddKey adds 1 to the current number of keys and checks if all the keys got collected in order to activate the finish line.*/
    public void AddKey()
    {
        //clamps the current number of keys + 1 between 0 and maxKeys
        currentKeys = Mathf.Clamp(currentKeys + 1, 0, maxKeys);
        if (currentKeys >= maxKeys) //checking if all the keys got collected
        {
            GameController gameController = GetComponentInParent(typeof(GameController)) as GameController;
            gameController.FinishLineDisplay(true); //activate the finish line
        }

    }

    /* ResetPlayer resets the player.*/
    public void ResetPlayer()
    {
        transform.position = new Vector2(1, -1);
        currentKeys = 0;
    }
}