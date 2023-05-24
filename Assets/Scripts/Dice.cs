using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dice : MonoBehaviour
{
    Rigidbody rb;
    public DiceFace[] diceFace;
    public WaypointNavigator[] player;
    public Button roll, pass;
    public int playerTurn;
    public int diceValue;
    bool landed;
    bool thrown;

    Vector3 initPosition;
    public bool diceContact = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        initPosition = transform.position;
    }

    private void Update()
    {
        if (rb.IsSleeping() && !landed && thrown)
        {
            landed = true;
            rb.useGravity = false;
            ValueCheck();
        }
        else if (rb.IsSleeping() && landed && diceValue == 0)
        {
            Reroll();
        }
    }

    public void RollDice()
    {
        if (!landed && !thrown)
        {
            transform.position = initPosition;
            roll.gameObject.SetActive(false);
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
            
        }
    }

    //Put the dice back at its initial position and reset the variable
    private void Reset()
    {
        //transform.position = initPosition;
        thrown = false;
        landed = false;
        rb.useGravity = false;
    }

    void Reroll()
    {
        Reset();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    void ValueCheck()
    {
        diceValue = 0;
        foreach(DiceFace face in diceFace)
        {
            if (face.GroundCheck())
            {
                diceValue = face.faceValue;
                Debug.Log("Rolled " + diceValue);
                StartCoroutine(player[playerTurn].MovePlayer(diceValue));
                EnablePass(); //Temporary enabling pass before tile function is implemented, put this in tile function when done
            }
        }
    }

    void EnablePass()
    {
        pass.gameObject.SetActive(true);
    }

    public void PassTurn()
    {
        pass.gameObject.SetActive(false);
        roll.gameObject.SetActive(true);
        Reset();
        if (playerTurn < player.Length - 1)
        {
            playerTurn++;
        }
        else
            playerTurn = 0;
    }
}
