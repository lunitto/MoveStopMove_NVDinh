using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBtn : BaseBtn
{
    protected override void OnClick()
    {
        UIManager.instance.CloseAll();
        UIManager.instance.ShowMainMenu();
        //GameManager.instance.RePlayer();
        GameManager.instance.SpawnMap(GameManager.instance.currentLevelIndex);
        GameManager.instance.SpawnNav(GameManager.instance.currentLevelIndex);
    }
    
}
