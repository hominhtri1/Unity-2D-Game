using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SplashScreen : MonoBehaviour
{
    public GameObject mainMenu;

    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            TextMeshProUGUI text =
                transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        
            text.text = "";

            Instantiate(mainMenu, new Vector3(0, 0, -1), Quaternion.identity);
        }
    }
}
