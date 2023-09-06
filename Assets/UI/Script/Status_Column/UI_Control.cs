using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Control : MonoBehaviour
{
    public bool start_set = false;
    public Vector2 positon_offset;
    public GameObject Information_Show_Gameobject;
    public UI_Show_Text UI_information;
    public GameObject information_box;
    List<GameObject> information_box_array = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        if(start_set == true)
        {
            int index = 0;
            float addi = 0;
            foreach (var information in UI_information.texts)
            {
                GameObject new_box = Instantiate(information_box);
                if(information.box!=null)new_box.GetComponent<Information_Box>().box = information.box;
                new_box.GetComponent<Information_Box>().information.text = information.information;
                new_box.GetComponent<Information_Box>().add_count(information.line_count);
                // new_box.GetComponent<Information_Box>().set_box_size();
                new_box.SetActive(false);
                new_box.GetComponent<Transform>().SetParent(Information_Show_Gameobject.transform);
                new_box.transform.localPosition = Vector3.zero;
                new_box.transform.position = new_box.transform.position + new Vector3(positon_offset.x,positon_offset.y-index*1.2f-addi,0);
                if(information.line_count>3)addi+=(information.line_count-3)*0.4f;
                information_box_array.Add(new_box);
                index++;

            }
        }
        
    }
    public void reset_pos()
    {
        foreach(var i in information_box_array)
        {
            Destroy(i);
        }
        information_box_array.Clear();
        int index = 0;
        float addi = 0;
        foreach (var information in UI_information.texts)
        {
            GameObject new_box = Instantiate(information_box);
            if(information.box!=null)new_box.GetComponent<Information_Box>().box = information.box;
            new_box.GetComponent<Information_Box>().information.text = information.information;
            new_box.GetComponent<Information_Box>().add_count(information.line_count);
            // new_box.GetComponent<Information_Box>().set_box_size();
            new_box.SetActive(false);
            new_box.GetComponent<Transform>().SetParent(Information_Show_Gameobject.transform);
            new_box.transform.localPosition = Vector3.zero;
            new_box.transform.position = new_box.transform.position + new Vector3(positon_offset.x,positon_offset.y-index*1.2f-addi,0);
            if(information.line_count>3)addi+=(information.line_count-3)*0.4f;
            information_box_array.Add(new_box);
            index++;

        }
    }
    public void reset_pos(Transform t,Vector3 pos)
    {
        foreach(var i in information_box_array)
        {
            Destroy(i);
        }
        information_box_array.Clear();
        int index = 0;
        float addi = 0;
        foreach (var information in UI_information.texts)
        {
            GameObject new_box = Instantiate(information_box);
            if(information.box!=null)new_box.GetComponent<Information_Box>().box = information.box;
            new_box.GetComponent<Information_Box>().information.text = information.information;
            new_box.GetComponent<Information_Box>().add_count(information.line_count);
            // print(new_box.GetComponent<Information_Box>().information.textInfo.lineCount.ToString()+" "+Time.time.ToString());
            // new_box.GetComponent<Information_Box>().set_box_size();
            new_box.SetActive(false);
            new_box.GetComponent<Transform>().SetParent(t);
            new_box.transform.position = new_box.transform.position + pos + new Vector3(positon_offset.x,positon_offset.y-index*1.2f-addi,0);
            if(information.line_count>3)addi+=(information.line_count-3)*0.4f;
            information_box_array.Add(new_box);
            index++;

        }
        // int index = 0;
        // foreach (var information in information_box_array)
        // {
           
        //     information.GetComponent<Transform>().SetParent(t);
        //     index++;

        // }
    }
    public void reset_pos_old(Transform t,Vector3 pos)
    {
        foreach(var i in information_box_array)
        {
            Destroy(i);
        }
        information_box_array.Clear();
        int index = 0;
        float addi = 0;
        foreach (var information in UI_information.texts)
        {
            GameObject new_box = Instantiate(information_box);
            if(information.box!=null)new_box.GetComponent<Information_Box>().box = information.box;
            new_box.GetComponent<Information_Box>().information.text = information.information;
            new_box.GetComponent<Information_Box>().add_count(information.line_count);
            // new_box.GetComponent<Information_Box>().set_box_size();
            new_box.SetActive(false);
            new_box.GetComponent<Transform>().SetParent(t);
            new_box.transform.position = new_box.transform.position + pos + new Vector3(positon_offset.x,positon_offset.y-index*1.2f-addi,0);
            if(information.line_count>3)addi+=(information.line_count-3)*0.4f;
            information_box_array.Add(new_box);
            index++;

        }
        // int index = 0;
        // foreach (var information in information_box_array)
        // {
           
        //     information.GetComponent<Transform>().SetParent(t);
        //     index++;

        // }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show_Text()
    {
        foreach (var box in information_box_array)
        {
            // box.GetComponent<Information_Box>().set_box_size();
            box.SetActive(true);
            box.GetComponent<Information_Box>().set_box_size();
        }
    }
    public void Disable_Text()
    {
        foreach (var box in information_box_array)
        {
            box.GetComponent<Information_Box>().set_box_size();

            box.SetActive(false);

        }
    }
    public void Change_description()
    {
        for(int i=0;i<information_box_array.Count;i++)
        {
            GameObject new_box = information_box_array[i];
            new_box.GetComponent<Information_Box>().information.text = UI_information.texts[i].information;
            new_box.GetComponent<Information_Box>().add_count(UI_information.texts[i].line_count);
        }
    }

    public void Change_text(string change_text,int index = 0,int line_count = 3)
    {
        information_box_array[index].GetComponent<Information_Box>().information.text = change_text;
        information_box_array[index].GetComponent<Information_Box>().add_count(line_count);
    }
    public void Add_text(string change_text,int line_count = 3)
    {

        int index = information_box_array.Count;
        float addi = 0;
        foreach (var information in UI_information.texts)
        {
            if(information.line_count>3)addi+=(information.line_count-3)*0.4f;
        }
        GameObject new_box = Instantiate(information_box);
        // if(information.box!=null)new_box.GetComponent<Information_Box>().box = information.box;
        new_box.GetComponent<Information_Box>().information.text = change_text;
        new_box.GetComponent<Information_Box>().add_count(line_count);
            // print(new_box.GetComponent<Information_Box>().information.textInfo.lineCount.ToString()+" "+Time.time.ToString());
            // new_box.GetComponent<Information_Box>().set_box_size();
        new_box.SetActive(false);
        new_box.GetComponent<Transform>().SetParent(Information_Show_Gameobject.transform);
        Debug.Log(index);
        new_box.transform.localPosition = Vector3.zero;
        new_box.transform.position = new_box.transform.position  + new Vector3(positon_offset.x,positon_offset.y-index*1.2f-addi,0);
        information_box_array.Add(new_box);
    }
}
