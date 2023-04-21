using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Control : MonoBehaviour
{
    public GameObject Information_Show_Gameobject;
    public UI_Show_Text UI_information;
    public GameObject information_box;
    List<GameObject> information_box_array = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var information in UI_information.texts)
        {
            GameObject new_box = Instantiate(information_box);
            new_box.GetComponent<Information_Box>().box = information.box;
            new_box.GetComponent<Information_Box>().information.text = information.information;
            new_box.SetActive(false);
            new_box.GetComponent<Transform>().SetParent(Information_Show_Gameobject.transform);
            information_box_array.Add(new_box);

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show_Text()
    {
        foreach (var box in information_box_array)
        {
            box.SetActive(true);

        }
    }
    public void Disable_Text()
    {
        foreach (var box in information_box_array)
        {
            box.SetActive(false);

        }
    }
}
