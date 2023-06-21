using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected CharacterAnimation characterAnim;
    [SerializeField] protected Material deathMaterial;
    [SerializeField] protected CapsuleCollider capsulCollider;
    //[SerializeField] protected GameObject body;

    public List<Character> enemyList = new List<Character>();
    public List<Weapon> pooledWeaponList = new List<Weapon>();
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public GameObject onHandWeapon;
    public WeaponPool weaponPool;
    protected WeaponType weaponType;
    public Transform rightHand;
    public bool isDead = false;
    public bool isMoving = false;

    public virtual void OnInit()
    {
        EnableCollider();
        enemyList.Clear();
        isDead = false;
    }

    public virtual void OnDeath()
    { }

    public virtual void EnableCollider()
    { }

    public virtual void DisableCollider()
    { }


    public virtual void ShowOnHandWeapon()
    {
        if (this.onHandWeapon != null)
        {
            this.onHandWeapon.SetActive(true);
        }
    }

    public virtual void HideOnHandWeapon()
    {
        if (this.onHandWeapon != null)
        {
            this.onHandWeapon.SetActive(false);
        }
    }
}
