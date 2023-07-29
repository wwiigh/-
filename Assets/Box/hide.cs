using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject treasure;
    void Start()
    {
        // treasure.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            treasure.SetActive(false);
        }
    }
}
