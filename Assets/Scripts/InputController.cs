using System;using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] 
    private float speed;
    [SerializeField] 
    private float distance;

    [Space] 
    [SerializeField] private float swipeDistance;
    
    private float yPos;
    private float zPos;

    private Coroutine currentControlCoroutine;

    private ControlType inputType = ControlType.Keyboard;
    void Start()
    {
        yPos = transform.position.y;
        zPos = transform.position.z;

        if (PlayerPrefs.HasKey(Constants.InputTypeIndex))
        {
            inputType = Constants.controlsDictionary.First(x => x.Key == PlayerPrefs.GetInt(Constants.InputTypeIndex)).Value;
        }
        
        StartControlWithType(inputType);

        UIController.onChangeControlType += StartControlWithType;
    }

    private IEnumerator SwipeControlRoutine()
    {
        var neededVector = new Vector3();
        var startPosX = 0f;
        var swipe = false;
        var swipeStarted = false;
        
        var startDragPos = Vector3.zero;
        var endDragPos = Vector3.zero;
        
        while (true)
        {
            if (Input.GetMouseButtonDown(0)
            &&  !swipeStarted)
            {
                startDragPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
                swipeStarted = true;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                endDragPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));

                if ((startDragPos - endDragPos).magnitude > 1f)
                {
                    neededVector = startDragPos - endDragPos;
                    startPosX = transform.position.x;
                    swipe = true;
                    swipeStarted = false;
                }
            }

            if (swipe)
            {
                transform.position = neededVector.x < 0f ?
                    new Vector3(Mathf.Lerp(transform.position.x, startPosX + swipeDistance, speed * Time.deltaTime), yPos, zPos):
                    new Vector3(Mathf.Lerp(transform.position.x, startPosX - swipeDistance, speed * Time.deltaTime), yPos, zPos);
            }
            
            if (transform.position.x > 6)
            {
                transform.position = new Vector3(6f, yPos, zPos);
            }
                
            if (transform.position.x < -6)
            {
                transform.position = new Vector3(-6f, yPos, zPos);
            }

            yield return null;
        }
    }
    
    private IEnumerator DragControlRoutine()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
            {
                var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, mousePos.x * distance, speed * Time.deltaTime), yPos , zPos);
                
                if (transform.position.x > 6)
                {
                    transform.position = new Vector3(6f, yPos, zPos);
                }
                
                if (transform.position.x < -6)
                {
                    transform.position = new Vector3(-6f, yPos, zPos);
                }
            }

            yield return null;
        }
    }
    
    private IEnumerator KeyboardControlRoutine()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.position = transform.position.x > -6 ? 
                    new Vector3(Mathf.Lerp(transform.position.x, transform.position.x - distance, speed * Time.deltaTime), yPos, zPos) : 
                    new Vector3(-6, yPos, zPos);
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position = transform.position.x < 6 ? 
                    new Vector3(Mathf.Lerp(transform.position.x, transform.position.x + distance, speed * Time.deltaTime), yPos, zPos) : 
                    new Vector3(6, yPos, zPos);
            }
        
            yield return null;
        }
    }

    private void StartControlWithType(ControlType type)
    {
        if (currentControlCoroutine != null)
        {
            StopCoroutine(currentControlCoroutine);
        }

        currentControlCoroutine = type switch
        {
            ControlType.Swipe => StartCoroutine(SwipeControlRoutine()),
            ControlType.Drag => StartCoroutine(DragControlRoutine()),
            ControlType.Keyboard => StartCoroutine(KeyboardControlRoutine()),
            _ => currentControlCoroutine
        };
    }
}

public enum ControlType
{
    Swipe,
    Drag,
    Keyboard
}
