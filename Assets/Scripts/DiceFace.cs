using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFace : MonoBehaviour
{
    [SerializeField]bool onGround;
    public int faceValue;

    void OnTriggerStay(Collider ground)
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

    public bool GroundCheck()
    {
        return onGround;
    }
}
