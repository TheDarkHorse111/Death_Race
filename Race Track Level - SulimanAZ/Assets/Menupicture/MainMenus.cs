using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenus : MonoBehaviour
{

    public void RaceTrackN(string input) { StaticName.SceneName = input; Debug.Log("N3"); SceneManager.LoadScene("MenuSelectcar"); }
    public void RaceTrackS(string input) { StaticName.SceneName = input; Debug.Log("S1"); SceneManager.LoadScene("MenuSelectcar"); }
    public void RaceTrackA(string input) { StaticName.SceneName = input; Debug.Log("A2"); SceneManager.LoadScene("MenuSelectcar"); }
    public void FightTrackN(string input) { StaticName.SceneName = input; Debug.Log("N4"); SceneManager.LoadScene("MenuSelectcar"); }
    public void PlayGame()
    {
        //SceneManager.LoadScene(sceneindex);
    }
    public void QuitGame()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
    public void QualityGame(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex);
    }
}
