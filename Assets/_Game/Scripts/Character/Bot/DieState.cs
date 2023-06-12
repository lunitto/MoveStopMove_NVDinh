using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    //float elapsedTime;
    //float duration;
    int botAlive = 5;
    public void OnEnter(Bot bot)
    {
        //duration = 2f;
        //elapsedTime = 0f;
        bot.OnDeath();
        bot.StopMoving();
        botAlive--;
        BotManager.instance.DesSpawn(bot);
    }
    public void OnExecute(Bot bot)
    {
        //if (elapsedTime < duration)
        //{
        //    elapsedTime += Time.deltaTime;
            
        //}
        //else 
        if (botAlive < 5)
        {
            Vector3 spawnRotate = new Vector3(0, Random.Range(0, 360), 0);
            Vector3 spawnPosition;


            int randomX = (int)Random.Range(-36, 16);
            int randomZ = (int)Random.Range(-24, 16);
            spawnPosition = new Vector3(randomX, 18, randomZ);

            Bot BotClone = bot.botManager.botPool.GetObject().GetComponent<Bot>();
            BotClone.transform.position = spawnPosition;
            BotClone.transform.rotation = Quaternion.Euler(spawnRotate);
            BotClone.OnInit();
            //BotManager.instance.SpawnBot();
            GameObject obj = bot.botManager.botPool.GetObject();

            botAlive++;
        }
        
    }

    public void OnExit(Bot bot)
    { }

}
