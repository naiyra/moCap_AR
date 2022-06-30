using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class DataHandler : MonoBehaviour
{
    private GameObject models;
    [SerializeField] private ButtonManager buttonprefab;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private List<Item> _items;
    private int current_id = 0;
    private static DataHandler instance;

    public static DataHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataHandler>();
            }

            return instance;
        }
    }

    private void Start()
    {
      
        LoadItems();
        CreateButton();
    }

    void LoadItems()
    {
        var items_obj = Resources.LoadAll("Items", typeof(Item));
        foreach (var item in items_obj)
        {
            _items.Add(item as Item);
        }
    }

    void CreateButton()
    {
        foreach (Item i in _items)
        {
            ButtonManager b = Instantiate(buttonprefab, buttonContainer.transform);
            b.ItemId = current_id;
            b.ButtonTexture = i.itemImage;
            current_id++;
        }
    }

    public void Setmodels(int id)
    {
        models = _items[id].itemPrefab;
        this.gameObject.GetComponent<SetModels>().setModel();
    }

    public GameObject Getmodels()
    {
        return models;
    }

 
}
