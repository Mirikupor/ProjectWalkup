using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeUI;

    void Update()
    {
        timeUI.text = DateTime.Now.ToString("hh:mm tt");
    }
}
