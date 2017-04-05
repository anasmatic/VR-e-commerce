using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OdaDataItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public bool introAnimationDone;
    private float ANIMATION_DURATION = 0.5f;
    float initTime;
    Vector3 finalPos;
    LineRenderer lineR;
    private bool isGazedAt;

    // Use this for initialization
    void Awake()
    {
        EventManager.StartListening(EventManager.GAZE_COMPLETED, onItemSelected);
        finalPos = transform.localPosition;

        lineR = gameObject.GetComponent<LineRenderer>();
        lineR.SetPosition(0, Vector3.zero);

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
        lineR.SetPosition(0, Vector3.zero);
        Action<object> onCompleteAction = ommak;
        LeanTween.moveLocal(gameObject, finalPos, ANIMATION_DURATION).setOnComplete(onCompleteAction, gameObject).setDelay(delay);
    }

    private void ommak(object item)
    {
        initTime = Time.time;
        introAnimationDone = true;
        
        //return;
        //GameObject subItem = item as GameObject;
        //if (subItem)
        //{
        //    lineR.SetPosition(0, Vector3.zero);
        //    lineR.SetPosition(1, subItem.transform.InverseTransformPoint(transform.parent.position));
        //}
    }

    // Update is called once per frame
    void Update () {
        //print("Time.time:"+Time.time);
        if (introAnimationDone)
        {
            transform.localPosition = finalPos + new Vector3(0.0f, Mathf.Cos(Time.time-initTime) * .05f, Mathf.Sin(Time.time - initTime) * .01f);
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
