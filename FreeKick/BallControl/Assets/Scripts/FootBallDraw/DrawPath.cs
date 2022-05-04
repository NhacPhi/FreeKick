using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Camera CameraOrthographic;

    [SerializeField]
    private TrailRenderer line;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private LayerMask layerMaskMid;

    [SerializeField]
    private GameObject prefabEndPoint;

    #endregion


    #region Local param
    private Vector3 startPo;
    private Vector3 positionMax;
    private Vector3 positionMin;

    private bool isPositionMin;
    private bool isPositionMax;
    private bool isDrawing;

    private bool isDetectEndPoint;
    private bool isDetectMidPoint;

    private GameObject obEndpoint;
    #endregion

    #region Public param
    [SerializeField, HideInInspector]
    public Vector3 endPoint;

    [SerializeField, HideInInspector]
    public Vector3 midPoint;

    [SerializeField, HideInInspector]
    public Vector3 startPoint;

    public bool isAddforce;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        isAddforce = false;
        // Instaniate one object to mark position of endpoint.
        obEndpoint = Instantiate(prefabEndPoint, new Vector3(10, 10, 10), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        if (isDrawing)
        {
            Drawing();
        }
    }
    #region Function
    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDraw();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDraw();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetPath();
        }
    }

    void StartDraw()
    {
        isDrawing = true;

        line.gameObject.SetActive(true);
    }
    void StopDraw()
    {
        isDrawing = false;

        StartCoroutine(ResetDraw());

        // Detect endPoint
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            isDetectEndPoint = true;
        }
        else
        {
            Debug.Log("Not Raycast");
            isDetectEndPoint = false;
        }
        obEndpoint.transform.position = hit.point;

        endPoint = hit.point;

        //Handle get mid point after draw

        if (isPositionMax && isPositionMin)
        {
            if (endPoint.x > 0)
            {
                midPoint = positionMin;
            }
            else
            {
                midPoint = positionMax;
            }
        }
        else
        {
            if (endPoint.x > 0)
            {
                midPoint = positionMax;
            }
            else
            {
                midPoint = positionMin;
            }
        }
        if(isDetectMidPoint && isDetectMidPoint)
        {
            isAddforce = false;
        }
        else
        {
            isAddforce = true;
            Debug.Log("Add force =  true");
        }
        //GameObject midOb = Instantiate(prefabEndPoint, midPoint, Quaternion.identity);

    }
    void Drawing()
    {
        Vector3 pos = CameraOrthographic.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskMid))
        {
            isDetectMidPoint = true;
        }
        else
        {
            Debug.Log("Not Raycast midlayer");
            isDetectMidPoint = false;
        }

        if (hit.point.x > positionMax.x && isDrawing)
        {
            positionMax = hit.point;
            isPositionMax = true;
        }
        if (hit.point.x < positionMin.x && isDrawing)
        {
            positionMin = hit.point;
            isPositionMin = true;
        }
    }
    IEnumerator ResetDraw()
    {
        yield return new WaitForSeconds(0.5f);
        line.gameObject.SetActive(false);
        transform.position = startPoint;
    }
    void  ResetPath()
    {
        positionMin = Vector3.zero;
        positionMax = Vector3.zero;
        isPositionMax = false;
        isPositionMin = false;
        midPoint = Vector3.zero;
        endPoint = Vector3.zero;
        isAddforce = false;
        isDetectEndPoint = false;
        isDetectMidPoint = false;
    }
    #endregion
}
