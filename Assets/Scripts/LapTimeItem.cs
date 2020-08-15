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

    UICorrecting[] m_uICorrectings;

    bool m_IsFocused = false;

    public void Initialize(float t_time, string t_lapName, Vector2 t_position, Vector2 t_sizeDelta)
    {
        baseImage.rectTransform.sizeDelta = t_sizeDelta;
        GetComponent<RectTransform>().anchoredPosition = t_position;

        time.text = Utility.SecondToText(t_time, false);
        name.text = t_lapName;

        m_uICorrectings = GetComponentsInChildren<UICorrecting>();
        for(int i = 0; i < m_uICorrectings.Length; i++)
        {
            m_uICorrectings[i].Initialize();
        }
    }

    public void UnFocus(RectTransform parentRect)
    {
        m_IsFocused = false;
        UnsetScaler(parentRect);
    }

    public void Focus(RectTransform parentRect, bool IsTimeStarted = true)
    {
        m_IsFocused = true;
        transform.parent = parentRect;
        for(int i = 0; i < m_uICorrectings.Length; i++)
        {
            m_uICorrectings[i].StartCorrecting(parentRect);
        }
    }

    public void NextFocus(RectTransform parentRect)
    {
        transform.parent = parentRect;
        for (int i = 0; i < m_uICorrectings.Length; i++)
        {
            m_uICorrectings[i].StartCorrecting(parentRect);
        }
    }

    public void UnsetScaler(RectTransform parentRect)
    {
        transform.parent = parentRect;
        for (int i = 0; i < m_uICorrectings.Length; i++)
        {
            m_uICorrectings[i].StopCorrecting();
        }
    }

    public void StartClock()
    {

    }


}
