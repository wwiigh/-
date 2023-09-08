using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Resolution : MonoBehaviour
{
    public List<ResolutionInfo> resolution = new List<ResolutionInfo>();
    public TMP_Text resolutionText;
    int nowSelect;
    public void Apply()
    {
        Screen.SetResolution(resolution[nowSelect].horizontal,resolution[nowSelect].vertical,FullScreenMode.FullScreenWindow);
    }
    public void Left()
    {
        nowSelect -= 1;
        if(nowSelect <= 0)nowSelect = 0;
        resolutionText.text = resolution[nowSelect].horizontal.ToString() + "X" + resolution[nowSelect].vertical.ToString();
    }
    public void Right()
    {
        nowSelect += 1;
        if(nowSelect >= resolution.Count-1)nowSelect = resolution.Count-1;
        resolutionText.text = resolution[nowSelect].horizontal.ToString() + "X" + resolution[nowSelect].vertical.ToString();
    }
    void OnEnable()
    {
        bool find = false;
        for(int i=0;i<resolution.Count;i++)
        {
            if(resolution[i].horizontal == Screen.width && resolution[i].vertical == Screen.height)
            {
                find = true;
                nowSelect = i;
                break;
            }
        }
        if(find==false)
        {
            ResolutionInfo resolutionInfo = new ResolutionInfo(){
                horizontal = Screen.width,
                vertical = Screen.height
            };
            resolution.Add(resolutionInfo);
            nowSelect = resolution.Count-1;
        }
        resolutionText.text = resolution[nowSelect].horizontal.ToString() + "X" + resolution[nowSelect].vertical.ToString();
    }
}
[System.Serializable]
public class ResolutionInfo
{
    public int horizontal,vertical;
}
