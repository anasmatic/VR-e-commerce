using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemDetails : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject exitButton;
    private bool isGazedAt;
    private int countdownNumber;
    private Transform player;
    //private ItemsManger mParent;
    private Aisle mParent;
    private bool isExitAnimationMode = false;
    private bool isEnterAnimationMode = false;
    private Vector3 startAnimationPos = new Vector3(0, 0, 4.6f);
    
    // Use this for initialization
    void Start()
    {
        //mParent = GetComponentInParent<ItemsManger>();
        mParent = GetComponentInParent<Aisle>();
        player = mParent.player;
        transform.position = startAnimationPos;
        isEnterAnimationMode = true;
        EventManager.StartListening(EventManager.GAZE_COMPLETED, onGazeCompleted);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isGazedAt = true;
        EventManager.TriggerEvent(EventManager.GAZE_WAIT_STARTED);
    }

    private IEnumerator gazeWait()
    {
        yield return new WaitForSeconds(3f);
        countdownNumber = 3;
        player.GetComponentInChildren<TextMesh>().text = "";
        CancelInvoke("countdown");
        if (isGazedAt)
        {
            print("here we go "+isEnterAnimationMode + " vs "+isExitAnimationMode);
            mParent.EnableAllItems();
            isExitAnimationMode = true;
        }
        //GazeInputModule.gazePointer.OnGazeExit(null, null);
    }

    private void onGazeCompleted()
    {
        StartCoroutine(gazeWait());
    }

    private void countdown()
    {
        countdownNumber--;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isGazedAt = false;
        CancelInvoke("countdown");
        player.GetComponentInChildren<TextMesh>().text = "";
        countdownNumber = 3;
        EventManager.TriggerEvent(EventManager.GAZE_STOPPED);
    }

	// Update is called once per frame
	void Update () {
        if (isEnterAnimationMode) {
            transform.RotateAround(player.position, Vector3.left, 100 * Time.deltaTime);
            if(Vector3.Distance( transform.position, new Vector3(0, 2.4f, 5)) < 0.3)
            {
                isEnterAnimationMode = false;
            }
            /*transform.position = Vector3.MoveTowards(transform.position, exitVector, 20 * Time.deltaTime);
            mSpriteRenderer.color = new Color(1, 1, 1, (mSpriteRenderer.color.a - 0.001f));
            if (Vector3.Distance(transform.position, exitVector) < 5)
            {
                DestroyImmediate(gameObject);
            }*/
        } else if (isExitAnimationMode){
            transform.RotateAround(player.position, Vector3.right, 100 * Time.deltaTime);
            print("transform : " + transform.position);
            if (Vector3.Distance(transform.position, startAnimationPos) < 0.3)
            {
                DestroyImmediate(gameObject);
                isExitAnimationMode = false;
            }
        }
	}
}
