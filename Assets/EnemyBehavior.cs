using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private float speed = 1f, range;
    public Transform player;
    private Rigidbody2D rigidBodyEnemy;
    private Vector2 movement;

    private void Start()
    {
        rigidBodyEnemy = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 direction = (player.position - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * (Mathf.Rad2Deg);
        rigidBodyEnemy.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rigidBodyEnemy.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

 /*   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            Destroy(gameObject);
        }
    }
 */
}
