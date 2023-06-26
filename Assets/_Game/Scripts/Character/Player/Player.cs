using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Material whiteMaterial;
    [SerializeField] private Rigidbody rb;
    public GameObject prefabWeapon;

    
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public override void OnInit()
    {
        base.OnInit();
        StopMoving();
        GetWeaponHand();
        characterAnim.ChangeAnim("idle");
        
        skinnedMeshRenderer.material = whiteMaterial;
        isMoving = false;
    }
    public override void OnDeath()
    {
        DisableCollider();
        characterAnim.ChangeAnim("dead");
        skinnedMeshRenderer.material = deathMaterial;
        UIManager.instance.SetRankText(GameManager.instance.currentAlive);
        BotManager.instance.DisableAllBots();
        UIManager.instance.HideJoystick();
        UIManager.instance.ShowLosePanel();
        isDead = true;
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

    public void GetWeaponHand()
    {
        GameObject wp = Instantiate(ShopWeapManager.instance.GetWeapon());
        if (onHandWeapon != null)
        {
            Destroy(onHandWeapon);
        }
        onHandWeapon = wp;
        ShowOnHandWeapon();
        onHandWeapon.transform.SetParent(rightHand.transform);
        onHandWeapon.transform.localPosition = Vector3.zero;
        onHandWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        onHandWeapon.GetComponent<BoxCollider>().enabled = false;
        wp.GetComponent<Weapon>().SetCharacterAndWeaponPool(this, this.weaponPool);
        weaponPool.prefabWeapon = wp;
        weaponPool.OnDestroy();
        weaponPool.OnInit();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon") && other.GetComponent<Weapon>().GetCharacter() != this) 
        {
            //OnDeath();
            //other.GetComponent<Weapon>().ReturnToPool();
            //Debug.Log("aaaaaaaaaaaaaaaa");
        }
    }
}
