using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{


    private LineRenderer lineRenderer;
    private float counter ;
    private float dist;

    public Transform origin;
    public Transform destination;

    public float linedrawspeed = 6f;
    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = .01f;
        lineRenderer.endWidth = .01f;

        lineRenderer.SetPosition(0, origin.position);

        dist = Vector3.Distance(origin.position, destination.position);
        Debug.Log(dist.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < dist)
        {

            counter = counter + .1f / linedrawspeed;
            //Debug.Log(counter.ToString());

            float x = Mathf.Lerp(0, dist, counter);
            Debug.Log(x.ToString());
            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;

            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;


            lineRenderer.SetPosition(1, pointAlongLine);
        }
        Debug.Log("counter"+counter.ToString());
    }
}
