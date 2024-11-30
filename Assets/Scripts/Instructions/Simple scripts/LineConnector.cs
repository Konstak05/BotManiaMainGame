using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnector : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    public Transform objectpos1,objectpos2;
    [SerializeField] private float linestart,linerefresh;

    void Start()
    {
        if(lineRenderer != null){lineRenderer.positionCount = 2;}
        Invoke("UpdateLinePosition",linestart);
    }

    void UpdateLinePosition()
    {
        if(lineRenderer != null){
            lineRenderer.SetPosition(0, objectpos1.position); 
            lineRenderer.SetPosition(1, objectpos2.position);
        }
        Invoke("UpdateLinePosition",linerefresh);
    }
}