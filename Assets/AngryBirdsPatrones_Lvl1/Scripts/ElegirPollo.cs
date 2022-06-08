using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElegirPollo : MonoBehaviour
{
    int pollo = 1;

    public void setPollo()
    {
        pollo = 2;
        SafeData();
    }

    public void SafeData()
    {
        PlayerPrefs.SetInt("pollo", pollo);
        SceneManager.LoadScene("level1");
    }
}
