using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object Manipulation (originally by Noah) handles what happens when you want to move props and objects in the world
/// </summary>


//Process
//________
// 1) click on an object with a collider and this script
// 2) the object collider disables 
// 3) the gizmo appears in the center of the object (rendered on top of the model)
// 4) transforming the gizmo also transforms the model
// 5) clicking off hides the gizmo and re-enables the collider
public class ObjectManipulation : MonoBehaviour
{


    //input stat checks
    private bool mouseIsOver, objIsSelected;
    // gizmo transform
    public Transform transGizmo;



    // Start is called before the first frame update
    void Start()
    {
        mouseIsOver = false;
        objIsSelected = false;

    }//end start

    // Update is called once per frame
    private void Update()
    {
        // when mouse left click
        if (Input.GetMouseButtonDown(0) && mouseIsOver)
        {
            ObjInteraction(mouseIsOver);
        }//end mouse left click

    }//end update


    //check for mouse over and exit
    private void OnMouseOver()
    {
        mouseIsOver = true;
    }//end mouse over

    private void OnMouseExit()
    {
        mouseIsOver = false;
    }//end mouse exit


    //run object select function
    private void ObjInteraction(bool _wasEnter)
    {
        //if we moused over and clicke the object
        if (_wasEnter)
        {
            //turn off/on selected
            objIsSelected = true;
            //turn off/on collider
            transform.GetComponent<BoxCollider>().enabled = false;
            //change gizmo
            transGizmo.gameObject.SetActive(true);
            transGizmo.position = transform.GetComponent<Renderer>().bounds.center;
        }
        else // otherwise of we moused off or unclicked
        {
            //turn off/on selected
            objIsSelected = false;
            //turn off/on collider
            transform.GetComponent<BoxCollider2D>().enabled = true;
            //change gizmo
            transGizmo.gameObject.SetActive(false);
            //transGizmo.position = transform.GetComponent<Renderer>().bounds.center;
        }//end of mouse exited
        
        
        

    }//end of object interaction

}//end of object manipulation
