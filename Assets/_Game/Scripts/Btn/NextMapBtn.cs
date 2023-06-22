using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMapBtn : BaseBtn
{
    protected override void OnClick()
    {
        UIManager.instance.HideWinPanel();
        GameManager.instance.SpawnMap(GameManager.instance.currentLevelIndex);
        GameManager.instance.SpawnNav(GameManager.instance.currentLevelIndex);
        for (int i = 0; i < BotManager.instance.size; i++)
        {
            BotManager.instance.SpawnBot();
        }
        GameManager.instance.PlayGame();
    }
}
