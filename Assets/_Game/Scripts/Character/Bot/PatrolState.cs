using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float elapsedTime;
    float duration;
    float randomRotateValue;
    public void OnEnter(Bot bot)
    {
        elapsedTime = 0f;
        duration = Random.Range(4f, 7f);
        //randomRotateValue = Random.Range(0.4f, 0.6f);
        bot.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(-120f, 120f), 0));

        bot.Move();
    }
    public void OnExecute(Bot bot)
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (bot.isWall)
            {
                bot.navMeshAgent.velocity = Vector3.zero; // dung lai
                bot.transform.rotation = Quaternion.LookRotation(-bot.transform.forward); // quay 180 do
                bot.isWall = false;
            }
            //bot.transform.Rotate(0, randomRotateValue, 0);
            if (bot.botAttack.enemy != null && !bot.botAttack.enemy.isDead)
            {
                bot.ChangeState(new AttackState());
            }

            bot.navMeshAgent.SetDestination(bot.destinationTransform.position); // di ve phia truoc mat
            
        }
        else
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    { }

}
