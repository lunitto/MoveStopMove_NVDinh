using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBtn : BaseBtn
{
    protected override void OnClick()
    {       
        GameManager.instance.DeleteCharacters();
        GameManager.instance.RespawnCharacters();
        GameManager.instance.DisnableALlCharacters();
        GameManager.instance.ResetTargetCircle();
        GameManager.instance.SpawnMap(GameManager.instance.currentLevelIndex);
        GameManager.instance.SpawnNav(GameManager.instance.currentLevelIndex);
        GameManager.instance.isGaming = false;
        GameManager.instance.isPause = false;
        GameManager.instance.isPause = false;
        BotManager.instance.DisableAllBots();
        UIManager.instance.ShowCoin();
        UIManager.instance.HideJoystick();
    }
    
}
