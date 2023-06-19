using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMaterialBtn : BaseBtn
{
    [SerializeField] int matOrder;
    public WeaponType weaponType;
    public Outline outline;

    protected override void OnClick()
    {
        ShopWeapManager.instance.ChooseMat(matOrder);
        ShopWeapManager.instance.HideOutlinesWithSameWeaponType(this.weaponType);
        ShowOutline();
    }

    public void ShowOutline()
    {
        outline.gameObject.SetActive(true);
    }
}
