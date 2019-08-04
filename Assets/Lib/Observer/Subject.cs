using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Subject will be a game event as a scriptable object.
// These subjects can be created at will - since they're scriptable objects, they can be called easily
// Put new instances into Resources folder
[CreateAssetMenu]
public class Subject : ScriptableObject
{
    public Subject() { }
    List<Observer> observers;
    

    public void AddObserver(Observer observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(Observer observer)
    {
        observers.Remove(observer);
    }


    protected void NotifyObservers(Entity entity, EventType eventType)
    {
        foreach(Observer observer in observers)
        {
            observer.onNotify(entity, eventType);
        }
    }
}
