using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private DashController dashController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashController = GetComponent<DashController>();
    }

    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }



    void FixedUpdate()
    {
        if (!dashController.IsDashing())
        {
            // Movement
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

}