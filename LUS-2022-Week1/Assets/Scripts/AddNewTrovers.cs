using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Please dont reuse this, its only to add Millie's character into the scene and set animations
/// </summary>
/// 

public class AddNewTrovers : MonoBehaviour
{

    //reference to character model prefab
    public Animator animTrover2;
    //reference to the slider
    public Slider animSlider;
    //a reference to the AfterImage script that makes the clone
    public AfterImage scrpt_AfterImage;


    public void SendAnimationChange()
    {
        animTrover2.SetInteger("poseInt", (int)animSlider.value);
    }

    public void CloneTrover()
    {
        scrpt_AfterImage.CreateAfterImage();
    }


}//end of add trovers
