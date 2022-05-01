using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{

    public bool IsGrounded { get; private set; }
    [SerializeField] float Dis;
    [SerializeField] LayerMask GroundMask;
    [SerializeField] PlayerMovment playerMovment;
    [SerializeField] CharacterController CH;
    [SerializeField] AudioSource WalkSand;
    [SerializeField] AudioSource RunSand;
    [SerializeField] AudioSource WalkCabin;
    [SerializeField] AudioSource RunCabin;
    private RaycastHit hit;
    void Update()
    {
       
        IsGrounded = Physics.Raycast(CH.bounds.center, Vector3.down,out hit, CH.bounds.extents.y + Dis,GroundMask);
       
        WalkSound();
    }
    private void WalkSound()
    {
        if (hit.collider == null)
        {
            StopWalkSounds();
            StopRunSounds(); 
            return;
        }
      
       
        var hitTag = hit.collider.gameObject.tag;
        if (playerMovment.IsMove && IsGrounded)
        {
           // Debug.Log("Walking");
           
            if (hitTag == TagsNames.SAND)
            {
                WalkCabin.Stop();
                WalkSandSound();
               // Debug.Log("WalkSand");
            }
           

            else  if (hitTag == TagsNames.CABIN)
            {
                WalkSand.Stop();
                WalkWoodSound();
            }
          
        }
        else
        {
            StopWalkSounds();
        }


        if (playerMovment.IsRunning && IsGrounded)
        {
            
            StopWalkSounds();
            if (playerMovment.Balance)
            {
                StopRunSounds();
            }
            if (hitTag == TagsNames.SAND)
            {
                WalkCabin.Stop();
                RunSandSound();
            }
            else if (hitTag == TagsNames.CABIN)
            {
                RunSand.Stop();
                RunWoodSound();
            }
        }
        else
        {
         
            StopRunSounds();
        }     

    }

    private void StopRunSounds()
    {
        RunCabin.Stop();
        RunSand.Stop();
    }
    private void StopWalkSounds()
    {
        WalkSand.Stop();
        WalkCabin.Stop();
    }
    private void WalkSandSound()
    {
        if (!WalkSand.isPlaying)
            WalkSand.Play(); 
    }

    private void RunSandSound()
    {

        if (!RunSand.isPlaying)
            RunSand.Play();

    }
    public void WalkWoodSound()
    {
        if (!WalkCabin.isPlaying)
            WalkCabin.Play();
    }
   
    public void RunWoodSound()
    {

        if (!RunCabin.isPlaying)
            RunCabin.Play();

    }
 
    void OnDrawGizmos()
    {
        Gizmos.DrawRay(CH.bounds.center, Vector3.down * (CH.bounds.extents.y + Dis));
    }
}
