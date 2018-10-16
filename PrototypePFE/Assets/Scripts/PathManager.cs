using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static PathManager instance;
    public List<Transform> paths;

    private void Awake()
    {
        instance = this;
    }

    public Transform GetPath(int id, ref bool finshedLevel)
    {
        if (id >= paths.Count)
        {
            finshedLevel = true;
            return null;
        }

        Random rand;
        int i = Random.Range(0, paths[id].childCount);
        return paths[id].GetChild(i);
    }
}
