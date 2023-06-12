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

    public int currentMaterialIndex;

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
    }
}

