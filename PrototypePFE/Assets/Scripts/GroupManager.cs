using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
    public static GroupManager Instance
    {
        get { return instance; }
    }
    private static GroupManager instance;

    public float stopDistance;
    public List<Group> groups;

    private void Awake()
    {
        instance = this;
        groups = new List<Group>();
    }

    // Use this for initialization
	void Start ()
	{
        groups.Add(new Group(FindObjectsOfType<Character>().ToList(), stopDistance, 0, Color.green));
	}
	
	// Update is called once per frame
	void Update ()
	{
	    foreach (Group group in groups)
	    {
	        group.Update();
	    }
	}

    public void Split(int groupID, int num)
    {
        Debug.Log("Split group: " + groupID + ", in " + num);

        if (groupID - 1 >= groups.Count)
            return;

        if (num >= groups[groupID - 1].characters.Count)
            return;

        print("asdasd");
        Group current = groups[groupID - 1];

        List<Character> newCharacters = new List<Character>();
        for (int i = 0; i < num; ++i)
            newCharacters.Add(current.characters[i]);

        int removeIt = 0;
        while (removeIt < num)
        {
            current.characters.RemoveAt(0);
            ++removeIt;
        }

        Group group = new Group(newCharacters, stopDistance, current.pathId, Color.red);
        groups.Add(group);
    }
}
