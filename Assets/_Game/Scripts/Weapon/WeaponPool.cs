using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoBehaviour
{
    [SerializeField]private Transform weaponClones;
    public GameObject prefabWeapon;
    public int poolSize = 5;
    public Character character;
    public List<GameObject> poolWeapon = new List<GameObject>();

    public static WeaponPool instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        weaponClones = GameObject.FindGameObjectWithTag("weaponclone").transform;
        OnInit();
        
    }

    public void OnInit()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newWeapon = Instantiate(prefabWeapon); // sinh ra 1 weapon moi
            newWeapon.SetActive(false);
            Weapon weapon = newWeapon.GetComponent<Weapon>();
            weapon.SetCharacterAndWeaponPool(this.character, this);
            weapon.transform.localScale = Vector3.one;
            weapon.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
            weapon.child.localRotation = Quaternion.Euler(Vector3.zero);
            weapon.GetComponent<BoxCollider>().enabled = true;
            newWeapon.transform.SetParent(weaponClones);
            character.pooledWeaponList.Add(weapon);
            poolWeapon.Add(newWeapon);
        }
    }

    public void OnDestroy()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Destroy(poolWeapon[i]);
        }
        poolWeapon.Clear();
        character.pooledWeaponList.Clear();
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in poolWeapon)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If we get here, all objects are in use
        GameObject newObj = Instantiate(prefabWeapon);
        poolWeapon.Add(newObj);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
