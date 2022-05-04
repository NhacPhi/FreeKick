using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class ControlPath : MonoBehaviour
{
    #region Fields
    [SerializeField]
    public DrawPath pen;

    [SerializeField]
    public float maxCurve;
    #endregion
    #region Local param
    private PathCreator path;

    Vector3 midPoint;

    private Vector3 startPoint;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<PathCreator>();
        path.bezierPath.SplitSegment(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), 0, 0.5f);
        path.bezierPath.SetPoint(0, transform.position);
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        path.bezierPath.MovePoint(6, pen.endPoint);


            //if (pen.endPoint.x - pen.midPoint.x > maxCurve)
            //{
            //    pen.midPoint.x = pen.endPoint.x - maxCurve;
            //}
            //else if (pen.endPoint.x - pen.midPoint.x < maxCurve)
            //{
            //    pen.midPoint.x = pen.endPoint.x + maxCurve;
            //}
        //float dis = 0 - pen.endPoint.z;
        //if (y < 2)
        //{
        //    y += 3;
        //    midPoint = new Vector3(x * 2 / 3, y, Mathf.Abs(dis / 2));
        //}
        //else
        //{
        //    midPoint = new Vector3(x * 2 / 3, y, Mathf.Abs(dis * 2 / 3));
        //}

        //if (midPoint.x > 4)
        //{
        //    midPoint.x = 4;
        //}

        //if (midPoint.x < -4)
        //{
        //    midPoint.x = -4;
        //}
        //if(pen.midPoint.y < startPoint.y)
        //{
        //    pen.midPoint.y = startPoint.y + 0.1f;
        //}
        path.bezierPath.MovePoint(3, pen.midPoint);
        //path.bezierPath.NotifyPathModified();
        path.editorData.BezierPathEdited();
    }
}
