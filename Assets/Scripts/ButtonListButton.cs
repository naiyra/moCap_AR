using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton: MonoBehaviour {

    [SerializeField]
    private Text mytext;

    // Start is called before the first frame update
     public void SetText(string textstring)
    {
        mytext.text = textstring;
    }

    // Update is called once per frame
    public void OnClick()
    {
        
    }
}
