using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FootOdaDataItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private bool introAnimationDone = false;
    private float ANIMATION_DURATION = 0.5f;
    private float delay;
    float initTime;
    Vector3 finalPos;
    LineRenderer lineR;
    private bool isGazedAt;

    // Use this for initialization
    void OnEnable()
    {
        //EventManager.StartListening(EventManager.GAZE_COMPLETED, onItemSelected);
        finalPos = transform.localPosition;
        AssiagnLineR();
    }

    void AssiagnLineR()
    {
        lineR = gameObject.GetComponent<LineRenderer>();
        
        if (gameObject.name == "Desc")
        {
            lineR.SetPosition(0, new Vector3(0, -70, 0));
        }
        else if (gameObject.name == "PricePanel")
        {
            lineR.SetPosition(0, new Vector3(0.7f, 0, 0));
        }
        else {
            lineR.SetPosition(0, Vector3.zero);
        }

    }

    private void onItemSelected()
    {
        if (!isGazedAt) return;

        SceneManager.LoadScene(1);//ShowRoomScene
    }

    public void IntroAnimation(float delay) {
        
        transform.localPosition = Vector3.zero;
        //        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        //        transform.rotation = Quaternion.Inverse(transform.rotation);
        AssiagnLineR();
        this.delay = delay;
        print("this.delay "+ this.delay);
        LeanTween.moveLocal(gameObject, finalPos, ANIMATION_DURATION).setOnComplete(onCompleteAction).setDelay(delay);
    }

    private void onCompleteAction(object item)
    {
        initTime = Time.time;
        introAnimationDone = true;
    }

    // Update is called once per frame
    void Update () {
        if (introAnimationDone)
        {
            gameObject.transform.localPosition = finalPos + new Vector3(0.0f, Mathf.Cos(Time.time-initTime) * .05f, Mathf.Sin(Time.time - initTime) * .01f);
        }
        lineR.SetPosition(1, gameObject.transform.InverseTransformPoint(transform.parent.position));
    }

    internal void ExitAnimation()
    {
        Action<object> onCompleteAction = KillMe;
        LeanTween.moveLocal(gameObject, Vector3.zero, ANIMATION_DURATION).setOnComplete(KillMe);
    }

    private void KillMe(object obj)
    {
        //Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == gameObject)
            isGazedAt = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == gameObject)
            isGazedAt = false;
    }
}
