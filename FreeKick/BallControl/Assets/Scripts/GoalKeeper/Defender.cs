using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField]
    LayerMask GroundLayer;

    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private float wallHeight = 1f;

    private bool isDefending = false;
    private bool isGround = false;

    private Rigidbody rb;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.localScale  = new Vector3(1f, wallHeight, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, 1.1f, GroundLayer);
        if (isGround && isDefending)
        {
            isDefending = false;
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    public void Defend()
    {
        isDefending = true;
    }
}
