using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isGazedAt;

    // Use this for initialization
    void Start () {
        EventManager.StartListening(EventManager.GAZE_COMPLETED, onItemSelected);
    }

    private void onItemSelected()
    {
        if (!isGazedAt) return;
        EventManager.TriggerEvent(EventManager.SHOW_ITEMS_DETAILS, gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.transform.parent.gameObject == gameObject)
            isGazedAt = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.transform.parent.gameObject == gameObject)
            isGazedAt = false;
    }
}
