using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("GetMouseButtonUp");
        //    if (AppManager.actionState == ActionState.none)
        //    {
                
        //        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
                
        //        if (targetObject)
        //        {
        //            screenPoint = Camera.main.WorldToScreenPoint(targetObject.gameObject.transform.position);
        //            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        //            //selectedObject = targetObject.transform.gameObject;                    
        //            EventManager.gameplayManager_SetSelectedCard(targetObject.transform.gameObject);
        //        }
        //    }
        //    Debug.Log("asas");

        //}


        //if (AppManager.actionState == ActionState.Dragging)
        //{
        //    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        //    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        //    EventManager.gameplayManager_DragCard(curPosition);
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    Debug.Log("GetMouseButtonUp");
        //    if (AppManager.actionState == ActionState.Dragging)
        //    {
        //        AppManager.actionState = ActionState.none;
        //    }
        //    else
        //    {
        //        AppManager.actionState = ActionState.none;
        //    }
        //}

        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
        //    if (targetObject)
        //    {
        //        Debug.Log(targetObject.name);
        //        //selectedObject = targetObject.transform.gameObject;
        //        AppManager.actionState = ActionState.Dragging;
        //    }
        //}

        //if (Input.GetMouseButtonUp(0) && selectedObject)
        //{
        //    //selectedObject = null;
        //    AppManager.actionState = ActionState.Dragging;
        //}
    }

    
}
