using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

[InitializeOnLoad]
public class LevelManager : MonoBehaviour {

    public Button LoadButton;

    static LevelManager()
    {
        Debug.Log("Game started...");
    }

	// Use this for initialization
	void Start () {
        LoadButton.onClick.AddListener(() => { test(); });
    }

    void test()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
