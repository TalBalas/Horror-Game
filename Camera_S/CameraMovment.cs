using DG.Tweening;
using System.Collections;
using UnityEngine;
public class CameraMovment : Resetable
{
    [SerializeField] float mouseSensitivity = 100f;
    private bool FreezeCamera = true;
    private bool IsBalance;
    [SerializeField] Vector3 dir;
    void Awake()
    {
        StartCoroutine(inisilazedCameraFreeze());
    }
    void Start()
    {      
        GameEvents.BalanceMode += OnBalanceMode;
        GameEvents.KillPlayer += OnPlayerDead;
        GameEvents.TurnAround += OnTurnAroundtrigger;
        GameEvents.InventoryPopUp += OnInventory;
        GameEvents.KillPlayer += OnKillPlayer;
        // GameEvents.WinGame += OnWinGame;
    }
    public override void Reset()
    {
        this.GetComponent<CameraMovment>().enabled = true;
        FreezeCamera = false;
    }
    void LateUpdate()
    {
        if (FreezeCamera||IsBalance) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        dir.y -= mouseY;
        dir.x += mouseX;
        dir.y = Mathf.Clamp(dir.y, -60, 60);
        transform.localRotation = Quaternion.Euler(dir.y, dir.x, 0);
    }
    private IEnumerator inisilazedCameraFreeze()
    {
        yield return new WaitForSeconds(0.5f);
        FreezeCamera = false;
    }
   
    void OnDestroy()
    {
       // GameEvents.PushAndPull -= OnPushAndPull;
        GameEvents.InventoryPopUp -= OnInventory;
        GameEvents.BalanceMode -= OnBalanceMode;
        GameEvents.KillPlayer -= OnPlayerDead;
        GameEvents.TurnAround -= OnTurnAroundtrigger;
        GameEvents.KillPlayer -= OnKillPlayer;
        //  GameEvents.WinGame -= OnWinGame;
    }
    private void OnKillPlayer(bool isdead)
    {
        if (isdead)
        {
            FreezeCamera = true;
        }
        else
        {
            FreezeCamera = false;
        }
    }
    private void OnWinGame(bool iswin)
    {
        if (iswin)
        {
            FreezeCamera = true;
        }
        else
        {
            FreezeCamera = false;

        }
    }
    private void OnBalanceMode(GameEvents.BalanceData data)
    {

        IsBalance = data.IsBalance;
        if (data.IsBalance)
        {
            ResetCamera(data);
        }
        else
        {

            dir.x = 180;
            dir.y = 0;
        }

    }
    private void OnTurnAroundtrigger(GameEvents.TurnAroundEvenetData data)
    {
            StartCoroutine(TurnAround(data));
    }
    private IEnumerator TurnAround(GameEvents.TurnAroundEvenetData data)
    {
        
        if (data.IsTurnAndNotBack)
        {
            yield return null;
            FreezeCamera = true;
            transform.DOLookAt(data.ObjectToTurn.position, 0.3f);
            yield return new WaitForSeconds(data.time);
            // dir.y = 0;
            // dir.x = 0;
            // transform.DOLookAt((dir-data.ObjectToTurn.position).normalized, 0.3f);
            StartCoroutine(ResetCamera(data));
        }
        else if (data.IsTurnAroundAndBack)
        {
            FreezeCamera = true;
            // var resetRotation = Quaternion.LookRotation((GateTransform.position - transform.position).normalized, Vector3.up);
            //  transform.DORotateQuaternion(resetRotation, 0.3f);
            transform.DOLookAt(data.ObjectToTurn.position, 0.3f);
            yield return new WaitForSeconds(data.time);
            //  var resetRotationBack = Quaternion.LookRotation((GateTransformBack.position - transform.position).normalized, Vector3.up);
            //  transform.DORotateQuaternion(resetRotationBack, 0.3f);
            transform.DOLookAt(data.BackwardTransform.position, 0.3f);
            StartCoroutine(UnfreezeCamera(data));
        }
    }
    private IEnumerator UnfreezeCamera(GameEvents.TurnAroundEvenetData data)
    {
        yield return new WaitForSeconds(0.5f);
        FreezeCamera = false;
    }
    private IEnumerator ResetCamera(GameEvents.TurnAroundEvenetData data)
    {
        yield return new WaitForSeconds(0.5f);
        dir.x = transform.localRotation.eulerAngles.y;
        dir.y = transform.localRotation.eulerAngles.x;
        //dir = Quaternion.LookRotation((data.ObjectToTurn.position - transform.position).normalized).eulerAngles *Mathf.Rad2Deg;
        FreezeCamera = false;
    }
    private void OnPlayerDead(bool isdead)
    {
       FreezeCamera = true;
    }
    private void ResetCamera(GameEvents.BalanceData data)
    {
        //transform.DORotateQuaternion(Quaternion.Euler(0, -90, 0), 0.3f);
        var resetRotation = Quaternion.LookRotation(data.End.position - data.Start.position, Vector3.up);

        transform.DORotateQuaternion(resetRotation, 0.3f);
      
    }
    private void OnInventory(bool inInventory)
    {
        FreezeCamera = inInventory;
    }
    private void OnPushAndPull(bool isactive)
    {
        FreezeCamera = isactive;
    }
}
