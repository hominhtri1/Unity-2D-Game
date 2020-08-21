using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private int currentIndex;
    private int previousIndex;

    void Start()
    {
        TextMeshProUGUI initialText =
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        
        initialText.color = Color.yellow;

        currentIndex = 0;
    }

    void Update()
    {
        previousIndex = currentIndex;

        if (Input.GetKeyDown("down") && currentIndex < 1)
            currentIndex += 1;
        else if (Input.GetKeyDown("up") && currentIndex > 0)
            currentIndex -= 1;
        
        TextMeshProUGUI previousText =
            transform.GetChild(previousIndex).gameObject.GetComponent<TextMeshProUGUI>();
        
        previousText.color = Color.white;

        TextMeshProUGUI currentText =
            transform.GetChild(currentIndex).gameObject.GetComponent<TextMeshProUGUI>();
        
        currentText.color = Color.yellow;

        if (Input.GetKey("return"))
            SceneManager.LoadScene(currentIndex + 1);
    }
}
