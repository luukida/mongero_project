using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMovement : MonoBehaviour
{
    public static JoystickMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<JoystickMovement>();
                if(instance == null)
                {
                    var instanceContainer = new GameObject("JoystickMovement");
                    instance = instanceContainer.AddComponent<JoystickMovement>();
                }
            }
            return instance;
        }
    }

    public bool isPlayerMoving;

    private static JoystickMovement instance;

    public GameObject smallStick;
    public GameObject bGStick;
    Vector3 stickFirstPosition;
    public Vector3 joyVec;
    Vector3 joystickFirstPosition;
    float stickRadius;

    void Start()
    {
        stickRadius = bGStick.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        joystickFirstPosition = bGStick.transform.position;
    }

    public void PointDown ()
    {
        bGStick.gameObject.SetActive(true);
        bGStick.transform.position = Input.mousePosition;
        smallStick.transform.position = Input.mousePosition;
        stickFirstPosition = Input.mousePosition;
        Debug.Log("PointDown");
        //SetTrigger
    }

    public void Drag (BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 DragPosition = pointerEventData.position;
        joyVec = ( DragPosition - stickFirstPosition ).normalized;

        float stickDistance = Vector3.Distance(DragPosition, stickFirstPosition);

        if(stickDistance < stickRadius)
        {
            //Debug.Log("if (stickRadius < 50)");
            smallStick.transform.position = stickFirstPosition + joyVec * stickDistance;
        }
        else
        {
            //Debug.Log("else");
            smallStick.transform.position = stickFirstPosition + joyVec * stickRadius;
        }

        isPlayerMoving = true;
    }

    public void Drop ()
    {
        bGStick.gameObject.SetActive(false);
        joyVec = Vector3.zero;
        bGStick.transform.position = joystickFirstPosition;
        smallStick.transform.position = joystickFirstPosition;
        Debug.Log("Drop");
        //SetTrigger

        isPlayerMoving = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
