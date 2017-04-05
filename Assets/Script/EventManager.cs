using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EventWithInt : UnityEvent<int> { }
[System.Serializable]
public class EventWithFloat : UnityEvent<float> { }
[System.Serializable]
public class EventWithDictionary : UnityEvent<IDictionary> { }
[System.Serializable]
public class EventWithGameObject : UnityEvent<GameObject> { }
[System.Serializable]
public class EventWithMsg : UnityEvent<string> { }
[System.Serializable]
public class EventWithTitleAndMsg : UnityEvent<string,string> { }
[System.Serializable]
public class EventWithVector3 : UnityEvent<Vector3> { }
[System.Serializable]
public class EventWithVector3AndInt : UnityEvent<Vector3,int> { }

public class EventManager : MonoBehaviour
{

    private Dictionary<string, UnityEvent> eventDictionary;
    private Dictionary<string, EventWithDictionary> eventWithDictionaryDictionary;
    private Dictionary<string, EventWithInt> eventWithIntDictionary;
    private Dictionary<string, EventWithFloat> eventWithFloatDictionary;
    private Dictionary<string, EventWithMsg> eventWithMsgDictionary;
    private Dictionary<string, EventWithTitleAndMsg> eventWithTitleAndMsgDictionary;
    private Dictionary<string, EventWithVector3> eventWithVector3;
    private Dictionary<string, EventWithVector3AndInt> eventWithVector3AndInt;
    private Dictionary<string, EventWithGameObject> eventWithGameObject;

