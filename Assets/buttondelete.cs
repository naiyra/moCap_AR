using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttondelete : MonoBehaviour
{
    [SerializeField]
    private GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(button.gameObject);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
