using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private Animator animation_controller;
    private Player fps_player_obj;

    public void Loads()
    {
        Debug.Log("restart button clicked");
        fps_player_obj = GameObject.Find("Player").GetComponent<Player>();
        animation_controller = fps_player_obj.GetComponent<Animator>();
        animation_controller.SetTrigger("restart");
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}