using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{

    public GameObject platform_prefab;
    float destroyPercent = 0.3f;
    public int length = 59;
    public int width = 3;

    // Start is called before the first frame update
    void Start()
    {
        GenerateTiles();
        
    }

    void GenerateTiles() {
        int destroyLine1 = Random.Range(1, length);
        int destroyLine2 = Random.Range(1, length);
        while(destroyLine2 == destroyLine1-1 || destroyLine2 == destroyLine1+1) {
            destroyLine2 = Random.Range(1, length);
        }
        for (int l = 0; l < length; l++) {
            for (int w = 0; w < width; w++) {
                float x = l * 2.5f - 74f;
                float z = w * 3.1f - 3f;
                GameObject platform = Instantiate(platform_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                platform.name = "platform" + l + "_" + w;
                platform.transform.position = new Vector3(x, 0f, z);
                if(l != 0 && l!=length-1 && (Random.Range(0f, 1f) < destroyPercent || l == destroyLine1 || l == destroyLine2)) {
                    Destroy(platform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
