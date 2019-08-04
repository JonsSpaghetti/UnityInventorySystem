using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This will be added to any gameobject that wants to listen to an event.
public class Observer : MonoBehaviour
{
    UnityEvent callOnNotify;
    public Observer() { }  
    public virtual void onNotify(Entity entity, EventType eventType)
    {
        Debug.Log($"Notified of {eventType} for {entity}!");
        callOnNotify.Invoke();
    }
}
