using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MazeRenderer : MonoBehaviour
{

    [SerializeField]
    [Range(1, 50)]
    private int width = 20;

    [SerializeField]
    [Range(1, 50)]
    private int height = 20;

    [SerializeField]
    private float size = 5f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    //[SerializeField]
    //private Transform skeletonPrefab = null;

    [SerializeField]
    private Transform blueGemPrefab = null;

    [SerializeField]
    private Transform goldBarPrefab = null;

    [SerializeField]
    private Transform silverCoinPrefab = null;

    [SerializeField]
    private Transform greenHealthPlusPrefab = null;

    [SerializeField]
    private Transform goldKeyPrefab = null;

    [SerializeField]
    private Transform swordPrefab = null;

    [SerializeField]
    private Transform shieldPrefab = null;


    private (int x, int y)[] validLocs = new (int x, int y)[10];

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        
        var maze = MazeGenerator.Generate(width, height);
        int num_gems = 10;
        int num_enemies = 5;
        Draw(maze, num_enemies, num_gems);
    }

    private void Draw(WallState[,] maze, int num_enemies, int num_gems)
    {

        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(width, 1, height);

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                var cell = maze[i, j];
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                if (cell.HasFlag(WallState.UP) || cell.HasFlag(WallState.LEFT)
                    || ((i == width - 1) && (cell.HasFlag(WallState.RIGHT)))
                    || ((j == 0) && ((cell.HasFlag(WallState.DOWN)))))
                {

                    if (cell.HasFlag(WallState.UP))
                    {
                        var topWall = Instantiate(wallPrefab, transform) as Transform;
                        topWall.position = position + new Vector3(0, 0, size / 2);
                        topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                    }

                    if (cell.HasFlag(WallState.LEFT))
                    {
                        var leftWall = Instantiate(wallPrefab, transform) as Transform;
                        leftWall.position = position + new Vector3(-size / 2, 0, 0);
                        leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                        leftWall.eulerAngles = new Vector3(0, 90, 0);
                    }

                    if (i == width - 1)
                    {
                        if (cell.HasFlag(WallState.RIGHT))
                        {
                            var rightWall = Instantiate(wallPrefab, transform) as Transform;
                            rightWall.position = position + new Vector3(+size / 2, 0, 0);
                            rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                            rightWall.eulerAngles = new Vector3(0, 90, 0);
                        }
                    }

                    if (j == 0)
                    {
                        if (cell.HasFlag(WallState.DOWN))
                        {
                            var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                            bottomWall.position = position + new Vector3(0, 0, -size / 2);
                            bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                        }
                    }
                }

                else
                {

                    validLocs.Append((i, j));
                }

            }
        }
        
        //Debug.Log(validLocs.Length);
        int index = Random.Range(0, validLocs.Length);
        (int x, int y) pos = validLocs[index];
        //Debug.Log(pos);

        var validPosition = new Vector3(-width / 2 + validLocs[index].x, 0, -height / 2 + validLocs[index].y);
        var BlueGemPrefab = Instantiate(blueGemPrefab, transform) as Transform;
        BlueGemPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        BlueGemPrefab.localScale = new Vector3(size, BlueGemPrefab.localScale.y, BlueGemPrefab.localScale.z);

        int index1 = Random.Range(0, validLocs.Length);
        validPosition = new Vector3(-width / 2 + validLocs[index1].x, 0, -height / 2 + validLocs[index1].y);
        var GoldBarPrefab = Instantiate(goldBarPrefab, transform) as Transform;
        GoldBarPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        GoldBarPrefab.localScale = new Vector3(size, GoldBarPrefab.localScale.y, GoldBarPrefab.localScale.z);

        int index2 = Random.Range(0, validLocs.Length);
        validPosition = new Vector3(-width / 2 + validLocs[index2].x, 0, -height / 2 + validLocs[index2].y);
        var SilverCoinPrefab = Instantiate(silverCoinPrefab, transform) as Transform;
        SilverCoinPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        SilverCoinPrefab.localScale = new Vector3(size, SilverCoinPrefab.localScale.y, SilverCoinPrefab.localScale.z);

        int index3 = Random.Range(0, validLocs.Length);
        validPosition = new Vector3(-width / 2 + validLocs[index3].x, 0, -height / 2 + validLocs[index3].y);
        var GreenHealthPlusPrefab = Instantiate(greenHealthPlusPrefab, transform) as Transform;
        GreenHealthPlusPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        GreenHealthPlusPrefab.localScale = new Vector3(size, GreenHealthPlusPrefab.localScale.y, GreenHealthPlusPrefab.localScale.z);

        int index4 = Random.Range(0, validLocs.Length);
        validPosition = new Vector3(-width / 2 + validLocs[index4].x, 0, -height / 2 + validLocs[index4].y);
        var GoldKeyPrefab = Instantiate(goldKeyPrefab, transform) as Transform;
        GoldKeyPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        GoldKeyPrefab.localScale = new Vector3(size, GoldKeyPrefab.localScale.y, GoldKeyPrefab.localScale.z);

        int index5 = Random.Range(0, validLocs.Length);
        validPosition = new Vector3(-width / 2 + validLocs[index5].x, 0, -height / 2 + validLocs[index5].y);
        var SwordPrefab = Instantiate(swordPrefab, transform) as Transform;
        SwordPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        SwordPrefab.localScale = new Vector3(size, SwordPrefab.localScale.y, SwordPrefab.localScale.z);

        int index6 = Random.Range(0, validLocs.Length);
        validPosition = new Vector3(-width / 2 + validLocs[index6].x, 0, -height / 2 + validLocs[index6].y);
        var ShieldPrefab = Instantiate(shieldPrefab, transform) as Transform;
        ShieldPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        ShieldPrefab.localScale = new Vector3(size, ShieldPrefab.localScale.y, ShieldPrefab.localScale.z);

    }

    // Update is called once per frame
    void Update()
    {

    }
}