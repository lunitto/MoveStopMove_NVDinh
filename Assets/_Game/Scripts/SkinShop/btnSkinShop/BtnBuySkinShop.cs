using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnBuySkinShop : BaseBtn
{
    public ItemType itemType;

    protected override void OnClick()
    {
        SkinShopManager.instance.BuyItem(itemType);
        SkinShopManager.instance.SaveUsingItemIndex(itemType);
        SkinShopManager.instance.SetOtherUsingIndexToMinusOne(SkinShopManager.instance.currentItem, itemType);
    }
}
