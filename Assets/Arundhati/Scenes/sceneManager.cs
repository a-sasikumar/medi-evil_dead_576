using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public void Loads()
    {
        Debug.Log("in loads()");
        // Debug.Log(SceneManager.GetActiveScene().name);
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}