using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class Map_Generate : MonoBehaviour
{
    [System.Serializable]
    public struct node_type
    {
        public char type;
        [Range(0, 1)]
        public float chance;
    }
    [System.Serializable]
    public struct node_num
    {
        public int howmany;
        [Range(0, 1)]
        public float chance;
    }
    [Header("用來決定生成點的隨機位置")]
    public Vector2 random_pos_max;
    public Vector2 random_pos_min;
    [Header("用來決定生成點的機率")]
    public node_type[] different_type;
    [Header("node本身")]
    public GameObject _node;
    // [Header("用來放點的perfab(用Button)")]
    // public Button button;
    [Header("用來放邊的perfab(用Image)")]
    public Image line;
    [Header("用來放Canvas")]
    public Canvas canvas;

    [Header("用來放地圖")]
    public Image Map;
    [Header("表示點與點間的間隔(x軸)")]
    public int X_Space = 20;
    [Header("表示點與點間的間隔(y軸)")]
    public int Y_Space = 20;
    [Header("表示邊的寬度")]
    public float Edge_width = 10;
    [Header("表示地圖的高度")]
    public int node_height = 10;
    [Header("表示地圖的寬度")]
    public int node_width = 5;
    [Header("表示每層最少的戰鬥數量")]
    public int fight_num;
    [Header("表示每層最少的事件數量")]
    public int event_num;


    [Header("用來距離開頭x有多遠")]
    public float node_start_size_x;
    [Header("用來距離開頭y有多遠")]
    public float node_start_size_y;
    [Header("表示點的大小")]
    public Vector2 node_size;

    [Header("每層出現數目")]
    public node_num[] nodes_num;
    [Header("現在關卡")]
    public static int now_level;
    [Header("現在層數")]
    public int now_height;

    [Header("保留給點的顯示,如戰鬥,事件的ui顯示等,尚未使用")]
    public Color[] colors;
    // [Header("顯示商店")]
    // public GameObject shop_object;
    // [Header("顯示祭壇")]
    // public GameObject altar_object;
    [Header("動畫控制")]
    public Animation _animation;
    [Header("UI地圖控制")]
    public UI_button_control ui_control;
    [Header("地圖點控制")]
    public Map_Node_Action node_action;

    [Header("地圖控制")]
    public Map_Generate map;
    [Header("顯示商店")]
    public GameObject shop_object;
    [Header("顯示祭壇")]
    public GameObject altar_object;
    [Header("顯示戰鬥")]
    public GameObject battle_object;
    [Header("顯示戰鬥")]
    public GameObject battle_object_equipment;
    [Header("顯示寶箱")]
    public GameObject treasure_object;
    [Header("顯示事件")]
    public GameObject event_object;
    public List<GameObject> All_Battle_Obj;
    [Header("Card panel")] 
    public GameObject CardPanel;


    private List<Node> nodes = new List<Node>();
    private List<Image> edges = new List<Image>();
    private int now_width = 0;
    // public Map_Save map_save;
    // private List<int> node_arr = new List<int>();
    // Start is called before the first frame update
    public void On_Click(char type)
    {
        node_action.PlayMusic();
        // print("onclick");
        // map_save.save_node(nodes);
        // map_save.save_level_height(now_level,now_height);
        switch(type)
        {
            case 's':
                ui_control.now_visit = "Shop";
                break;
            case 'f':
                ui_control.now_visit = "Battle";
                break;
            case 'F':
                ui_control.now_visit = "Battle";
                break;
            case 'e':
                ui_control.now_visit = "Events";
                break;
            case 't':
                ui_control.now_visit = "Treasure";
                break;
            case 'b':
                ui_control.now_visit = "Battle";
                break;
            case 'h':
                ui_control.now_visit = "Altar";
                break;
            case 'm':
                ui_control.now_visit = "Events";
                break;
            default:
            break;
        }
        
    }
    public void Set_width_height(int _width)
    {
        now_width = _width;
        now_height -=1;
        // print(now_width);
        save();
        
    }
    public void Go_to_next()
    {
        Show_status();
        if(now_level == 3 && now_height==-1)
        {
            node_action.ending();
        }
        if(now_height==-1)
        {
            ReGenerate_Map();
            Play_Animation();
            Global.player_hp = Global.player_max_hp;
        }
    }

    void Start()
    {
        // print(Application.dataPath);
        Generate_Map();
        Clear_Battle();
        FindObjectOfType<BattleController>().EnterNewLevel();
    }
    void Play_Animation()
    {
        _animation.Play();
    }
    void ReGenerate_Map()
    {
        clear();
        Init();
        Gen_Map();
        Regenerate_Point();
        Create_Edge();
        Visualize_Edge();
        Generate_Special_events(node_height-1,'f');
        Generate_Special_events(Random.Range(2,4),'f');
        Generate_Special_events(4,'m');
        Generate_Special_events(0,'b');
        Generate_Special_events(1,'h');
        Generate_reset();
        check_big_monster(0,2,0);
        check_small_monster(0,2,0);
        now_level = now_level + 1;
        if(now_level>3)
        {
            Map_System.New_Game();
            UI_System.New_Game();
            Event_System.New_Game();
            Bag_Save.Load_Data(FindObjectOfType<Bag_System>());
            Global.init();
            now_level=1;
        }
        Global.current_level = now_level;
        now_height = 9;
        save();
        Show_status();
        check_status();
        set_click_action();
        FindObjectOfType<BattleController>().EnterNewLevel();
    }
    void clear()
    {
        foreach(var i in nodes)
        {
            Destroy(i.gameObject);
        }
        nodes.Clear();
        foreach(var i in edges)
        {
            Destroy(i.gameObject);
        }
        edges.Clear();
    }
    void Generate_Map()
    {
        string LoadData = PlayerPrefs.GetString("Map_Data","");
        // LoadData = "";
        if(LoadData!="")
        {
            Map_Save  MyData = JsonUtility.FromJson<Map_Save>(LoadData);
            if(MyData.is_new==0&&MyData.now_height>=0)
            {
                Global.ReadData();
                Init();
                copy(MyData);
                Regenerate_Point();
                //map_save.copy(nodes);
                now_height = MyData.return_height();
                now_level = MyData.return_level()>3?1:MyData.return_level();
                now_width = MyData.return_width();
                Visualize_Edge();
                Show_status();
                check_status();
                set_click_action();
                Global.current_level = now_level;
                return;
            }
        }
        Global.init();
        //初始
        Init();
        //顯示點
        Gen_Map();
        Regenerate_Point();
        //創造邊
        Create_Edge();
        //將邊畫出來
        Visualize_Edge();
        //從地圖最下方往上找路徑生成戰鬥,事件
        // for(int i=0;i<node_width;i++)
        // {
        //     node_arr.Add(i);
        //     Generate(node_arr, node_height-1, i);
        //     node_arr.Clear();
        // }
        // List<List<int>> node_arr = new List<List<int>>();
        // List<int> tmp_node_arr = new List<int>();
        // Generate_V3_set_route(node_arr,0,2,tmp_node_arr);
        // Generate_V3_set_event(node_arr);
        Generate_Special_events(node_height-1,'f');
        Generate_Special_events(Random.Range(2,4),'f');
        Generate_Special_events(4,'m');
        Generate_Special_events(0,'b');
        Generate_Special_events(1,'h');
        // Generate_V2();
        
        Generate_reset();
        //將被標為戰鬥,事件的點更新，更新圖層
        check_big_monster(0,2,0);
        check_small_monster(0,2,0);
        now_level = 1;
        Map_System.New_Game();
        UI_System.New_Game();
        Event_System.New_Game();
        Bag_Save.Load_Data(FindObjectOfType<Bag_System>());
        now_height = 9;
        Global.current_level = now_level;
        save();
        // map_save.save_node(nodes);
        // map_save.save_level_height(now_level,now_height);
        // map_save.is_new = 0;
        Show_status();
        check_status();
        set_click_action();
    }
    // Update is called once per frame
    void check_status()
    {
        if(SceneManager.GetActiveScene().name != "map2" &&SceneManager.GetActiveScene().name != "Map")
        {
            foreach(var n in nodes)
            {
                n.node.enabled = false;
            }
        }
    }
    void Update()
    {
        // print(nodes[0].GetComponent<RectTransform>().position);
    }
    //最上方為height = 0
    //左邊往右數width為0 1 2
    void Init()
    {

        float node_size_x = node_size.x;
        float node_size_y = node_size.y;
        for (int i = 0; i < node_height; i++)
        {
            for (int j = 0; j < node_width; j++)
            {
                GameObject tmp_node = Instantiate(_node);
                // tmp_node.GetComponent<Transform>().position = Vector3.zero;
                //設定位置
                Button b = tmp_node.GetComponent<Node>().node;
                b.GetComponent<Transform>().SetParent(Map.transform);
                float random_x = Random.Range(random_pos_min.x,random_pos_max.x);
                float random_y = Random.Range(random_pos_min.y,random_pos_max.y);
                b.GetComponent<RectTransform>().offsetMax = new Vector2(node_start_size_x + (j + 1) * node_size_x + (j) * X_Space+random_x, node_start_size_y - i * node_size_y - (i) * Y_Space+random_y);
                b.GetComponent<RectTransform>().offsetMin = new Vector2(node_start_size_x + (j) * node_size_x + (j) * X_Space+random_x, node_start_size_y - (i + 1) * node_size_y - (i) * Y_Space+random_y);
                
                //對點作初始
                tmp_node.GetComponent<Node>().node = b;
                tmp_node.GetComponent<Node>().Init();
                tmp_node.GetComponent<Node>().Set_height(i);
                tmp_node.GetComponent<Node>().Set_width(j);
                tmp_node.GetComponent<Node>().Set_node_size(node_size);

                //設定on click
                
                b.onClick.AddListener(delegate { On_Click(tmp_node.GetComponent<Node>().Return_type()); });
                b.onClick.AddListener(delegate { Set_width_height(tmp_node.GetComponent<Node>().return_width()); });
                tmp_node.GetComponent<RectTransform>().localPosition= new Vector3(
                    tmp_node.GetComponent<RectTransform>().localPosition.x,
                    tmp_node.GetComponent<RectTransform>().localPosition.y,0);

                //加入到nodes裡
                nodes.Add(tmp_node.GetComponent<Node>());
            }
        }
    }

    //生成地圖
    void Gen_Map()
    {
        //設定height=0,width=2為boss,故顯示
        nodes[2].Set_valid();

        //每層隨機挑2~4點顯示
        int random_nodes_num=0;
        for (int i = 1; i < node_height; i++)
        {
            float probability = Random.value;
            
            for (int ii = 0; ii < nodes_num.Length; ii++)
            {
                if (probability <= nodes_num[ii].chance)
                {
                    random_nodes_num = nodes_num[ii].howmany;
                    break;
                }
                else probability-=nodes_num[ii].chance;
            }
            for (int j = 0; j < random_nodes_num; j++)
            {
                int random_nodes_index = Random.Range(0, node_width);
                while (nodes[i * node_width + random_nodes_index].Return_valid() == true)
                {
                    random_nodes_index++;
                    if (random_nodes_index >= node_width) random_nodes_index = 0;
                }
                nodes[i * node_width + random_nodes_index].Set_valid();
            }
        }
    }

    //創邊
    void Create_Edge()
    {
        //設定每列的顯示點中最大index
        int[] bigest_valid = new int[node_height];
        for (int i = 0; i < node_height; i++) bigest_valid[i] = 0;
        for (int i = 0; i < node_height; i++)
        {
            for (int j = 0; j < node_width; j++)
            {
                if (nodes[i * node_width + j].Return_valid() == true) bigest_valid[i] = j;
            }
        }
        //尋找上一層的點，接邊上去
        for (int i = node_height - 1; i > 0; i--)
        {
            int now_next = 0;
            for (int j = 0; j < node_width; j++)
            {
                if (nodes[i * node_width + j].Return_valid() == false) continue;
                for (int k = now_next; k < node_width; k++)
                {
                    if (nodes[(i - 1) * node_width + k].Return_valid() == false) continue;
                    float x = Random.value;
                    if ((nodes[i * node_width + j].parent.Count == 0 || bigest_valid[i] == j || x > 0.5)&&(nodes[i * node_width + j].parent.Count <3||nodes[(i - 1) * node_width + k].next.Count<3))
                    {
                        nodes[i * node_width + j].parent.Add(k);
                        nodes[(i - 1) * node_width + k].next.Add(j);
                        now_next = k;
                    }
                    else break;
                }
            }
        }
    }
    //把邊畫出來
    void Visualize_Edge()
    {
        for (int i = 0; i < node_height; i++)
        {
            for (int j = 0; j < node_width; j++)
            {
                Node tmp = nodes[i * node_width + j];
                Vector3 a = tmp.node.GetComponent<RectTransform>().position;
                for (int k = 0; k < tmp.next.Count; k++)
                {
                    Vector3 b = nodes[(i + 1) * node_width + tmp.next[k]].node.GetComponent<RectTransform>().position;
                    Image l = Instantiate(line);
                    l.GetComponent<Transform>().SetParent(Map.transform);
                    l.GetComponent<RectTransform>().position = Middle_Pos(a, b) ;
                    l.GetComponent<RectTransform>().rotation = Vector_To_Quaternion(a, b);
                    l.GetComponent<RectTransform>().sizeDelta = new Vector2(Edge_width, Vector_Length(a, b));
                    edges.Add(l);
                }

            }
        }
    }

    Vector3 Middle_Pos(Vector3 a, Vector3 b)
    {
        return (a + b) / 2;
    }
    float Vector_Length(Vector3 a, Vector3 b)
    {
        return (a - b).magnitude;
    }
    //設定角度
    Quaternion Vector_To_Quaternion(Vector3 a, Vector3 b)
    {
        Vector3 angle = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, a - b));
        return Quaternion.Euler(angle);
    }
    //生成戰鬥,事件
    void Generate(List<int> node_arr, int height, int width)
    {
        if (nodes[node_width * height + width].Return_valid() == false) return;
        if (height == 1)
        {
            int need_fight = fight_num;
            int need_event = event_num;
            for (int i = 1; i < node_arr.Count; i++)
            {
                if (nodes[i * node_width + node_arr[i]].Return_type() == 'f') need_fight--;
                if (nodes[i * node_width + node_arr[i]].Return_type() == 'e') need_event--;
            }
            // print(need_event);
            // print(need_fight);
            for (int i = 0; i < need_fight; i++)
            {
                int x = Random.Range(1, node_height);
                int count = 0;
                while (nodes[x * node_width + node_arr[x - 1]].Return_type() != 'n')
                {
                    count++;
                    if (count >= node_height) break;
                    x = x + 1;
                    if (x >= node_height) x = 1;

                }
                nodes[x * node_width + node_arr[x - 1]].Set_type('f');
            }
            for (int i = 0; i < need_event; i++)
            {
                int x = Random.Range(1, node_height);
                int count = 0;
                while (nodes[x * node_width + node_arr[x - 1]].Return_type() != 'n')
                {
                    count++;
                    if (count >= node_height) break;
                    x = x + 1;
                    if (x >= node_height) x = 1;
                }
                nodes[x * node_width + node_arr[x - 1]].Set_type('e');
            }
            return;
        }
        for (int i = 0; i < nodes[height * node_width + width].parent.Count; i++)
        {
            node_arr.Add(nodes[height * node_width + width].parent[i]);
            Generate(node_arr, height - 1, nodes[height * node_width + width].parent[i]);
            node_arr.RemoveAt(node_arr.Count - 1);
        }
    }
    //更新點的狀態並顯示出來
    void Show_status()
    {
        for (int i = 0; i < node_height; i++)
        {
            for (int j = 0; j < node_width; j++)
            {
                int count = Map.transform.childCount;
                Node n = nodes[i * node_width + j];
                n.node.transform.SetSiblingIndex(count - 1);
                // print(n.return_height());
                if(i==9 && now_height==i)
                {
                    // print("show status");
                    n.node.GetComponent<Image>().color = new Color(1,1,1,1);
                    // n.node.GetComponent<Image>().enabled = (false);
                    n.node.GetComponent<Button>().enabled = (true);
                }
                else if(i==now_height)
                {
                    if(nodes[(i+1)*node_width + now_width].parent.Contains(j))
                    {

                        // print("show status");
                        n.node.GetComponent<Image>().color = new Color(1,1,1,1);
                        // n.node.GetComponent<Image>().enabled = (false);
                        n.node.GetComponent<Button>().enabled = (true);
                    }
                    else
                    {
                        n.node.GetComponent<Image>().color = new Color(1,1,1,0.5f);
                        n.node.GetComponent<Button>().enabled = (false);
                    }
                }
                else
                {
                    n.node.GetComponent<Image>().color = new Color(1,1,1,0.5f);
                    n.node.GetComponent<Button>().enabled = (false);
                }
                if (n.Return_valid() == false)
                {
                    n.node.GetComponent<Button>().enabled = (false);
                    n.node.GetComponent<Image>().enabled = (false);
                }

            }
        }
    }
    //暫時用作辨別事件和戰鬥
    Color Get_Color(char type)
    {
        switch (type)
        {
            case 's':
                return Color.black;
            case 'f':
                return Color.red;
            case 'F':
                return Color.black;
            case 'e':
                return Color.yellow;
            case 't':
                return Color.black;
            case 'n':
                return Color.black;
            case 'b':
                return Color.blue;
            default:
                return Color.black;
        }
    }

    void Regenerate_Point()
    {
        for (int i = 1; i < node_height; i++)
        {
            int valid_num = 0;

            for (int j = 0; j < node_width; j++)
            {
                if (nodes[i * node_width + j].Return_valid() == true)
                {
                    valid_num++;
                }
            }
            Node[] tmp_nodes = new Node[valid_num];
            int index = 0;
            for (int j = 0; j < node_width; j++)
            {
                if (nodes[i * node_width + j].Return_valid() == true)
                {
                    tmp_nodes[index++] = nodes[i * node_width + j];
                }
            }
            float new_space_x = node_size.x * node_width + X_Space * (node_width) - valid_num * node_size.x;
            new_space_x = new_space_x / (valid_num - 1) *0.7f;
            for (int j = 0; j < index; j++)
            {
                float random_x = Random.Range(random_pos_min.x,random_pos_max.x);
                float random_y = Random.Range(random_pos_min.y,random_pos_max.y);
                tmp_nodes[j].node.GetComponent<RectTransform>().offsetMax = new Vector2(node_start_size_x +(j + 1) * node_size.x + (j) * new_space_x+random_x, node_start_size_y-i * node_size.y - (i) * Y_Space+random_y);
                tmp_nodes[j].node.GetComponent<RectTransform>().offsetMin = new Vector2(node_start_size_x +(j) * node_size.x + (j) * new_space_x+random_x, node_start_size_y-(i + 1) * node_size.y - (i) * Y_Space+random_y);
            }

        }
    }

    void Generate_V2()
    {
        int[] use = new int[node_height];
        for (int i = 0; i < node_height; i++) use[i] = 0;
        use[0] = 1;
        for (int i = 0; i < fight_num; i++)
        {
            int x = Random.Range(0, node_height);
            while (use[x] == 1)
            {
                x = x + 1;
                if (x >= node_height) x = 0;
            }
            use[x] = 1;
            for (int j = 0; j < node_width; j++)
            {
                nodes[x * node_width + j].Set_type('f');
            }
        }
        for (int i = 0; i < event_num; i++)
        {
            int x = Random.Range(0, node_height);
            while (use[x] == 1)
            {
                x = x + 1;
                if (x >= node_height) x = 0;
            }
            use[x] = 1;
            for (int j = 0; j < node_width; j++)
            {
                nodes[x * node_width + j].Set_type('e');
            }
        }
    }
    void Generate_V3_set_route(List<List<int>> node_arr, int height, int width, List<int> tmp_node_arr)
    {
        if (nodes[node_width * height + width].Return_valid() == false) return;
        if (height == node_height - 1)
        {
            List<int> tmp = new List<int>();
            for (int i = 0; i < tmp_node_arr.Count; i++)
            {
                tmp.Add(tmp_node_arr[i]);
            }
            node_arr.Add(tmp);
            return;
        }
        for (int i = 0; i < nodes[height * node_width + width].next.Count; i++)
        {
            int tmp_count = tmp_node_arr.Count;
            tmp_node_arr.Add(nodes[height * node_width + width].next[i]);
            Generate_V3_set_route(node_arr, height + 1, nodes[height * node_width + width].next[i], tmp_node_arr);
            tmp_node_arr.RemoveAt(tmp_count);
        }
        return;
    }

    void Generate_V3_set_event(List<List<int>> node_arr)
    {
        List<int> events = new List<int>();
        List<int> fights = new List<int>();

        for (int i = 0; i < node_arr.Count; i++)
        {
            events.Add(event_num);
            fights.Add(fight_num);
        }
        int ii = 0;
        int node_index = Random.Range(0, node_arr.Count);
        while (node_arr.Count > 0)
        {
            ii++;
            for (int i = 0; i < node_arr[node_index].Count; i++)
            {
                //print(k.ToString()+" "+i.ToString()+" "+node_arr[k][i].ToString());
                if (nodes[(i + 1) * node_width + node_arr[node_index][i]].Return_type() == 'e') events[node_index]--;
                if (nodes[(i + 1) * node_width + node_arr[node_index][i]].Return_type() == 'f') fights[node_index]--;
            }
            for (int i = 0; i < events[node_index]; i++)
            {
                int counts = 0;
                int x = Random.Range(1, node_height);
                while (nodes[x * node_width + node_arr[node_index][x - 1]].can_assign == false)
                {
                    counts++;
                    x = x + 1;
                    if (x >= node_height) x = 1;
                    // print(x);
                    if (counts > 10) break;
                }
                nodes[x * node_width + node_arr[node_index][x - 1]].can_assign = false;
                nodes[x * node_width + node_arr[node_index][x - 1]].Set_type('e');
            }
            for (int i = 0; i < fights[node_index]; i++)
            {
                int counts = 0;
                int x = Random.Range(1, node_height);
                while (nodes[x * node_width + node_arr[node_index][x - 1]].can_assign == false)
                {
                    counts++;
                    x = x + 1;
                    if (x >= node_height) x = 1;
                    if (counts > 10) break;
                }
                nodes[x * node_width + node_arr[node_index][x - 1]].can_assign = false;
                nodes[x * node_width + node_arr[node_index][x - 1]].Set_type('f');
            }

            int fight_index = 0;
            for (int i = node_arr[node_index].Count - 1; i >= 0; i--)
            {
                nodes[(i + 1) * node_width + node_arr[node_index][i]].can_assign = false;
                if (nodes[(i + 1) * node_width + node_arr[node_index][i]].Return_type() == 'f') fight_index++;

                if (fight_index >= 3)
                {
                    Generate_V3_set_use(node_arr, i + 1, node_arr[node_index][i]);
                }
            }
            node_arr.RemoveAt(node_index);
            node_index = Random.Range(0, node_arr.Count);
        }
    }

    void Generate_V3_set_use(List<List<int>> node_arr, int height, int width)
    {
        nodes[height * node_width + width].can_assign = false;
        nodes[height * node_width + width].node.GetComponent<Image>().color = Color.yellow;
        for (int i = 0; i < nodes[height * node_width + width].parent.Count; i++)
        {
            Generate_V3_set_use(node_arr, height - 1, nodes[height * node_width + width].parent[i]);
        }
    }

    void Generate_reset()
    {
        
        for (int j = 0; j < nodes.Count; j++)
        {
            float probability = Random.value;
            
            if (nodes[j].Return_type() == 'n')
            {
                for (int i = 0; i < different_type.Length; i++)
                {
                    if (probability <= different_type[i].chance)
                    {
                        nodes[j].Set_type(different_type[i].type);
                        // nodes[j].Set_type('F');
                        break;
                    }
                    else probability-=different_type[i].chance;
                }
            }
        }
    }

    void Generate_Special_events(int height, char type)
    {
        for (int j = 0; j < node_width; j++)
        {
            nodes[height * node_width + j].Set_type(type);
        }
    }
    public void save()
    {
        List<int> node_parent_arr = new List<int>();
        List<int> node_parent_index_arr = new List<int>();
        List<int> node_next_arr = new List<int>();
        List<int> node_next_index_arr = new List<int>();
        List<bool> node_assign_arr = new List<bool>();
        List<bool> node_valid_arr = new List<bool>();
        List<char> node_type_arr = new List<char>();
        int _is_new = 0;
        int _now_level = now_level;
        int _now_height = now_height;
        int _now_width = now_width;
        foreach (var item in nodes)
        {
            
            // Node tmp = new Node();
            // tmp.node = button;
            // tmp.copy(item);
            // height = item.return_height();
            // width = item.return_width();
            node_next_index_arr.Add(item.next.Count);
            foreach (var item2 in item.next)
            {
                node_next_arr.Add(item2);
            }
            node_parent_index_arr.Add(item.parent.Count);

            foreach (var item2 in item.parent)
            {
                node_parent_arr.Add(item2);
            }
            // little_monster = item.little_monster;
            // big_monster = item.big_monster;
            // events = item.events;
            // treasure = item.treasure;
            // altar = item.altar;
            // boss = item.boss;
            // shop = item.shop;
            node_valid_arr.Add(item.Return_valid());
            node_type_arr.Add(item.Return_type());
            node_assign_arr.Add(item.can_assign);
            // node_arr.Add(tmp);    
        } 
        Map_Save save_data = new Map_Save{
            is_new = _is_new,
            now_level = _now_level,
            now_height = _now_height,
            now_width = _now_width,
            node_assign_arr = node_assign_arr,
            node_parent_arr = node_parent_arr,
            node_parent_index_arr = node_parent_index_arr,
            node_next_arr = node_next_arr,
            node_next_index_arr = node_next_index_arr,
            node_valid_arr = node_valid_arr,
            node_type_arr = node_type_arr
        };
        string jsonInfo = JsonUtility.ToJson(save_data,true);
        PlayerPrefs.SetString("Map_Data",jsonInfo);
        // File.WriteAllText(Application.dataPath+"/Save_Data/Map_Data", jsonInfo);

    }
    public void copy(Map_Save data)
    {
        int index_parent = 0;
        int index_next = 0;
        for(int i=0;i<data.node_assign_arr.Count;i++)
        {
            // nodes[i].Set_height(height);
            // nodes[i].Set_width(width);
            // level = node_parent_arr.Count;
            for(int j=0;j<data.node_parent_index_arr[i];j++)
            {
                nodes[i].parent.Add(data.node_parent_arr[index_parent+j]);
            }
            index_parent += data.node_parent_index_arr[i];
            for(int j=0;j<data.node_next_index_arr[i];j++)
            {
                nodes[i].next.Add(data.node_next_arr[index_next+j]);
            }
            index_next += data.node_next_index_arr[i];
            
            if(data.node_valid_arr[i]==true)
                nodes[i].Set_valid();
            nodes[i].Set_type(data.node_type_arr[i]);
            nodes[i].can_assign = data.node_assign_arr[i];
        }
    }

    void check_big_monster(int _height,int _width,int _num)
    {
        for(int i=0;i<nodes[node_width*_height + _width].next.Count;i++)
        {
            int index = nodes[node_width*_height + _width].next[i];
            if(nodes[node_width*(_height+1) + index].Return_valid()==false)continue;
            if(nodes[node_width*(_height+1) + index].Return_type()=='F')
            {
                if(_num==2)
                {
                    float _type = Random.value;
                    if(_type<0.25)nodes[node_width*(_height+1) + index].Set_type('t');
                    else if(_type<0.5)nodes[node_width*(_height+1) + index].Set_type('s');
                    else if(_type<0.75)nodes[node_width*(_height+1) + index].Set_type('h');
                    else if(_type<=1)nodes[node_width*(_height+1) + index].Set_type('e');
                    check_big_monster(_height+1,index,_num);
                }
                else 
                check_big_monster(_height+1,index,_num+1);
            }
            else
            check_big_monster(_height+1,index,_num);
        }
    }
    void check_small_monster(int _height,int _width,int _num)
    {
        if(_height==8)return;
        for(int i=0;i<nodes[node_width*_height + _width].next.Count;i++)
        {
            int index = nodes[node_width*_height + _width].next[i];
            if(nodes[node_width*(_height+1) + index].Return_valid()==false)continue;
            if(nodes[node_width*(_height+1) + index].Return_type()=='f')
            {
                if(_num==4)
                {
                    float _type = Random.value;
                    if(_type<0.25)nodes[node_width*(_height+1) + index].Set_type('t');
                    else if(_type<0.5)nodes[node_width*(_height+1) + index].Set_type('s');
                    else if(_type<0.75)nodes[node_width*(_height+1) + index].Set_type('h');
                    else if(_type<=1)nodes[node_width*(_height+1) + index].Set_type('e');
                    check_small_monster(_height+1,index,_num);
                }
                else 
                check_small_monster(_height+1,index,_num+1);
            }
            else
            check_small_monster(_height+1,index,_num);
        }
    }

    public void set_click_action()
    {
        // print("set button");
        for(int i=0;i<nodes.Count;i++)
        {
            Node n = nodes[i];
            Button b = n.node;
            if(n.Return_valid()==false)continue;
            n.map = map;
            n.shop_object = shop_object;
            n.altar_object = altar_object;
            n.battle_object = battle_object;
            n.treasure_object = treasure_object;
            n.event_object = event_object;
            n.All_Battle_Obj = All_Battle_Obj;
            n.battle_object_equipment = battle_object_equipment;
            // switch (n.Return_type())
            // {
            //     case 's':
            //         // node.GetComponent<Image>().sprite = shop;
            //         b.onClick.AddListener(delegate { node_action.click_action_shop(); });
            //         break;
            //     case 'f':
            //         // node.GetComponent<Image>().sprite = little_monster;
            //         b.onClick.AddListener(delegate { node_action.click_action_battle(); });
            //         break;
            //     case 'F':
            //         // node.GetComponent<Image>().sprite = big_monster;
            //         b.onClick.AddListener(delegate { node_action.click_action_battle(); });
            //         break;
            //     case 'e':
            //         // node.GetComponent<Image>().sprite = events;
            //         b.onClick.AddListener(delegate { node_action.click_action_event(); });
            //         break;
            //     case 'm':
            //         // node.GetComponent<Image>().sprite = events;
            //         b.onClick.AddListener(delegate { node_action.click_action_story(now_level); });
            //         break;
            //     case 't':
            //         // node.GetComponent<Image>().sprite = treasure;
            //         b.onClick.AddListener(delegate { node_action.click_action_treasure(); });
            //         break;
            //     case 'n':
            //         // node.GetComponent<Image>().sprite = null;
            //         // b.onClick.AddListener(delegate { node_action.click_action_battle(); });
            //         break;
            //     case 'b':
            //         // node.GetComponent<Image>().sprite = boss;
            //         b.onClick.AddListener(delegate { node_action.click_action_battle(); });
            //         break;
            //     case 'h':
            //         // node.GetComponent<Image>().sprite = altar;
            //         b.onClick.AddListener(delegate { node_action.click_action_altar(); });
            //         break;
            // }
        }
    }
    // public void click_action_shop()
    // {
    //     shop_object.SetActive(true);

    // }
    // public void click_action_altar()
    // {
    //     altar_object.SetActive(true);
    // }
    void Clear_Battle()
    {
        foreach (var item in All_Battle_Obj)
        {
            item.SetActive(false);
        }
        CardPanel.SetActive(false);
    }
}