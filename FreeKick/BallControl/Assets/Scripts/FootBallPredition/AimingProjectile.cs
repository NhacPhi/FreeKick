using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class AimingProjectile : MonoBehaviour
{
    #region Fields

    public Transform midPoint;

    public Transform endPoint;

    
    #endregion

    #region Local param
    private PathCreator path;
    private Vector3 startPoint;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        path = GetComponent<PathCreator>();
        path.bezierPath.SplitSegment(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), 0, 0.5f);
        path.bezierPath.SetPoint(0, startPoint);
    }

    // Update is called once per frame
    void Update()
    {
        path.bezierPath.MovePoint(3, midPoint.position);
        path.bezierPath.MovePoint(6, endPoint.position);
        path.editorData.BezierPathEdited();
    }
}
