using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] protected Character character;
    [SerializeField] protected Transform rightHand;
    [SerializeField] protected Transform targetWeapon;
    [SerializeField] protected WeaponPool weaponPool;
    [SerializeField] protected float attackRange;
    [SerializeField] protected CharacterAnimation characterAnimation;

    public Character enemy;
    public void FindNearestTarget()
    {
        this.enemy = null;
        if (character.enemyList.Count > 0)
        {
            float minDistance = 100f;
            for (int i = 0; i < this.character.enemyList.Count; i++)
            {
                //bool alive = this.character.enemyList[i].enabled;
                float distance = Vector3.Distance(transform.position, this.character.enemyList[i].transform.position);
                if (distance < minDistance) //&& alive)
                {
                    this.enemy = this.character.enemyList[i];
                    minDistance = distance;
                }
            }
        }
    }

    public void RotateToTarget()
    {
        if (this.enemy != null)
        {
            Vector3 dir;
            dir = this.enemy.transform.position - this.transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    public void TargetWeapon(GameObject obj, Vector3 enemyPos)
    {
        Vector3 dir = enemyPos - obj.transform.position;
        dir.y = 0;
        dir = dir.normalized;
        targetWeapon.position = obj.transform.position + dir * this.attackRange;
    }
    public IEnumerator FlyWeaponToTarget(GameObject obj, Vector3 target, float speed)
    {
        while (Vector3.Distance(obj.transform.position, target) > 0.1f && obj.activeSelf)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, speed * Time.deltaTime);

            obj.transform.Rotate(0, 0, -speed * 2);
            yield return null;
        }

        character.weaponPool.ReturnToPool(obj); // sau khi bay den target thi cat vao pool
        yield return null;
    }
    
}
