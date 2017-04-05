using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemInAisleDetails : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject exitButton;
    public GameObject addButton;
    public Transform _3dModel;
    private bool isGazedAt;
    private int countdownNumber;
    private Transform player;
    //private ItemsManger mParent;
    private Aisle mParent;
    private bool isExitAnimationMode = false;
    private bool isEnterAnimationMode = false;
    private static Vector3 startAnimationPos = new Vector3(0, 10, 10f);
    private static Vector3 initPos;


    // Use this for initialization
    void Start()
    {
        initPos = transform.position;
        //mParent = GetComponentInParent<ItemsManger>();
        mParent = GetComponentInParent<Aisle>();
        if(mParent)
            player = mParent.player;
        //transform.position = startAnimationPos;
        isEnterAnimationMode = true;
        EventManager.StartListening(EventManager.GAZE_COMPLETED, onGazeCompleted);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isGazedAt = true;
        EventManager.TriggerEvent(EventManager.GAZE_WAIT_STARTED);
    }
    
    private void onGazeCompleted()
    {
        if (isGazedAt)
        {
            EventManager.TriggerEvent(EventManager.EXIT_DETAILS);
            isExitAnimationMode = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isGazedAt = false;
        EventManager.TriggerEvent(EventManager.GAZE_STOPPED);
    }

	// Update is called once per frame
	void Update () {
        if (isEnterAnimationMode) {
            //transform.RotateAround(player.position, Vector3.left, 100 * Time.deltaTime);
            //if(Vector3.Distance( transform.position, initPos) < 0.3)
            //{
                isEnterAnimationMode = false;
            //}
            
            /*transform.position = Vector3.MoveTowards(transform.position, exitVector, 20 * Time.deltaTime);
            mSpriteRenderer.color = new Color(1, 1, 1, (mSpriteRenderer.color.a - 0.001f));
            if (Vector3.Distance(transform.position, exitVector) < 5)
            {
                DestroyImmediate(gameObject);
            }*/
        } else if (isExitAnimationMode){
            //transform.RotateAround(player.position, Vector3.right, 100 * Time.deltaTime);
            //if (Vector3.Distance(transform.position, startAnimationPos) < 5)
            //{
                isExitAnimationMode = false;
                DestroyImmediate(gameObject);
            //}
        }
        if (_3dModel)
        {
            _3dModel.RotateAround(_3dModel.position, Vector3.up, 20 * Time.deltaTime);
        }
	}
}
