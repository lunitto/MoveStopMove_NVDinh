using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Character Properties:")]
    [SerializeField] protected CharacterAnimation characterAnim;
    [SerializeField] protected Material deathMaterial;
    [SerializeField] protected CapsuleCollider capsulCollider;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    protected Material currentBodyMat;

    [Header("Weapon Properties:")]
    public GameObject onHandWeapon;
    public WeaponPool weaponPool;
    protected WeaponType weaponType;

    [Header("Lists:")]
    public List<Character> enemyList = new List<Character>();
    public List<Weapon> pooledWeaponList = new List<Weapon>();

    [Header("Character limbs:")]
    [SerializeField] protected GameObject body;
    public Transform rightHand;

    [Header("Bool Variables:")]
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

    public virtual void SetSkinnedMeshRenderer(Material mat)
    {
        skinnedMeshRenderer.material = mat;
    }

    public virtual void SetCurrentBodyMat(Material mat)
    {
        currentBodyMat = mat;
    }
}
