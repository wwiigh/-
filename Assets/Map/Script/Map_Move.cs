using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Move : MonoBehaviour
{
    public BoxCollider2D bounds;
    public float scale;
    Vector3 bound_min,bound_max;
    Vector2 first,second;
    Vector3 pos;
    bool need_move;
    // Start is called before the first frame update
    void Start()
    {
        bound_min = bounds.bounds.min;
        bound_max = bounds.bounds.max;
        first.x = transform.position.x;
        first.y = transform.position.y;
    }
    void OnGUI()
    {
        if(Event.current.type == EventType.MouseDown)
        {
            first = Event.current.mousePosition;
        }
        if(Event.current.type == EventType.MouseDrag)
        {
            second = Event.current.mousePosition;
            Vector3 first_pos = Camera.main.ScreenToWorldPoint(new Vector3(first.x,first.y,0));
            Vector3 second_pos = Camera.main.ScreenToWorldPoint(new Vector3(second.x,second.y,0));
            pos = second_pos - first_pos;
            pos = pos * scale;
            first = second;
            need_move = true;
        }
        else need_move = false;
    }
    // Update is called once per frame
    void Update()
    {
        bool canmove ;
        if(Map_System.now_state == Map_System.map_state.normal || 
        Map_System.now_ui == Map_System.map_ui.open)
        {
            canmove = true;
        }
        else
        {
            canmove = false;
        }
        if(canmove==true&& need_move)
        {
            var y = transform.position.y;
            y = y - pos.y;
            y = Mathf.Clamp(y,bound_min.y,bound_max.y);
            transform.position = new Vector3(transform.position.x,y,0);
        }
    }
}
