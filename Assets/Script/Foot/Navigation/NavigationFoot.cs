using UnityEngine;
using System.Collections;
using System;

public class NavigationFoot : MonoBehaviour 
{
	//the viewer's game object
	public GameObject view_object	= null;

	//speed at which we move between waypoints
	public float speed				= 5f;

	//a list of all waypoints
	private WaypointFoot[]	_waypoints;

	//the current waypoint
	public WaypointFoot	_current;
    private float view_object_init_Y;

    void Start () 
	{
        EventManager.StartListening(EventManager.GOING_DOWN_STAIRS, onGoingDownSpecialCase);
		//first, if the view object is null, use the camera object
		if(view_object == null)
		{
			view_object = Camera.main.gameObject;
		}
        view_object_init_Y = view_object.transform.position.y;


        //now, find all the waypoints that have been placed in the scene
        _waypoints						= FindAll();

			
		
        if(_current)
		    //set that waypoint to occupied
		    _current.Occupy();
        else
            //and search them for the one nearest to the view object
            _current						= Nearest();



        //and warp the viewpoint to the currently occupied position 
        view_object.transform.position	= _current.position;


        //finally, update the rest of the waypoints to reflect their active / inactive status
        UpdateAll();
    }

    private void onGoingDownSpecialCase()
    {
        isGoingDown = true;
    }

    void Update () 
	{
		if( _waypoints.Length > 0)
		{
			//if so, check all the waypoints to see if one of them has been hit
 			for(int i = 0; i < _waypoints.Length; i++)
			{
				//if a waypoint has been hit, it's an active waypoint, and the person is pressing the trigger, activate it
				if(_waypoints[i].triggered)
				{
					//exit the current waypoint
                    if(_current)
					    _current.Depart();
			
					//set the current waypoint to be the new waypoint
					_current	= _waypoints[i];
						
					//update all the waypoints to reflect their new active/inactive status
					UpdateAll();
				}
			}	
			
			//if the current waypoint isn't occupied (ie, it has been changed) and we aren't already on it, move towards it
			if(_current && _current.occupied == false && view_object.transform.position != _current.position)
			{
				MoveTo(_current);
			}
		}
	}

	//finds all the waypoint prefabs in the scene (tagged as "Waypoint") and puts them in an array
 	public WaypointFoot[] FindAll() 
	{	
		GameObject[] waypoint_object 	= GameObject.FindGameObjectsWithTag("Waypoint");
		
		WaypointFoot[] waypoint 			= new WaypointFoot[waypoint_object.Length];

		for(int i = 0; i < waypoint_object.Length; i++)
		{
			waypoint[i] = waypoint_object[i].GetComponent<WaypointFoot>();
		}
		
		return waypoint;
	}

    float angel = 0;
    private bool isGoingUp = false;
    private bool isGoingDown = false;

    //moves the player to the current waypoint - if the player is within .05 it snaps them directly on it
    public void MoveTo(WaypointFoot waypoint)
	{
		float distance = Vector3.Distance(view_object.transform.position, waypoint.position);
		if(distance > 0.5f)
		{
            //view_object.transform.position = Vector3.Lerp(view_object.transform.position, waypoint.position, 1.2f*Time.deltaTime);
            view_object.transform.position = Vector3.MoveTowards(view_object.transform.position, waypoint.position, 5 * Time.deltaTime);
            if (distance > 1f)
            {
                angel += .3f;
                float view_object_y = view_object.transform.position.y /*+ view_object_init_Y */- (Mathf.Sin(angel) * .05f);
               // print("view_object_y :"+view_object_y+", lerp_Y"+ view_object.transform.position+", wayPos:"+ waypoint.position);
                view_object.transform.position = new Vector3(view_object.transform.position.x, view_object_y, view_object.transform.position.z);
            }
            
        }
		else
		{
			view_object.transform.position = waypoint.position;
			
			_current.Occupy();

            UpdateAll();
            //stairs :
            if (_current.id == FootIDs.StairsGoUp)
            {
                if (isGoingDown)
                {
                    isGoingDown = false;
                    //you have arrived, stop !
                }
                else
                {
                    isGoingUp = true;
                    _current.neighborhood[0].triggered = true;
                    MoveTo(_current.neighborhood[0]);
                }
            }
            else if (_current.id == FootIDs.StairsWay && isGoingUp)
            {
                _current.neighborhood[1].triggered = true;
                MoveTo(_current.neighborhood[1]);
            }
            else if (_current.id == FootIDs.StairsWay && isGoingDown)
            {
                _current.neighborhood[0].triggered = true;
                MoveTo(_current.neighborhood[0]);
            }
            else if (_current.id == FootIDs.StairsGoDown)
            {
                if (isGoingUp)
                {
                    isGoingUp = false;
                    //you have arrived, stop !
                }
                else
                {
                    isGoingDown = true;
                    _current.neighborhood[0].triggered = true;
                    MoveTo(_current.neighborhood[0]);
                }
            }

		}
	}


	//this searches all the waypoints to find the one closest to the view
	public WaypointFoot Nearest () 
	{
		int nearest_waypoint_index	= 0;
		float distance_to_nearest	= float.PositiveInfinity;

		for(int i = 0; i < _waypoints.Length; i++)
		{
			float distance_to_waypoint = Vector3.Distance(view_object.transform.position, _waypoints[i].position);
			
			if(distance_to_waypoint < distance_to_nearest)
			{
				nearest_waypoint_index	= i;
				distance_to_nearest 	= distance_to_waypoint;
			}
		}
		
		return _waypoints[nearest_waypoint_index];
	}


	//this tells all the waypoint prefabs to update their status
	public void UpdateAll()
	{
		for(int i = 0; i < _waypoints.Length; i++)
		{
			_waypoints[i].UpdateActivation();
		}		
	}
}
