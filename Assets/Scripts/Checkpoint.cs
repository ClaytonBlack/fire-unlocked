using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Checkpoint : Collidable
{
    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private bool isLit = false;

    [SerializeField]
    private bool isLevelEnd = false;
    protected override void Start()
    {
        base.Start();
        animator.SetBool("Lit", isLit);
    }

    protected override void OnCollision(Collider2D collider)
    {
        if (collider.name == "Caveman")
        {
            if (!isLit)
            {
                isLit = true;
                animator.SetBool("Lit", isLit);
            }
            Player caveman = (Player)collider.gameObject.GetComponent(typeof(Player));
            if (caveman != null)
                caveman.SetCheckpoint();

            if (isLevelEnd)
            {
                SceneManager.LoadScene(2);
            }

        }
    }
}
