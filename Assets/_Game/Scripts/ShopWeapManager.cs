using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct outlineStruct
{
    public WeaponType weaponType;
    public Outline[] outlines;
}
public class ShopWeapManager : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Text weaponName;
    [SerializeField] private Text weaponCost;
    public outlineStruct[] dictOutline;

    [Header("Weapon")]
    public Weapon[] weapons;
    public GameObject[] weaponMats;

    [Header("Index")]
    public int currentWeapIndext = 0;
    public int usingWeaponIndex;

    public static ShopWeapManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeNext()
    {
        UnDisplayWeapon(currentWeapIndext);
        UnDisplayAllWeaponMats();
        currentWeapIndext++;

        if (currentWeapIndext == weaponMats.Length)
        {
            currentWeapIndext = 0;
        }
        DisplayWeapon(currentWeapIndext);
        DisplayWeaponMats(currentWeapIndext);
    }

    public void ChangeBack()
    {
        UnDisplayWeapon(currentWeapIndext);
        UnDisplayAllWeaponMats();
        currentWeapIndext--;

        if (currentWeapIndext < 0)
        {
            currentWeapIndext = weapons.Length - 1;
        }
        DisplayWeapon(currentWeapIndext);
        DisplayWeaponMats(currentWeapIndext);
    }

    public void BuyWeapon()
    {
        if (weapons[currentWeapIndext].isPurchased == false)
        {
            weapons[currentWeapIndext].isPurchased = true;
            
            weaponCost.text = "using";
            usingWeaponIndex = currentWeapIndext;
            
        }
        else if (weapons[currentWeapIndext].isPurchased == true)
        {
            weaponCost.text = "using";
            usingWeaponIndex = currentWeapIndext;
            
        }
    }

    public void ChooseMat(int order)
    {
        Weapon wp = weapons[currentWeapIndext];
        wp.currentMaterialIndext = order;
        
        wp.ChangeMaterial(wp.currentMaterialIndext);
    }

    public void HideOutlinesWithSameWeaponType(WeaponType weaponType)
    {
        for (int i = 0; i < dictOutline.Length; i++)
        {
            if (dictOutline[i].weaponType == weaponType)
            {
                for (int j = 0; j < dictOutline[i].outlines.Length; j++)
                {
                    dictOutline[i].outlines[j].gameObject.SetActive(false);
                }
            }
        }
    }

    public void UnDisplayAllWeaponMats()
    {
        for (int i = 0; i < weaponMats.Length; i++)
        {
            weaponMats[i].gameObject.SetActive(false);
        }
    }

    public void DisplayWeaponMats(int index)
    {
        weaponMats[index].gameObject.SetActive(true);
    }
    public void UnDisplayWeapon(int index)
    {
        weapons[index].gameObject.SetActive(false);
    }

    public void DisplayWeapon(int index)
    {
        weapons[index].gameObject.SetActive(true);
        DisplayWeaponButtonText(index);
    }
    public void DisplayWeaponButtonText(int index)
    {
        weaponName.text = weapons[currentWeapIndext].weaponData.weaponName;
        if (weapons[index].isPurchased == false)
        {
            weaponCost.text = weapons[currentWeapIndext].weaponData.weaponCost.ToString();
        }
        else
        {
            if (index == usingWeaponIndex)
            {
                weaponCost.text = "using";
            }
            else
            {
                weaponCost.text = "use";
            }
        }
    }

    public GameObject GetWeapon()
    {
        return weapons[usingWeaponIndex].gameObject;
    }

}
