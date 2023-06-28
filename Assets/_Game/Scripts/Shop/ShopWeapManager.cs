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
    public int currentWeapIndex;
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
        UnDisplayWeapon(currentWeapIndex);
        UnDisplayAllWeaponMats();
        currentWeapIndex++;

        if (currentWeapIndex == weaponMats.Length)
        {
            currentWeapIndex = 0;
        }
        DisplayWeapon(currentWeapIndex);
        DisplayWeaponMats(currentWeapIndex);
    }

    public void ChangeBack()
    {
        UnDisplayWeapon(currentWeapIndex);
        UnDisplayAllWeaponMats();
        currentWeapIndex--;

        if (currentWeapIndex < 0)
        {
            currentWeapIndex = weapons.Length - 1;
        }
        DisplayWeapon(currentWeapIndex);
        DisplayWeaponMats(currentWeapIndex);
    }

    public void BuyWeapon()
    {
        if (weapons[currentWeapIndex].isPurchased == false && DataManager.ins.playerData.coin >= weapons[currentWeapIndex].weaponData.weaponCost)
        {
            weapons[currentWeapIndex].isPurchased = true;
            DataManager.ins.playerData.coin -= weapons[currentWeapIndex].weaponData.weaponCost;
            UIManager.instance.UpdateUICoin();
            weaponCost.text = Const.TEXT_USING; 
            usingWeaponIndex = currentWeapIndex;
            //DataManager.ins.playerData.usingWeaponIndex = usingWeaponIndex;
            DataManager.ins.playerData.isPurchasedWeapon[currentWeapIndex] = true;
        }
        else if (weapons[currentWeapIndex].isPurchased == true)
        {
            weaponCost.text = Const.TEXT_USING;
            usingWeaponIndex = currentWeapIndex;
            DataManager.ins.playerData.usingWeaponIndex = usingWeaponIndex;
            DataManager.ins.playerData.isPurchasedWeapon[currentWeapIndex] = true;
        }
    }

    public void ChooseMat(int order)
    {
        Weapon wp = weapons[currentWeapIndex];
        wp.currentMaterialIndex = order;
        
        wp.ChangeMaterial(wp.currentMaterialIndex);
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
        weaponName.text = weapons[currentWeapIndex].weaponData.weaponName;
        if (weapons[index].isPurchased == false)
        {
            weaponCost.text = weapons[currentWeapIndex].weaponData.weaponCost.ToString();
        }
        else
        {
            if (index == usingWeaponIndex)
            {
                weaponCost.text = Const.TEXT_USING;
            }
            else
            {
                weaponCost.text = Const.TEXT_USE;
            }
        }
    }

    public GameObject GetWeapon()
    {
        return weapons[usingWeaponIndex].gameObject;
    }

}
