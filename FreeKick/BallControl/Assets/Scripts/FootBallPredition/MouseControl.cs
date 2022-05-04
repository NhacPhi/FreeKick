using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    #region Fields
    public GameObject rootMidPoint;

    //public GameObject rootEndPoint;

    #endregion
    #region Local param
    private Vector3 startPointMouse;

    private Transform rootMidPointPosition;

    private float detalDistance;

    private float yMid = 0;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
       rootMidPointPosition = rootMidPoint.transform.GetChild(0).gameObject.transform;
        Debug.Log(rootMidPointPosition.localPosition);
        yMid = rootMidPointPosition.localPosition.y;

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        RotateRootMidPoint();
    }
    void HandleInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Set start mouse position
            startPointMouse = Input.mousePosition;
            //Debug.Log("Mouse position: " + startPointMouse);
            detalDistance = 0;

        }
        if(Input.GetMouseButton(0))
        {
            // Handle;
            detalDistance =(float) (Input.mousePosition.x - startPointMouse.x)/300;
            if (detalDistance > 1)
                detalDistance = 1;
            if (detalDistance < -1)
                detalDistance = -1;
            //Debug.Log("Distance: " + detalDistance);
        }
        if(Input.GetMouseButtonUp(0))
        {
            // Shoot
            //Debug.Log("Shoot");
        }
    }
    void RotateRootMidPoint()
    {
        rootMidPoint.transform.localEulerAngles= new Vector3(0, 0, 60 * detalDistance);
        rootMidPointPosition.localPosition =  new Vector3(rootMidPointPosition.localPosition.x, yMid + 2 * Mathf.Abs(detalDistance), rootMidPointPosition.localPosition.z);
    }
}