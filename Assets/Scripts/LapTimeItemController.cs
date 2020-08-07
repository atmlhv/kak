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

    int m_nextLapIndex;


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
        
        for(int i = 0; i < lapTimes.lapTimeList.Count; i++)
        {
            lapTimeItemList.Add(Instantiate(lapTimeItemPrefab,transform));
            lapTimeItemList[i].Initialize(
                lapTimes.lapTimeList[i].time,
                LapDataManager.GetLapName(lapTimes.gameTitle, lapTimes.lapTimeList[i].lapID),
                new Vector2(0, -SideBarController.TimerHeight - i * (Screen.height - SideBarController.TimerHeight) / lapTimes.lapTimeList.Count),
                new Vector2(SideBarController.SideBarWidth, 100f)
                );
        }

        UpdateNextLapIndex();

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
            lapTimeItemList[m_nextLapIndex].Highlight(false);
        }
        m_nextLapIndex = timeController.nextLapIndex;
        lapTimeItemList[m_nextLapIndex].Highlight(true);
    }
}
