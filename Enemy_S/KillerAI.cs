using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;


public class KillerAI : Resetable
{
    public enum State { Patrol, Chase, Stop }
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] bool StopOnLastPoint;
    [SerializeField] EnemyData enemyData;
    [SerializeField] Animator anim;
    [SerializeField] float SoundPanicDistance;
    [SerializeField] AudioSource SoundPanicSOund;
    [SerializeField] Transform ObjectToTurnWhenDead;
    [SerializeField] bool FeezeCameraInvoke;
    public float ChangeViewRadiusValue;

    private Transform stoppedPatrolPoint;
    private int currentPatrolPoint;
    private NavMeshAgent navMesh;
    private State currentState;
    private bool didectStanding = false;
    public static bool FinishedHouseArea;
    private bool OneTime;

    public override void Reset()
    {
        didectStanding = false;
        this.GetComponent<KillerAI>().enabled = true;
        GameEvents.TurnAround?.Invoke(new GameEvents.TurnAroundEvenetData { IsTurnAndNotBack = false, FreezCamera = false });
        OneTime = false;
        currentPatrolPoint = 0;
    }
    void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        GameEvents.Visible += OnVisible;
    }
    void OnEnable()
    {
        SetState(State.Patrol);
    }
    void OnDestroy()
    {
        GameEvents.Visible -= OnVisible;
    }
 
    void Update()
    {
        /* if (Time.time < LastTest + DelayTestPosition) return;
         LastTest = Time.time;*/
        // Debug.Log(currentState);
        switch (currentState)
            {
                    case State.Patrol:
               
                if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) <= enemyData.ArriveDis)
                {
                        bool finalPatrolPoint = false;
                     
                        stoppedPatrolPoint = patrolPoints[currentPatrolPoint];
                        do
                        {
                       
                        currentPatrolPoint++;
                        if (currentPatrolPoint >= patrolPoints.Length && StopOnLastPoint) 
                        {
                            finalPatrolPoint = true;
                        } 
                         currentPatrolPoint = currentPatrolPoint % patrolPoints.Length;
                        } while (!patrolPoints[currentPatrolPoint].gameObject.activeInHierarchy);

                    if (StopOnLastPoint && finalPatrolPoint )
                    {
                      
                        if (!didectStanding)
                        {
                            didectStanding = true;
                            FinishedHouseArea = true;
                          
                        }
                        SetState(State.Stop);
                    }
                    else
                    {
                        SetState(State.Patrol);
                    }
                       
                  //  Debug.Log(Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) + "<=" + ArriveDis);
                }
            
                break;
            case State.Chase:
                KillerChase();
                break;
            case State.Stop:
                Stop();
                break;
         


            }

      

        if ((DistanceFromPlayer() < enemyData.KillDistance) && !OneTime)
        {
            OneTime = true;
            GameEvents.KillPlayer?.Invoke(true);
            GameEvents.TurnAround?.Invoke(new GameEvents.TurnAroundEvenetData { FreezCamera = FeezeCameraInvoke, ObjectToTurn = ObjectToTurnWhenDead, IsTurnAndNotBack = true });
            SoundPanicSOund.Stop();
            return;
        }
        if (DistanceFromPlayer() < SoundPanicDistance)
        {
            if (!SoundPanicSOund.isPlaying)
            {
                SoundPanicSOund.Play();
            }
         
        }
        else
        {
            SoundPanicSOund.Stop();
        }
     
    }
    private float DistanceFromPlayer()
    {
      float dis =  Vector3.Distance(transform.position, PlayerLocator.Instance.transform.position);
        return dis;
    }
    private void OnVisible(bool isvisible)
    {
      //  if (isKlled) return;
        if (isvisible )
        {
            SetState(State.Chase);
         
        }
        else
        {
            SetState(State.Patrol);
        }
    }
    public void SetState(State newState)
    {
        currentState = newState;
       
        switch (newState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                KillerChase();
                break;
            case State.Stop:
                Stop();
                break;
        }
    }
    private void Patrol()
    {
        navMesh.SetDestination(patrolPoints[currentPatrolPoint].position);
        anim.SetBool("stand", false);
        anim.SetBool("Stab", false);
        anim.SetBool("Run", false);
        navMesh.isStopped = false;
        navMesh.speed = enemyData.PatrolSpeed;
      
    }
 
    private void KillerChase()
    {
        navMesh.SetDestination(PlayerLocator.Instance.transform.position);
        anim.SetBool("stand", false);
        anim.SetBool("Stab", false);
        anim.SetBool("Run", true);
        navMesh.isStopped = false;
        navMesh.speed = enemyData.ChaseSpeed;
      
    }
  
    private void Stop()
    {
        anim.SetBool("stand", true);
        anim.SetBool("Run", false);
        anim.SetBool("Stab", false);
        transform.rotation = stoppedPatrolPoint.rotation;
        navMesh.isStopped = true;
        enemyData.ViewRadius = ChangeViewRadiusValue;
    }
}
