using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DownShowRoomController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Button homeButton;
    private bool isGazedAt = false;

    // Use this for initialization
    void Start()
    {
        EventManager.StartListening(EventManager.GAZE_COMPLETED, onGazeCompleted);
    }

    private void onGazeCompleted()
    {
        if (isGazedAt)
            SceneManager.LoadScene(0);//IntroScene   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("a7o");
        if (homeButton.gameObject == eventData.pointerEnter)
            isGazedAt = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (homeButton.gameObject == eventData.pointerEnter)
            isGazedAt = false;
    }
}
