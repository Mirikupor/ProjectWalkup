using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCube : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
