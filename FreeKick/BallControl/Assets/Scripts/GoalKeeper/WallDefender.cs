using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDefender : MonoBehaviour
{
    [SerializeField]
    private int numberOfDefender = 3;
    [SerializeField]
    private GameObject DefenderPrefab;

    private List<GameObject> Defenders;

    // Start is called before the first frame update
    void Start()
    {
        Defenders = new List<GameObject>();
        for (int i = 0; i < numberOfDefender; i++)
        {
            GameObject childObj = Instantiate(DefenderPrefab);
            childObj.transform.parent = gameObject.transform;
            childObj.transform.position = gameObject.transform.position + new Vector3(0, 0, i * 1.2f);
            Defenders.Add(childObj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Defend();
        }
    }

    public void Defend()
    {
        for (int i = 0; i < Defenders.Count; i++)
        {
            Defenders[i].GetComponent<Defender>().Defend();
        }
    }
}
