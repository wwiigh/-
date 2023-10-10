using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_item3 : MonoBehaviour
{
        public GameObject obj1, obj2, obj3;
    // public UI_Show_Text[] Cardinfo, Iteminfo, Equiinfo, Relicinfo;
    // public Sprite cardIMG, itemIMG, equiIMG, relicIMG;
    public get_booty get_Booty;
    Vector2 MousePos;
    Vector2 obj1Pos, obj2Pos, obj3Pos;
    float ItemWidth;
    float ItemHight;
    void Start()
    {
        
    }
        GameObject description;
    void OnEnable()
    {
        obj1Pos = obj1.GetComponent<Transform>().position;
        obj2Pos = obj2.GetComponent<Transform>().position;
        obj3Pos = obj3.GetComponent<Transform>().position;
        ItemWidth = obj1.GetComponent<RectTransform>().rect.width;
        ItemHight = obj1.GetComponent<RectTransform>().rect.height;
        obj1Pos = Camera.main.WorldToScreenPoint(obj1Pos);
        obj2Pos = Camera.main.WorldToScreenPoint(obj2Pos);
        obj3Pos = Camera.main.WorldToScreenPoint(obj3Pos);
        obj1Pos = new Vector2(obj1Pos.x - ItemWidth/2, obj1Pos.y - ItemWidth/2);
        obj2Pos = new Vector2(obj2Pos.x - ItemWidth/2, obj2Pos.y - ItemWidth/2);
        obj3Pos = new Vector2(obj3Pos.x - ItemWidth/2, obj3Pos.y - ItemWidth/2);


    }
    // Update is called once per frame
    Vector2 LastMousePos;
    string DescriptionBox_name = "err", DescriptionBox_description = "err";
    void Update()
    {
        MousePos = Input.mousePosition;
        int WhereMouse = WhereIsMouse(MousePos);
        // Debug.Log("show:"+WhereMouse);
        if(WhereMouse > 0 && MousePos != LastMousePos){
            Destroy(description);
            switch(WhereMouse){
                case 1:
                    card_name(get_Booty.award_card[0].card_id);
                    break;
                case 2:
                    card_name(get_Booty.award_card[1].card_id);
                    break;
                case 3:
                    card_name(get_Booty.award_card[2].card_id);       
                    break;
            }
            description = DescriptionBox.Show(DescriptionBox_name, DescriptionBox_description);
            GameObject T = GameObject.FindWithTag("treasure");
            T = T.transform.GetChild(0).gameObject;
            description.transform.parent = T.transform;
            LastMousePos = MousePos;
        }
        else if(WhereIsMouse(MousePos) == 0){
            Destroy(description);
        }
    }
    int WhereIsMouse(Vector2 Mouse){
        // Debug.Log(Mouse.x + "and" + obj1Pos.x  + "ans");
        if(Mouse.x - obj1Pos.x < ItemWidth && Mouse.x - obj1Pos.x > 0 && Mouse.y - obj1Pos.y < ItemHight && Mouse.y - obj1Pos.y > 0){
            // Debug.Log(CardObj.name);
            // Debug.Log("in card");
            return 1;
        }
        if(Mouse.x - obj2Pos.x < ItemWidth && Mouse.x - obj2Pos.x > 0 && Mouse.y - obj2Pos.y < ItemHight && Mouse.y - obj2Pos.y > 0){
            // Debug.Log("in item");
            return 2;
        }
        if(Mouse.x - obj3Pos.x < ItemWidth && Mouse.x - obj3Pos.x > 0 && Mouse.y - obj3Pos.y < ItemHight && Mouse.y - obj3Pos.y > 0){
            // Debug.Log("in equi");
            return 3;
        }
        
        return 0;
    }
    void card_name(int id){
        string ret = "error";
        string ret2 = "error";
        switch(id){
            case 1:
                ret = "掩護";
                ret2 = "丟棄1張牌，獲得3(5)點護甲，\n從抽牌堆檢索1張耗能為0的卡片";
                break;
            case 2:
                ret = "延燒";
                ret2 = "翻倍目標的燃燒層數";
                break;
            case 3:
                ret = "幻痛";
                ret2 = "立即觸發一次目標的燃燒傷害";
                break;
            case 4:
                ret = "警戒";
                ret2 = "抽2(3)張牌，選擇1張牌，給予保留";
                break;
            case 5:
                ret = "萬全準備";
                ret2 = "下3(4)個回合結束時，\n選擇1張牌，在打出前給予保留";
                break;
            case 6:
                ret = "固守";
                ret2 = "丟棄1張牌，獲得3(5)點護甲，\n從抽牌堆檢索1張耗能為0的卡片";
                break;
            case 7:
                ret = "輪轉";
                ret2 = "本回合內每次獲得護甲時抽1張牌，\n可觸發2(3)次";
                break;
            case 8:
                ret = "臨機應變";
                ret2 = "若目標的意圖是攻擊，\n將1張隨機技能牌加入手中，\n否則將1張隨機攻擊牌加入手中";
                break;
            case 9:
                ret = "乘勝追擊";
                ret2 = "造成6(8)點傷害，若本回合\n已打過攻擊牌與技能牌，\n獲得1能量並抽1張牌";
                break;
            case 10:
                ret = "無影手";
                ret2 = "每丟棄1張牌，\n對隨機敵人造成5(8)點傷害，移除";
                break;
            case 11:
                ret = "備用武器";
                ret2 = "每回合第1次丟棄牌時，抽1張牌，移除";
                break;
            case 12:
                ret = "反彈";
                ret2 = "造成7(10)點傷害，\n下張打出的牌(「反彈」除外)進入抽牌堆而不是棄牌堆";
                break;
            case 13:
                ret = "雙重斬擊";
                ret2 = "造成4(5)點傷害2次";
                break;
            case 14:
                ret = "先聲奪人";
                ret2 = "獲得3點護甲，\n若此牌是本回合第1張打出的牌，\n獲得1能量並抽1(2)張牌";
                break;
            case 15:
                ret = "養精蓄銳";
                ret2 = "獲得等同當前回合數的能量，移除";
                break;
            case 16:
                ret = "還擊";
                ret2 = "造成12(16)點傷害，\n若上回合有受到生命值傷害，獲得1能量並抽1張牌";
                break;
            case 17:
                ret = "反擊架勢";
                ret2 = "護甲被突破時\n對攻擊者造成9(12)點傷害及給予1層易傷，移除";
                break;
            case 18:
                ret = "重擊";
                ret2 = "造成30(40)點傷害，\n選擇最多3張牌在打出前給予保留";
                break;
            case 19:
                ret = "裝甲強化";
                ret2 = "獲得18(20)點護甲，\n若原本沒有護甲，\n額外獲得8(12)點臨時護甲";
                break;
            case 20:
                ret = "爆發";
                ret2 = "當前能量翻倍，移除";
                break;
            case 21:
                ret = "暖身";
                ret2 = "獲得2點臨時力量(與2點臨時敏捷)";
                break;
            case 22:
                ret = "反射斬擊";
                ret2 = "對隨機敵人造成10(13)點傷害；\n被移除時，\n對血量最低的敵人造成10(13)點傷害";
                break;
            case 23:
                ret = "佯攻";
                ret2 = "造成1點傷害，獲得5點臨時護甲\n(與2點護甲)，獲得1點能量；\n被移除時，將一張此牌的複製加入手牌";
                break;
            case 24:
                ret = "羅剎";
                ret2 = "對所有敵人造成3(4)點傷害2次；\n被移除時獲得2(3)點力量";
                break;
            case 25:
                ret = "帽子戲法";
                ret2 = "移除一張手牌，將其複製加入手牌。\n移除。(保留)";
                break;
            case 26:
                ret = "弱點鎖定";
                ret2 = "下回合造成的傷害翻倍";
                break;
            case 27:
                ret = "灼地";
                ret2 = "對所有敵人造成3點傷害，\n並給予5(7)層燃燒";
                break;
            case 28:
                ret = "急中生智";
                ret2 = "抽3(4)張牌，隨機丟棄其中一張";
                break;
            case 29:
                ret = "第六感";
                ret2 = "選擇一個消費，抽1張牌。\n獨一：若抽到的牌消費與你選擇的相同\n，獲得1點能量並抽1(2)張牌";
                break;
            case 30:
                ret = "預謀";
                ret2 = "造成7(10)點傷害，抽1張牌，\n選1張牌放回抽牌堆最上方";
                break;
            case 31:
                ret = "烈焰護身";
                ret2 = "獲得10(13)點臨時護甲，\n本回合每次受到攻擊時\n，給予攻擊者4(5)層燃燒";
                break;
            case 32:
                ret = "火焰附魔";
                ret2 = "每次攻擊時額外給予2(3)層燃燒。\n移除。";
                break;
            case 33:
                ret = "縝密計算";
                ret2 = "隨機移除一張手中\n耗能最高的卡牌並抽X張牌\n，X為該牌的耗能";
                break;
            case 34:
                ret = "以牙還牙";
                ret2 = "保留，造成9點傷害。\n這張牌在手中時，每次受到X點傷害，\n基礎傷害就增加(2)X，打出後重置";
                break;
            case 35:
                ret = "血償";
                ret2 = "造成8點傷害，恢復4(7)%血量，移除";
                break;
            case 36:
                ret = "最後手段";
                ret2 = "造成7(9)點傷害，\n若這張牌在手牌的最左邊，\n改為造成21(27)點傷害";
                break;
            case 37:
                ret = "影子分身";
                ret2 = "打出卡牌時(「影子分身」除外)，\n將一張消費為0、基礎攻擊、護甲減半、\n帶有移除和消逝的複製加入手中。可觸發3次。\n因此能力產生的卡牌不會觸發此效果";
                break;
            case 38:
                ret = "專注";
                ret2 = "獲得5點護甲，從抽牌堆檢索1張牌，\n使其獲得保留、消費+2(1)與回合結束時消費-1效果";
                break;
            case 39:
                ret = "招架";
                ret2 = "獲得6點護甲，\n若此牌是本回合第1張打出的牌\n，獲得6(9)點臨時護甲";
                break;
            case 40:
                ret = "引爆";
                ret2 = "給予所有敵人5層燃燒，\n燃燒層數為40(30)以上的敵人\n立即失去等同3倍層數的生命\n，隨後清空層數";
                break;
            case 41:
                ret = "Jackpot!";
                ret2 = "抽3張牌，若抽到的所有牌消費都相同\n，對所有敵人造成13(17)點傷害";
                break;
            case 42:
                ret = "迴旋踢";
                ret2 = "造成6(8)點傷害1次。\n每當這張牌從棄牌堆加入手牌\n，基礎傷害次數+1";
                break;
            case 43:
                ret = "直拳";
                ret2 = "造成10點傷害，給予1(2)層虛弱。\n若上一張打出的牌消費為0，獲得1點能量";
                break;
            case 44:
                ret = "上鉤拳";
                ret2 = "造成10點傷害，給予1(2)層易傷。\n若上一張打出的牌消費為0，獲得1點能量";
                break;
            case 45:
                ret = "突進";
                ret2 = "獲得3(5)點臨時護甲，\n從直拳與上鉤拳中選擇一張加入手牌\n，並給予消逝與移除";
                break;
            case 46:
                ret = "劍氣";
                ret2 = "對所有敵人造成5點傷害。\n獨一：將一張「納刀(+)」加入手牌，\n並給予移除。若本回合打出過「劍氣」\n，改為造成2次傷害";
                break;
            case 47:
                ret = "拔刀斬";
                ret2 = "造成11點傷害。\n力量在這張牌上有3倍效果(，臨時力量有5倍效果)。\n若本回合尚未打出過攻擊牌，獲得1點能量";
                break;
            case 48:
                ret = "模仿";
                ret2 = "丟棄1張牌，獲得3(5)點護甲\n，從抽牌堆檢索1張耗能為0的卡片";
                break;
            case 49:
                ret = "殘心";
                ret2 = "臨時力量與臨時敏捷\n在回合結束時改為減少一半的層數\n，而不是全部層數。移除";
                break;
            case 50:
                ret = "玄武架勢";
                ret2 = "回合開始時，若上回合沒有打出過攻擊牌\n，獲得5點臨時力量。移除";
                break;
            case 51:
                ret = "青龍架勢";
                ret2 = "回合開始時，\n若上回合沒有打出過技能牌，\n獲得5點臨時敏捷。移除";
                break;
            case 52:
                ret = "跳步";
                ret2 = "獲得5(8)點臨時護甲，\n若沒有臨時敏捷，獲得2點臨時敏捷，\n否則獲得1點敏捷";
                break;
            case 53:
                ret = "突刺";
                ret2 = "造成7(10)點傷害，\n若沒有臨時力量，獲得2點臨時力量，\n否則獲得1點力量";
                break;
            case 54:
                ret = "棄械";
                ret2 = "丟棄1張牌，\n獲得4(5)點護甲與4(6)點臨時護甲";
                break;
            case 55:
                ret = "破邪";
                ret2 = "造成6點傷害，\n若擊殺非召喚物敵人，\n恢復3(5)%理智";
                break;
            case 56:
                ret = "無念";
                ret2 = "獲得10點護甲(與4點臨時護甲)，\n恢復7%san值，立即結束回合。移除";
                break;
            case 57:
                ret = "點燃";
                ret2 = "給予一個敵人7(10)層燃燒";
                break;
            case 58:
                ret = "納刀";
                ret2 = "從棄牌堆檢索1張消費為1的攻擊牌\n(，被移除時獲得1點能量)";
                break;
            case 59:
                ret = "燕返";
                ret2 = "丟棄2(1)張牌，造成5(6)點傷害3次";
                break;                
            case 60:
                ret = "利刃";    
                ret2 = "造成7點傷害，隨後，\n若目標沒有護甲，給予1(2)層易傷";  
                break;
        }
        DescriptionBox_name = ret;
        DescriptionBox_description = ret2;
        // return ret;
    }
    void item_name(int id){
        string ret = "error";
        string ret2 = "error";
        switch(id){
            case 101:
                ret = "史萊姆泥";
                ret2 = "給予全部敵人2層虛弱";
                break;
            case 102:
                ret = "斷裂的長矛";
                ret2 = "下一次傷害+3";
                break;
            case 103:
                ret = "透明的絲捐";
                ret2 = "下次受到傷害免疫";
                break;
            case 104:
                ret = "獠牙";
                ret2 = "吸取全部敵人10%血量";
                break;
            case 105:
                ret = "蟹甲";
                ret2 = "獲得10點護甲";
                break;
            case 106:
                ret = "普通石塊";
                ret2 = "造成全部敵人10點傷害";
                break;
            case 107:
                ret = "舍利子";
                ret2 = "回復15點血";
                break;
            case 108:
                ret = "史萊姆球";
                ret2 = "給予全部敵人5層虛弱";
                break;
            case 109:
                ret = "飛矢";
                ret2 = "單體敵人少於15%血量時，直接斬殺，但不對BOSS生效";
                break;
            case 110:
                ret = "誘人的肉";
                ret2 = "回復10血量 -1SAN";
                break;
            case 111:
                ret = "病毒";
                ret2 = "敵人每回生命 -4%";
                break;
            case 112:
                ret = "毒液";
                ret2 = "敵人生命-10%";
                break;
            case 113:
                ret = "有生機的枯木";
                ret2 = "使敵人增益效果消失";
                break;
            case 114:
                ret = "火";
                ret2 = "使全部敵人獲得10層燃燒";
                break;
            case 115:
                ret = "樹皮";
                ret2 = "減少全部敵人35%傷害";
                break;
            case 116:
                ret = "絲滑如娟布的暗影";
                ret2 = "有機率閃避來自所有敵人傷害 35%";
                break;
            case 117:
                ret = "祈禱聲";
                ret2 = "使全部敵人下次行動失效";
                break;
            case 118:
                ret = "骨灰";
                ret2 = "回復15點血";
                break;
            case 119:
                ret = "破舊的紅布";
                ret2 = "全部敵人下一次無法攻擊";
                break;
            case 120:
                ret = "肉塊";
                ret2 = "補15血 -15 San";
                break;
            case 201:
                ret = "生命藥水";
                ret2 = "補15血量";
                break;
            case 202:
                ret = "力量藥水";
                ret2 = "獲得3點力量";
                break;
            case 203:
                ret = "理智藥水";
                ret2 = "回復 15 SAN ";
                break;
            case 204:
                ret = "飛劍";
                ret2 = "對全部敵人造成15%傷害";
                break;
            case 205:
                ret = "詛咒";
                ret2 = "全部敵人每回合會扣5%生命";
                break;
            case 206:
                ret = "瘋癲藥水";
                ret2 = "讓全部敵人有9%發不出招 ";
                break;
            case 207:
                ret = "薩滿超載";
                ret2 = "血量上限減少10點，當前所有手牌消費-1";
                break;
            case 208:
                ret = "腦子";
                ret2 = "隨機補0~40%血量，SAN-10";
                break;
            case 209:
                ret = "另一個腦子";
                ret2 = "隨機增加0~40%傷害，SAN-10";
                break;
            case 210:
                ret = "左眼";
                ret2 = "看破對手，敵人傷害-0~40%，SAN-10";
                break;
            case 211:
                ret = "右眼";
                ret2 = "回復SAN0~40%，血量-10";
                break;
            
        }
        DescriptionBox_name = ret;
        DescriptionBox_description = ret2;
        
    }
    void equi_name(int id){
        string ret = "error";
        string ret2 = "error";
        switch(id){
            case 1:
                ret = "虛無之刃";
                ret2 = "下一張打出的牌消費變為0，基礎傷害、護甲變為1，打出後移除";
                break;
            case 2:
                ret = "殞命之旅";
                ret2 = "受到等同最大HP值5%的生命值傷害，\n獲得等同受到傷害2倍的護甲。戰鬥結束時恢復因此效果失去的生命值。";
                break;
            case 3:
                ret = "銀色手鐲";
                ret2 = "獲得5護甲";
                break;
            case 4:
                ret = "克圖格亞的圖騰";
                ret2 = "本回合每次攻擊額外給予1層燃燒";
                break;
            case 5:
                ret = "妖刀村正";
                ret2 = "獲得1點力量，從抽牌堆檢索一張消費為1的攻擊牌，並給予移除";
                break;
            case 6:
                ret = "俄羅斯輪盤";
                ret2 = "以(1/目前充能)機率造成40點固定傷害；\n此效果每場戰鬥只觸發1次， 觸發後充能歸0";
                break;
            case 7:
                ret = "黑貓頭顱";
                ret2 = "獲得5層力量";
                break;
            case 8:
                ret = "妖精護身符";
                ret2 = "獲得5層壁壘";
                break;
            case 9:
                ret = "幽靈護手";
                ret2 = "失去等同最大生命值20%的血量，獲得5點力量";
                break;
            case 10:
                ret = "破爛草衣";
                ret2 = "獲得3層壁壘";
                break;
            case 11:
                ret = "吸血刃";
                ret2 = "本回合造成攻擊傷害時回復等同造成傷害50%的血量";
                break;
            case 12:
                ret = "打火機";
                ret2 = "給予一個敵人5層燃燒，並立即觸發一次燃燒效果";
                break;
            case 13:
                ret = "淨化御守";
                ret2 = "隨機移除一個自身的負面狀態";
                break;
            case 14:
                ret = "鏽刀";
                ret2 = "獲得1點臨時力量";
                break;
            case 15:
                ret = "電磁砲";
                ret2 = "造成11點穿透傷害";
                break;
            case 16:
                ret = "韻律手環";
                ret2 = "獲得4點護甲";
                break;
            case 17:
                ret = "動力裝甲";
                ret2 = "獲得1點能量";
                break;
            case 18:
                ret = "疾行靴";
                ret2 = "獲得3點敏捷";
                break;
            case 19:
                ret = "流浪者(圍巾)";
                ret2 = "丟棄所有手牌，抽7張牌";
                break;
            case 20:
                ret = "苦行";
                ret2 = "消耗所有充能，造成等同充能量的穿透傷害";
                break;
            case 21:
                ret = "簡約胸章";
                ret2 = "每有一個普通裝備，獲得4點護甲";
                break;
            case 22:
                ret = "火藥(拳套)";
                ret2 = "將所有力量轉換為3倍的臨時力量";
                break;
            case 23:
                ret = "護身符";
                ret2 = "移除1張手牌";
                break;
            case 24:
                ret = "純白誓約(法杖)";
                ret2 = "移除所有手牌，每移除1張恢復2點san值";
                break;
            case 25:
                ret = "漆黑法典";
                ret2 = "san值-10，所有敵人失去10點HP";
                break;
            case 26:
                ret = "雙節棍";
                ret2 = "獲得1點力量";
                break;
            case 27:
                ret = "鋼盾";
                ret2 = "獲得1點敏捷";
                break;
            case 28:
                ret = "日冕";
                ret2 = "給予所有敵人13層燃燒。若有裝備「月影」，再給予1層虛弱";
                break;
            case 29:
                ret = "月影";
                ret2 = "所有敵人身上每有一個負面狀態，獲得5點護甲";
                break;
            case 30:
                ret = "紅寶石戒指";
                ret2 = "抽2張牌";
                break;
        }
        DescriptionBox_name = ret;
        DescriptionBox_description = ret2;
    }
    void relic_name(int id){
        string ret = "error";
        string ret2 = "error";
        switch(id){
            case 13:
                ret = "燒火棍";
                ret2 = "燃燒的傷害+3";
                break;
            case 14:
                ret = "平衡鳥";
                ret2 = "每回合打出的第一張牌若為攻擊牌，\n獲得1點臨時敏捷，否則獲得1點臨時力量";
                break;
            case 15:
                ret = "塔羅牌";
                ret2 = "每回合抽到的第一張牌若是攻擊牌，\n對所有敵人造成3點傷害，否則獲得4點護甲";
                break;
            case 16:
                ret = "渾沌物質";
                ret2 = "San值上限-10，血量上限+10，力量+2";
                break;
            case 17:
                ret = "鐵匠錘";
                ret2 = "戰鬥開始時隨機升級一張卡片";
                break;
            case 18:
                ret = "鐮刀";
                ret2 = "每擊殺一名敵人，獲得1點力量";
                break;
            case 19:
                ret = "拳套";
                ret2 = "每打出兩張攻擊牌後，獲得1點力量";
                break;
            case 20:
                ret = "迴力鏢";
                ret2 = "戰鬥開始時隨機獲得一個增強，隨機獲得一個虛弱";
                break;
            case 21:
                ret = "香爐";
                ret2 = "獲得2點臨時力量(與2點臨時敏捷)";
                break;
            case 22:
                ret = "南瓜頭";
                ret2 = "敵人失手的機率10%";
                break;
            case 23:
                ret = "限制器";
                ret2 = "2費以上的卡變成2費";
                break;
            case 24:
                ret = "氧化劑";
                ret2 = "當使用有燃燒的卡時，多加5層燃燒";
                break;
            case 25:
                ret = "物理聖杖";
                ret2 = "每打出1張攻擊牌，所有敵人失去3點HP";
                break;
            case 26:
                ret = "金幣";
                ret2 = "掉落的金錢大幅增加25%";
                break;
            case 27:
                ret = "章魚雕像";
                ret2 = "戰鬥開始時獲得2點敏捷";
                break;
            case 28:
                ret = "正20面骰";
                ret2 = "每回合攻擊牌的傷害隨機-9到+10";
                break;
            case 29:
                ret = "古神的褻語";
                ret2 = "戰鬥開始時，所有敵人失去3點力量";
                break;
            case 30:
                ret = "哈絲塔的祝福";
                ret2 = "San值上限+10";
                break;
        }
        DescriptionBox_name = ret;
        DescriptionBox_description = ret2;
    }
}
