using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    float elapsedTime;
    float duration;
    public int botAlive = 5;
    public void OnEnter(Bot bot)
    {
        //duration = 2f;
        //elapsedTime = 0f;
        
        //bot.OnDeath();
        bot.StopMoving();

        //Debug.Log("aaaaaaaaaaaaaaaaa");
        BotManager.instance.DesSpawn(bot);
        //BotManager.instance.SpawnBot();

        botAlive--;
        //BotManager.instance.SpawnBot();
    }  
    public void OnExecute(Bot bot)
    {
        
         if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            
        }
        else 
        if (botAlive <5)
        {
            //BotManager.instance.DesSpawn(bot);
            //Debug.Log("aaaaaaaaaaaaaaaaa");
            //BotManager.instance.SpawnBot();
            botAlive++;
        }
        
    }

    public void OnExit(Bot bot)
    { }

}
