using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 input;

    public LayerMask stopMovement;

    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
    }

    void MoveInput()
    {
        if(!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;
            
            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                
                if(IsSpaceAvailable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    private bool IsSpaceAvailable(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(targetPos, 0.5f, stopMovement) == null;
    }
}
