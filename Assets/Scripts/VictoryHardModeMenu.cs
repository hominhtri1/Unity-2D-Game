using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryHardModeMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            Time.timeScale = 1;

            SceneManager.LoadScene("Main Menu");
        }
    }
}
