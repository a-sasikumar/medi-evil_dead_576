using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public string level_to_go_to;

    public void OnCollisionEnter()
    {
        Debug.Log("collision");
    	SceneManager.LoadScene(level_to_go_to);
    }
}