using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text coinText;
    private int coinCounter;

    public void addCoin()
    {
        coinCounter++;
        coinText.text = coinCounter+" KiloBytes";
    }
}
