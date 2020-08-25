using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour
{
    private int index;

    void Start()
    {
        TextMeshProUGUI initialText =
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        
        initialText.color = Color.blue;

        index = 0;
    }

    void Update()
    {
        TextMeshProUGUI currentText =
            transform.GetChild(index).gameObject.GetComponent<TextMeshProUGUI>();
        
        currentText.color = Color.black;

        if (Input.GetKeyDown("down") && index < 1)
            index += 1;
        else if (Input.GetKeyDown("up") && index > 0)
            index -= 1;

        TextMeshProUGUI newText =
            transform.GetChild(index).gameObject.GetComponent<TextMeshProUGUI>();
        
        newText.color = Color.blue;

        if (Input.GetKeyDown("return"))
        {
            switch (index)
            {
                case 0:
                    Mario player = GameObject.Find("Mario").GetComponent<Mario>();
                    player.Retry();
                    break;
                case 1:
                    Time.timeScale = 1;
                    SceneManager.LoadScene("Main Menu");
                    break;
            }
        }
    }
}
