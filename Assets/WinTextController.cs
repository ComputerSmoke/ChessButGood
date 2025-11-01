using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTextController : MonoBehaviour
{
    public GameObject whiteWon;
    public GameObject blackWon;
    public GameObject idk;
    // Update is called once per frame
    void Update()
    {
        if (Game.winner == 0) ShowWon(whiteWon);
        else if (Game.winner == 1) ShowWon(blackWon);
        else ShowWon(idk);
    }
    private void ShowWon(GameObject winObject)
    {
        whiteWon.SetActive(false);
        blackWon.SetActive(false);
        idk.SetActive(false);
        winObject.SetActive(true);
    }
}
