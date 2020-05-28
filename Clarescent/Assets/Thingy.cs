﻿using System.Collections;
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

    public void UpdateText()
    {
        switch(setting.controller)
        {
            case Controller.Keyboard:
                pro.text = "Keyboard\n& Mouse";
                break;
            case Controller.Controller:
                pro.text = "PS4/XBOX";
                break;
            case Controller.Unique:
                pro.text = "Joycons";
                break;
        }
    }

}
