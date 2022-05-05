using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // Stop the ball when goalkeeper catch it
            Debug.Log("Catch ball");
            //other.gameObject.GetComponent<Ball>().beingCatch();
        }
    }
}
