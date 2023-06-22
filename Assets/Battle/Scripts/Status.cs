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


        summoned,
        accumulation,
        power_compete,
        damage_adjust,
        observe
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
                
            case status.summoned:
                return "召喚物";
            case status.accumulation:
                return "蓄能";
            case status.power_compete:
                return "較勁";
            case status.damage_adjust:
                return "傷害變動";
            case status.observe:
                return "觀察";
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
                
            case status.burn:
                return "回合開始時失去<color=red>" + level.ToString() + "</color>點生命，隨後層數減半";
            case status.weak:
                return "造成的傷害<color=red>-25%</color>，持續<color=red>" + level.ToString() + "</color>回合";
            case status.bleed:
                return "因攻擊失去生命時，失去<color=red>2</color>點生命，還可觸發<color=red>" + level.ToString() + "</color>次";
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
                
            case status.summoned:
                return "被召喚出的生物。不可解除";
            case status.accumulation:
                return "已積蓄<color=green>" + level.ToString() + "</color>點能量，到達一定層數可能會有特殊效果";
            case status.power_compete:
                return "每次受到生命值傷害時減少相應層數，層數因傷害降至0時獲得<color=red>2</color>層易傷";
            case status.damage_adjust:
                return "造成的傷害" + ( (level > 0)? "<color=green>+":"<color=red>") + level.ToString() + "%</color>";
            case status.observe:
                return "根據玩家當回合打出的第一張牌決定行動";
            default:
                return "Unknown";
        }
    }

    public static bool IsCountable(status _status){
        if (_status == status.hard_shell || _status == status.doom || _status == status.symbioticA || _status == status.symbioticB ||
            _status == status.absorb || _status == status.auto_guard || _status == status.energy_absorb || _status == status.fluid ||
            _status == status.blink || _status == status.summoned || _status == status.observe)
            return false;
        else return true;
    }

    public static bool DecreaseOnTurnEnd(status _status){
        if (_status == status.taunt || _status == status.prepare || _status == status.weak || _status == status.frail ||
            _status == status.vulnerable || _status == status.dream || _status == status.information_erase || _status == status.swallow )
            return true;
        else return false;
    }

    public Sprite GetImage(status _status){
        return image[(int) _status];
    }
}
