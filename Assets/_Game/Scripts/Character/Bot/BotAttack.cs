using System.Collections;
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


    public override IEnumerator Attack()
    {

        Vector3 enemyPos = enemy.transform.position;
        bot.ShowOnHandWeapon();
        characterAnimation.ChangeAnim("attack");
        RotateToTarget();

        yield return new WaitForSeconds(0.4f);//thời gian vung tay cho đến khi vũ khí rời bàn tay là 0.4s

        if (character.isDead)
        {
            yield break;
        }
        bot.HideOnHandWeapon();
        Weapon newWeapon = weaponPool.GetObject().GetComponent<Weapon>(); // lay weapon tu` pool
        newWeapon.transform.position = rightHand.transform.position; // dat weapon vao tay phai character
        TargetWeapon(newWeapon.gameObject, enemyPos);
        newWeapon.Fly(targetWeapon.position, newWeapon.weaponData.flySpeed);

        yield return null;
    }



}
