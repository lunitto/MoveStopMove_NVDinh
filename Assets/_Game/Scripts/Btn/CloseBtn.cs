using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtn : BaseBtn
{
    [SerializeField] private Player player;

    protected override void OnClick()
    {
        player.GetWeaponHand();
    }
}
