using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    int dir;
    float speed;

    // Start is called before the first frame update
    void Start()
    {  
         transform.position = new Vector3(transform.position.x, Random.Range(-2.4f, -2.8f), transform.position.z);
         dir = Random.Range(0, 2);
         speed = Random.Range(0.09f, 0.11f);
    }

    // Update is called once per frame
    void Update()
    {
        float y = transform.position.y;
        if(dir == 0) {
            transform.position += Vector3.up * Time.deltaTime * speed;
            if (y > -2.4) {
                dir = 1;
            } 
        } else if(dir == 1) {
            transform.position -= Vector3.up * Time.deltaTime * speed;
            if (y <-2.8) {
                dir = 0;
            }            
        }
    }
}
