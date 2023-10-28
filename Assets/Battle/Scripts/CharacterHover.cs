using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHover : MonoBehaviour
{
    float speed = 0.2f;
    GameObject frame_template;
    GameObject frame;
    bool mouse_in = false;
    GameObject[] corners = new GameObject[4];
    Vector3[] original_pos = new Vector3[4];
    Vector3[] target_size = new Vector3[4];
    Vector3[] target_position = new Vector3[4];
    Color target_color;
    bool showing = false;

    public void HoverIn(){
        if (FindObjectOfType<BattleController>().GetState() == BattleController.BattleState.SelectEnemy
            && tag == "Enemy"){
            if (!BattleController.HasTauntEnemy() || GetComponent<Character>().GetStatus(Status.status.taunt) > 0){
                mouse_in = true;
                ShowFrame();
                SetTargetPosition();
                SetTargetSize();
                target_color = new(1, 1, 1, 1);
            }
        }
    }

    public void HoverOut(){
        if (FindObjectOfType<BattleController>().GetState() == BattleController.BattleState.SelectEnemy && tag == "Enemy"){
            mouse_in = false;
            SetTargetPosition();
            SetTargetSize();
            target_color = new(1, 1, 1, 0);
        }
    }

    public void Click(){
        if (FindObjectOfType<BattleController>().GetState() == BattleController.BattleState.SelectEnemy && tag == "Enemy"){
            if (!BattleController.HasTauntEnemy() || GetComponent<Character>().GetStatus(Status.status.taunt) > 0){
                HoverOut();
                FindObjectOfType<BattleController>().EnemySelected(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if (showing){
            for (int i = 0; i < 4; i++){
                // change size
                if ((corners[i].transform.localScale - target_size[i]).magnitude < 0.001f) corners[i].transform.localScale = target_size[i];
                else corners[i].transform.localScale = Vector3.Lerp(corners[i].transform.localScale, target_size[i], speed);

                // change color
                if (GetColorDiff(corners[i].GetComponent<Image>().color, target_color) < 0.001f){
                    corners[i].GetComponent<Image>().color = target_color;
                    if (target_color == new Color(1, 1, 1, 0)) showing = false;
                }
                else corners[i].GetComponent<Image>().color = Color.Lerp(corners[i].GetComponent<Image>().color, target_color, speed);

                // change position
                if ((corners[i].transform.localPosition - target_position[i]).magnitude < 0.001f) corners[i].transform.localPosition = target_position[i];
                corners[i].transform.localPosition = Vector3.Lerp(corners[i].transform.localPosition, target_position[i], speed);
            }
        }
    }

    float maxSize = 0.75f;
    void SetTargetSize(){
        if (mouse_in){
            target_size[0] = new(0.5f, 0.5f);
            target_size[1] = new(-0.5f, 0.5f);
            target_size[2] = new(0.5f, -0.5f);
            target_size[3] = new(-0.5f, -0.5f);
        }
        else{
            target_size[0] = new(maxSize, maxSize);
            target_size[1] = new(-maxSize, maxSize);
            target_size[2] = new(maxSize, -maxSize);
            target_size[3] = new(-maxSize, -maxSize);
        }
    }

    float multiplier = 1.25f;
    void SetTargetPosition(){
        Vector3 offset = GetComponent<Character>().GetOffset();
        for (int i = 0; i < 4; i++){
            if (mouse_in) target_position[i] = original_pos[i] + offset;
            else target_position[i] = original_pos[i] * multiplier + offset;
        }
    }

    bool corners_set = false;
    void SetCorners(){
        if (!frame){
            Debug.Log("CharacterHover: No frame");
            return;
        }
        if (corners_set) return;

        for (int i = 0; i < 4; i++){
            if (!corners[i]) corners[i] = frame.transform.GetChild(i).gameObject;
        }

        Vector3 size = GetComponent<Character>().GetSize();
        if (size.x == 0) size.x = 300;
        if (size.y == 0) size.y = 300;

        Vector3 offset = GetComponent<Character>().GetOffset();

        // original_pos[0] = new Vector3(-100, 100) + offset;
        // original_pos[1] = new Vector3(100, 100) + offset;
        // original_pos[2] = new Vector3(-100, -100) + offset;
        // original_pos[3] = new Vector3(100, -100) + offset;

        original_pos[0] = new Vector3(-(size.x / 2 - 50),  (size.y / 2 - 50));
        original_pos[1] = new Vector3( (size.x / 2 - 50),  (size.y / 2 - 50));
        original_pos[2] = new Vector3(-(size.x / 2 - 50), -(size.y / 2 - 50));
        original_pos[3] = new Vector3( (size.x / 2 - 50), -(size.y / 2 - 50));

        if (!showing){
            for (int i = 0; i < 4; i++){
                corners[i].transform.localPosition = original_pos[i] * multiplier + offset;
            }
        }
        
        corners_set = true;
    }

    void ShowFrame(){
        if (!frame_template) frame_template = FindObjectOfType<GlobalAssets>().characterFrameTemplate;
        if (!frame) frame = Instantiate(frame_template, transform);
        SetCorners();
        showing = true;
    }

    GameObject GetFrame(){
        return frame;
    }

    float GetColorDiff(Color a, Color b){
        return Mathf.Abs(a.r - b.r) + Mathf.Abs(a.g - b.g) + Mathf.Abs(a.b - b.b) + Mathf.Abs(a.a - b.a);
    }
}
