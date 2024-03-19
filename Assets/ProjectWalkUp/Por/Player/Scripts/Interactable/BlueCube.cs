using UnityEngine;

public class BlueCube : Interactable
{
    [SerializeField]
    private Material materialColor;

    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        gameObject.GetComponent<Renderer>().material = materialColor;
    }
}
