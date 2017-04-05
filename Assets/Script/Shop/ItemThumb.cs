using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemThumb : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Transform player;
    private ItemsManger mParent;
    public SpriteRenderer itemImg;
    public TextMesh itemTitle;
    public TextMesh itemPrice;
    private bool isGazedAt;
    private int countdownNumber = 3;//TODO replace with gaze circle laoder

    // Use this for initialization
    void Start () {
        mParent = GetComponentInParent<ItemsManger>();
        player = mParent.player;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(gazeWait());
    }

    private IEnumerator gazeWait()
    {
        CancelInvoke("countdown");
        print("selected : " + GetComponentInChildren<TextMesh>().text);
        isGazedAt = true;
        InvokeRepeating("countdown", 0, 1);
        yield return new WaitForSeconds(3f);
        countdownNumber = 3;
        player.GetComponentInChildren<TextMesh>().text = "";
        CancelInvoke("countdown");
        if (isGazedAt)
        {
            mParent.ItemSelected(0);//to do pass id to featch data
        }
        //GazeInputModule.gazePointer.OnGazeExit(null, null);
    }

    private void countdown()
    {
        player.GetComponentInChildren<TextMesh>().text = "" + countdownNumber;
        countdownNumber--;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isGazedAt = false;
        CancelInvoke("countdown");
        player.GetComponentInChildren<TextMesh>().text = "";
        countdownNumber = 3;
    }
}
