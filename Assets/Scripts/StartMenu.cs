﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private int index;

    void Start()
    {
        Cursor.visible = false;

        TextMeshProUGUI initialText =
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        
        initialText.color = Color.yellow;

        index = 0;
    }

    void Update()
    {
        TextMeshProUGUI currentText =
            transform.GetChild(index).gameObject.GetComponent<TextMeshProUGUI>();
        
        currentText.color = Color.white;

        if (Input.GetKeyDown("down") && index < 1)
            index += 1;
        else if (Input.GetKeyDown("up") && index > 0)
            index -= 1;

        TextMeshProUGUI newText =
            transform.GetChild(index).gameObject.GetComponent<TextMeshProUGUI>();
        
        newText.color = Color.yellow;

        if (Input.GetKey("return"))
            SceneManager.LoadScene(index + 1);
    }
}