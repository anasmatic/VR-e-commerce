using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IDs
{
    TV = 0,
    Mobile = 1,
    PC = 2,
    Console = 3,
    Door = 4,
    Stairs = 5
}

public class OdaManger : MonoBehaviour {

    public GameObject[] groupTV;
    public GameObject[] groupMobile;
    public GameObject[] groupConsole;
    public GameObject[] groupPC;
    int currentId = -1;
    OdaItem currentOdaItem;
    private GameObject[][] groups = new GameObject[4][];
    void Awake()
    {
        EventManager.StartListening(EventManager.WAYPOINT_ACTIVATE, activateObjectAtWaypoint);
        EventManager.StartListening(EventManager.NEW_MAIN_ITEM_SELECTED, OnNewMainItemSelected);
    }

    private void OnNewMainItemSelected(GameObject newItem)
    {
        if (currentOdaItem)
        {
            currentOdaItem.CollapseAnimation();
            if (currentOdaItem.groupID == currentId)//active if only same group we are browsing now
                currentOdaItem.ActivateCollider();
        }

        currentOdaItem = newItem.GetComponent<OdaItem>();
    }

    // Use this for initialization
    void Start () {
        //Enum IDs is the refrance of these indexs
        groups[(int)IDs.TV ] = groupTV;
        groups[(int)IDs.Mobile ] = groupMobile;
        groups[(int)IDs.Console ] = groupConsole;
        groups[(int)IDs.PC ] = groupPC;

        for (int i = 0; i < groups.Length; i++)
        {
            SetGroupIDsInItems(i);
            DeactivateGroup(i);
        }
    }

    private void SetGroupIDsInItems(int groupId)
    {
        if (groupId > -1)
        {
            OdaItem item;
            foreach (GameObject i in groups[groupId])
            {
                item = i.GetComponent<OdaItem>();
                item.SetGroupID(groupId);
            }
        }
    }

    private void activateObjectAtWaypoint(int id)
    {
        DeactivateGroup(currentId);
        currentId = id;
        ActivateGroup(currentId);
    }

    private void ActivateGroup(int groupId)
    {
        if (groupId > -1)
        {
            OdaItem item;
            foreach (GameObject i in groups[groupId])
            {
                item = i.GetComponent<OdaItem>();
                item.ActivateCollider();
            }
        }
    }
    private void DeactivateGroup(int groupId)
    {
        if (groupId > -1)
        {
            OdaItem item;
            foreach (GameObject i in groups[groupId])
            {
                item = i.GetComponent<OdaItem>();
                item.DeactivateCollider();
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
