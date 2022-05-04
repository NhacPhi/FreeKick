using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
public class ControlBallFollower : MonoBehaviour
{
    #region Fields

    #endregion

    #region Local param
    private PathFollower path;
    private Rigidbody rigid;
    private Animator animator;
    private Vector3 startPoint;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<PathFollower>();
        rigid = GetComponent<Rigidbody>();
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }
    #region Function
    void HandleInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Reset
            ResetBall();
        }
        if(Input.GetMouseButtonUp(0))
        {
            // Shoot
            ShootBall();
        }
    }

    void ResetBall()
    {
        path.SetFly(false);
        transform.position = startPoint;
        path.ResetPath();
        rigid.isKinematic = true;
        rigid.isKinematic = false;
    }
    void ShootBall()
    {
        path.SetFly(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Enter");
            path.SetFly(false);
        }
    }
    #endregion
}
