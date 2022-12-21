using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

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

    //public int[,] validLocs = new int[10,10];
    public Dictionary<int, int> validLoc = new Dictionary<int, int>();

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
                    if (!(validLoc.ContainsKey(i) && validLoc[i].Equals(j)))
                    {
                        validLoc.Add(i, j);
                    }

                    //validLoc.Append(i, j);

                    //int num = Random.Range(1, 6);
                    //if (num >= 3)
                    //{
                    //    var skeleton = Instantiate(skeletonPrefab, transform) as Transform;
                    //    skeleton.position = position + new Vector3(0, 0, size / 10);
                    //    skeleton.localScale = new Vector3(size, skeleton.localScale.y, skeleton.localScale.z);
                    //}

                    //var blueGemPrefab = Instantiate(blueGemPrefab, transform) as Transform;
                    //blueGemPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
                    //blueGemPrefab.localScale = new Vector3(size, blueGemPrefab.localScale.y, blueGemPrefab.localScale.z);


                    //goldBarPrefab = Instantiate(wallPrefab, transform) as Transform;
                    //goldBarPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
                    //goldBarPrefab.localScale = new Vector3(size, goldBarPrefab.localScale.y, goldBarPrefab.localScale.z);

                    //index = random.Next(validLoc.Count);
                    //KeyValuePair<int, int> pair2 = validLoc.ElementAt(index);
                    //validPosition = new Vector3(-width / 2 + pair2.Key, 0, -height / 2 + pair2.Value);
                    //silverCoinPrefab = Instantiate(wallPrefab, transform) as Transform;
                    //silverCoinPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
                    //silverCoinPrefab.localScale = new Vector3(size, silverCoinPrefab.localScale.y, silverCoinPrefab.localScale.z);

                    //index = random.Next(validLoc.Count);
                    //KeyValuePair<int, int> pair3 = validLoc.ElementAt(index);
                    //validPosition = new Vector3(-width / 2 + pair3.Key, 0, -height / 2 + pair3.Value);
                    //greenHealthPlusPrefab = Instantiate(wallPrefab, transform) as Transform;
                    //greenHealthPlusPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
                    //greenHealthPlusPrefab.localScale = new Vector3(size, greenHealthPlusPrefab.localScale.y, greenHealthPlusPrefab.localScale.z);

                    //index = random.Next(validLoc.Count);
                    //KeyValuePair<int, int> pair4 = validLoc.ElementAt(index);
                    //validPosition = new Vector3(-width / 2 + pair4.Key, 0, -height / 2 + pair4.Value);
                    //goldKeyPrefab = Instantiate(wallPrefab, transform) as Transform;
                    //goldKeyPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
                    //goldKeyPrefab.localScale = new Vector3(size, goldKeyPrefab.localScale.y, goldKeyPrefab.localScale.z);


                    //index = random.Next(validLoc.Count);
                    //KeyValuePair<int, int> pair5 = validLoc.ElementAt(index);
                    //validPosition = new Vector3(-width / 2 + pair5.Key, 0, -height / 2 + pair5.Value);
                    //swordPrefab = Instantiate(wallPrefab, transform) as Transform;
                    //swordPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
                    //swordPrefab.localScale = new Vector3(size, swordPrefab.localScale.y, swordPrefab.localScale.z);

                    //index = random.Next(validLoc.Count);
                    //KeyValuePair<int, int> pair6 = validLoc.ElementAt(index);
                    //validPosition = new Vector3(-width / 2 + pair6.Key, 0, -height / 2 + pair6.Value);
                    //shieldPrefab = Instantiate(wallPrefab, transform) as Transform;
                    //shieldPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
                    //shieldPrefab.localScale = new Vector3(size, shieldPrefab.localScale.y, shieldPrefab.localScale.z);

                }

            }
        }
        
        System.Random random = new System.Random();
        int index = random.Next(validLoc.Count);
        KeyValuePair<int, int> pair = validLoc.ElementAt(index);
        var validPosition = new Vector3(-width / 2 + pair.Key, 0, -height / 2 + pair.Value);
        var BlueGemPrefab = Instantiate(blueGemPrefab, transform) as Transform;
        BlueGemPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        BlueGemPrefab.localScale = new Vector3(size, BlueGemPrefab.localScale.y, BlueGemPrefab.localScale.z);

        index = random.Next(validLoc.Count);
        KeyValuePair<int, int> pair1 = validLoc.ElementAt(index);
        validPosition = new Vector3(-width / 2 + pair1.Key, 0, -height / 2 + pair1.Value);
        var GoldBarPrefab = Instantiate(goldBarPrefab, transform) as Transform;
        GoldBarPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        GoldBarPrefab.localScale = new Vector3(size, GoldBarPrefab.localScale.y, GoldBarPrefab.localScale.z);

        index = random.Next(validLoc.Count);
        KeyValuePair<int, int> pair2 = validLoc.ElementAt(index);
        validPosition = new Vector3(-width / 2 + pair2.Key, 0, -height / 2 + pair2.Value);
        var SilverCoinPrefab = Instantiate(silverCoinPrefab, transform) as Transform;
        SilverCoinPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        SilverCoinPrefab.localScale = new Vector3(size, SilverCoinPrefab.localScale.y, SilverCoinPrefab.localScale.z);

        index = random.Next(validLoc.Count);
        KeyValuePair<int, int> pair3 = validLoc.ElementAt(index);
        validPosition = new Vector3(-width / 2 + pair3.Key, 0, -height / 2 + pair3.Value);
        var GreenHealthPlusPrefab = Instantiate(greenHealthPlusPrefab, transform) as Transform;
        GreenHealthPlusPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        GreenHealthPlusPrefab.localScale = new Vector3(size, GreenHealthPlusPrefab.localScale.y, GreenHealthPlusPrefab.localScale.z);

        index = random.Next(validLoc.Count);
        KeyValuePair<int, int> pair4 = validLoc.ElementAt(index);
        validPosition = new Vector3(-width / 2 + pair4.Key, 0, -height / 2 + pair4.Value);
        var GoldKeyPrefab = Instantiate(goldKeyPrefab, transform) as Transform;
        GoldKeyPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        GoldKeyPrefab.localScale = new Vector3(size, GoldKeyPrefab.localScale.y, GoldKeyPrefab.localScale.z);


        index = random.Next(validLoc.Count);
        KeyValuePair<int, int> pair5 = validLoc.ElementAt(index);
        validPosition = new Vector3(-width / 2 + pair5.Key, 0, -height / 2 + pair5.Value);
        var SwordPrefab = Instantiate(swordPrefab, transform) as Transform;
        SwordPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        SwordPrefab.localScale = new Vector3(size, SwordPrefab.localScale.y, SwordPrefab.localScale.z);

        index = random.Next(validLoc.Count);
        KeyValuePair<int, int> pair6 = validLoc.ElementAt(index);
        validPosition = new Vector3(-width / 2 + pair6.Key, 0, -height / 2 + pair6.Value);
        var ShieldPrefab = Instantiate(shieldPrefab, transform) as Transform;
        ShieldPrefab.position = validPosition + new Vector3(0, 0, -size / 2);
        ShieldPrefab.localScale = new Vector3(size, ShieldPrefab.localScale.y, ShieldPrefab.localScale.z);

    }

    // Update is called once per frame
    void Update()
    {

    }
}