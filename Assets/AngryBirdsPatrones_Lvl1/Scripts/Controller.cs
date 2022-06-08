using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    int pollo;
    public GameObject pollo1;
    public GameObject pollo2;
    

    // Start is called before the first frame update
    void Awake()
    {
        pollo = PlayerPrefs.GetInt("pollo",0);
        print(pollo);
        if (pollo == 2)
        {
            pollo2.SetActive(true);
        }
        else
        {
            pollo1.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
