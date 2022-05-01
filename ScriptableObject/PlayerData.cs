using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [Header("Speed Influence")]
    public int WalkSpeed;
    public int RunSpeed;
    public int CrouchSpeed;
    public int HoldHeavySpeed;
    public int BalanceSpeed;

    [Header("Hight Influence")]
    public float CrouchValue;
    public float StandingHightValue = 1;
    public float CrouchCenterY;
    public float StandCenterY;

    [Header("Gravity Influence")]
    public float JumpForce;
    public float Gravity = -9.81f;
    public float GravitiyMultipliere;

    [Space]
    public float FlowSliderValue;
    public float HideRadius;

    [Header("Bridge Influence")]
    public float InfluenceSpeed;
    public float TimeToInfluence;
    public float SpeedKeyBalance;

    [Header("Holding Influence")]
    public float Speed;
    public float MaxDistance;
    public float MaxHighYtransform;
}
