using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] Character character;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != this.gameObject && character.isDead == false)
        {
            if (other.gameObject.CompareTag(Const.BOT) || other.gameObject.CompareTag(Const.PLAYER))
            {
                Character otherCharacter = other.gameObject.GetComponent<Character>();
                if (otherCharacter.isDead == false )
                {
                    character.enemyList.Add(other.gameObject.GetComponent<Character>());
                }
                
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != this.gameObject)
        {
            if (other.gameObject.CompareTag(Const.BOT) || other.gameObject.CompareTag(Const.PLAYER))
            {
                character.enemyList.Remove(other.gameObject.GetComponent<Character>());
            }
        }
    }

}
