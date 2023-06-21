using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBtn : BaseBtn
{
    protected override void OnClick()
    {
        GameManager.instance.DeleteCharacters();
        //GameManager.instance.RespawnCharacters();
        GameManager.instance.ResetTargetCircle();

        GameManager.instance.isGaming = false;
        
        GameManager.instance.isWin = false;
        //BotManager.instance.DisableAllBots();
       
        
        
        UIManager.instance.HideJoystick();
    }
}
