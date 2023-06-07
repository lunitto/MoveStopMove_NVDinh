using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject joystickBackground;
    [SerializeField] private GameObject joystickHandle;
    [SerializeField] private CharacterAnimation characterAnimation;
    [SerializeField] private Player player;

    private Vector3 firstMousePosition;
    private Vector3 currentMousePosition;
    private Vector3 direction;
    private float joystickRadius = 120f;
    
    public Rigidbody rb;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstMousePosition = Input.mousePosition;
            joystickBackground.transform.position = firstMousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            currentMousePosition = Input.mousePosition;
            direction = currentMousePosition - firstMousePosition;
            joystickHandle.transform.position = currentMousePosition;
            if (Vector3.Distance(joystickHandle.transform.position, joystickBackground.transform.position) > joystickRadius)
            {
                joystickHandle.transform.position = joystickBackground.transform.position - (joystickBackground.transform.position - joystickHandle.transform.position).normalized * joystickRadius;
            }
            if (Vector3.Distance(joystickHandle.transform.position, joystickBackground.transform.position) > joystickRadius / 2)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
                rb.velocity = new Vector3(direction.x, 0, direction.y).normalized * speed;
                characterAnimation.ChangeAnim("run");
            }
            player.isMoving = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            characterAnimation.ChangeAnim("idle");
            joystickBackground.transform.position += new Vector3(10000, 0, 0);//hide joystick
            rb.velocity = new Vector3(0, 0, 0);
            player.isMoving = false;
        }
    }
}
