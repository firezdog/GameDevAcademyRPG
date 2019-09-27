using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Animation : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D playerBody;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        MovePlayer(horizontal, vertical);
        AnimatePlayer(horizontal, vertical);
    }

    private void MovePlayer(float horizontal, float vertical)
    {
        playerBody.velocity = new Vector2(horizontal, vertical) * moveSpeed;    
    }

    void AnimatePlayer(float horizontal, float vertical) {
        bool moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving);
        if (moving) {
            animator.SetFloat("DirectionX", horizontal);
            animator.SetFloat("DirectionY", vertical);
        }
    }

}
