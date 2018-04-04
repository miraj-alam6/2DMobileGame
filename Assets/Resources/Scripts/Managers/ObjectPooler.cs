using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This object pooler class is based off of a youtube tutorial by the channel Brackeys
// https://www.youtube.com/watch?v=tdSmKaJvCoA

public class ObjectPooler : MonoBehaviour {


    public static ObjectPooler instance;

    [System.Serializable]
    public class Pool{
        public string name;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    //string is TagName Queue is the queue used for the specific object pool.
    public Dictionary<string, Queue<GameObject>> poolDictionary;
	
    void Awake(){
        //if(instance == null){
        //    transform.parent = null;
        //    instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else{

        //    Destroy(this.gameObject);
        //}
        instance = this;

    }


    // Use this for initialization
	void Start () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools){
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size;i++){
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.name, objectPool);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation){

        if(!poolDictionary.ContainsKey(tag)){
            Debug.LogWarning("Pool with tag " + tag +" doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();


        if(objectToSpawn.activeSelf){
            //if the object is already active, that means that the object pooler has overflowed, and as such
            //we need to find out if the object is a fireball, if it is, then it has to be removed from the
            //list of fireballs, need to make a  getcomponent call, but this is rare so it is okay.
            //This is simply to make the code more robust. In actual game this should never occur because the
            //object pool will be big enough that this won't ever occur.
            if(objectToSpawn.CompareTag(TagNames.Fireball)){
                GameplayController.instance.removeFireball(objectToSpawn.GetComponent<Offense>());
            }
        }
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
