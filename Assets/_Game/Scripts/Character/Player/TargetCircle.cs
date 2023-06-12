using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private float rotateSpeed;
    public Transform enemyTransform;
    //public bool active;

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            transform.Rotate(0, rotateSpeed, 0);
            if (enemyTransform != null)
            {
                transform.position = enemyTransform.position;
            }
        }
      
    }

    public void Active()
    {
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }
        if (playerAttack.enemy != null )
        {
            this.enemyTransform = playerAttack.enemy.transform;
        }
    }

    public void Deactive()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
}
