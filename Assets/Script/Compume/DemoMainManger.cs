using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMainManger : MonoBehaviour {

    //Computer
    //public MainItem[] mainItemsArr = new MainItem[] { };
    public MainItem currentSelection;
    // Use this for initialization
    void Start () {
        EventManager.StartListening(EventManager.NEW_MAIN_ITEM_SELECTED, OnNewMainItemSelected);
	}

    private void OnNewMainItemSelected(GameObject mainItemGameObject)
    {
        if (currentSelection)
            currentSelection.CollapseAnimation();

        currentSelection = mainItemGameObject.GetComponent<MainItem>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
