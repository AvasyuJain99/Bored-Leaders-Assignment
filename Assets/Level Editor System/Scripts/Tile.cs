using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private List<Hurdle> observers = new List<Hurdle>();

    public void AttachObserver(Hurdle observer)
    {
        observers.Add(observer);
    }

    public void DetachObserver(Hurdle observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (Hurdle observer in observers)
        {
            observer.PlaceHurdle(transform.position);
        }
    }

    public void PlaceHurdle(HurdleData hurdleData)
    {
        if (HasHurdle())
        {
            RemoveHurdle();
        }

        GameObject hurdleObj = Instantiate(hurdleData.hurdlePrefab, transform.position, Quaternion.identity);
        Hurdle hurdle = hurdleObj.GetComponent<Hurdle>();
        if (hurdle != null)
        {
            AttachObserver(hurdle);
            hurdle.PlaceHurdle(transform.position);
        }
    }

    public void RemoveHurdle()
    {
        if (HasHurdle())
        {
            Hurdle hurdle = GetComponentInChildren<Hurdle>();
            if (hurdle != null)
            {
                DetachObserver(hurdle);
                Destroy(hurdle.gameObject);
                Debug.Log("Hurdle Destroyed");
            }
        }
    }

    public bool HasHurdle()
    {
        return GetComponentInChildren<Hurdle>() != null;
    }
}
