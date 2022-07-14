using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class MousePickup : MonoBehaviour

{

        //    For those want to lock any axis(Y axis is locked for this example):
        //Change your OnMouseDrag with this:

        //void OnMouseDrag()
        //    {
        //        transform.position = new Vector3(GetMouseWorldPos().x + mOffset.x, transform.position.y, GetMouseWorldPos().z + mOffset.z);
        //    }

    private Vector3 mOffset;
    private float mZCoord;

    private Vector3 startPos;
    private Quaternion startRot;
    public float maxDistanceAllowed;

    private float timeSinceLastCalled;
    private float delay = 3f;

    private void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;
                
    }

    void OnMouseDown()
    {

        mZCoord = Camera.main.WorldToScreenPoint(

            gameObject.transform.position).z;


        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

    }



    private Vector3 GetMouseAsWorldPoint()
    {

        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;



        // z coordinate of game object on screen

        mousePoint.z = mZCoord;


        // Convert it to world points

        return Camera.main.ScreenToWorldPoint(mousePoint);

    }



    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;

        transform.Rotate(0, Input.mouseScrollDelta.y * 20, 0);
    }


    private void LateUpdate()
    {
        timeSinceLastCalled += Time.deltaTime;

        if (timeSinceLastCalled > delay)
        {
            //call your function
            CheckPosition();
            //reset timer
            timeSinceLastCalled = 0f;
        }
    }


    private void CheckPosition()
    {
        if(maxDistanceAllowed <= 0) { maxDistanceAllowed = 4; }

        float dist = Vector3.Distance(startPos, transform.position);
        //print(dist);
        if(dist > maxDistanceAllowed) { transform.position = startPos; transform.rotation = startRot; }
        
    }

}//end mouse pickup