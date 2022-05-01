
using UnityEngine;
public class Hiding : MonoBehaviour
{
    [SerializeField] LayerMask HiderPlaces;
    [SerializeField] PlayerData playerData;
    public bool IsHiding;
    [SerializeField] GameObject Text;
    [SerializeField] PlayerAnimation playerAnimationManager;
    void Awake()
    {
        playerAnimationManager = GetComponent<PlayerAnimation>();
    }
    void Update()
    {
       bool IscanHide = CheckCanHide();

        OpenDoor(IscanHide);
        
    }
   
    public bool CheckCanHide()
    {
        if (IsHiding) return false;
        /*var colliders = Physics.OverlapSphere(transform.position, playerData.HideRadius, HiderPlaces);
        if (colliders.Length > 0)
        {
            Debug.Log(colliders[0].name);
        }*/
        if (Physics.CheckSphere(transform.position, playerData.HideRadius, HiderPlaces)) 
        {
            Text.SetActive(true);
          
            return true;
        }
        else
        {
            Text.SetActive(false);
            return false;
        }   
    }
    private void OpenDoor(bool isCanHide)
    {
        if (isCanHide && !IsHiding && Input.GetKeyDown(KeyCode.F))
        {
            Text.SetActive(false);
           var HidingPlaces = Physics.OverlapSphere(transform.position, playerData.HideRadius, HiderPlaces);
            var curretHidingPlace = HidingPlaces[0].transform;
            var hidingObject = curretHidingPlace.GetComponent<HidingObject>();
           
            // if the object dosent have the componemt HidingObject check is parent and counitneo finding object
            while (hidingObject==null && curretHidingPlace != null)
            {
                curretHidingPlace = curretHidingPlace.parent;
                hidingObject = curretHidingPlace.GetComponent<HidingObject>();
            }

            switch (hidingObject.type)
            {
                case HidingObject.Type.Closet:
                  //  playerAnimationManager.OpenDoorAnim();
                    break;
                case HidingObject.Type.Table:
                 //   playerAnimationManager.OpenDoorAnim();
                    break;
                default:
                    break;
            }


            IsHiding = true;
            GameEvents.PlayerHiding?.Invoke(true);
            Debug.Log("Inside");
        }
        else if (IsHiding && Input.GetKeyDown(KeyCode.F))
        {
            ExitDoorMechanic();
        }
    }
    private void ExitDoorMechanic()
    {
        IsHiding = false;
        GameEvents.PlayerHiding?.Invoke(false);
        //  playerAnimationManager.ExitDoorAnim();
        Debug.Log("Exit");

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playerData.HideRadius);
    }
}
