using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
public class ControlBall : MonoBehaviour
{
    #region Field 
    public float startSpeed = 0;

    public DrawPath pen;

    public float force = 0;

    [SerializeField]
    private LayerMask layerMask;
    #endregion
    #region Local param
    private PathFollower pathFollower;
    private Rigidbody rigid;
    private Animator animator;
    private Vector3 startPoint;
    private Vector3 Direction;
    // Speed
    bool isFly;
    bool isForce;
    float speed;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        pathFollower = GetComponent<PathFollower>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startPoint = transform.position;
        isFly = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log("Ray cast layer close");
                isForce = true;
            }
            else
            {
                isForce = false;
            }

            Direction = hit.point - pen.startPoint;
        }

        HandleInput();
        if (isFly)
        {
            if(speed > 10)
            speed = speed - Time.deltaTime;
            pathFollower.speed = speed;
        }
        else
        {
            pathFollower.SetFly(false);
        }
    }
    #region Function
    void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(pen.isAddforce && isForce)
            {
                rigid.AddForce(Direction * force);
                animator.SetBool("isRotate", true);
                Debug.Log("Add force :"+ Direction.normalized * force);
            }else
            {
                // Strart fly
                animator.SetBool("isRotate", true);
                speed = startSpeed;
                isFly = true;
                pathFollower.SetFly(true);
            }
           
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            // Reset
            ResetBall();
        }
    }
    void ResetBall()
    {
        isFly = true;
        animator.SetBool("isRotate", false);
        speed = startSpeed;
        transform.position = startPoint;
        rigid.isKinematic = true;
        rigid.isKinematic = false;
        pathFollower.SetFly(false);
        Direction = Vector3.zero;
        isForce = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Enter");
            isFly = false;
        }
    }
    #endregion
}
