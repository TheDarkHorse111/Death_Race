using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuspaused : MonoBehaviour
{
    public static bool setpausemenu=false;
    public GameObject pausedmenu;
    //private void Start()
    //{
    //    setpausemenu = false;
    //}
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (setpausemenu)
            {
                //if (setpausemenuoption) 
                //{ }
                //else 
                //{ Restart(); }
                Restart();
            }
            else 
            {
                Paused();
            }
            
        }
    }
    public void Restart() 
    {
        pausedmenu.SetActive(false);
        Time.timeScale = 1f;
        setpausemenu = false;
    }
    public void Paused()
    {
        pausedmenu.SetActive(true);
        Time.timeScale = 0f;
        setpausemenu = true;
    }
    public void Backmainmenu() 
    {
        setpausemenu = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
