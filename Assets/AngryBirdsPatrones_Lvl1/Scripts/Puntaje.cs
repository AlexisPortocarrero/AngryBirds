using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntaje : MonoBehaviour
{
    Text txt;

    int cantidad = 0;

    // Start is called before the first frame update
    void Start()
    {
        txt = GameObject.Find("text").gameObject.GetComponent<Text>();

    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("piso"))
        {
            cantidad += 100;
            txt.text = "Puntaje: " + cantidad;
        }

        if (other.gameObject.CompareTag("chancho"))
        {
            cantidad += 400;
            Destroy(other.gameObject);
        }
    }
}
