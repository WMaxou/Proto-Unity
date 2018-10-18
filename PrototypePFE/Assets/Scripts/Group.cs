using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Group
{
    private GameObject cube;

    public Transform destination;
    public int pathId;
    private bool finshlevel;

    public List<Character> characters;

    public Group(List<Character> _characters, float stopDist, int _pathId, Color color)
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<MeshRenderer>().material.color = color;
        characters = _characters;
        pathId = _pathId;

        foreach (Character character in _characters)
        {
            character.navmesh.stoppingDistance = stopDist;
            character.GetComponent<MeshRenderer>().material.color = color;
            character.@group = this;
        }
        UpdatePath();
    }

    public void Update()
    {
        if (finshlevel)
        {
            SceneManager.LoadScene(0);
            return;
        }

        if (IsArrived())
            UpdatePath();

        if (destination != null)
        {
            cube.transform.position = destination.position;
            cube.transform.Rotate(Vector3.up, 30 * Time.deltaTime);
        }
    }

    public void RemoveCharacter(Character toremove)
    {
        characters.Remove(toremove);

        if (characters.Count == 0)
            GroupManager.Instance.removeGroup(this);
    }

    public void SetDesination(Transform _destination)
    {
        destination = _destination;
        foreach (Character character in characters)
        {
            character.navmesh.destination = destination.position;
        }
    }

    public void UpdatePath()
    {
        destination = PathManager.Instance.GetPath(pathId, ref finshlevel);
        ++pathId;

        if (finshlevel == false)
        {
            foreach (Character character in characters)
            {
                character.navmesh.destination = destination.transform.position;
            }
        }
    }

    bool IsArrived()
    {
        foreach (Character character in characters)
        {
            if (character.IsArrived() == false)
                return false;
        }

        return true;
    }
}
