using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObjective : MonoBehaviour
{
    [SerializeField] GameObject VisualInInventory;
    [SerializeField] GameObject HandIcon;
    [SerializeField] float MaxDistance;
    void OnMouseOver()
    {
        HandIcon.SetActive((Vector3.Distance(transform.position, PlayerLocator.Instance.transform.position) <= MaxDistance));
    }
    void OnMouseExit()
    {
        HandIcon.SetActive(false);
    }
    void OnMouseDown()
    {
   //     GameEvents.ItemToTake?.Invoke(gameObject);
      //  GameEvents.InventoryItems?.Invoke(Inventory.ItemsType.Key, Inventory.IndexTypes);
        VisualInInventory.SetActive(true);
    }
   
   
}
