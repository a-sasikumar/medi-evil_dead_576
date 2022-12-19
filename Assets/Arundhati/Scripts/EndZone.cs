using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public Claire claire;
    // Start is called before the first frame update
    void Start()
    {
        claire = (Claire)GameObject.Find("Claire").GetComponent<Claire>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ////////////////////////////////////////////////
        // WRITE CODE HERE:
        // if Claire reaches this platform, make it green, make "has_won" true in Claire.cs / see Claire.cs for what to do here
        ////////////////////////////////////////////////
        GetComponent<Renderer>().material.color = Color.green;
        claire.has_won = true;
    }
}
