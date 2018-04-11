using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObstacleMap : MonoBehaviour {
    public GameObject ParentObstacle;
    private Transform[] Obstacles;
    private GameObject player;
    public int[,] map;
    private int minX;
    private int minZ;
    private int maxX;
    private int maxZ;
    // Use this for initialization
    void Start () {
        Obstacles = ParentObstacle.GetComponentsInChildren<Transform>();
        minX = 1000;
        maxX = -1000;
        minZ = 1000;
        maxZ = -1000;
        foreach (Transform t in Obstacles)
        {
            if (minX > t.position.x) minX = (int)(t.position.x + 0.5);
            if (minZ > t.position.z) minZ = (int)(t.position.z + 0.5);
            if (maxX < t.position.x) maxX = (int)(t.position.x + 0.5);
            if (maxZ < t.position.z) maxZ = (int)(t.position.z + 0.5);
        }
        map = new int[maxX - minX + 1, maxZ - minZ + 1];
        foreach (Transform t in Obstacles)
        {
            map[(int)(t.position.x + 0.5 - minX), (int)(t.position.z + 0.5 - minZ)] = int.MinValue;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        map[(int)(player.transform.position.x + 0.5 - minX), (int)(player.transform.position.z + 0.5 - minZ)] = int.MaxValue;
        //calcRewards();
    }
	
	// Update is called once per frame
	void Update () {
        //map[(int)(player.transform.position.x + 0.5 - minX), (int)(player.transform.position.z + 0.5 - minZ)] = int.MaxValue;
        //updateRewards();
	}

    private void calcRewards()
    {
        Debug.Log(minX + " " + minZ);
        Queue<Vector3> q = new Queue<Vector3>();
        bool[,] visited = new bool[maxX - minX + 1, maxZ - minZ + 1];
        visited[(int)(player.transform.position.x + 0.5 - minX), (int)(player.transform.position.z + 0.5 - minZ)] = true;
        q.Enqueue(player.transform.position + new Vector3(0.5f - minX, 0, 0.5f - minZ));
        Debug.Log("=>" + q.Count);
        while(q.Count!=0)
        {
            Vector3 curr = q.Dequeue();
            for(int i=-1;i<2;i++)
            {
                for(int j=-1;j<2;j++)
                {
                    Vector3 neighb = curr + new Vector3(i, 0, j);
                    Debug.Log(neighb + "----" + map[(int)neighb.x, (int)neighb.z] + " ---"+ legal(neighb) + "---" + visited[(int)neighb.x, (int)neighb.z]);
                    if(legal(neighb))
                    {
                        if (!visited[(int)neighb.x, (int)neighb.z] &&
                            map[(int)neighb.x, (int)neighb.z] != int.MinValue)
                        {
                            Debug.Log(neighb);
                            visited[(int)neighb.x, (int)neighb.z] = true;
                            map[(int)neighb.x, (int)neighb.z] = map[(int)curr.x, (int)curr.z] - 1;
                            q.Enqueue(neighb);
                        }
                        else
                        {
                            Debug.Log("--->" + neighb);
                        }
                    }
                }
            }
        }
    }

    private void updateRewards()
    {
        Queue<Vector3> q = new Queue<Vector3>();
        bool[,] visited = new bool[maxX - minX + 1, maxZ - minZ + 1];
        visited[(int)(player.transform.position.x + 0.5 - minX), (int)(player.transform.position.z + 0.5 - minZ)] = true;
        q.Enqueue(player.transform.position + new Vector3(0.5f - minX, 0, 0.5f - minZ));
        while (q.Count != 0)
        {
            Vector3 curr = q.Dequeue();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    Vector3 neighb = curr + new Vector3(i, 0, j);
                    if (legal(neighb))
                    {
                        if (!visited[(int)neighb.x, (int)neighb.z] &&
                            map[(int)neighb.x, (int)neighb.z] != int.MinValue)
                        {
                            visited[(int)neighb.x, (int)neighb.z] = true;
                            if (map[(int)neighb.x, (int)neighb.z] == map[(int)curr.x, (int)curr.z] - 1)
                            {
                                continue;
                            }
                            map[(int)neighb.x, (int)neighb.z] = map[(int)curr.x, (int)curr.z] - 1;
                            q.Enqueue(neighb);
                        }
                    }
                }
            }
        }
    }

    private bool legal(Vector3 v)
    {
        if(v.x >= 0 && v.z >= 0 &&
            v.x < (maxX-minX) && v.z < (maxZ-minZ))
        {
            return true;
        }
        return false;
    }

    public Vector3 getBestAction(Vector3 zombie)
    {
        Vector3 pos = zombie + new Vector3(0.5f - minX, 0, 0.5f - minZ);
        Vector3 cell = Vector3.zero;
        Vector3 bestCell = Vector3.zero;
        int max = int.MinValue;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                cell = new Vector3(i, 0, j);
                Debug.Log(map[(int)(pos.x + cell.x), (int)(pos.z + cell.z)]);
                if (legal(pos + cell))
                {
                    if (max < map[(int)(pos.x + cell.x), (int)(pos.z + cell.z)])
                    {
                        max = map[(int)(pos.x + cell.x), (int)(pos.z + cell.z)];
                        bestCell = cell;
                    }
                }
            }
        }
        return bestCell;
    }
}
