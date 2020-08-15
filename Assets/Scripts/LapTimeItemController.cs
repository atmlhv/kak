using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTimeItemController : Controller
{
    [SerializeField]
    LapTimeItem lapTimeItemPrefab;

    SideBarController sideBarController;
    TimeController timeController;

    LapDataManager.LapTimes lapTimes;

    List<LapTimeItem> lapTimeItemList;

    int m_nextLapIndex = 0;
    float m_lapTimeHeight;
    float m_focusedLapTimeHeightMultiplier = 3f;
    [SerializeField]
    RectTransform m_nextLapTimeItemScaler;
    [SerializeField]
    RectTransform m_nextNextLapTimeItemScaler;
    Animator m_animator;
    RectTransform m_rectTransform;

    bool m_IsUpdating = false;

    public const string unfocusedLapItemName = "UnfocusedLapTimeItem";
    public const string nextLapItemName = "NextLapTimeItem";
    public const string nextNextLapItemName = "NextNextLapTimeItem";

    public override void InitializeController(GameManager gameManager)
    {
        base.InitializeController(gameManager);
        sideBarController = FindObjectOfType<SideBarController>();
        timeController = FindObjectOfType<TimeController>();
    }

    public override void InitializeManagedItems()
    {
        base.InitializeManagedItems();
        
        lapTimeItemList = new List<LapTimeItem>();
        lapTimes = timeController.applicatedLapTimes;
        m_animator = GetComponent<Animator>();
        m_rectTransform = GetComponent<RectTransform>();
        

        m_lapTimeHeight = lapTimes.lapTimeList.Count + m_focusedLapTimeHeightMultiplier - 1f;

        //lapTimeItemのInitialize
        for (int i = 0; i < lapTimes.lapTimeList.Count; i++)
        {
            int heightIndex;
            if (i == 0)
            {
                heightIndex = i;
            }
            else
            {
                heightIndex = i + (int)(m_focusedLapTimeHeightMultiplier - 1);
            }


            lapTimeItemList.Add(Instantiate(lapTimeItemPrefab,transform));
            lapTimeItemList[i].Initialize(
                lapTimes.lapTimeList[i].time,
                LapDataManager.GetLapName(lapTimes.gameTitle, lapTimes.lapTimeList[i].lapID),
                AnchoredPositionInSideBar(heightIndex, m_lapTimeHeight),
                SizeDeltaInSideBar(m_lapTimeHeight)
                );
        }

        //scalerのInitialize
        m_nextLapTimeItemScaler.anchoredPosition = AnchoredPositionInSideBar(0, m_lapTimeHeight);
        m_nextLapTimeItemScaler.sizeDelta = SizeDeltaInSideBar(m_lapTimeHeight);

        m_nextNextLapTimeItemScaler.anchoredPosition = AnchoredPositionInSideBar(3, m_lapTimeHeight);
        m_nextNextLapTimeItemScaler.sizeDelta = SizeDeltaInSideBar(m_lapTimeHeight);

        lapTimeItemList[0].transform.parent = m_nextLapTimeItemScaler;

        lapTimeItemList[m_nextLapIndex].Focus(m_nextLapTimeItemScaler);
        lapTimeItemList[m_nextLapIndex+1].NextFocus(m_nextNextLapTimeItemScaler);
        
        m_animator.SetTrigger("Initialize");

    }

    Vector2 AnchoredPositionInSideBar(int heightIndex, float lapTimeHeight)
    {
        return new Vector2(0, -SideBarController.TimerHeight - heightIndex * (Screen.height - SideBarController.TimerHeight) / lapTimeHeight);
    }

    Vector2 SizeDeltaInSideBar(float lapTimeHeight)
    {
        return new Vector2(SideBarController.SideBarWidth, (Screen.height - SideBarController.TimerHeight) / lapTimeHeight);
    }

    private void Update()
    {
        if(m_nextLapIndex < timeController.nextLapIndex)
        {
            if (m_IsUpdating == false)
            {
                UpdateNextLapIndex();
            }
        }
        
    }

    private void UpdateNextLapIndex()
    {
        m_IsUpdating = true;
        m_animator.SetTrigger("UpdateLap");
    }

    public void UnfocusAnimationCallback()
    {
        //scalerから子供を剥がす
        lapTimeItemList[m_nextLapIndex].UnsetScaler(m_rectTransform);
        if(m_nextLapIndex < lapTimeItemList.Count - 1)
        {
            lapTimeItemList[m_nextLapIndex + 1].UnsetScaler(m_rectTransform);
        }

        //nextNextScalerのpivotを戻す
        SetYPivotInRuntime(m_nextNextLapTimeItemScaler, 1f);

        //Scalerの位置を動かす
        m_nextLapTimeItemScaler.anchoredPosition = AnchoredPositionInSideBar(m_nextLapIndex + 1,m_lapTimeHeight);
        m_nextNextLapTimeItemScaler.anchoredPosition = AnchoredPositionInSideBar(m_nextLapIndex + 4, m_lapTimeHeight);

        m_nextLapTimeItemScaler.localScale = Vector2.right + Vector2.up * m_focusedLapTimeHeightMultiplier;
        m_nextNextLapTimeItemScaler.localScale = Vector2.one;

        //LapTimeItemのセッティング
        if (timeController.nextLapIndex > 0)
        {
            lapTimeItemList[m_nextLapIndex].UnFocus(m_rectTransform);
        }
        m_nextLapIndex = timeController.nextLapIndex;
        lapTimeItemList[m_nextLapIndex].Focus(m_nextLapTimeItemScaler);
        if (timeController.nextLapIndex < lapTimeItemList.Count - 1)
        {
            lapTimeItemList[m_nextLapIndex+1].NextFocus(m_nextNextLapTimeItemScaler);
        }
        m_IsUpdating = false;
    }

    public void StartClock()
    {
        lapTimeItemList[0].StartClock();
        //m_animator.SetTrigger("StartClock");
    }

    public void PivotChangeAnimationCallback()
    {
        SetYPivotInRuntime(m_nextNextLapTimeItemScaler, 0);
    }

    public void SetYPivotInRuntime(RectTransform rectTransform, float y_pivot)
    {
        rectTransform.pivot = Vector2.right + Vector2.up * y_pivot;
        rectTransform.anchoredPosition = rectTransform.anchoredPosition + Vector2.down * 2f*(.5f - y_pivot) * rectTransform.sizeDelta.y;
    }
    
}
