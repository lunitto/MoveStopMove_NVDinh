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
        if(GameManager.instance.isGaming == false)
        {
            return;
        }
        if (character.isDead == true)
        {
            targetCircle.Deactive();
            return;
        }

        if (character.isMoving)
        {
            canAttack = false;
        }
        if(Input.GetMouseButton(0))
        {
            if (!character.onHandWeapon.activeSelf)
            {
                character.ShowOnHandWeapon();
            }
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

        if (canAttack && character.isMoving == false && enemy != null )
        {
            RotateToTarget();
            StartCoroutine(Attack());
            StartCoroutine(DelayAttack(1f));       
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
   
    public override IEnumerator Attack()
    {
        if (enemy != null)
        {
            Vector3 enemyPos = enemy.transform.position;
            character.ShowOnHandWeapon();// hien thi weapon tren tay
            characterAnimation.ChangeAnim(Const.ANIM_ATTACK);// vung tay trong 0.4s

            float elapsedTime = 0f;
            float duration = 0.4f;
            while (elapsedTime < duration)
            {
                if(character.isMoving)// neu character di chuyen thi cancel vung tay, dong thoi cancel weapon fly
                {
                    goto label;
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }              
                yield return null;
            }
            character.HideOnHandWeapon();// tat hien thi weapon tren tay
            Weapon newWeapon = character.weaponPool.GetObject().GetComponent<Weapon>(); // lay weapon tu` pool          
            newWeapon.transform.position = rightHand.transform.position; // dat weapon vao tay character

            TargetWeapon(newWeapon.gameObject, enemyPos);// dam bao weapon bay qua center cua enemy

            StartCoroutine(FlyWeaponToTarget(newWeapon.gameObject, targetWeapon.position, newWeapon.weaponData.flySpeed));
            //newWeapon.Fly(targetWeapon.position, newWeapon.weaponData.flySpeed);
            character.enemyList.Clear();
            //play sound
            if (SoundManager.instance.IsInDistance(this.transform))
            {
                SoundManager.instance.Play(SoundType.Throw);
            }
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
            if (character.isMoving) // neu di chuyen thi cancel coroutine, vi khi do canAttack == true
            {
                goto label1;
            }
            else
            {
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
        canAttack = true;
        if (character.isDead == false && GameManager.instance.isGaming == true )
        {
            characterAnimation.ChangeAnim(Const.ANIM_IDLE);
        }
        //character.enemyList.Clear();
        character.ShowOnHandWeapon();
    label1:;
    }

}
