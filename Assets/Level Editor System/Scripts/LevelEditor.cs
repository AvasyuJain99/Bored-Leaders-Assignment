using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class LevelEditor : MonoBehaviour
{
    public HurdleData captureCageData;
    public HurdleData mineData;

    private Tile selectedTile;
    private HurdleData currentHurdleData;

    private void Awake()
    {
       
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null)
                {
                    if (currentHurdleData != null)
                    {
                        tile.PlaceHurdle(currentHurdleData);
                        currentHurdleData = null;
                    }
                    else
                    {
                        if (tile.HasHurdle())
                        {
                            tile.RemoveHurdle();
                        }
                        else
                        {
                            selectedTile = tile;
                            selectedTile.NotifyObservers();
                        }
                    }
                }
            }
        }
    }

    public void SetCurrentHurdle(HurdleData hurdleData)
    {
        currentHurdleData = hurdleData;
    }
}
