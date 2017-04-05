using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInAisle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject _3dModle;
    public GameObject _button;
    public GameObject _desc;
    public GameObject _price;
    public GameObject _bg;

    private Transform player;
    private Aisle mParent;
    private GameObject gazedObject;
    private bool isGazedAtSomthing;

    // Use this for initialization
    void Start () {
        mParent = GetComponentInParent<Aisle>();
        player = mParent.player;
        _desc.GetComponent<TextMeshController>().m_maxWidth = 7.2f;

        EventManager.StartListening(EventManager.GAZE_COMPLETED, onGazeCompleted);
    }
	
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("_3dModle: " + _3dModle.transform.rotation);
        gazedObject = eventData.pointerEnter;
        isGazedAtSomthing = true;
        EventManager.TriggerEvent(EventManager.GAZE_WAIT_STARTED);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gazedObject = null;
        isGazedAtSomthing = false;
        EventManager.TriggerEvent(EventManager.GAZE_STOPPED);
    }

    private void onGazeCompleted()
    {
        if (isGazedAtSomthing) {
            //load item
            EventManager.TriggerEvent(EventManager.SHOW_ITEMS_DETAILS,gameObject);
            isGazedAtSomthing = false;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - player.position);
        //_desc.GetComponent<TextMeshController>().m_maxWidth = 7.2f;
    }

}
