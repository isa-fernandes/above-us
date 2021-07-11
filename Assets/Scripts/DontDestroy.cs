using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void start()
    {
        Scene sceneLoaded = SceneManager.GetActiveScene();
    }
    void update()
    {
        Scene sceneLoaded = SceneManager.GetActiveScene();

        Debug.Log(sceneLoaded);
    }
    // Update is called once per frame
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        
    }

   
}
