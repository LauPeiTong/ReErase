using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //speed, rigidbody
    public float speed = 5f;
    Rigidbody2D rbody;

    //joystick
    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
         rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        //********************moving direction**************************
        Vector2 moveVector = new Vector2(moveX, moveY);
        // if(moveVector.x != 0 || moveVector.y != 0){
        //     lookDirection = moveVector;
        // }
        // anim.SetFloat("Look X", lookDirection.x);
        // anim.SetFloat("Look Y", lookDirection.y);
        // anim.SetFloat("Speed", moveVector.magnitude);

        //********************moving**************************
        Vector2 position = rbody.position;
        position += moveVector * speed * Time.fixedDeltaTime;
        rbody.MovePosition(position);
    }
}
