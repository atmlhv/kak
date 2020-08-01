using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimeItem : SideBarItem
{
    [SerializeField]
    Text name;

    [SerializeField]
    Text time;

    public void Initialize(float t_time, string t_lapName, Vector2 t_position, Vector2 t_sizeDelta)
    {
        transform.localPosition = t_position;
        baseImage.rectTransform.sizeDelta = t_sizeDelta;

        time.text = Utility.SecondToText(t_time, false);
        name.text = t_lapName;
    }

    public void Highlight(bool IsOnHighlight)
    {
        if (IsOnHighlight)
        {
            name.color = Color.yellow;
            time.color = Color.yellow;
        }
        else
        {
            name.color = Color.white;
            time.color = Color.white;
        }
    }
}
