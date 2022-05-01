using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerMovment : Resetable
{
    [SerializeField] PlayerData playerData;
    [SerializeField] Slider StaminaSlider;
    [SerializeField] Transform CAM;
    [SerializeField] AudioSource FallingAudio;
    [SerializeField] AudioSource HardBreathing;
    [SerializeField] Animator HandAnim;
    public bool IsStop;
    public bool IsCanJump;
    public bool IsCanCrouch;
    public bool IsMove;
    public bool Balance { get; set; }
    public bool IsRunning;
    private bool IsCrouch;
    private PlayerGrounded playerGrounded;
    private int CurrentSpeed;
    private float VelocityY;
    // private PlayerAnimation playerAnimationManager;
    private CharacterController CH;
    private float Horizontal;
    private float Verical;
    private bool isWin;
    private float BalanceKeyValue;
    private Quaternion startingRotation;
    private float balanceStartTime;
    private bool StaminaDepleted;
    private bool IsGravity = true;
    private bool TunrAroundandBack { get; set; }
    private bool TunrAround { get; set; }
    private bool IsHiding { get; set; }
    private bool IsLaserAction { get; set; }
    void Awake()
    {
        CH = GetComponent<CharacterController>();
        playerGrounded = GetComponent<PlayerGrounded>();
        //playerAnimationManager = GetComponent<PlayerAnimation>();
        GameEvents.InventoryPopUp += OnInventory;
        GameEvents.BalanceMode += OnBalanceMode;
        GameEvents.KillPlayer += OnPlayerDead;
        GameEvents.TurnAround += TurnAroundTrigger;
        GameEvents.PlayerHiding += OnHide;
        GameEvents.LaserAction += OnLaserAction;
      //  GameEvents.WinGame += OnWinGame;
    }
    void OnDestroy()
    {
        //  GameEvents.PushAndPull -= OnPushAndPull;
        GameEvents.InventoryPopUp -= OnInventory;
        GameEvents.BalanceMode -= OnBalanceMode;
        GameEvents.KillPlayer -= OnPlayerDead;
        GameEvents.TurnAround -= TurnAroundTrigger;
        GameEvents.PlayerHiding -= OnHide;
        GameEvents.LaserAction -= OnLaserAction;
        // GameEvents.WinGame -= OnWinGame;
        // GameEvents.HoldingObject -= OnHolding;
    }
    void Start()
    {
        CurrentSpeed = playerData.WalkSpeed;
        StaminaSlider.value = StaminaSlider.maxValue;
    }
  
    void Update()
    {
        Movment();
        WalkInput();
        Gravity();
        Crouch();
        Stamina();
        Jump();
    }
    public override void Reset()
    {
        this.GetComponent<PlayerMovment>().enabled = true;
        Debug.Log("Reset Work ");
       transform.rotation = Quaternion.Euler(0, 0, 0);
        IsStop = false;
        StaminaSlider.value = StaminaSlider.maxValue;
    }

    private void WalkInput()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Verical = Input.GetAxisRaw("Vertical");
    }
    private void OnWinGame(bool iswin)
    {
        isWin = iswin;
    }
    private void OnHide(bool ishide)
    {
        IsHiding = ishide;
    }
    private void OnBalanceMode(GameEvents.BalanceData data)
    {

        Balance = data.IsBalance;
        if (Balance)
        {
            balanceStartTime = Time.time;
            IsGravity = false;
            StartCoroutine(ResetPlayerOnBridge(data.Start, 0.3f));
            // StartCoroutine(ResetPlayerOnBridge(data));
        }
        else
        {
            IsGravity = true;
            if (IsStop) return;
            transform.DOLocalRotate(startingRotation.eulerAngles, 0.3f);
        }

    }
    IEnumerator ResetPlayerOnBridge(Transform point, float time)
    {
        Vector3 startpoint = transform.position;
        Vector3 positionDelta = point.position - startpoint;
        float timer = 0;
        while (timer < time)
        {
            CH.Move(positionDelta * Time.deltaTime / time);
            timer += Time.deltaTime / time;
            yield return null;
        }
        transform.position = point.position;
        startingRotation = transform.localRotation;
    }
    /*  IEnumerator ResetPlayerOnBridge(GameEvents.BalanceData data)
      {
          var lerp = Vector3.zero;
         do
         {
             var target = new Vector3(data.Start.position.x, transform.position.y, data.Start.position.z);
             lerp = Vector3.Lerp(Vector3.zero, target - transform.position, 5 * Time.deltaTime);
             CH.Move(lerp);
             //Debug.Log(new Vector3(lerp.x, 0, lerp.z).magnitude);
             yield return null;
         } while (new Vector3(lerp.x,0,lerp.z).magnitude > 0.002f);
      }*/
    private void TurnAroundTrigger(GameEvents.TurnAroundEvenetData data)
    {
        TunrAroundandBack = data.IsTurnAroundAndBack;
        TunrAround = data.IsTurnAndNotBack;
       
    }
    private void OnLaserAction(bool isOnlaserAction)
    {
        IsLaserAction = isOnlaserAction;
    }
    private void Movment()
    {

        Vector3 dirMove = Vector3.zero;
        if (IsStop) return;
       
            if (Balance)
            {
               
                dirMove = (CAM.forward * Verical).normalized * playerData.BalanceSpeed;
                InfluenceBalance();
                ADkeysBalance();

            }
            else if (TunrAroundandBack|| TunrAround || isWin || IsLaserAction)
            {
               
                dirMove = Vector3.zero;
               
            }
            else
            {
                dirMove = (CAM.right * Horizontal + CAM.forward * Verical).normalized * CurrentSpeed;
          
            }
       


        dirMove.y = VelocityY;
        CH.Move(dirMove * Time.deltaTime);
        if (Horizontal != 0 || Verical != 0)
        {
            IsMove = true;
        }
        else
        {
            HandAnim.SetBool("Run", false);
            IsMove = false;
        }


        Running();
    }
    private void OnPlayerDead(bool isdead)
    {
        if (isdead)
        {
           // IsMove = false;
            IsStop = true;
        }
    }
    public void TouchedFallTrigger(bool left)
    {
        if (!Balance) return;
        GameEvents.BalanceMode?.Invoke(new GameEvents.BalanceData { IsBalance = false });
        CH.Move(left ? CAM.transform.right : -CAM.transform.right * 3f);
        transform.DOLocalRotate((transform.localRotation * Quaternion.AngleAxis(left ? -90 : 90, Vector3.up) * Quaternion.AngleAxis(30, Vector3.left)).eulerAngles, 0.5f);
        FallingAudio.Play();

    }
    private void InfluenceBalance()
    {
        var angleValue = transform.localRotation.eulerAngles.z;
        if (angleValue > 180)
        {
            angleValue -= 360;
        }
        transform.Rotate(0, 0, angleValue * Time.deltaTime *playerData.InfluenceSpeed);
     
        if (Time.time - balanceStartTime >= playerData.TimeToInfluence && angleValue == 0)
        {
           
            transform.Rotate(0, 0, 1f);
        }
    
    }
    private void ADkeysBalance()
    {
        if (Horizontal < 0) /// left
        {
            if (BalanceKeyValue > 0) BalanceKeyValue = 0;
            BalanceKeyValue -= Time.deltaTime;
            transform.Rotate(0, 0, BalanceKeyValue * playerData.SpeedKeyBalance);

        }
        else if (Horizontal > 0) ///right
        {
            if (BalanceKeyValue < 0) BalanceKeyValue = 0;
            BalanceKeyValue += Time.deltaTime;
            transform.Rotate(0, 0, BalanceKeyValue * playerData.SpeedKeyBalance);
        }
    }

    private void Running()
    {
        if (IsCrouch || Balance ||TunrAroundandBack) return;
       // Debug.Log("RunFunction");
        if (Input.GetKey(KeyCode.LeftShift) && !StaminaDepleted)
        {
            HandAnim.SetBool("Run", true);
            StaminaInfluence();
        //    Debug.Log("Run");
            IsRunning = true;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            HandAnim.SetBool("Run", false);
            StaminaDepleted = false;
            CurrentSpeed = playerData.WalkSpeed;
            IsRunning = false;
        }
        if (StaminaSlider.value == StaminaSlider.minValue)
        {
            HandAnim.SetBool("Run", false);
            StaminaDepleted = true;
            CurrentSpeed = playerData.WalkSpeed;
            IsRunning = false;
        }
        if (!IsMove || IsHiding) IsRunning = false; return;
    }
    private void Stamina()
    {
        if (IsCrouch || Balance || TunrAroundandBack|| IsHiding) return;

        if (!IsRunning)
        {
            StaminaSlider.value += playerData.FlowSliderValue * Time.deltaTime;
            if (StaminaSlider.value > (StaminaSlider.maxValue - StaminaSlider.minValue) / 3f * 2) HardBreathing.Stop();
        }
        else
        {
            StaminaSlider.value -= playerData.FlowSliderValue * Time.deltaTime;
        }
    }
    private void Crouch()
    {
        if (IsRunning || Balance || TunrAroundandBack) return;

        if (Input.GetKey(KeyCode.LeftControl) && IsCrouch)
        {
            CH.height = playerData.CrouchValue;
            CH.center = new Vector3(CH.center.x, playerData.CrouchCenterY, CH.center.z);
            CurrentSpeed = playerData.CrouchSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && IsCrouch)
        {
            CH.height = playerData.StandingHightValue;
            CH.center = new Vector3(CH.center.x, playerData.StandCenterY, CH.center.z);
            CurrentSpeed = playerData.WalkSpeed;
        }
    }

    private void Jump()
    {
        if (!playerGrounded.IsGrounded || Balance || TunrAroundandBack|| IsHiding) return;
        if (Input.GetKeyDown(KeyCode.Space) && playerGrounded.IsGrounded && IsCanJump)
        {
            HandAnim.SetBool("Run", false);
            // playerAnimationManager.SwitchToJumpAnim(true);
            VelocityY = playerData.JumpForce;

        }
    }
    private void Gravity()
    {
       // if (!IsGravity || Balance || GateTrigger|| IsHiding) return;
      
        if (playerGrounded.IsGrounded && VelocityY <= -10)
        {
            VelocityY = 0;
           
        }
        if (!playerGrounded.IsGrounded)
        {
            VelocityY -= playerData.Gravity * Time.deltaTime;
         
        }

        //  Debug.Log(VelocityY);
    }

    private void StaminaInfluence()
    {
        if (Balance || TunrAroundandBack ||IsCrouch|| IsHiding) return; 

        if (StaminaSlider.value < (StaminaSlider.maxValue - StaminaSlider.minValue) / 3f)
        {
            CurrentSpeed = playerData.RunSpeed - 2;
            HardBreathingSOUND();
        }
        else if (StaminaSlider.value < (StaminaSlider.maxValue - StaminaSlider.minValue) / 3f * 2)
        {
            CurrentSpeed = playerData.RunSpeed - 1;
        }
        else
        {
            CurrentSpeed = playerData.RunSpeed;
            HardBreathing.Stop();
        }


    }
    private void HardBreathingSOUND()
    {
        if (!HardBreathing.isPlaying)
            HardBreathing.Play();
    }
    private void OnInventory(bool inInventory)
    {
        IsStop = inInventory;
    }
    private void OnPushAndPull(bool isactive)
    {
        IsStop = isactive;
    }
    private void OnHolding(GameObject heldObject)
    {
        bool isHeavy = false;
        if (heldObject != null)
        {
            if (heldObject.GetComponent<Heavy>() != null)
            {
                isHeavy = true;
            }
        }
        if (isHeavy)
        {
            // CurrentSpeed = playerData.HoldHeavySpeed;
            Debug.Log("Hold");
        }
        else
        {
            //CurrentSpeed = playerData.WalkSpeed;
        }
        IsCanJump = !isHeavy;
        IsCrouch = !isHeavy;






    }
}


