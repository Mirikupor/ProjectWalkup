using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool onEvents;

    public string propmtMessage;

    public void BaseInteract()
    {
        if (onEvents) GetComponent<InteractionEvent>().onInteract.Invoke();
        
        Interact();
    }

    protected virtual void Interact()
    {

    }
}
