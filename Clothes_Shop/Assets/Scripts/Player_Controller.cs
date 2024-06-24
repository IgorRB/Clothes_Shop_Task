using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] Animator[] charAnimators;

    private float xInput, yInput;
    // private Vector3 direction;
    private Rigidbody2D myRb;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        myRb.velocity = new Vector3(xInput, yInput, 0).normalized * moveSpeed;

        foreach(Animator animator in charAnimators)
        {
            if(animator.gameObject.activeInHierarchy == true) SetAnimation(animator);
        }
    }

    private void SetAnimation(Animator anim)
    {
        if (myRb.velocity.magnitude == 0) anim.SetBool("isMoving", false);
        else
        {
            anim.SetFloat("horizontal", xInput);
            anim.SetFloat("vertical", yInput);
            anim.SetBool("isMoving", true);
        }
    }
}
