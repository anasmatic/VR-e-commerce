using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CategoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //private Explodable _exploadable;

    private SpriteRenderer mSpriteRenderer;
    private CategoriesManger mParent;
    private GameObject player;
    private Vector3 exitVector = new Vector3(0, -20, 0);
    public int id = 0;
    private float distanceFromPlayer;
    private float inteactivityDistance;
    private bool isGazedAt;
    private bool isExitAnimationMode = false;
    private bool isChosenAnimationMode = false;

    // Use this for initialization
    void Start()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        mParent = GetComponentInParent<CategoriesManger>();
        inteactivityDistance = mParent.inteactivityDistance;
        EventManager.StartListening(EventManager.GAZE_COMPLETED, onGazeCompleted);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isGazedAt = true;
        EventManager.TriggerEvent(EventManager.GAZE_WAIT_STARTED);
    }
    
    private void onGazeCompleted()
    {
        TakeAction();
    }
    private void TakeAction()
    {
        if (isGazedAt) {
            isGazedAt = false;
            transform.GetComponent<BoxCollider>().enabled = false;
            mParent.StartExitAnimation(gameObject, id);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventManager.TriggerEvent(EventManager.GAZE_STOPPED);
        
        isGazedAt = false;
    }

    internal void ExitAnimation()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        isExitAnimationMode = true;
    }

    internal void ChosenAnimation()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(ChosenAnimationWait());
    }
    internal IEnumerator ChosenAnimationWait()
    {
        yield return new WaitForSeconds(0.5f);
        isChosenAnimationMode = true;
    }
    // Update is called once per frame
    void Update () {
        if (isChosenAnimationMode)
        {
            Vector3 pos = player.transform.position;
            pos.z += 2;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10 * Time.deltaTime);
            mSpriteRenderer.color = new Color(1, 1, 1, (mSpriteRenderer.color.a - 0.01f));
            if (Vector3.Distance(transform.position, player.transform.position) < 1)
            {
                mParent.clear();
                OnPointerExit(null);
                isChosenAnimationMode = false;
                DestroyImmediate(gameObject);
            }
        }
        else if (isExitAnimationMode)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitVector, 20 * Time.deltaTime);
            mSpriteRenderer.color = new Color(1, 1, 1, (mSpriteRenderer.color.a - 0.001f));
            if(Vector3.Distance(transform.position,exitVector) < 5)
            {
                enabled = false;
                player.GetComponentInChildren<GvrReticlePointer>().OnPointerExit(gameObject);
                DestroyImmediate(gameObject);
            }
        }
        else
        {
            //print(GetComponentInChildren<TextMesh>().text + ", z=" + transform.localPosition.z);
            distanceFromPlayer = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
            //if (distanceFromPlayer < GetComponentInParent<CategoriesManger>().inteactivityDistance)
            //{
            mSpriteRenderer.color = new Color(1, 1, 1, inteactivityDistance / distanceFromPlayer);
            //}
            if (mSpriteRenderer.color.a > .8)
                transform.GetComponent<BoxCollider>().enabled = true;
            else
                transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
