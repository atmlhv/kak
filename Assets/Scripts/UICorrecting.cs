using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICorrecting : MonoBehaviour
{
    Vector2 m_initialPosition;
    Vector2 m_initialScale;
    RectTransform m_rectTransform;

    Transform m_followTransform;

    bool IsCorrecting = false;

    public void Initialize()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_initialPosition = m_rectTransform.anchoredPosition;
        m_initialScale = m_rectTransform.localScale;
    }

    public void StartCorrecting(RectTransform followTransform)
    {
        m_followTransform = followTransform;
        IsCorrecting = true;
    }

    public void StopCorrecting()
    {
        IsCorrecting = false;
    }

    private void Update()
    {
        if (IsCorrecting)
        {
            m_rectTransform.anchoredPosition = m_initialPosition / m_followTransform.localScale;
            m_rectTransform.localScale = m_initialScale / m_followTransform.localScale;
        }
    }

}
