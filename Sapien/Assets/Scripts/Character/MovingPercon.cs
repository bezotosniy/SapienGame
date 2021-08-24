using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingPercon : MonoBehaviour
{
    
    GameObject camera;
    CharacterController ch;
   
   
    [Range(0, 10)]
    public float speed;
    [Range(0, 10)]
    public float speedPoint;
    [Range(0, 10)]
    public float speedRot;
    [Range(0, 10)]
    public float jumpForce;
    public Animator Anim;
   [SerializeField]
    private  float GravityMode;
    [Range(0, 50)]
    public float minusGrav;
    [Range(0, 2)]
    public float timeJump;

    private Vector3 moveVector;

    private bool VarMoving = true, MovingBool, isGrounded, jumpClosed = true;
    public bool second;
   
    // Start is called before the first frame update 
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
     
        ch = GetComponent<CharacterController>();
    }

    void GetMoveDirection()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = GravityMode;
        moveVector.z = Input.GetAxis("Vertical");

        moveVector = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * moveVector;
    }
    
    void Moving()
    {
        if (VarMoving)
        {
            moveVector = Vector3.zero;
            if (jumpClosed) 
                GetMoveDirection();
                
            Debug.Log($"<color=yellow>{moveVector}</color>" );

            if (moveVector.x != 0 || moveVector.z != 0) {
                Anim.SetBool("Moving", true); 
                MovingBool = true; 
                
                //Rotate to move direction
                RotateToDirection(transform , transform.position + moveVector);
                //Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveVector.x , 0 , moveVector.z));
                //targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
                //transform.rotation = targetRotation;
            } 
            else {
                Anim.SetBool("Moving", false); 
                MovingBool = false; 
            }

            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                //Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedRot, 0.0f);
                //transform.rotation = Quaternion.LookRotation(direct);
                
            }
            ch.Move(Time.deltaTime * speed * moveVector);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        GamingGravity();
    }
    

    void RotateToDirection(Transform source, Vector3 point)
    {
        Vector3 direction = point - source.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation = Quaternion.Slerp(source.rotation, targetRotation, Time.deltaTime * 5);
        source.rotation = targetRotation;
    }
   
    public void SecondMoving(bool go,Vector3 point, NavMeshAgent agent)
    {
        if (go && !second)
        {
            VarMoving = false;
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                second = true;
                VarMoving = true; agent.enabled = false;
            }
            if (!Vector3.Equals(agent.velocity , Vector3.zero))
            {
                Anim.SetBool("Moving", true);
                MovingBool = true; 
                agent.updateRotation = false;
                RotateToDirection(agent.transform , point);

            }
            if (Vector3.Distance( transform.position, point) <.3f)
            {
                agent.enabled = false;
                MovingBool = false; 
                VarMoving = true;
                second = true;
                Anim.SetBool("Moving", false);
            }
        }
    }
    IEnumerator JumpInPlace()
    {
        yield return new WaitForSeconds(timeJump);
        GravityMode = jumpForce; 
        yield return new WaitForSeconds(2.05f- timeJump);
        jumpClosed = true;
    }
    private void GamingGravity()
    {

        if ((ch.collisionFlags & CollisionFlags.Below) != 0)
        {
            GravityMode = -10;
            isGrounded = true;
        }
        else
        {
            GravityMode -= minusGrav * Time.deltaTime; isGrounded = false;
        }

    


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpClosed)
        {

            if (MovingBool)
            {
                GravityMode = jumpForce;
                Anim.SetTrigger("Jump");
                Debug.Log("JUMP in RUN");
            }
            else
            {
                Anim.SetTrigger("JumpInPlace"); 
                jumpClosed = false; 
                StartCoroutine(JumpInPlace());
            }
        }
    }

    
}
