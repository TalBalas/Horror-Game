using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DeathManager : MonoBehaviour
{
    [SerializeField] GameObject deathText;
  //  [SerializeField] Animator Kiileranim;
    [SerializeField] PlayerMovment playerMovment;
    [SerializeField] CameraMovment cameraMovment;
   // [SerializeField] KillerAI killerAI;
   /// [SerializeField] FieldOfView fieldOfView;
    private DeathTrigger deathTrigger;
    [SerializeField] LayerMask layerMask;
    void Awake()
    {
        GameEvents.KillPlayer += OnKillPlayer;
    }
    void OnDestroy()
    {
        GameEvents.KillPlayer -= OnKillPlayer;
    }
   
    public void OnKillPlayer(bool isdead)
    {
       
        if (isdead) 
        {
            StartCoroutine(WhenKillPlayer());
        } 
    }
    IEnumerator WhenKillPlayer()
    {
        Cursor.lockState = CursorLockMode.None;
        var OverlapCollider = Physics.OverlapSphere(transform.position, 0.5f, layerMask);
        deathTrigger = null;
       // Debug.Log(OverlapCollider.Length);
        foreach (var item in OverlapCollider)
        {
            deathTrigger = item.GetComponent<DeathTrigger>();
            
            if (deathTrigger != null)
            {
               
                break;
            }
        }
       // Kiileranim.SetBool("Stab", true);
        playerMovment.enabled = false;
        cameraMovment.enabled = false;
       // killerAI.enabled = false;
       // fieldOfView.enabled = false;
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        deathText.SetActive(true);
    
     
    }
    public void OnClickDie()
    {
        DeathLogic();
    }
    private void DeathLogic()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(deathTrigger == null)
        {
          
            Debug.LogError("Player died and no death trigger Found !!! ");
            return;
        }
       
        foreach (var resetable in deathTrigger.resetables )
        {
            resetable.Reset();
        }
        Time.timeScale = 1;
        deathText.SetActive(false);
        deathTrigger = null;
    }
    
}
