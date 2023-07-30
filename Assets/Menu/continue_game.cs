using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class continue_game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(toLoad);
    }

    // Update is called once per frame
    void toLoad(){
        SceneManager.LoadScene("loadlevel");
    }
}
