using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameEvents : MonoBehaviour
{
    public static Action<bool> Visible;
  //public static Action<bool> PushAndPull;
    public static Action<bool> InventoryPopUp;
    public static Action<ItemObject> EquipedItem;
    public static Action<GameObject> HoldingObject;
    public static Action<bool> PlayerHiding;
    public static Action<BalanceData> BalanceMode;
    public static Action<bool> PauseGame;
    public static Action<bool> KillPlayer;
    public static Action<TurnAroundEvenetData> TurnAround;
    public static Action<bool> LaserAction;
    public static Action<bool> MazeTrigger;
  //  public static Action<bool> WinGame;
    public struct TurnAroundEvenetData
    {
        public float time;
        public bool FreezCamera;
        public bool IsTurnAroundAndBack;
        public bool IsTurnAndNotBack;
        public Transform ObjectToTurn;
        public Transform BackwardTransform;
    }
    public struct BalanceData
    {
        public bool IsBalance;
        public Transform Start;
        public Transform End;
    }
}
