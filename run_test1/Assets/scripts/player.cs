
using UnityEngine;

public enum PlayerState
{
    Jump,
    DJump,
    Run,
    Fall,
    Death
}

public class player : MonoBehaviour {


    public PlayerState PS;
    public float Jumppower=850f;
    
    private float AxisY;
    

   // public float DJumppower = 350f;
    
    Rigidbody rb;
    

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //MassPower = rb.mass;
        AxisY = transform.position.y;
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            if (PS == PlayerState.Jump)
            {
                DJump();
            }
            if (PS == PlayerState.Run)
            {
                Jump();
            }
        }

        if (PS == PlayerState.Jump || PS == PlayerState.DJump)
        {

            if (AxisY > transform.position.y)
            {
                Fall();

            }

            AxisY = transform.position.y;
        }
    }

    private void FixedUpdate()
    {
        //Jump();
        //DJump();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (PS != PlayerState.Run)
        {
            Run();
        }
    }

    void Jump()
    {
        PS = PlayerState.Jump;
        AxisY = transform.position.y;
        //rb.mass = 1f;
        rb.AddForce(Vector3.up*Jumppower,ForceMode.Impulse);
        animator.SetTrigger("Jump");
        //animator.SetBool("Ground", false);
    }

    void DJump()
    {
        PS = PlayerState.DJump;
        //rb.mass = 1f;
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up*Jumppower,ForceMode.Impulse);
        animator.SetTrigger("DJump");
    }

    void Run()
    {
 
        PS = PlayerState.Run;
        
        
        //animator.SetBool("Ground", true);
    }

    void Fall()
    {
        PS = PlayerState.Fall;
        rb.AddForce(-Vector3.up * Jumppower,ForceMode.Impulse);
        //rb.mass = 50f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
             
            coinmanager.instance.GetCoin();

            Destroy(other.gameObject);
            
            return;
        }

        if (other.gameObject.tag == "Finish" && PS != PlayerState.Death)
        {
            PS = PlayerState.Death;

            coinmanager.instance.GameOver();
            
        }

    }

   /*
    private void GetCoin()
    {
        throw new NotImplementedException();
    } 
    */
}
