using System.Collections.Generic;
using UnityEngine;

public class BoardMatrix
{
    private float blockSize = 1f;

    private List<Vector3> SpawnPointPosition;

    public BoardMatrix(int cols, int rows, Vector3 startingPos)
    {
        if (cols % 2 == 0 && rows % 2 == 0)
        {
            SpawnPointPosition = new List<Vector3>();

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    var vec = new Vector3(
                        (startingPos.x + blockSize * (j <= (rows / 2 - 1) ? -(rows / 2 - j) :
                            (j - rows / 2))) + (blockSize / 2),
                        0f,
                        (startingPos.z + blockSize * (i <= (cols / 2 - 1) ? (cols / 2 - i) :
                            -(i - cols / 2))) - (blockSize / 2));

                    SpawnPointPosition.Add(vec);
                }
            }
        }
        else
        {
            SpawnPointPosition = new List<Vector3>();

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    var vec = new Vector3(
                        (startingPos.x +
                            blockSize * (j <= (rows / 2 - 1) ? -(rows / 2 - j) : (j - rows / 2))),
                        0f,
                        (startingPos.z +
                            blockSize * (i <= (cols / 2 - 1) ? (cols / 2 - i) : -(i - cols / 2))));

                    SpawnPointPosition.Add(vec);
                }
            }
        }
    }

    public Vector3 GetSpawnPointPosition(int count)
    {
        return SpawnPointPosition[count];
    }
}
