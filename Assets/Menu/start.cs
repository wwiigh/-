using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    // Start is called before the first frame update 
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ClickToMap);
    }

    // Update is called once per frame
    void ClickToMap(){
        SceneManager.LoadScene("loadlevel");
        Map_System.New_Game();
        UI_System.New_Game();
        Event_System.New_Game();
    }
}
