using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] protected Character character;
    [SerializeField] protected Transform rightHand;
    [SerializeField] protected Transform targetWeapon;
    [SerializeField] protected WeaponPool weaponPool;
    [SerializeField] protected float attackRange;
    [SerializeField] protected CharacterAnimation characterAnimation;
    [SerializeField] private TargetCircle targetCircle;

    private bool canAttack;
    public Character enemy;

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

        if (canAttack && character.isMoving == false && enemy != null)
        {
            RotateToTarget();
            StartCoroutine(Attack());
            StartCoroutine(DelayAttack(2f));
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

    public void FindNearestTarget()
    {
        this.enemy = null;
        if (character.enemyList.Count > 0)
        {
            float minDistance = 100f;
            for (int i = 0; i < this.character.enemyList.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, this.character.enemyList[i].transform.position);
                if (distance < minDistance)
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
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            character.HideOnHandWeapon();
            GameObject obj = character.weaponPool.GetObject(); // lay weapon tu` pool
            obj.transform.position = rightHand.transform.position; // dat weapon vao tay character
            Vector3 dir = enemyPos - obj.transform.position;
            dir.y = 0;
            dir = dir.normalized;
            targetWeapon.position = obj.transform.position + dir * attackRange;
            StartCoroutine(FlyWeaponToTarget(obj, targetWeapon.position, 10f));
        }
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
        character.ShowOnHandWeapon();
    }


    public IEnumerator FlyWeaponToTarget(GameObject obj, Vector3 target, float speed)
    {
        while (Vector3.Distance(obj.transform.position, target) > 0.1f && obj.activeSelf)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, speed * Time.deltaTime);
            
            obj.transform.Rotate(0, 0, -speed);
           
            yield return null;
        }
        character.weaponPool.ReturnToPool(obj); // sau khi bay den target thi cat vao pool
        yield return null;
    }

    
}
