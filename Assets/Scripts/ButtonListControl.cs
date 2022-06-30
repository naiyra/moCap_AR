using UnityEngine;

public class ButtonListControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private string[] intArray;
   
    void Start()

    {
         
        foreach (string i in intArray) {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
           button.SetActive(true);
           button.transform.SetParent(buttonTemplate.transform.parent, false);
            
           
    }
    
    }

   
   
}

