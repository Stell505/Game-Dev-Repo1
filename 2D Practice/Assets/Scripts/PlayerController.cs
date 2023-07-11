using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Character Movement Variables
    public float moveSpeed;
    //public bool isMoving;
    public Animator animator;
    public Rigidbody2D rb;

    private Vector2 movement;
    public LayerMask interactableLayer;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        //Directional Input (W,A,S,D)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Update movement and idle animations
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        //Face the correct idle direction after moving
        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || 
            Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastY", Input.GetAxisRaw("Vertical"));
        }

        //Object Interaction on F Key
        if(Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }

    }

    private void FixedUpdate()
    {
            Move();

    }

    private void Move()
    {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("LastX")/2, animator.GetFloat("LastY")/2);
        var interactPos = transform.position - new Vector3(0, .5f, 0) + facingDir;

        Debug.DrawLine(transform.position - new Vector3(0, .5f, 0), interactPos, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

}
