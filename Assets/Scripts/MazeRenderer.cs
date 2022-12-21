using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


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

    [SerializeField]
    private Transform diamondPrefab = null;

    [SerializeField]
    private Transform skeletonPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
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
                    int num = Random.Range(1, 6);
                    if (num >= 3)
                    {
                        var skeleton = Instantiate(skeletonPrefab, transform) as Transform;
                        skeleton.position = position + new Vector3(0, 0, size / 10);
                        skeleton.localScale = new Vector3(size, skeleton.localScale.y, skeleton.localScale.z);
                    }
                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}