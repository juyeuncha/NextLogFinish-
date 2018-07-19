
using UnityEngine;

public enum PlayerState
{
    Jump,
    DJump,
    Run,
    Fall,
    DFall,
    Death
}

public class player : MonoBehaviour {


    public PlayerState PS;
    public float Jumppower=850f;
    private float AxisY;
    Rigidbody rb;
    public Animator animator;
    //public int JumpCount=2;


   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //MassPower = rb.mass;
        AxisY = transform.position.y;
        
        //JumpCount=0;
        
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
                if (PS == PlayerState.Fall)
                {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * Jumppower, ForceMode.Impulse);
                }
                /*if (PS == PlayerState.Fall)
                {
                    DJump();
                
                }*/

            /*if (anim["Character_Fall00"].time<1)
            {
                Debug.Log();
            }*/

            /*if (PS == PlayerState.Fall)
            {
                rb.velocity = Vector3.zero;
                DJump();
            }*/
            
        }



        if (PS == PlayerState.Jump)
        {

            if (AxisY > transform.position.y)
            {
                Fall();

            }
        }

        if (PS == PlayerState.DJump)
        {
            if(AxisY > transform.position.y)
            {
                 DFall();
            }
        }

            AxisY = transform.position.y;
        
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
        /*JumpCount++;
        Debug.Log(JumpCount);*/
        AxisY = transform.position.y;
        //rb.mass = 1f;
        rb.AddForce(Vector3.up*Jumppower,ForceMode.Impulse);
        animator.SetTrigger("Jump");
        //animator.SetBool("Ground", false);
    }

    void DJump()
    {
        PS = PlayerState.DJump;
        /*JumpCount++;
        Debug.Log(JumpCount);*/
        //rb.mass = 1f;
        //rb.velocity = Vector3.zero;
        
        rb.AddForce(Vector3.up*Jumppower,ForceMode.Impulse);
        
        animator.SetTrigger("DJump");
    }

    void Run()
    {
 
        PS = PlayerState.Run;
        /*JumpCount = 0;
        Debug.Log(JumpCount);*/
        //animator.SetBool("Ground", true);
    }

    void Fall()
    {
        PS = PlayerState.Fall;
        rb.AddForce(-Vector3.up * Jumppower,ForceMode.Impulse);
        animator.SetTrigger("Fall");
        //rb.mass = 50f;
    }

    void DFall()
    {
        PS = PlayerState.DFall;
        rb.AddForce(-Vector3.up * Jumppower, ForceMode.Impulse);
        animator.SetTrigger("Fall");
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
