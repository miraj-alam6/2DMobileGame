using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour {
    public string levelFolder;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetLevelFolder(string l){
        levelFolder = l;
    }
    public void LoadLevel(string scene){
        if(SceneManager.GetSceneByName(levelFolder + scene) != null){
            SceneManager.LoadScene(levelFolder +"/" +scene);
        }
    }
}
