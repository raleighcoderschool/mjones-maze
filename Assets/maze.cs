using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class maze : MonoBehaviour {

    public Tilemap tilemap;
    public Tile oneexit;
    public Tile twoexitside;
    public Tile twoexitacross;
    public Tile threeexit;
    public Tile fourexit;

    // bit packed array
    // 1 - right opening
    // 2 - top
    // 4 - left
    // 8 - bottom
    static private int xSize = 3; // max 200x200 or 1500x30
    static private int ySize = 3;
    private int[,] map = new int[xSize, ySize];
    private int currentx = 0;
    private int currenty = 0;

    List<string> SwapElements(List<string> list, int k, int l)
    {
        string first = list[k];
        list[k] = list[l];
        list[l] = first;
        return list;
    }

    private void ChooseRandomDirection(int x, int y)
    {
        List<string> possibleDirections = new List<string>();
        possibleDirections.Insert(0, "top");
        possibleDirections.Insert(0, "bottom");
        possibleDirections.Insert(0, "right");
        possibleDirections.Insert(0, "left");

        possibleDirections = SwapElements(possibleDirections, Random.Range(0, possibleDirections.Count), Random.Range(0, possibleDirections.Count));
        possibleDirections = SwapElements(possibleDirections, Random.Range(0, possibleDirections.Count), Random.Range(0, possibleDirections.Count));
        possibleDirections = SwapElements(possibleDirections, Random.Range(0, possibleDirections.Count), Random.Range(0, possibleDirections.Count));
        possibleDirections = SwapElements(possibleDirections, Random.Range(0, possibleDirections.Count), Random.Range(0, possibleDirections.Count));

        while(possibleDirections.Count > 0)
        {
            if (possibleDirections[0] == "top")
            {
                if(y!=0 && map[x,y-1]==0)
                {
                    map[x, y] = map[x, y] + 2;
                    map[x, y-1] = map[x, y-1] + 8;
                    ChooseRandomDirection(x, y - 1);
                }
                else
                {
                    possibleDirections.RemoveAt(0);
                }
            }
            else if (possibleDirections[0] == "bottom")
            {
                if (y != ySize-1 && map[x, y + 1] == 0)
                {
                    map[x, y] = map[x, y] + 8;
                    map[x, y + 1] = map[x, y + 1] + 2;
                    ChooseRandomDirection(x, y + 1);
                }
                else
                {
                    possibleDirections.RemoveAt(0);
                }
            }
            else if (possibleDirections[0] == "right")
            {
                if (x != xSize - 1 && map[x+1, y] == 0)
                {
                    map[x, y] = map[x, y] + 1;
                    map[x+1, y] = map[x+1, y] + 4;
                    ChooseRandomDirection(x+1, y);
                }
                else
                {
                    possibleDirections.RemoveAt(0);
                }
            }
            else if (possibleDirections[0] == "left")
            {
                if (x != 0 && map[x - 1, y] == 0)
                {
                    map[x, y] = map[x, y] + 4;
                    map[x - 1, y] = map[x - 1, y] + 1;
                    ChooseRandomDirection(x - 1, y);
                }
                else
                {
                    possibleDirections.RemoveAt(0);
                }
            }
        }
    }

    private List<Vector2Int> SolveMaze(int startx, int starty, int endx, int endy)
    {
        List<Vector2Int> solution = new List<Vector2Int>();
        int[,] visited = new int[xSize, ySize];



        return solution;
    }

    // Use this for initialization
    void Start () {
        ChooseRandomDirection(0, 0);
        for (int i = 0; i < xSize; i++)
            for (int k = 0; k < ySize; k++)
            {
                int tileNum = map[i, k];
                //int tileNum = map[k, i];
                // single opening
                if (tileNum==1 || tileNum == 2 || tileNum == 4 || tileNum == 8)
                {
                    tilemap.SetTile(new Vector3Int(i, k, 0), oneexit);

                    if (tileNum == 1)
                    {
                        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -90f), Vector3.one);
                        tilemap.SetTransformMatrix(new Vector3Int(i, k, 0), matrix);
                    }
                    if (tileNum == 2)
                    {
                        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 180f), Vector3.one);
                        tilemap.SetTransformMatrix(new Vector3Int(i, k, 0), matrix);
                    }
                    if (tileNum == 4)
                    {
                        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.one);
                        tilemap.SetTransformMatrix(new Vector3Int(i, k, 0), matrix);
                    }
                    if (tileNum == 8)
                    {

                    }
                }

                // double side by side
                if (tileNum == 3 || tileNum == 6 || tileNum == 12 || tileNum == 9)
                {
                    tilemap.SetTile(new Vector3Int(i, k, 0), twoexitside);
                    if (tileNum == 3)
                    {
                        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 270f), Vector3.one);
                        tilemap.SetTransformMatrix(new Vector3Int(i, k, 0), matrix);
                    }
                    if (tileNum == 6)
                    {
                        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 180f), Vector3.one);
                        tilemap.SetTransformMatrix(new Vector3Int(i, k, 0), matrix);
                    }
                    if (tileNum == 12)
                    {
                        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.one);
                        tilemap.SetTransformMatrix(new Vector3Int(i, k, 0), matrix);
                    }
                    if (tileNum == 9)
                    {
                    }
                }

                // double acress
                if (tileNum == 5 || tileNum == 10)
                {
                    tilemap.SetTile(new Vector3Int(i, k, 0), twoexitacross);
                    
                    if (tileNum == 5)
                    {
                        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.one);
                        tilemap.SetTransformMatrix(new Vector3Int(i, k, 0), matrix);
                    }
                    if (tileNum == 10)
                    { 

                    }
                }
                
                // three exits
                if (tileNum == 7 || tileNum == 14 || tileNum == 13 || tileNum == 11)
                {
                    tilemap.SetTile(new Vector3Int(i, k, 0), threeexit);

                    if (tileNum == 7)
                    {
                        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 270f), Vector3.one);
                        tilemap.SetTransformMatrix(new Vector3Int(i, k, 0), matrix);
                    }
                    if (tileNum == 14)
                    {
                        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 180f), Vector3.one);
                        tilemap.SetTransformMatrix(new Vector3Int(i, k, 0), matrix);
                    }
                    if (tileNum == 13)
                    {
                        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.one);
                        tilemap.SetTransformMatrix(new Vector3Int(i, k, 0), matrix);
                    }
                    if (tileNum == 11)
                    {

                    }
                }

                // four exits
                if (tileNum == 15)
                {
                    tilemap.SetTile(new Vector3Int(i, k, 0), fourexit);
                    
                    Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 0f), Vector3.one);
                    tilemap.SetTransformMatrix(new Vector3Int(i, k, 0), matrix);
                }

            }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
