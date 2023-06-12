using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
public class PlayerAttack : CharacterAttack
{   
    [SerializeField] private TargetCircle targetCircle;

    private bool canAttack;
   

    protected void Start()
    {
        canAttack = true; // character dung yen tu dau game van co the attack enemy di vao circle
        targetCircle.Deactive();
    }

    protected void Update()
    {
        if (character.isDead == true)
        {
            targetCircle.Deactive();
            return;
        }

        if (character.isMoving)
        {
            canAttack = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            canAttack = true;
        }

        if (character.enemyList.Count > 0)
        {
            FindNearestTarget();
        }
        else
        {
            enemy = null;
        }

        if (canAttack && character.isMoving == false && enemy != null && !enemy.isDead)
        {
            RotateToTarget();
            StartCoroutine(Attack());
            StartCoroutine(DelayAttack(1f));
            CheckEnemy();
        }

        if (character.enemyList.Count > 0)
        {
            targetCircle.Active();
        }
        else
        {
            targetCircle.Deactive();
        }
        
    }

    public void CheckEnemy()
    {
        for (int i = 0; i < character.enemyList.Count; i++)
        {
            if (!this.gameObject.activeInHierarchy)
            {
                character.enemyList.Remove(character.enemyList[i]);
            }
        }
    }
   
    public IEnumerator Attack()
    {
        if (enemy != null)
        {
            Vector3 enemyPos = enemy.transform.position;
            
            characterAnimation.ChangeAnim("attack");
            float elapsedTime = 0f;
            float duration = 0.4f;
            while (elapsedTime < duration)
            {
                if(character.isMoving)
                {
                    goto label;
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }              
                yield return null;
            }
            character.HideOnHandWeapon();
            GameObject obj = character.weaponPool.GetObject(); // lay weapon tu` pool
            obj.transform.position = rightHand.transform.position; // dat weapon vao tay character
            TargetWeapon(obj, enemyPos);
            StartCoroutine(FlyWeaponToTarget(obj, targetWeapon.position, 10f));
        }
        label:;
        yield return null;
    }

    public IEnumerator DelayAttack(float delayTime)
    {
        canAttack = false;
        float elapsedTime = 0f;
        float duration = delayTime;
        while (elapsedTime < duration)
        {           
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canAttack = true;
        if (!character.isDead)
        {
            characterAnimation.ChangeAnim("idle");
        }
        character.enemyList.Clear();
        character.ShowOnHandWeapon();
    }

}
