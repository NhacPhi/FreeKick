using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBall : MonoBehaviour
{
    //public Transform testPoint;
    [SerializeField]
    private Transform ballTarget;
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform CatchBallTransform;
    [SerializeField]
    private float m_JumpHeight = 2f;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float reactDistance = 6f;

    [SerializeField]
    LayerMask GroundLayer;

    private Rigidbody rb;
    private BoxCollider boxCollider;

    private bool isSave;
    private bool isPrepare;
    private bool isGround;

    private Vector3 saveDirection;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.center = new Vector3(reactDistance, 1, 0);
        isSave = false;
        isPrepare = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
        //    saveDirection = (testPoint.position - transform.position).normalized;
        //    rb.AddForce(saveDirection * m_JumpHeight, ForceMode.Impulse);
        //    StartCoroutine(Ready(3f));
        //}

        if (Input.GetKey(KeyCode.H))
        {
            Vector3 moveTarget = new Vector3(ballTarget.position.x, transform.position.y, ballTarget.position.z);
            Vector3 direction = moveTarget - transform.position;
            rb.MovePosition(transform.position + direction * 4f * Time.deltaTime);
            //rb.AddForce(direction * 4f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPrepare = true;
        }    

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetState();
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, 1.1f, GroundLayer);
        if (isGround)
        {
            if (isSave)
            {
                Save();
                isSave = false;
                isPrepare = false;
                Debug.Log("Save ball");
            }
            else if (isPrepare)
            {
                Prepare();
            }    
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Ball")
        {

            if (obj != null)
            {
                //obj.GetComponent<Ball>().beingCatch();
                CatchBall();
            }
        }
    }

    public void Save()
    {
        // save front and above
        if (ballTarget.position.z > transform.position.z + 2 || ballTarget.position.z < transform.position.z - 2)
        {
            rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
        }
        saveDirection = (ballTarget.position - transform.position).normalized;
        rb.AddForce(saveDirection * m_JumpHeight, ForceMode.Impulse);
        StartCoroutine(Ready(3f));
        //Debug.Log("Save ball");
    }

    public void Prepare()
    {
        Vector3 moveTarget = new Vector3(ballTarget.position.x, transform.position.y, ballTarget.position.z);
        Vector3 direction = moveTarget - transform.position;
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            isSave = true;
            //Vector3 focusPosition = other.gameObject.transform.position;
            //ballTarget.position = new Vector3(transform.position.x, focusPosition.y, focusPosition.z);
        }
    }

    public void ResetState()
    {
        // random position
        transform.position = startPoint.position + new Vector3(Random.Range(-3, 3), 0, 0);
        if (Random.Range(0, 100) > 50)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }    

        transform.rotation = Quaternion.identity;
        CatchBallTransform.localPosition = Vector3.zero;
        CatchBallTransform.localRotation = Quaternion.identity;
        ResetVelocity();
        rb.Sleep();
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void CatchBall()
    {
        ResetVelocity();
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
    }

    private IEnumerator Ready(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ResetState();
    }
}
