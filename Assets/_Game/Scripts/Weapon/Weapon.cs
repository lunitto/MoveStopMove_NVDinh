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

    public void SetCharacterAndWeaponPool(Character character, WeaponPool weaponPool)
    {
        this.character = character;
        this.weaponPool = weaponPool;
    }

    public IEnumerator ReturnToPoolAfterSeconds()
    {
        yield return new WaitForSeconds(1);
        weaponPool.ReturnToPool(this.gameObject);
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bot") )//|| other.gameObject.CompareTag("player"))
        {
            Character character = other.gameObject.GetComponent<Character>();
            if(character != this.character)
            {
                //weaponPool.ReturnToPool(this.gameObject);
            }
        }
    }
}

