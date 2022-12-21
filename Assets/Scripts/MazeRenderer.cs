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


    //private (int x, int y)[] validLocs = new (int x, int y)[10];
    public List<int> validLocs = new List<int>();
    public List<int> validLocsActual = new List<int>();
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
                    validLocs.Add(i);
                    validLocs.Add(j);
                    //Debug.Log(i);
                    //Debug.Log(j);
                }
            }
        }

        validLocs.Reverse();
        //for (int a = 0; a < validLocs.Count; a++)
        //{
        //    Debug.Log(validLocs[a]);
        //}
        int k = 0;

        var validPosition = new Vector3(-width / 2 + validLocs[k], 0, -height / 2 + validLocs[k+1]);
        var BlueGemPrefab = Instantiate(blueGemPrefab, transform) as Transform;
        BlueGemPrefab.position = validPosition + new Vector3(0, 0.5f, -size / 2);
        BlueGemPrefab.localScale = new Vector3(size/2, BlueGemPrefab.localScale.y/2, BlueGemPrefab.localScale.z/2);


        validPosition = new Vector3(-width / 2 + validLocs[k+2], 0, -height / 2 + validLocs[k+3]);
        var GoldBarPrefab = Instantiate(goldBarPrefab, transform) as Transform;
        GoldBarPrefab.position = validPosition + new Vector3(0, 0.5f, -size / 2);
        GoldBarPrefab.localScale = new Vector3(size/2, GoldBarPrefab.localScale.y/2, GoldBarPrefab.localScale.z/2);


        validPosition = new Vector3(-width / 2 + validLocs[k+4], 0, -height / 2 + validLocs[k+5]);
        var SilverCoinPrefab = Instantiate(silverCoinPrefab, transform) as Transform;
        SilverCoinPrefab.position = validPosition + new Vector3(0, 0.5f, -size / 2);
        SilverCoinPrefab.localScale = new Vector3(size/2, SilverCoinPrefab.localScale.y/2, SilverCoinPrefab.localScale.z/2);


        validPosition = new Vector3(-width / 2 + validLocs[k+6], 0, -height / 2 + validLocs[k+7]);
        var GreenHealthPlusPrefab = Instantiate(greenHealthPlusPrefab, transform) as Transform;
        GreenHealthPlusPrefab.position = validPosition + new Vector3(0, 0.5f, -size / 2);
        GreenHealthPlusPrefab.localScale = new Vector3(size/2, GreenHealthPlusPrefab.localScale.y/2, GreenHealthPlusPrefab.localScale.z/2);


        validPosition = new Vector3(-width / 2 + validLocs[k+8], 0, -height / 2 + validLocs[k+9]);
        var GoldKeyPrefab = Instantiate(goldKeyPrefab, transform) as Transform;
        GoldKeyPrefab.position = validPosition + new Vector3(0, 0.5f, -size / 2);
        GoldKeyPrefab.localScale = new Vector3(size/2, GoldKeyPrefab.localScale.y/2, GoldKeyPrefab.localScale.z/2);


        validPosition = new Vector3(-width / 2 + validLocs[k+10], 0, -height / 2 + validLocs[k+11]);
        var SwordPrefab = Instantiate(swordPrefab, transform) as Transform;
        SwordPrefab.position = validPosition + new Vector3(0, 0.5f, -size / 2);
        SwordPrefab.localScale = new Vector3(size/2, SwordPrefab.localScale.y/2, SwordPrefab.localScale.z/2);
    

        validPosition = new Vector3(-width / 2 + validLocs[k+12], 0, -height / 2 + validLocs[k+13]);
        var ShieldPrefab = Instantiate(shieldPrefab, transform) as Transform;
        ShieldPrefab.position = validPosition + new Vector3(0, 0.5f, -size / 2);
        ShieldPrefab.localScale = new Vector3(size/2, ShieldPrefab.localScale.y/2, ShieldPrefab.localScale.z/2);

    }

    // Update is called once per frame
    void Update()
    {

    }
}