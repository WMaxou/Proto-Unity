using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour
{
    public InputField inputfield;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		    SceneManager.LoadScene(0);
    }


    public void EndInput()
    {
        string text = inputfield.text;
        if (string.IsNullOrEmpty(text))
            return;

        int i = text.IndexOf(':');
        string func = inputfield.text.Substring(0, i);
        int groupid;
        int numToSplit;
        if (int.TryParse(text.Substring(i + 1, 1), out groupid) == false)
            return;

        if (int.TryParse(text.Substring(i + 2, 1), out numToSplit) == false)
            return;

        if (func == "Split" || func == "split")
            GroupManager.Instance.Split(groupid, numToSplit);
    }
}
