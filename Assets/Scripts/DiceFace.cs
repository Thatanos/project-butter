using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFace : MonoBehaviour
{
    [Tooltip("Input value opposite of this face.")]
    public int faceValue;
    [SerializeField][Tooltip("Exposed parameter to check if it faces the ground.")]
    bool onGround;


    void OnTriggerStay(Collider ground) //check if the trigger stays on board
    {
        if (ground.tag == "Board")
        {
            onGround = true;
        }
    }

    void OnTriggerExit(Collider ground)
    {
        if (ground.tag == "Board")
        {
            onGround = false;
        }
    }

    public bool GroundCheck() //plublic function to return if it's grounded
    {
        return onGround;
    }
}
