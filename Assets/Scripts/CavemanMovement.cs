using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavemanMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    
    void Update()
    {
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
    }
}
