using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoMovement : Collidable
{
    enum MovementPath
    {
        Horizontal,
        Vertical,
        Random
    };

    [SerializeField]
    int damageAmount = 1;

    [SerializeField]
    MovementPath movementPath = MovementPath.Horizontal;
    float xDirection = 1.0f;
    float yDirection = 1.0f;

    [SerializeField]
    private float moveSpeed = 1.0f;

    private bool resetCollision = false;

    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        float xMove = 0.0f;
        float yMove = 0.0f;

        if (movementPath == MovementPath.Horizontal || movementPath == MovementPath.Random)
            xMove = moveSpeed * xDirection * Time.fixedDeltaTime;

        if (movementPath == MovementPath.Vertical || movementPath == MovementPath.Random)
            yMove = moveSpeed * yDirection * Time.fixedDeltaTime;

        Vector3 moveDelta = new Vector3(xMove, yMove, 0);

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x), LayerMask.GetMask("Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x, 0, 0);
        }
        else
        {
            transform.Translate(-moveDelta.x, 0, 0);
            xDirection *= -1.0f;
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y), LayerMask.GetMask("Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y, 0);
        }
        else
        {
            transform.Translate(0, -moveDelta.y, 0);
            yDirection *= -1.0f;
        }
    }

    protected override void OnCollision(Collider2D collider)
    {
        if (collider.name == "Caveman" && resetCollision == false)
        {
            resetCollision = true;
            Player caveman = (Player)collider.gameObject.GetComponent(typeof(Player));
            if (caveman != null)
                caveman.TakeDamage(damageAmount);
        }
    }

}
