using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class load : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Map_System.New_Game();
        UI_System.New_Game();
        Event_System.New_Game();
        SceneManager.LoadScene("Map");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
