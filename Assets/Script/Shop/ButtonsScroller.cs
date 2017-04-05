using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsScroller : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler {

    public Button buttonRight;
    public Button buttonLeft;
    public Transform manger;
    private float step = 30f;
    private const int STEP = 30;
    private bool isRight = false;
    private bool isLeft = false;


    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation;
        if (isLeft || isRight)
        {
            if (isLeft)
            {
                step = STEP * -1;
            }
            else if (isRight)
            {
                step = STEP;
            }
        }
        else
        {
            step *= .9f;
        }
        if (Math.Abs(step) > 0.05f)
        {
            targetRotation = manger.rotation * Quaternion.Euler(new Vector3(0, (step), 0));
            manger.rotation = Quaternion.Slerp(manger.rotation, targetRotation, Time.deltaTime);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (eventData.pointerEnter == buttonLeft.gameObject){
            isRight = false;
            isLeft = true;
        }else if (eventData.pointerEnter == buttonRight.gameObject)
        {
            isLeft = false;
            isRight = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isLeft = false;
        isRight = false;
    }
    
}
