using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandableCollapsabe : MonoBehaviour {

    private bool _isExpaneded;
    public bool isExpaneded
    {
        get
        {
            return this._isExpaneded;
        }
        set
        {
            this._isExpaneded = value;
        }
    }

    private bool _isCollapsed;
    public bool isCollapsed
    {
        get
        {
            return this._isCollapsed;
        }
        set
        {
            this._isCollapsed = value;
        }
    }

    internal readonly int STATE_FAIL = 0;
    internal readonly int STATE_EXPANDED = 2;
    internal readonly int STATE_COLLAPSED = 1;

    public int GetState()
    {
        if (isCollapsed && !isExpaneded)
            return STATE_COLLAPSED;
        if (!isCollapsed && isExpaneded)
            return STATE_EXPANDED;

        return STATE_FAIL;
    }
    /*
    use in successful gaze complete, to cahnge the item state 
    */
    public void toggleState()
    {
        if (isCollapsed && !isExpaneded)
        {
            isExpaneded = true;
            isCollapsed = false;
        }
        else if (!isCollapsed && isExpaneded)
        {
            isExpaneded = false;
            isCollapsed = true;
        }
    }
}
