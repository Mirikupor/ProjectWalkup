using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class MouseUI : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MouseLockedState()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MouseNoneStat()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
