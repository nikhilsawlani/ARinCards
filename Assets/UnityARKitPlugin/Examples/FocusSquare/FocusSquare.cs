﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class FocusSquare : MonoBehaviour
{

    public enum FocusState
    {
        Initializing,
        Finding,
        Found
    }

    public GameObject findingSquare;
    public GameObject foundSquare;
    public GameObject cube;
    public GameObject fddata;
    public GameObject button;
    public GameObject pointCloud;

    private bool isClicked;
    private bool canSetActive;
    //for editor version
    public float maxRayDistance = 30.0f;
    public LayerMask collisionLayerMask;
    public float findingSquareDist = 0.5f;

    private FocusState squareState;
    public FocusState SquareState
    {
        get
        {
            return squareState;
        }
        set
        {
            squareState = value;

            if (canSetActive == true)
            {

                foundSquare.SetActive(squareState == FocusState.Found);
                findingSquare.SetActive(squareState != FocusState.Found);

            }
        }
    }

    bool trackingInitialized;

    // Use this for initialization
    void Start()
    {
        SquareState = FocusState.Initializing;
        trackingInitialized = true;

        isClicked = false;
        canSetActive = true;
    }


    bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultTypes)
    {
        List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultTypes);
        if (hitResults.Count > 0)
        {
            foreach (var hitResult in hitResults)
            {
                foundSquare.transform.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);

                if (canSetActive == true)
                {

                    button.SetActive(true);

                }

                if (isClicked == true)
                {

                    cube.transform.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);

                    cube.transform.LookAt(Camera.main.transform.position);

                    cube.transform.eulerAngles = new Vector3(0, cube.transform.eulerAngles.y, 0);


                    fddata.transform.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);

                    fddata.transform.LookAt(Camera.main.transform.position);

                    fddata.transform.eulerAngles = new Vector3(0, fddata.transform.eulerAngles.y, 0);

                    //                  cube.transform.LookAt (Camera.current.transform);
                    //
                    //                  print ("ROTATION INFORMATION before!!!!!");
                    //                  print ("x rotation = " + cube.transform.rotation.x);
                    //                  print ("y rotation = " + cube.transform.rotation.y);
                    //                  print ("z rotation = " + cube.transform.rotation.z);
                    //
                    //                  //cube.transform.Rotate (cube.transform.rotation.x, 0, 0);
                    //
                    //                  cube.transform.rotation = Quaternion.Euler (0, cube.transform.rotation.y, cube.transform.rotation.z);
                    //
                    //                  print ("ROTATION INFORMATION after!!!!!");
                    //                  print ("x rotation = " + cube.transform.rotation.x);
                    //                  print ("y rotation = " + cube.transform.rotation.y);
                    //                  print ("z rotation = " + cube.transform.rotation.z);
                    //
                    isClicked = false;

                }
                foundSquare.transform.rotation = UnityARMatrixOps.GetRotation(hitResult.worldTransform);
                Debug.Log(string.Format("x:{0:0.######} y:{1:0.######} z:{2:0.######}", foundSquare.transform.position.x, foundSquare.transform.position.y, foundSquare.transform.position.z));
                return true;
            }
        }
        return false;
    }
    public void ClickButton()
    {

        isClicked = true;

        canSetActive = false;

        button.SetActive(false);


        foundSquare.SetActive(false);

        findingSquare.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {

        //use center of screen for focusing
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, findingSquareDist);

#if UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(center);
        RaycastHit hit;

        //we'll try to hit one of the plane collider gameobjects that were generated by the plugin
        //effectively similar to calling HitTest with ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent
        if (Physics.Raycast(ray, out hit, maxRayDistance, collisionLayerMask))
        {
            //we're going to get the position from the contact point
            foundSquare.transform.position = hit.point;
            Debug.Log(string.Format("x:{0:0.######} y:{1:0.######} z:{2:0.######}", foundSquare.transform.position.x, foundSquare.transform.position.y, foundSquare.transform.position.z));

            //and the rotation from the transform of the plane collider
            SquareState = FocusState.Found;
            foundSquare.transform.rotation = hit.transform.rotation;
            return;
        }


#else
        var screenPosition = Camera.main.ScreenToViewportPoint(center);
        ARPoint point = new ARPoint {
            x = screenPosition.x,
            y = screenPosition.y
        };

        // prioritize reults types
        ARHitTestResultType[] resultTypes = {
            ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
            // if you want to use infinite planes use this:
            //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
            //ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
            //ARHitTestResultType.ARHitTestResultTypeFeaturePoint
        }; 

        foreach (ARHitTestResultType resultType in resultTypes)
        {
            if (HitTestWithResultType (point, resultType))
            {
                SquareState = FocusState.Found;
                return;
            }
        }

#endif

        //if you got here, we have not found a plane, so if camera is facing below horizon, display the focus "finding" square
        if (trackingInitialized)
        {
            SquareState = FocusState.Finding;

            //check camera forward is facing downward
            if (Vector3.Dot(Camera.main.transform.forward, Vector3.down) > 0)
            {

                //position the focus finding square a distance from camera and facing up
                findingSquare.transform.position = Camera.main.ScreenToWorldPoint(center);

                //vector from camera to focussquare
                Vector3 vecToCamera = findingSquare.transform.position - Camera.main.transform.position;

                //find vector that is orthogonal to camera vector and up vector
                Vector3 vecOrthogonal = Vector3.Cross(vecToCamera, Vector3.up);

                //find vector orthogonal to both above and up vector to find the forward vector in basis function
                Vector3 vecForward = Vector3.Cross(vecOrthogonal, Vector3.up);


                findingSquare.transform.rotation = Quaternion.LookRotation(vecForward, Vector3.up);

            }
            else
            {
                //we will not display finding square if camera is not facing below horizon
                findingSquare.SetActive(false);
            }

        }

    }


}