using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    int dir;
    float speed;
    bool sinking;
    float sinkStart;
    public bool shouldSink = true;

    // Start is called before the first frame update
    void Start()
    {  
        transform.position = new Vector3(transform.position.x, Random.Range(-2.4f, -2.8f), transform.position.z);
        dir = Random.Range(0, 2);
        speed = Random.Range(0.09f, 0.11f);
        sinking = false;
        sinkStart = 0f;
        if (!name.StartsWith("platform0")) {
            shouldSink = true;
        }
    }

    // Update is called once per frame
    void Update()
    {           
        float y = transform.position.y;
        if(!sinking) {
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
        } else {
            if(shouldSink && Time.time - sinkStart  > 1.5f) {
                Debug.Log(shouldSink);
                transform.position -= Vector3.up * Time.deltaTime * speed * 3;
                if (y < -4) {
                    Destroy(gameObject);
                }
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // if (!level.virus_landed_on_player_recently)
            //     level.timestamp_virus_landed = Time.time;
            if(!sinking) {
                sinking = true;
                sinkStart = Time.time;
            }
        }
    }
}
