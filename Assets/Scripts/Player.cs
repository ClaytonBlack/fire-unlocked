using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 checkpointPosition;
    [SerializeField]
    private int startingHitPoints = 3;
    private int currentHitPoints = 3;
    private BoxCollider2D boxCollider;
    [SerializeField]
    private float moveSpeed = 1.0f;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
        checkpointPosition = transform.position;
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        float y = Input.GetAxisRaw("Vertical") * moveSpeed * Time.fixedDeltaTime;

        Vector3 moveDelta = new Vector3(x, y, 0);

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y), LayerMask.GetMask("Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x), LayerMask.GetMask("Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x, 0, 0);
        }
    }

    public void SetCheckpoint()
    {
        checkpointPosition = transform.position;
    }

    public void TakeDamage(int points)
    {
        currentHitPoints--;

        if (currentHitPoints <= 0)
        {
            transform.position = checkpointPosition;
            currentHitPoints = startingHitPoints;
        }
    }
}
