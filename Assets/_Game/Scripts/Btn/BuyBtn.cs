using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBtn : BaseBtn
{
    protected override void OnClick()
    {
        ShopWeapManager.instance.BuyWeapon();
    }
}
