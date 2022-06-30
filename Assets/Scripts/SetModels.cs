using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;

public class SetModels : MonoBehaviour
{
    // Start is called before the first frame update
 
    public void setModel()
    {
        
        GameObject model= this.GetComponent<DataHandler>().Getmodels();
        GameObject ht = GameObject.Find("Human Body Tracking");
        HumanBodyTracker humBt = ht.GetComponent<HumanBodyTracker>();
        humBt.changed = true;
        humBt.skeletonPrefab = model.gameObject;
        
      
    }
}
 