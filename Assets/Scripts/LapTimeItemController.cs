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
        lapTimeItemList[m_nextLapIndex+1].NextFocus();

        
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
            UpdateNextLapIndex();
        }
        
    }

    private void UpdateNextLapIndex()
    {
        if (timeController.nextLapIndex > 0)
        {
            lapTimeItemList[m_nextLapIndex].UnFocus();
        }
        m_nextLapIndex = timeController.nextLapIndex;
       // lapTimeItemList[m_nextLapIndex].Focus();
    }
}
