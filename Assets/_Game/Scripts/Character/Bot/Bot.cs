using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Rigidbody rb;

    private IState currentState = new IdleState();

    public bool isWall;
    public Transform destinationTransform;
    public GameObject prefabWeapon;
    public NavMeshAgent navMeshAgent;

    public BotAttack botAttack;
    public BotManager botManager;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnExecute(this);
        if (isDead == true)
        {
            ChangeState(new DieState());
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        StopMoving();
        ChangeState(new IdleState());
        botAttack.enemy = null;
        GetWeaponHand();
        skinnedMeshRenderer.material = whiteMaterial;
        isMoving = false;
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
    public override void OnDeath()
    {
        DisableCollider();
        characterAnim.ChangeAnim("dead");
        //ChangeState(new DieState());
        HideOnHandWeapon();
        isDead = true;
        skinnedMeshRenderer.material = deathMaterial;
    }
    public override void EnableCollider()
    {
        capsulCollider.enabled = true;
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
    }

    public override void DisableCollider()
    {
        capsulCollider.enabled = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }

    public void Move()
    {
        characterAnim.ChangeAnim("run");
        navMeshAgent.isStopped = false;
        isMoving = true;
    }
    public void StopMoving()
    {
        //rb.velocity = new Vector3(0, 0, 0);
        characterAnim.ChangeAnim("idle");
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = new Vector3(0, 0, 0);
        isMoving = false;
    }
    public void GetWeaponHand()
    {
        GameObject wp = Instantiate(prefabWeapon);
        onHandWeapon = wp;
        ShowOnHandWeapon();
        onHandWeapon.transform.SetParent(rightHand.transform);
        onHandWeapon.transform.localPosition = Vector3.zero;
        onHandWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        onHandWeapon.GetComponent<BoxCollider>().enabled = false;
        wp.GetComponent<Weapon>().SetCharacterAndWeaponPool(this, this.weaponPool);
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }
        if (other.CompareTag("weapon") && other.GetComponent<Weapon>().GetCharacter() != this)
        {
            //OnDeath();
            //ChangeState(new DieState());
            isDead = true;

        }

        if (other.CompareTag("wall"))
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
