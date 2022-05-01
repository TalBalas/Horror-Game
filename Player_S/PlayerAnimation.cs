using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator PlayerAnim;
    public void SwitchToWalkAnim(bool iswalk)
    {
        PlayerAnim.SetBool(TagsNames.WALKANIM, iswalk);
    }
    public void SwitchToRunAnim(bool isRun)
    {
        PlayerAnim.SetBool(TagsNames.RUNANIM, isRun);
    }
    public void SwitchToJumpAnim(bool isJump)
    {
        PlayerAnim.SetBool(TagsNames.JUMPANIM, isJump);
    }

    public void OpenDoorAnim()
    {
        PlayerAnim.SetTrigger(TagsNames.OPENDOOR);
    }
    public void ExitDoorAnim()
    {
        PlayerAnim.SetTrigger(TagsNames.EXITDOOR);
    }
}
