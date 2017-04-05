using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SubItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public bool introAnimationDone;
    private float ANIMATION_DURATION = 0.5f;
    private Vector3[] subitemsPositions;
    int id;
    float initTime;
    LineRenderer lineR;
    private bool isGazedAt;

    // Use this for initialization
    void Awake()
    {
        EventManager.StartListening(EventManager.GAZE_COMPLETED, onItemSelected);

        subitemsPositions = new Vector3[] {
            new Vector3(1.4f,1,1.2f),//2
            new Vector3(1.3f,.8f,-1.3f),//8
            new Vector3(1.4f,-1,1),//4
            new Vector3(1.4f,-1.1f,-.9f),//6
            new Vector3(1.6f,1.5f,0),//1
            new Vector3(1.6f,-1.5f,.2f),//5
            new Vector3(1.5f,0,1.5f),//3
            new Vector3(1.5f,-.3f,-1.6f)//7
            
        };
        
        lineR = gameObject.GetComponent<LineRenderer>();
        lineR.SetPosition(0, Vector3.zero);

    }

    private void onItemSelected()
    {
        if (!isGazedAt) return;

        SceneManager.LoadScene(1);//ShowRoomScene
    }

    public void IntroAnimation(int id) {
        initTime = Time.time;
        this.id = id;
        transform.localPosition = Vector3.zero;
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        transform.rotation = Quaternion.Inverse(transform.rotation);
        lineR.SetPosition(0, Vector3.zero);
        Action<object> onCompleteAction = ommak;
        //LeanTween.moveLocal(gameObject, subitemsPositions[id], ANIMATION_DURATION).setOnComplete(onCompleteAction, gameObject);
    }

    private void ommak(object item)
    {
        introAnimationDone = true;
        
        /*return;
        GameObject subItem = item as GameObject;
        if (subItem)
        {
            lineR.SetPosition(0, Vector3.zero);
            lineR.SetPosition(1, subItem.transform.InverseTransformPoint(transform.parent.position));
        }*/
    }
    // Update is called once per frame
    void Update () {
        //print("Time.time:"+Time.time);
        if (introAnimationDone)
        {
            transform.localPosition = subitemsPositions[id] + new Vector3(0.0f, Mathf.Cos(Time.time-initTime) * .05f, Mathf.Sin(Time.time - initTime) * .01f);
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
        Destroy(gameObject);
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
