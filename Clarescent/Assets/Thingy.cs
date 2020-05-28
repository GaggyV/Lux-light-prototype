using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Thingy : MonoBehaviour
{
    TextMeshProUGUI pro;
    [SerializeField] Settings setting;
    void Start()
    {
        pro = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    void UpdateText()
    {
        switch(setting.controller)
        {
            case Controller.Keyboard:
                pro.text = "Keyboard\n& Mouse";
                break;
            case Controller.Controller:
                pro.text = "Controller";
                break;
            case Controller.Unique:
                pro.text = "Joycons";
                break;
        }
    }

    private void OnMouseDown()
    {
        print("hello");
        setting.IterateController();
        UpdateText();
    }


}
