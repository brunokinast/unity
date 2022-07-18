using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {
    public static int currentLevel;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void finishLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
    }
}
