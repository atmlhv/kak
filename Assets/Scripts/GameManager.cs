using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Controller
{
    public TimeController timeController { get { return GetController<TimeController>(m_timeController, m_childrenControllers); } }
    TimeController m_timeController;

    public SideBarController unitController { get { return GetController<SideBarController>(m_sideBarController, m_childrenControllers); } }
    SideBarController m_sideBarController;

    public override void InitializeController(GameManager gameManager)
    {
        LapDataManager.ImportLapTimes();
        base.InitializeController(gameManager);
    }
    public override void InitializeManagedItems()
    {
        base.InitializeManagedItems();
    } 

    // Start is called before the first frame update
    void Start()
    {
        InitializeController(this);
        InitializeManagedItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            timeController.StartClock();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            timeController.StopClock();
        }
    }
}
