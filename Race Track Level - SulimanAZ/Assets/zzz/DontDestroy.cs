using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public string scenename;
    private void Awake()
    {
        scenename = SceneManager.GetActiveScene().name;
    }
    private void Update()
    {
        scenename = SceneManager.GetActiveScene().name;
        if (scenename == "Main Menu" || scenename == "MenuSelectcar")
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}
