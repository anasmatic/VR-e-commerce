using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FootIDs
{
    NoGroup = -1,//DONOT tag items with 'NoGroup' FootID with tag 'Group'
    _0 = 0,_1 = 1,_2 = 2,_3 = 3,_4 = 4,_5 = 5,_6 = 6,_7 = 7,_8 = 8,_9 = 9,
    _10 = 10, _11 = 11, _12 = 12, _13 = 13, _14 = 14, _15 = 15, _16 = 16, _17 = 17, _18 = 18, _19 = 19,
    _20 = 20, _21 = 21, _22 = 22, _23 = 23, _24 = 24, _25 = 25, _26 = 26, _27 = 27, _28 = 28, _29 = 29,
    _30 = 30, _31 = 31, _32 = 32, _33 = 33, _34 = 34, _35 = 35, _36 = 36, _37 = 37, _38 = 38, _39 = 39,
    Door = 44,
    StairsGoUp = 551,//DONOT tag items with 'StairsGoUp' FootID with tag 'Group'
    StairsGoDown = 552,//DONOT tag items with 'StairsGoDown' FootID with tag 'Group'
    StairsWay = 555,//DONOT tag items with 'StairsWay' FootID with tag 'Group'
    GroupsCount = 45
}

public class FootOdaManger : MonoBehaviour {

    public GameObject[] groupTV;
    public GameObject[] groupMobile;
    public GameObject[] groupConsole;
    public GameObject[] groupPC;
    int currentId = -1;
    FootOdaItem currentOdaItem;
    private GameObject[] groups = new GameObject[(int)FootIDs.GroupsCount];
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
            if ((int)currentOdaItem.groupID == currentId)//active if only same group we are browsing now
                currentOdaItem.ActivateCollider();
        }

        currentOdaItem = newItem.GetComponent<FootOdaItem>();
    }

    // Use this for initialization
    void Start () {
        //Enum IDs is the refrance of these indexs
        /*groups[(int)FootIDs._0 ] = groupTV;
        groups[(int)FootIDs.Mobile ] = groupMobile;
        groups[(int)FootIDs.Console ] = groupConsole;
        groups[(int)FootIDs.PC ] = groupPC;*/
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("Group");
        for (int i = 0; i < allItems.Length; i++)
        {
            groups[(int)allItems[i].GetComponent<FootOdaGroup>().groupID] = allItems[i];
        }
        for (int i = 0; i < groups.Length; i++)
        {
            SetGroupIDsInItems(i);
            DeactivateGroup(i);
        }
    }

    private void SetGroupIDsInItems(int groupId)
    {
        if (groupId > -1 && groupId < groups.Length && groups[groupId] != null)
        {
            foreach (FootOdaItem item in groups[groupId].GetComponentsInChildren<FootOdaItem>())
            {
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
        if (groupId > -1 && groupId < groups.Length && groups[groupId] != null)
        {
            foreach (FootOdaItem item in groups[groupId].GetComponentsInChildren<FootOdaItem>())
            {
                item.ActivateCollider();
            }
        }
    }
    private void DeactivateGroup(int groupId)
    {
        if (groupId > -1 && groupId < groups.Length && groups[groupId] != null)
        {
            foreach (FootOdaItem item in groups[groupId].GetComponentsInChildren<FootOdaItem>())
            {
                item.DeactivateCollider();
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
