
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
    private float AxisY;
    Rigidbody rb;
    public Animator animator;
    public int JumpCount=2;
    public GlobVar globvar;

   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AxisY = transform.position.y; 
        JumpCount=0;
        
    }
    
    //update

        public void KeyCtrl()
        {
            if (Input.GetKeyDown(KeyCode.Space) && JumpCount > 0)
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
                    rb.AddForce(Vector3.up * globvar.jumppower, ForceMode.Impulse);
                    JumpCount--;
                }

            }
        }

        public void JumpCtrl() {
            if (PS == PlayerState.Jump)
            {

                if (AxisY > transform.position.y)
                {
                    Fall();

                }
            }

            if (PS == PlayerState.DJump)
            {
                if (AxisY > transform.position.y)
                {
                    DFall();
                }
            }

            AxisY = transform.position.y;
        }   
    
    //endofupdate

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
        rb.AddForce(Vector3.up*globvar.jumppower,ForceMode.Impulse);
        animator.SetTrigger("Jump");
        JumpCount--;
    }

    void DJump()
    {
        PS = PlayerState.DJump;        
        rb.AddForce(Vector3.up*globvar.jumppower,ForceMode.Impulse);
        
        animator.SetTrigger("DJump");
        JumpCount--;
    }

    void Run()
    {
 
        PS = PlayerState.Run;
        JumpCount = 2;
        
    }

    void Fall()
    {
        if (rb == null) { return; }
        PS = PlayerState.Fall;
        rb.AddForce(-Vector3.up * globvar.jumppower,ForceMode.Impulse);
        animator.SetTrigger("Fall");
    }

    void DFall()
    {
        PS = PlayerState.DFall;
        rb.AddForce(-Vector3.up * globvar.jumppower, ForceMode.Impulse);
        animator.SetTrigger("DFall");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
             
            GameManager.instance.GetCoin();

            Destroy(other.gameObject);
            
            return;
        }

        if (other.gameObject.tag == "Finish" && PS != PlayerState.Death)
        {
            PS = PlayerState.Death;

            GameManager.instance.GameOver();
            
        }

    }

}
