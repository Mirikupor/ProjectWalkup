using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    //List<RaycastResult> results = new List<RaycastResult>();

    private void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = new Vector2(Screen.width / 2, Screen.height / 2);
            EventSystem.current.RaycastAll(data, results);
            foreach (var result in results)
            {
                ExecuteEvents.ExecuteHierarchy<ISubmitHandler>( result.gameObject, data, ExecuteEvents.submitHandler );
            }
        }*/
    }
}
