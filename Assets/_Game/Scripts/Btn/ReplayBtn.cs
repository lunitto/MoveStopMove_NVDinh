using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayBtn : BaseBtn
{
    protected override void OnClick()
    {
        UIManager.instance.HideLosePanel();
        GameManager.instance.DeleteAllCharacter();
        GameManager.instance.SpawnMap(GameManager.instance.currentLevelIndex);
        GameManager.instance.SpawnNav(GameManager.instance.currentLevelIndex);
        
        GameManager.instance.ReSpawnCharacter();
        GameManager.instance.PlayGame();
    }
}
