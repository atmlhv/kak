using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelopTextController : Controller
{
    [SerializeField]
    TelopText teloptextPrefab;

    TelopController telopController;
    //TimeController timeController;

    //LapDataManager.LapTimes lapTimes;

    List<TelopText> telopTextList;

    //int m_nextLapIndex;


    public override void InitializeController(GameManager gameManager)
    {
        base.InitializeController(gameManager);
        telopController = FindObjectOfType<TelopController>();
        //timeController = FindObjectOfType<TimeController>();
    }

    public override void InitializeManagedItems()
    {
        base.InitializeManagedItems();

        
        telopTextList = new List<TelopText>();
        //lapTimes = timeController.applicatedLapTimes;

        //一旦仮で1個作る
        telopTextList.Add(Instantiate(teloptextPrefab, transform));
        //一旦場所指定を適当に
        telopTextList[0].Initialize(new Vector2(0,0));

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
