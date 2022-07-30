using UnityEngine;

public class Player : MonoBehaviour
{
    public FixedJoystick Joystick;
    public float speed = 3;
    

    private Rigidbody Rigidbody;
    private Animator Animator;
    private Vector3 moveVec;
    private SphereCollider SphereCollider;


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        SphereCollider = GetComponentInChildren<SphereCollider>();
    }



    private void FixedUpdate()
    {
        float x = Joystick.Horizontal;
        float z = Joystick.Vertical;

        moveVec = new Vector3(x, 0, z) * speed * Time.deltaTime;
        Rigidbody.MovePosition(Rigidbody.position + moveVec);
        

        if (moveVec.sqrMagnitude == 0)
            return;

        Quaternion dirQuat = Quaternion.LookRotation(moveVec);
        Quaternion moveQuat = Quaternion.Slerp(Rigidbody.rotation, dirQuat, 0.3f);
        Rigidbody.MoveRotation(moveQuat);

        

    }

    private void LateUpdate()
    {
        Animator.SetFloat("Move", moveVec.sqrMagnitude*1000);
    }


    private void OnCollisionEnter(Collision ohter)
    {
        if(ohter.gameObject.tag == "Enemies")
        {
            Animator.SetTrigger("Attack");
        }
        

    }


    private void OnCollisionExit(Collision collision)
    {
        
    }
}
