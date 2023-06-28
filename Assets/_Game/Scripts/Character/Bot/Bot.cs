using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character, INavMeshAgent
{
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform targetWeapon;

    private IState currentState = new IdleState();
    public bool isWall;
    public Transform destinationTransform;

    public GameObject prefabWeapon;
    public NavMeshAgent navMeshAgent;
    public Target indicator;

    public BotAttack botAttack;
    public BotManager botManager;
    public GameObject botName;
    public bool isHaveWeapon;


    // Start is called before the first frame update
    void Start()
    {
        //OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnExecute(this);
        if (isDead == true)
        {
            return;
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ActiveNavmeshAgent();
        ChangeState(new IdleState());       
        botAttack.enemy = null;
        indicator.enabled = true;
        skinnedMeshRenderer.material = Colors.instance.characterColors[(int)Random.Range(0, Colors.instance.characterColors.Length)];
 
    }

    
    public override void OnDeath()
    {
        DisableCollider();
        characterAnim.ChangeAnim(Const.ANIM_DEAD);
        //ChangeState(new DieState());
        HideOnHandWeapon();
        indicator.enabled = false;
        skinnedMeshRenderer.material = deathMaterial;
        GameManager.instance.DeleteThisElementInEnemyLists(this);
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public override void EnableCollider()
    {
        capsulCollider.enabled = true;
        
    }

    public override void DisableCollider()
    {
        capsulCollider.enabled = false;
        
    }

    public void Move()
    {
        characterAnim.ChangeAnim(Const.ANIM_RUN);
        navMeshAgent.isStopped = false;
        //isMoving = true;
    }
    public void StopMoving()
    {
        characterAnim.ChangeAnim(Const.ANIM_IDLE);        
        navMeshAgent.velocity = new Vector3(0, 0, 0);
        if(navMeshAgent.enabled)
            navMeshAgent.isStopped = true;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }
        if (other.CompareTag(Const.WEAPON) && other.GetComponent<Weapon>().GetCharacter() != this)
        {
            //OnDeath();
            //ChangeState(new DieState());
            //isDead = true;

        }

        if (other.CompareTag(Const.WALL))
        {
            isWall = true;
        }
        
    }

    public void ActiveNavmeshAgent()
    {
        navMeshAgent.enabled = true;
    }

    public void DeActiveNavmeshAgent()
    {
        navMeshAgent.enabled = false;
    }


}
