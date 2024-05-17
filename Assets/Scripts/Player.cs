using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVector;
    public float speed;

    Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // add force
        //rigidbody2D.AddForce(inputVector);
        // velocity
        //rigidbody2D.velocity = inputVector;
        // move
        Vector2 nextVector = inputVector.normalized * speed * Time.fixedDeltaTime;
        rigidbody2D.MovePosition(rigidbody2D.position + nextVector);
    }
}
