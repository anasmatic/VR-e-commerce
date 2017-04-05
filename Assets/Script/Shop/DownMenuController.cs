using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DownMenuController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Button homeButton;
    public CategoriesManger categoriesManger;
    //public ItemsManger itemsManger;
    public Aisle itemsManger;
    private GameObject gazedBtn;
    private bool isGazedAt = false;

    // Use this for initialization
    void Start()
    {
        homeButton.gameObject.SetActive(false);
        EventManager.StartListening(EventManager.GAZE_COMPLETED, onGazeCompleted);
    }

    private void onGazeCompleted()
    {
        if (gazedBtn)
        {
            gazedBtn.SetActive(false);
            print("gazedBtn : " + gazedBtn);
            if (gazedBtn == homeButton.gameObject)
            {
                StartCoroutine(gazeDone());
            }
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isGazedAt = true;
        gazedBtn = eventData.pointerEnter;
        EventManager.TriggerEvent(EventManager.GAZE_WAIT_STARTED);
    }

    private IEnumerator gazeDone()
    {
        //TODO:use Event
        itemsManger.HideWithAnimation(true);
        categoriesManger.drawCategoriesAsCircle();
        yield return new WaitForSeconds(0.5f);
        itemsManger.DeleteAllItems();
    }


    private void TakeAction()
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventManager.TriggerEvent(EventManager.GAZE_STOPPED);
        gazedBtn = null;
    }

	
	// Update is called once per frame
	void Update () {
		
	}

    internal void hideHomeButton()
    {
        homeButton.gameObject.SetActive(false);
    }
    internal void showHomeButton()
    {
        homeButton.gameObject.SetActive(true);
    }
}
