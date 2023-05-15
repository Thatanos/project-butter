using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;
    public DiceFace[] diceFace;
    public WaypointNavigator[] player;
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
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
        else if (landed && thrown)
        {
            Reset();
        }
    }

    private void Reset()
    {
        transform.position = initPosition;
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
                switch (playerTurn)
                {
                    case 0:
                        StartCoroutine(player[0].MovePlayer(diceValue));
                        break;
                    case 1:
                        player[1].MovePlayer(diceValue);
                        break;
                    case 2:
                        player[2].MovePlayer(diceValue);
                        break;
                    case 3:
                        player[3].MovePlayer(diceValue);
                        break;
                }
            }
        }
    }
}
