
using UnityEngine;

public enum PlayerState
{
    Jump,
    DJump,
    Run,
    Death
}

public class player : MonoBehaviour {


    public PlayerState PS;
    public float Jumppower=850f;

   // public float DJumppower = 350f;
    
    Rigidbody rb;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        rb.AddForce(new Vector3(0, Jumppower, 0));

        animator.SetTrigger("Jump");
        //animator.SetBool("Ground", false);
    }

    void DJump()
    {
        PS = PlayerState.DJump;
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector3(0, Jumppower, 0));
    }

    void Run()
    {
        PS = PlayerState.Run;

        //animator.SetBool("Ground", true);
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
