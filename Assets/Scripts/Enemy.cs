using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]

    public Rigidbody2D rb2d;
    public Animator animator;
    public Transform rig;
    // the platform move back and forth between two positions
    public Transform start, end;


    [Header("Settings")]

    public Vector2 speed = Vector2.right;
    public Vector3 velocity = Vector3.zero;
    [Range(0, .3f)][SerializeField] private float movementSmoothing = .05f; // How much to smooth out the movement

    [SerializeField] bool facingRight = true;  // For determining which way the player is currently facing.
    public bool isGrounded; // is the player on the ground?



    [Header("Movement")]


    public Vector2 movement;


    public float distanceToStart;
    public float distanceToEnd;


    public float _speed = 3.0f;
    public bool _switch = false;


    void OnValidate()
    {
        start = transform.Find("Start").transform;
        end = transform.Find("End").transform;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(start.position, end.position);
        Gizmos.DrawSphere(start.position, .2f);
        Gizmos.DrawSphere(end.position, .2f);
    }

    void Awake()
    {
        // get components, stop game if not found
        rb2d = rig.GetComponent<Rigidbody2D>();
        animator = rig.GetComponent<Animator>();
        if (rb2d == null || animator == null)
        {
            Debug.LogError("Rigidbody2D and Animator required");
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    void FixedUpdate()
    {
        if (transform == null) return;

        // determine direction (non-physics)
        if (!_switch)
            rig.position = Vector3.MoveTowards(rig.position, start.position, _speed * Time.deltaTime);
        else
            rig.position = Vector3.MoveTowards(rig.position, end.position, _speed * Time.deltaTime);


        distanceToStart = Vector3.Distance(rig.position, start.position);
        distanceToEnd = Vector3.Distance(rig.position, end.position);


        // check that it reached the start || end position
        if (distanceToStart < .5f)
        {
            _switch = true;
            Flip();
        }
        else if (distanceToEnd < .5f)
        {
            _switch = false;
            Flip();
        }

    }





    public void Move(float move)
    {
        // set the speed variable
        animator.SetFloat("Speed", Mathf.Abs(move));

        //only control the player if isGrounded or airControl is turned on
        if (isGrounded)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * speed.x, rb2d.linearVelocity.y);
            // And then smoothing it out and applying it to the character
            rb2d.linearVelocity = Vector3.SmoothDamp(rb2d.linearVelocity, targetVelocity, ref velocity, movementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if ((move > 0 && !facingRight) || (move < 0 && facingRight))
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = rig.transform.localScale;
        theScale.x *= -1;
        rig.transform.localScale = theScale;
    }

}
