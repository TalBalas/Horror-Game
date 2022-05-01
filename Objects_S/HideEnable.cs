using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideEnable : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] GameObject[] ObjectsToDisable;
    [SerializeField] GameObject[] ObjectsToEnable;
    float hideTime;
    void Awake()
    {
       GameEvents.PlayerHiding += OnPlayerHiding;
        OnPlayerHiding(false);
    }
    void Update()
    {
        if (duration == 0) return;
        if (hideTime == 0) return;
        if(hideTime + duration <= Time.time)
        {
            OnPlayerHiding(false);
        }
    }
    void OnDestroy()
    {
        GameEvents.PlayerHiding -= OnPlayerHiding;
    }
    private void OnPlayerHiding(bool ishide)
    {
        foreach (var point in ObjectsToDisable)
        {
            point.SetActive(!ishide);
        }
        foreach (var point in ObjectsToEnable)
        {
            point.SetActive(ishide);
        }
        if (ishide)
        {
            hideTime = Time.time;
        }
        else
        {
            hideTime = 0;
        }
    }
}
