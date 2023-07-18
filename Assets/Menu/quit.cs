using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
public class quit : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ClickToQuit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ClickToQuit(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif 
    }
}
