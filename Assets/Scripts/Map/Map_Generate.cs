using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("保留給點的顯示,如戰鬥,事件的ui顯示等,尚未使用")]
    public Color[] colors;
    private List<Node> nodes = new List<Node>();
    private List<int> node_arr = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        Generate_Map();
    }

    void Generate_Map()
    {
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
        Generate_V2();
        
        Generate_reset();
        //將被標為戰鬥,事件的點更新，更新圖層
        Show_status();
    }
    // Update is called once per frame
    void Update()
    {
        
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
            print(need_event);
            print(need_fight);
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
            new_space_x = new_space_x / (valid_num - 1);
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
                    print(x);
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
                        break;
                    }
                    else probability-=different_type[i].chance;
                }
            }
        }
    }
}

