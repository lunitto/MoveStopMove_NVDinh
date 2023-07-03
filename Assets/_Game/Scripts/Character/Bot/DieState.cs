using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    float duration;
    float elasedTime;
    int tempAlive;


    public void OnEnter(Bot bot)
    {
        elasedTime = 0f;
        duration = 2f;
        //bot.StopMoving();
        
        //bot.OnDeath();
        GameManager.instance.currentAlive--;
        tempAlive = GameManager.instance.currentAlive;
    }

    public void OnExecute(Bot bot)
    {
        if (elasedTime < duration)
        {
            elasedTime += Time.deltaTime;
        }
        else
        {
            BotManager.instance.DeSpawn(bot);
            if (tempAlive > BotManager.instance.size)
            {
                //Debug.Log("aaaa");
                BotManager.instance.SpawnBot();
            }
            //BotManager.instance.SpawnBot();
        }
    }

    public void OnExit(Bot bot) { }

}
