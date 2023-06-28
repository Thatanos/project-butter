using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFunction : MonoBehaviour
{
    public string[] tileType;
    public Dice dice;
    public void FuncCheck(int tileNumber)
    {
        switch (tileType[tileNumber])
        {
            case "K":
                KancilFunc();
                break;
            case "T":
                TidurFunc();
                break;
            case "S":
                //SongsangFunc
                break;
            case "N":
                //NasibFunc
                break;
            case "P":
                //PerangkapFunc
                break;
        }
    }
    public void KancilFunc()
    {
        Debug.Log("KancilTile, moving " + dice.diceValue);
        StartCoroutine(dice.player[dice.playerTurn].MovePlayer(dice.diceValue));
        dice.EnablePass();
    }

    public void TidurFunc()
    {
        Debug.Log("Tidur~ Tidur~");
        dice.playerSleep[dice.playerTurn] = true;
        dice.EnablePass();
    }

    public void SongsangFunc()
    {
        int num = Random.Range(0, 6);
        switch (num)
        {
            case 0:
                //func1
                break;
            case 1:
                //func2
                break;
            case 2:
                //func3
                break;
            case 3:
                //func4
                break;
            case 4:
                //func5
                break;
            case 5:
                //func6
                break;
            case 6:
                //func7
                break;
        }
    }
}
