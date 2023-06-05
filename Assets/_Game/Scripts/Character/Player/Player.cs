using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public override void OnInit()
    {
        base.OnInit();
        StopMoving();
        characterAnim.ChangeAnim("idle");
        skinnedMeshRenderer.material = whiteMaterial;
        isMoving = false;
    }
    public override void OnDeath()
    {
        DisableCollider();
        characterAnim.ChangeAnim("dead");
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

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    public void Idle()
    {
        characterAnim.ChangeAnim("idle");
    }

    public void Dance()
    {
        StopMoving();
        characterAnim.ChangeAnim("dance");
    }

    public void GetWeapon()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon")) 
        {
            OnDeath();
        }
    }
}
