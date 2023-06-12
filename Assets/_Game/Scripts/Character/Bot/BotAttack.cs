using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttack : CharacterAttack
{
    [SerializeField] private CharacterAnimation characterAnim;
    [SerializeField] private Bot bot;
    
    
    // Update is called once per frame
    public void Update()
    {
        if (bot.enemyList.Count > 0)
        {
            FindNearestTarget();
        }
        else
        {
            enemy = null;
        }
    }


    public IEnumerator Attack()
    {
        if (enemy != null)
        {
            Vector3 enemyPos = enemy.transform.position;
            bot.StopMoving();
            characterAnimation.ChangeAnim("attack");
            RotateToTarget();
            float elapsedTime = 0f;
            float duration = 0.4f;
            while (elapsedTime < duration)
            {

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            if (character.isDead)
            {
                yield break;
            }
            bot.HideOnHandWeapon();
            GameObject obj = weaponPool.GetObject(); // lay weapon tu` pool
            obj.transform.position = rightHand.transform.position; // dat weapon vao tay character
            TargetWeapon(obj, enemyPos);
            StartCoroutine(FlyWeaponToTarget(obj, targetWeapon.position, 10f));
        }
        yield return null;
    }

    

}
