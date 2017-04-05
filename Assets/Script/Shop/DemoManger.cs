using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoManger : MonoBehaviour {
    public CategoriesManger categoriesManger;
    public Aisle aisle;
    // Use this for initialization
    void Start () {
        EventManager.StartListening(EventManager.SHOW_ITEMS_AISLE, showAisle);
        EventManager.StartListening(EventManager.SHOW_ITEMS_DETAILS, showItemDetails);
        EventManager.StartListening(EventManager.EXIT_DETAILS, exitItemDetails);
    }

    private void exitItemDetails()
    {
        aisle.showWithAnimation();
    }

    private void showItemDetails(GameObject item)
    {
        item.GetComponent<ItemInAisle>().enabled = false;
        aisle.ShowDetails(item);
        aisle.HideWithAnimation(false);
    }

    private void showAisle(int catID)
    {
        aisle.gameObject.SetActive(true);
        aisle.formAisle(catID);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
