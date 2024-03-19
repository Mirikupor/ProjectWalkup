using UnityEditor;

[CustomEditor (typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;

        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.propmtMessage = EditorGUILayout.TextField("Prompt Message", interactable.propmtMessage);
            EditorGUILayout.HelpBox("EventOnlyInteract can only use UnityEvents.", MessageType.Info);

            if(interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.onEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else
        {
            base.OnInspectorGUI();

            if (interactable.onEvents)
            {
                if (interactable.GetComponent<InteractionEvent>() == null)
                    interactable.gameObject.AddComponent<InteractionEvent>();
            }
            else
            {
                if (interactable.GetComponent<Interactable>() != null)
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
            }
        }
    }
}
