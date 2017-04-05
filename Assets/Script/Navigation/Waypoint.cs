using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Waypoint : MonoBehaviour
{
    public IDs id = IDs.TV;
    public bool occupied = false;
    public bool active = true;
    public bool focused = false;
    public bool triggered = false;

    public Color active_color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
    public Color hilight_color = new Color(0.8f, 0.8f, 1.0f, 0.125f);
    public Color disabled_color = new Color(0.125f, 0.125f, .125f, 0.0f);

    public float animation_scale = 1.5f;
    public float animation_speed = 3.0f;

    private Vector3 _origional_scale = Vector3.one;

    private float _hilight = 0.0f;
    private float _hilight_fade_speed = 0.25f;

    //public Rigidbody rigid_body;
    private Material _material;

    public Vector3 position = Vector3.zero;

    public Waypoint[] neighborhood;


    void Awake()
    {
        //rigid_body = gameObject.GetComponent<Rigidbody>();
        _material = gameObject.GetComponent<MeshRenderer>().material;
        _origional_scale = transform.localScale;

        if (position == Vector3.zero)
        {
            position = gameObject.transform.position + new Vector3(0,1.5f,0);
        }

        //UpdateActivation();
    }

    void Start()
    {
        EventManager.StartListening(EventManager.GAZE_COMPLETED, onItemSelected);
    }

    private void onItemSelected()
    {
        if (!focused) return;
        
        Trigger();
        if(id != IDs.Door)
            EventManager.TriggerEvent(EventManager.WAYPOINT_ACTIVATE, (int)id);
        
    }

    void LateUpdate()
	{
		if(active && !occupied)
		{
			Animate();
		}
		else
		{
			if(active)
			{
				Deactivate();
			}
		}
	}


	public void UpdateActivation()
	{
		Deactivate();

		for(int i = 0; i < neighborhood.Length; i++)
		{
			if(neighborhood[i].occupied == true)
			{
				Activate();
			}
		}
	}


	public void Occupy()
	{
		occupied	= true;
        if (id == IDs.Door)
        {
            if(SceneManager.GetActiveScene().name == "ModernApartment")
                Initiate.Fade("UdacityModernApartment", Color.black, 0.5f);
            else
                Initiate.Fade("ModernApartment", Color.black, 0.5f);
        }
            //SceneManager.LoadScene(0);
    }


	public void Depart()
	{
		occupied	= false;
        active      = true;	
	}


	public void Activate()
	{
		_material.color			= active_color;
		transform.localScale	= _origional_scale;
		
		active					= true;

        GetComponent<MeshRenderer>().enabled = true;
    }


	public void Deactivate()
	{
		_material.color			= disabled_color;
		transform.localScale	= _origional_scale * 0.5f;
		
		active					= false;
		triggered 				= false;

        GetComponent<MeshRenderer>().enabled = false;
	}


	public void Trigger()
	{
		if(focused && active && !occupied)
		{
			triggered	= true;
			occupied	= false;
			_hilight	= 1.0f;
		}
	}


	public void Enter()
	{
		if(!focused && active)
		{
            focused		= true;
			_hilight 	= .5f;
		}
	}


	public void Exit()
	{
		focused		= false;
		_hilight 	= 1.0f;
	}


	private void Animate()
	{
		float pulse_animation	= Mathf.Abs(Mathf.Cos(Time.time * animation_speed));
		
		_material.color			= Color.Lerp(active_color, hilight_color, _hilight);
        			
                _hilight 				= Mathf.Max(_hilight - _hilight_fade_speed, 0.0f);

                Vector3 hilight_scale	= Vector3.one * (_hilight + (focused ? 0.5f : 0.0f));
        //        print((_origional_scale + hilight_scale) +","+ (_origional_scale * animation_scale + hilight_scale));
         //       print("localScale:" + transform.localScale);
        transform.localScale	= Vector3.Lerp(_origional_scale + hilight_scale, _origional_scale * animation_scale + hilight_scale, pulse_animation);
        

    }
}
