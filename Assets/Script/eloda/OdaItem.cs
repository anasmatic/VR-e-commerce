using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OdaItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public DataManger dataContainer;
    ExpandableCollapsabe expandableCollapsabe;
    public int groupID;
    private bool isGazedAt;

    // Use this for initialization
    void Start () {
        EventManager.StartListening(EventManager.GAZE_COMPLETED, onItemSelected);
        expandableCollapsabe = gameObject.AddComponent<ExpandableCollapsabe>();
        expandableCollapsabe.isCollapsed = true;
        
    }
	
    public void DeactivateCollider()
    {
        BoxCollider[] boxColls = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider i in boxColls)
        {
            i.enabled = false;
        }
    }

    public void ActivateCollider()
    {
        BoxCollider[] boxColls = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider i in boxColls)
        {
            i.enabled = true;
        }
    }

    private void onItemSelected()
    {
        if (!isGazedAt) return;

        //EventManager.TriggerEvent(EventManager.NEW_MAIN_ITEM_SELECTED, gameObject);
        if (expandableCollapsabe.GetState() == expandableCollapsabe.STATE_COLLAPSED)
        {
            dataContainer.gameObject.SetActive(true);
            dataContainer.Entro();
            DeactivateCollider();
            EventManager.TriggerEvent(EventManager.NEW_MAIN_ITEM_SELECTED, gameObject);
            //TODO: check if can rotate
        }
        else if (expandableCollapsabe.GetState() == expandableCollapsabe.STATE_EXPANDED)
        {
            //CollapseAnimation();
        }
        expandableCollapsabe.toggleState();
    }

    internal void SetGroupID(int groupId)
    {
        groupID = groupId;
    }

    public void CollapseAnimation()
    {
        dataContainer.Outro();
    }


    // Update is called once per frame
    void Update () {
		
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