    public static readonly string GAZE_WAIT_STARTED = "GAZE_WAIT_STARTED";
    public static readonly string GAZE_STOPPED = "GAZE_STOPPED";
    public static readonly string GAZE_COMPLETED = "GAZE_COMPLETED";
    public static readonly string SHOW_ITEMS_AISLE = "SHOW_ITEMS_AISLE";
    public static readonly string SHOW_ITEMS_DETAILS = "SHOW_ITEMS_DETAILS";
    public static readonly string EXIT_DETAILS = "EXIT_DETAILS";
    public static readonly string NEW_MAIN_ITEM_SELECTED = "NEW_SELECTION";
    public static readonly string WAYPOINT_ACTIVATE = "WAYPOINT_ACTIVATE";
    public static readonly string GOING_DOWN_STAIRS = "GOING_DOWN_STAIRS";

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a Transform in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
            eventWithDictionaryDictionary = new Dictionary<string, EventWithDictionary>();
            eventWithIntDictionary = new Dictionary<string, EventWithInt>();
            eventWithFloatDictionary = new Dictionary<string, EventWithFloat>();
            eventWithMsgDictionary = new Dictionary<string, EventWithMsg>();
            eventWithTitleAndMsgDictionary = new Dictionary<string, EventWithTitleAndMsg>();
            eventWithVector3 = new Dictionary<string, EventWithVector3>();
            eventWithVector3AndInt = new Dictionary<string, EventWithVector3AndInt>();
            eventWithGameObject = new Dictionary<string, EventWithGameObject>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }
    public static void StartListening(string eventName, UnityAction<IDictionary> listener)
    {
        EventWithDictionary thisEvent = null;
        if (instance.eventWithDictionaryDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new EventWithDictionary();
            thisEvent.AddListener(listener);
            instance.eventWithDictionaryDictionary.Add(eventName, thisEvent);
        }
    }
    public static void StartListening(string eventName, UnityAction<int> listener)
    {
        EventWithInt thisEvent = null;
        if (instance.eventWithIntDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new EventWithInt();
            thisEvent.AddListener(listener);
            instance.eventWithIntDictionary.Add(eventName, thisEvent);
        }
    }
    public static void StartListening(string eventName, UnityAction<float> listener)
    {
        EventWithFloat thisEvent = null;
        if (instance.eventWithFloatDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new EventWithFloat();
            thisEvent.AddListener(listener);
            instance.eventWithFloatDictionary.Add(eventName, thisEvent);
        }
    }
    public static void StartListening(string eventName, UnityAction<string> listener)
    {
        EventWithMsg thisEvent = null;
        if (instance.eventWithMsgDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new EventWithMsg();
            thisEvent.AddListener(listener);
            instance.eventWithMsgDictionary.Add(eventName, thisEvent);
        }
    }
    public static void StartListening(string eventName, UnityAction<string,string> listener)
    {
        EventWithTitleAndMsg thisEvent = null;
        if (instance.eventWithTitleAndMsgDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new EventWithTitleAndMsg();
            thisEvent.AddListener(listener);
            instance.eventWithTitleAndMsgDictionary.Add(eventName, thisEvent);
        }
    }
    public static void StartListening(string eventName, UnityAction<Vector3> listener)
    {
        EventWithVector3 thisEvent = null;
        if (instance.eventWithVector3.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new EventWithVector3();
            thisEvent.AddListener(listener);
            instance.eventWithVector3.Add(eventName, thisEvent);
        }
    }
    public static void StartListening(string eventName, UnityAction<Vector3,int> listener)
    {
        EventWithVector3AndInt thisEvent = null;
        if (instance.eventWithVector3AndInt.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new EventWithVector3AndInt();
            thisEvent.AddListener(listener);
            instance.eventWithVector3AndInt.Add(eventName, thisEvent);
        }
    }
    public static void StartListening(string eventName, UnityAction<GameObject> listener)
    {
        EventWithGameObject thisEvent = null;
        if (instance.eventWithGameObject.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new EventWithGameObject();
            thisEvent.AddListener(listener);
            instance.eventWithGameObject.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void StopListening(string eventName, UnityAction<IDictionary> listener)
    {
        if (eventManager == null) return;
        EventWithDictionary thisEvent = null;
        if (instance.eventWithDictionaryDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void StopListening(string eventName, UnityAction<int> listener)
    {
        if (eventManager == null) return;
        EventWithInt thisEvent = null;
        if (instance.eventWithIntDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void StopListening(string eventName, UnityAction<float> listener)
    {
        if (eventManager == null) return;
        EventWithFloat thisEvent = null;
        if (instance.eventWithFloatDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void StopListening(string eventName, UnityAction<string> listener)
    {
        if (eventManager == null) return;
        EventWithMsg thisEvent = null;
        if (instance.eventWithMsgDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void StopListening(string eventName, UnityAction<string,string> listener)
    {
        if (eventManager == null) return;
        EventWithTitleAndMsg thisEvent = null;
        if (instance.eventWithTitleAndMsgDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void StopListening(string eventName, UnityAction<Vector3> listener)
    {
        if (eventManager == null) return;
        EventWithVector3 thisEvent = null;
        if (instance.eventWithVector3.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void StopListening(string eventName, UnityAction<Vector3,int> listener)
    {
        if (eventManager == null) return;
        EventWithVector3AndInt thisEvent = null;
        if (instance.eventWithVector3AndInt.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void StopListening(string eventName, UnityAction<GameObject> listener)
    {
        if (eventManager == null) return;
        EventWithGameObject thisEvent = null;
        if (instance.eventWithGameObject.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
    public static void TriggerEvent(string eventName, IDictionary dict)
    {
        EventWithDictionary thisEvent = null;
        if (instance.eventWithDictionaryDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(dict);
        }
    }
    public static void TriggerEvent(string eventName, int msg)
    {
        EventWithInt thisEvent = null;
        if (instance.eventWithIntDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(msg);
        }
    }
    public static void TriggerEvent(string eventName, float msg)
    {
        EventWithFloat thisEvent = null;
        if (instance.eventWithFloatDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(msg);
        }
    }
    public static void TriggerEvent(string eventName, string msg)
    {
        EventWithMsg thisEvent = null;
        if (instance.eventWithMsgDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(msg);
        }
    }
    public static void TriggerEvent(string eventName, string title, string msg)
    {
        EventWithTitleAndMsg thisEvent = null;
        if (instance.eventWithTitleAndMsgDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(title,msg);
        }
    }
    public static void TriggerEvent(string eventName, Vector3 msg, int id)
    {
        EventWithVector3AndInt thisEvent = null;
        if (instance.eventWithVector3AndInt.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(msg,id);
        }
    }
    public static void TriggerEvent(string eventName, GameObject gameObject)
    {
        EventWithGameObject thisEvent = null;
        if (instance.eventWithGameObject.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(gameObject);
        }
    }
}