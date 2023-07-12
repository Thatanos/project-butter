using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dice : MonoBehaviour
{
    [Tooltip("Assign to objects with collision on the dice faces, make sure they have the DiceFace scripts on it.")] 
    public DiceFace[] diceFace;
    [Tooltip("Assign this to the player navigator of each player.")]
    public WaypointNavigator[] player;
    [Tooltip("Exposed parameter for debugging, check whether the player is sleeping or not.")]
    public bool[] playerSleep;
    [Tooltip("Assign both of this to the button for rolling the dice and passing the turn.")]
    public Button roll, pass;
    [Tooltip("Exposed parameter for debugging, check which player turn it is right now.")]
    public int playerTurn;
    [Tooltip("Exposed parameter for debugging, check the value of the dice rolled.")]
    public int diceValue;
    [Tooltip("Exposed parameter for debugging, check whether the dice contact the board or not.")]
    public bool diceContact = false;

    bool landed; //Parameter for checking if the dice stopped moving.
    bool thrown; //Parameter for the roll function to work.
    TileFunction tf; //Imported method from other script
    Rigidbody rb;
    Vector3 initPosition; //Parameter for the default position for the dice.
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Assign rb to object's rigid body
        rb.useGravity = false; //Stops the dice from falling
        initPosition = transform.position; //Get the initial function
        tf = GameObject.FindObjectOfType<TileFunction>(); //Assign the tf function to active object
    }

    private void Update()
    {
        if (rb.IsSleeping() && !landed && thrown) //check the value when the dice has stopped moving
        {
            landed = true;
            rb.useGravity = false;
            ValueCheck();
        }
        else if (rb.IsSleeping() && landed && diceValue == 0) //reroll the dice if it doesnt land on faces
        {
            Reroll();
        }
    }

    public void RollDice()
    {
        if (!landed && !thrown)
        {
            transform.position = initPosition;
            roll.gameObject.SetActive(false); //disable roll button
            thrown = true;
            rb.useGravity = true; //enable gravity
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500)); //add spinning forces to the dice
            
        }
    }

    //Put the dice back at its initial position and reset the variable
    private void Reset()
    {
        //transform.position = initPosition; //(deprecated)used to reset the position of the dice
        thrown = false;
        landed = false;
        rb.useGravity = false;
    }

    void Reroll() //reroll the dice
    {
        Reset();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    void ValueCheck()
    {
        diceValue = 0; //reset the dice value to avoid false data
        foreach(DiceFace face in diceFace) //checks each faces in diceFace if any of it returns value
        {
            if (face.GroundCheck())
            {
                diceValue = face.faceValue; //assigns the value from dice to the parameter
                Debug.Log("Rolled " + diceValue);
                StartCoroutine(player[playerTurn].MovePlayer(diceValue)); //call the move function for player
                tf.FuncCheck(player[playerTurn].currentWaypointIndex); //Temporary enabling pass before tile function is implemented, put this in tile function when done
            }
        }
    }

    public void EnablePass()
    {
        pass.gameObject.SetActive(true); //reenable pass turn button
    }

    public void PassTurn()
    {
        pass.gameObject.SetActive(false); //disable pass turn button
        roll.gameObject.SetActive(true); //enable roll button
        Reset(); //reset dice parameter
        if (playerTurn < player.Length - 1) //check if the player turn is between range
        {
            playerTurn++;
            if (playerSleep[playerTurn]) //check if the player is sleeping
            {
                playerSleep[playerTurn] = false;
                playerTurn++; //removes sleep and skip turn
            }
        }
        else
        {
            playerTurn = 0; //reset to first player if exceed the range
            if (playerSleep[playerTurn]) //check if player is sleeping
            {
                playerSleep[playerTurn] = false;
                playerTurn++; //remove sleep and skip turn
            }
        }
    }
}
