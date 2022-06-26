using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.XR.ARFoundation.Samples;

public class manage : MonoBehaviour
{
    // Start is called before the first frame update
    bool start = false, stop = false;
    public void startandstopRecord() 
    {
        if (!start)
        {
            start = true;
            stop = false;
        }
        else
        {
            start = false;
            stop = true;
        }
        GameObject model = GameObject.Find("Human Body Tracking");
        model.GetComponent<HumanBodyTracker>().newSkeletonGO.GetComponent<AnimationRecorder>().startRecord = start;
        model.GetComponent<HumanBodyTracker>().newSkeletonGO.GetComponent<AnimationRecorder>().stopRecord = stop;
        


    }
    public AnimationClip anim;

    public GameObject animationModel;
    public void animationSetter() 
    {
        //GameObject model = GameObject.Find();  //(search for gameobject to set animation on)
        animationModel.GetComponent<animationApplier>().anim = anim;
        animationModel.GetComponent<animationApplier>().runAnimation();
    }
    

}
