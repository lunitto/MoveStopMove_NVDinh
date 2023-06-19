using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform child;
    public MeshRenderer meshRenderer;
    private WeaponPool weaponPool;
    private Character character;

    public int currentMaterialIndext;
    public bool isPurchased;
    public bool isStuckAtObstacle;

    protected void Start()
    {
        
        ChangeMaterial(currentMaterialIndext);
        isStuckAtObstacle = false;
    }
    public void ChangeMaterial(int index)
    {
        meshRenderer.material = weaponData.GetWeaponMaterial(index);
    }

    public Character GetCharacter()
    {
        return this.character;
    }
    public void SetCharacterAndWeaponPool(Character character, WeaponPool weaponPool)
    {
        this.character = character;
        this.weaponPool = weaponPool;
    }

    public IEnumerator ReturnToPoolAfterSeconds()
    {
        yield return new WaitForSeconds(0);
        weaponPool.ReturnToPool(this.gameObject);
        yield return null;
    }

    public virtual void Fly(Vector3 target, float flySpeed)
    {
        StartCoroutine(FlyStraight(target, flySpeed));
    }

    public virtual IEnumerator FlyStraight(Vector3 target, float flySpeed)
    {
        while (Vector3.Distance(this.transform.position, target) > 0.1f && this.gameObject.activeSelf && !this.isStuckAtObstacle)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, flySpeed * Time.deltaTime);
            yield return null;
        }

        if (!isStuckAtObstacle)
        {
            this.weaponPool.ReturnToPool(this.gameObject);
        }

        yield return null;
    }

    public IEnumerator StuckAtObstacle()
    {
        isStuckAtObstacle = true;
        yield return new WaitForSeconds(1);
        isStuckAtObstacle = false;
        weaponPool.ReturnToPool(this.gameObject);
        yield return null;
    }
    public void ReturnToPool()
    {
        weaponPool.ReturnToPool(this.gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bot") || other.gameObject.CompareTag("player"))
        {
            //Debug.Log("aaaaaaaaaaaaaaaa");
            Character otherCharacter = other.gameObject.GetComponent<Character>();
            if(otherCharacter != this.character)
            {
                otherCharacter.OnDeath();
                weaponPool.ReturnToPool(this.gameObject);
            }
        }

        if (other.gameObject.CompareTag("obstacle"))
        {
            StartCoroutine(StuckAtObstacle());
        }
    }
}

