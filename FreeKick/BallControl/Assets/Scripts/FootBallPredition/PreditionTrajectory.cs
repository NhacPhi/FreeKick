using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class PreditionTrajectory : MonoBehaviour
{
    #region Fields
    public PathCreator pathCreator;

    public EndOfPathInstruction endOfPathInstruction;

    public GameObject prefab;

    [SerializeField]
    private int numberPrefabs = 0;

    #endregion

    #region Local param
    private GameObject[] arrayPrefabs;

    float deltaDistance;
    float distanceTravelled = 0;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        arrayPrefabs = new GameObject[numberPrefabs];
        for (int i = 0; i < arrayPrefabs.Length; i++)
        {
            GameObject ob = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            arrayPrefabs[i] = ob;
        }
        deltaDistance = (float)1 / arrayPrefabs.Length;
        distanceTravelled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if (pathCreator != null)
            {
                for (int i = 0; i < arrayPrefabs.Length; i++)
                {
                    arrayPrefabs[i].gameObject.transform.position = pathCreator.path.GetPointAtTime(distanceTravelled, endOfPathInstruction);
                    distanceTravelled += deltaDistance;
                    //Debug.Log("distanceTravelled: " + distanceTravelled);
                }
            }
        }
       if(Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < arrayPrefabs.Length; i++)
            {
                arrayPrefabs[i].gameObject.SetActive(false);
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < arrayPrefabs.Length; i++)
            {
                arrayPrefabs[i].gameObject.SetActive(true);
            }
        }
        distanceTravelled = 0;
    }

}
