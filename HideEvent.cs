using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideEvent : MonoBehaviour
{
    void Awake()
    {
        GameEvents.PlayerHiding += OnHide;
    }
    void OnDestroy()
    {
        GameEvents.PlayerHiding -= OnHide;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameEvents.PlayerHiding?.Invoke(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameEvents.PlayerHiding?.Invoke(false);
        }
          
    }
    private void OnHide(bool isHide)
    {

    }
}
