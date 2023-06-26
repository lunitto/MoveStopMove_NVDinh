using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantController : ItemController
{
    public override void OnButtonClick(int index)
    {
        //hien thi buy button
        base.OnButtonClick(index);
        //mac do cho player
        player.WearPants(index);
    }
}
