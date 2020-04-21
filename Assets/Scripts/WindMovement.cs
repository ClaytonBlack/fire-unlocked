using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMovement : Collidable
{
    [SerializeField]
    int damageAmount = 1;

    [SerializeField]
    enum MovementPath
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField]
    MovementPath movementPath = MovementPath.Up;

    [SerializeField]
    private float moveSpeed = 1.0f;

    private readonly float xDirection = 1.0f;

    protected override void Start()
    {
        base.Start();

        switch(movementPath)
        {
            case MovementPath.Up:
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90.0f);
                break;
            case MovementPath.Left:
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180.0f);
                break;
            case MovementPath.Down:
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 270.0f);
                break;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(xDirection * Time.fixedDeltaTime * moveSpeed, 0.0f, 0.0f);
    }

    protected override void OnCollision(Collider2D collider)
    {
        if (collider.name == "Caveman")
        {
            Player caveman = (Player)collider.gameObject.GetComponent(typeof(Player));
            if (caveman != null)
            {
                caveman.TakeDamage(damageAmount);
                Destroy(this.gameObject);
            }
        }
    }

}
