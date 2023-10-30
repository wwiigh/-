using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] Sprite[] image;
    public enum status{
        strength,
        temporary_strength,
        dexterity,
        temporary_dexterity,
        rampart,
        taunt,
        spike,
        invincible,
        hard_shell,
        rock_solid,
        doom,
        symbioticA,
        symbioticB,
        rage,
        grief,
        absorb,
        grudge,
        auto_guard,
        energy_absorb,
        fluid,
        blink,
        oppress,
        prepare,
        fortify,
        fast_hand,
        second_weapon,
        bounce_back,
        counter,
        lock_on_prepare,
        lock_on,
        fire_armor,
        fire_enchantment,
        doppelganger,
        imitate,
        remnant,
        turtle_stance,
        dragon_stance,
        explosive_force,
        evade,
        blood_drain,


        burn,
        weak,
        bleed,
        frail,
        vulnerable,
        mental_weak,
        dream,
        draw_less,
        information_erase,
        swallow,
        compress,
        decay,
        immobile,
        unfortune,


        summoned,
        accumulation,
        power_compete,
        damage_adjust,
        dice20,
        void_sword
    }
    public static string GetName(status _status){
        switch(_status){
            case status.strength:
                return "力量";
            case status.temporary_strength:
                return "臨時力量";
            case status.dexterity:
                return "敏捷";
            case status.temporary_dexterity:
                return "臨時敏捷";
            case status.rampart:
                return "壁壘";
            case status.taunt:
                return "嘲諷";
            case status.spike:
                return "荊棘";
            case status.invincible:
                return "無敵";
            case status.hard_shell:
                return "堅硬外殼";
            case status.rock_solid:
                return "堅硬";
            case status.doom:
                return "厄運";
            case status.symbioticA:
                return "共生";
            case status.symbioticB:
                return "共生";
            case status.rage:
                return "憤怒";
            case status.grief:
                return "哀痛";
            case status.absorb:
                return "吸收";
            case status.grudge:
                return "怨念";
            case status.auto_guard:
                return "自動護衛";
            case status.energy_absorb:
                return "能量吸收";
            case status.fluid:
                return "流體";
            case status.blink:
                return "眨眼";
            case status.oppress:
                return "壓迫";
            case status.prepare:
                return "萬全準備";
            case status.fortify:
                return "固守";
            case status.fast_hand:
                return "無影手";
            case status.second_weapon:
                return "備用武器";
            case status.bounce_back:
                return "反彈";
            case status.counter:
                return "反擊架勢";
            case status.lock_on_prepare:
                return "弱點鎖定-預備";
            case status.lock_on:
                return "弱點鎖定";
            case status.fire_armor:
                return "烈焰護身";
            case status.fire_enchantment:
                return "火焰附魔";
            case status.doppelganger:
                return "影子分身";
            case status.imitate:
                return "模仿";
            case status.remnant:
                return "殘心";
            case status.turtle_stance:
                return "玄武架勢";
            case status.dragon_stance:
                return "青龍架勢";
            case status.explosive_force:
                return "爆發力";
            case status.evade:
                return "迴避";
            case status.blood_drain:
                return "吸血";

            case status.burn:
                return "燃燒";
            case status.weak:
                return "虛弱";
            case status.bleed:
                return "流血";
            case status.frail:
                return "脆弱";
            case status.vulnerable:
                return "易傷";
            case status.mental_weak:
                return "精神衰弱";
            case status.dream:
                return "夢境";
            case status.draw_less:
                return "抽牌減少";
            case status.information_erase:
                return "情報抹消";
            case status.swallow:
                return "吞噬";
            case status.compress:
                return "空間壓縮";
            case status.decay:
                return "腐朽";
            case status.immobile:
                return "無法行動";
            case status.unfortune:
                return "不幸";
                
            case status.summoned:
                return "召喚物";
            case status.accumulation:
                return "蓄能";
            case status.power_compete:
                return "較勁";
            case status.damage_adjust:
                return "傷害變動";
            case status.dice20:
                return "正20面骰";
            case status.void_sword:
                return "虛無之刃";
            default:
                return "Unknown";
        }
    }

    public static string GetDescription(status _status, int level){
        switch(_status){
            case status.strength:
                return "造成的攻擊傷害" + ( (level > 0)? "<color=green>+":"<color=red>") + level.ToString() + "</color>";
            case status.temporary_strength:
                return "造成的攻擊傷害" + ( (level > 0)? "<color=green>+":"<color=red>") + level.ToString() + "</color>，持續1回合";
            case status.dexterity:
                return "從卡牌中獲得的護甲" + ( (level > 0)? "<color=green>+":"<color=red>") + level.ToString() + "</color>";
            case status.temporary_dexterity:
                return "從卡牌中獲得的護甲" + ( (level > 0)? "<color=green>+":"<color=red>") + level.ToString() + "</color>，持續1回合";
            case status.rampart:
                return "每回合結束時獲得<color=green>" + level.ToString() + "</color>點臨時護甲";
            case status.taunt:
                return "必須優先選擇此敵人作為攻擊目標，持續<color=red>" + level.ToString() + "</color>回合";
            case status.spike:
                return "受到攻擊時返還<color=green>" + level.ToString() + "</color>點傷害";
            case status.invincible:
                return "免疫下<color=green>" + level.ToString() + "</color>次受到的傷害";
            case status.hard_shell:
                return "每次攻擊最多使此目標失去1點生命";
            case status.rock_solid:
                return "受到的生命值傷害<color=green>-" + level.ToString() + "</color>";
            case status.doom:
                return "受到生命值傷害時，對攻擊者造成一半的傷害";
            case status.symbioticA:
                return "在奇數回合無敵，友方死亡時失去此效果";
            case status.symbioticB:
                return "在偶數回合無敵，友方死亡時失去此效果";
            case status.rage:
                return "友方死亡時，獲得<color=green>" + level.ToString() + "</color>點力量";
            case status.grief:
                return "友方死亡時，獲得<color=green>" + level.ToString() + "</color>點護甲";
            case status.absorb:
                return "友方死亡時，恢復<color=green>10</color>點生命，獲得<color=green>3</color>點力量";
            case status.grudge:
                return "死亡時給予所有友方<color=green>" + level.ToString() + "</color>點力量";
            case status.auto_guard:
                return "回合開始時，若上回合沒有受到過傷害，獲得<color=green>1</color>層嘲諷";
            case status.energy_absorb:
                return "每次失去護甲時，獲得等量層數的蓄能";
            case status.fluid:
                return "每次受到生命值傷害時，獲得等量的臨時護甲";
            case status.blink:
                return "每次打出消費為3以上的卡牌時，將其無效並移除";
            case status.oppress:
                return "回合結束時對玩家造成<color=red>" + level.ToString() + "</color>點傷害";
            case status.prepare:
                return "回合結束時選擇1張牌，在打出前給予保留，持續<color=green>" + level.ToString() + "</color>回合";
            case status.fortify:
                return "本回合內每次獲得護甲時抽1張牌，還可觸發<color=green>" + level.ToString() + "</color>次";
            case status.fast_hand:
                return "每丟棄1張牌就對隨機敵人造成<color=green>" + level.ToString() + "</color>點傷害";
            case status.second_weapon:
                return "每回合第1次丟棄卡牌時，抽<color=green>" + level.ToString() + "</color>張牌";
            case status.bounce_back:
                return "下<color=green>" + level.ToString() + "</color>張打出的牌進入抽牌堆而不是棄牌堆";
            case status.counter:
                return "護甲被突破時對攻擊者造成<color=green>" + level.ToString() + "</color>點傷害並給予<color=green>1</color>層易傷";
            case status.lock_on_prepare:
                return "下回合造成的傷害翻倍";
            case status.lock_on:
                return "造成的傷害翻倍";
            case status.fire_armor:
                return "本回合每次受到攻擊時給予攻擊者<color=green>" + level.ToString() + "</color>層燃燒";
            case status.fire_enchantment:
                return "每次攻擊額外給予<color=green>" + level.ToString() + "</color>層燃燒";
            case status.doppelganger:
                return "打出卡牌時(「影子分身」除外)，\n將一張消費為0、基礎攻擊、護甲減半、帶有移除和消逝的複製加入手中\n還可觸發<color=green>" + level.ToString() + "</color>次。因此能力產生的卡牌不會觸發此效果";
            case status.imitate:
                return "將下<color=green>" + level.ToString() + "</color>張打出的牌複製一張(「模仿」除外)";
            case status.remnant:
                return "臨時力量與臨時敏捷在回合結束時改為減少一半的層數，而不是全部層數";
            case status.turtle_stance:
                return "回合開始時，若上回合沒有打出過攻擊牌，獲得<color=green>" + level.ToString() + "</color>點臨時力量";
            case status.dragon_stance:
                return "回合開始時，若上回合沒有打出過技能牌，獲得<color=green>" + level.ToString() + "</color>點臨時敏捷";
            case status.explosive_force:
                return "下一次造成的傷害<color=green>+" + level.ToString() + "</color>";
            case status.evade:
                return "有<color=green>" + level.ToString() + "%</color>機率無視受到的傷害";
            case status.blood_drain:
                return "造成攻擊傷害時回復等同造成傷害<color=green>50%</color>的生命值，還可觸發<color=green>" + level.ToString() + "</color>次";

            case status.burn:
                return "回合開始時失去<color=red>" + level.ToString() + "</color>點生命，隨後層數減半";
            case status.weak:
                return "造成的傷害<color=red>-25%</color>，持續<color=red>" + level.ToString() + "</color>回合";
            case status.bleed:
                return "回合結束時失去<color=red>1</color>點生命，持續<color=red>" + level.ToString() + "</color>回合";
                // return "因攻擊失去生命時，失去<color=red>2</color>點生命，還可觸發<color=red>" + level.ToString() + "</color>次";
            case status.frail:
                return "從卡牌獲得的護甲<color=red>-25%</color>，持續<color=red>" + level.ToString() + "</color>回合";
            case status.vulnerable:
                return "受到的攻擊傷害<color=red>+25%</color>，持續<color=red>" + level.ToString() + "</color>回合";
            case status.mental_weak:
                return "每回合結束時，理智<color=red>-" + level.ToString() + "</color>";
            case status.dream:
                return "你無法看到卡牌的耗能，持續<color=red>" + level.ToString() + "</color>回合";
            case status.draw_less:
                return "下回合少抽<color=red>" + level.ToString() + "</color>張牌";
            case status.information_erase:
                return "回合結束時，將一張隨機手牌變成「空白」，持續<color=red>" + level.ToString() + "</color>回合";
            case status.swallow:
                return "回合結束時，隨機移除一張手牌，持續<color=red>" + level.ToString() + "</color>回合";
            case status.compress:
                return "下回合少獲得<color=red>" + level.ToString() + "</color>點能量";
            case status.decay:
                return "回合開始時，失去等同於最大生命值<color=red>" + level.ToString() + "%</color>的生命";
            case status.immobile:
                return "下<color=red>" + level.ToString() + "</color>次行動失效";
            case status.unfortune:
                return "每回合有<color=red>" + level.ToString() + "%</color>機率行動失效";
                
            case status.summoned:
                return "被召喚出的生物。不可解除";
            case status.accumulation:
                return "已積蓄<color=green>" + level.ToString() + "</color>點能量，到達一定層數可能會有特殊效果";
            case status.power_compete:
                return "每次受到生命值傷害時減少相應層數，\n層數因傷害降至0時獲得<color=red>2</color>層易傷";
            case status.damage_adjust:
                return "造成的攻擊傷害" + ( (level > 0)? "<color=green>+":"<color=red>") + level.ToString() + "%</color>";
            case status.dice20:
                return "造成的攻擊傷害" + ( (level > 0)? "<color=green>+":"<color=red>") + level.ToString() + "</color>";
            case status.void_sword:
                return "下<color=green>" + level.ToString() + "</color>張打出的牌消費變為0，基礎傷害、護甲變為1，打出後移除";
            default:
                return "Unknown";
        }
    }

    public static bool IsCountable(status _status){
        switch(_status){
            case status.hard_shell:
            case status.doom:
            case status.symbioticA:
            case status.symbioticB:
            case status.absorb:
            case status.auto_guard:
            case status.energy_absorb:
            case status.fluid:
            case status.blink:
            case status.summoned:
            case status.lock_on_prepare:
            case status.lock_on:
            case status.remnant:
                return false;
            default:
                return true;
        }
    }

    public static bool IsNegative(status _status){
        switch(_status){
            case status.burn:
            case status.weak:
            case status.bleed:
            case status.frail:
            case status.vulnerable:
            case status.mental_weak:
            case status.dream:
            case status.draw_less:
            case status.information_erase:
            case status.swallow:
            case status.compress:
                return true;
            default:
                return false;
        }
    }

    public static bool DecreaseOnTurnEnd(status _status){
        switch(_status){
            case status.bleed:
            case status.taunt:
            case status.prepare:
            case status.weak:
            case status.frail:
            case status.dream:
            case status.information_erase:
            case status.swallow:
                return true;
            default:
                return false;
        }
    }

    public static bool DecreaseOnTurnStart(status _status){
        if (_status == status.vulnerable)
            return true;
        else return false;
    }

    public static bool ClearOnTurnEnd(status _status){
        switch(_status){
            case status.fortify:
            case status.lock_on:
                return true;
            default:
                return false;
        }
    }

    public static bool ClearOnTurnStart(status _status){
        switch(_status){
            case status.fire_armor:
                return true;
            default:
                return false;
        }
    }

    public Sprite GetImage(status _status){
        return image[(int) _status];
    }

    static public bool Is_SymboticA_Active(GameObject obj){
        if (obj.GetComponent<Character>().GetStatus(status.symbioticA) > 0 && BattleController.GetCurrentTurn() % 2 == 1) return true;
        return false;
    }
    static public bool Is_SymboticB_Active(GameObject obj){
        if (obj.GetComponent<Character>().GetStatus(status.symbioticB) > 0 && BattleController.GetCurrentTurn() % 2 == 0) return true;
        return false;
    }
}
