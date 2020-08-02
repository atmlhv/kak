using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelopTextController : Controller
{
    [SerializeField]
    TelopText teloptextPrefab;

    TelopController telopController;

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
    }

    private void AddTelopText()
    {
        telopTextList.Add(Instantiate(teloptextPrefab, transform));
        telopTextList[0].Initialize(new Vector2(telopController.TelopFrameWidth, 0), "test");

        //ホントはここでそれっぽいテキストを渡せるようにする
    }


    // Update is called once per frame
    void Update()
    {
        //何もないなら追加
        if (telopTextList.Count <= 0)
        {
            AddTelopText();
        }
        //先頭のテロップだけ見て消すか判断する
        if (telopTextList[0].transform.localPosition.x + telopTextList[0].GetComponent<RectTransform>().sizeDelta.x <= 0)
        {
            Destroy(telopTextList[0].gameObject);
            telopTextList.RemoveAt(0);
        }
    }
}
