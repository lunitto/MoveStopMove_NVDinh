using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCloseSkinShop : BaseBtn
{
    [SerializeField] private Player player;
    protected override void OnClick()
    {
        SkinShopManager.instance.OnCloseSkinShop();
        player.Idle();
    }
}
