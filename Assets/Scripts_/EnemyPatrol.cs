using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;         // Speed of the enemy patrol
    public float moveDistance = 5f;  // Distance to patrol back and forth
    private Vector2 startingPosition; // Starting position of the enemy
    private bool movingRight = true;  // Direction the enemy is moving

    void Start()
    {
        startingPosition = transform.position; // Store the starting position
    }

    void Update()
    {
        // Move the enemy
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= startingPosition.x + moveDistance)
            {
                movingRight = false; // Change direction
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= startingPosition.x - moveDistance)
            {
                movingRight = true; // Change direction
            }
        }
    }
}

