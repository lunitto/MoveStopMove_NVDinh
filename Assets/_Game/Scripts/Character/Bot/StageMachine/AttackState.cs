using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float elapsedTime;
    float duration;
    public void OnEnter(Bot bot)
    {
        elapsedTime = 0f;
        duration = 1.1f;
        bot.StopMoving();
        bot.StartCoroutine(bot.botAttack.DelayAttack(Random.Range(0.5f, 1f)));
        bot.StartCoroutine(bot.botAttack.Attack());
    }
    public void OnExecute(Bot bot)
    {
        if(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    { }

}
