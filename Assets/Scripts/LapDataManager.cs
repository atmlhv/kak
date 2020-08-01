using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class LapDataManager
{

    //後々csvを渡してlapTime系をインポートできるようにする。今はハードコーディングしておく
    public static void ImportLapTimes()
    {
        lapTimesList = new List<LapTimes>()
        {
            new LapTimes("DQ11S","player1",
            new List<LapTime>()
            {
                new LapTime(100,9.5f),
                new LapTime(200,200.5f),
                new LapTime(300,300.5f),
                new LapTime(400,400.5f),
                new LapTime(500,500.5f),
                new LapTime(600,600.5f),
                new LapTime(700,700.5f),
                new LapTime(800,800.5f),
            }
            ),
              new LapTimes("DQ11S","player1",
            new List<LapTime>()
            {
                new LapTime(100,100.5f),
                new LapTime(200,200.5f),
                new LapTime(300,300.5f),
                new LapTime(400,400.5f),
                new LapTime(500,500.5f),
                new LapTime(600,600.5f),
                new LapTime(700,700.5f),
                new LapTime(800,810.5f),
            }
            ),
        };

        lapNamesList = new List<LapNames>()
        {
            new LapNames("DQ11S",new List<LapName>(){
                new LapName(100,"ラップタイム1"),
                new LapName(200,"ラップタイム2"),
                new LapName(300,"ラップタイム3"),
                new LapName(400,"ラップタイム4"),
                new LapName(500,"ラップタイム5"),
                new LapName(600,"ラップタイム6"),
                new LapName(700,"ラップタイム7"),
                new LapName(800,"ラップタイム8"),
            })
        };
    }


    public static void UpdateLapTimes()
    {

    }


    //ラップ名。ここから引き出してくるのではなくて、LapTimesに入れてしまうか別クラスを作るかしてもいいかも？
    public static string GetLapName(string t_gameName,int t_ID)
    {
        return lapNamesList.Find(x => x.gameTitle == t_gameName).lapNameList.Find(x => x.lapID == t_ID).lapName;
    }

    //該当タイトル、プレイヤーの最速ラップタイムを取得
    public static LapTimes FastestLapTimes(string t_gameName,string t_playerName)
    {
        List<LapTimes> findLapTimes = lapTimesList.FindAll(x => x.gameTitle == t_gameName).FindAll(x => x.playerName == t_playerName);
        float t_time = 0;
        int t_id = -1;
        for(int i = 0; i < findLapTimes.Count; i++)
        {
            if(t_id < 0 || t_time > findLapTimes[i].lapTimeList[findLapTimes[i].lapTimeList.Count - 1].time)
            {
                t_time = findLapTimes[i].lapTimeList[findLapTimes[i].lapTimeList.Count - 1].time;
                t_id = i;
            }
        }

        return findLapTimes[t_id];
    }


    //ラップタイムはここに入れておく
    public static List<LapTimes> lapTimesList;

    //ラップタイム名はここに入れておく
    public static List<LapNames> lapNamesList;

    //ラップタイムデータ
    public class LapTimes
    {
        public string gameTitle { get; internal set; }
        public string playerName { get; internal set; }
        public List<LapTime> lapTimeList { get; internal set; }

        public LapTimes(string t_gameTitle,string t_playerName, List<LapTime> t_lapTimeList)
        {
            gameTitle = t_gameTitle;
            playerName = t_playerName;
            lapTimeList = t_lapTimeList;
        }
    }

    //ラップタイムIDと名前の紐づけ
    public class LapNames
    {
        public string gameTitle { get; internal set; }
        public List<LapName> lapNameList { get; internal set; } 

        public LapNames(string t_gameTitle, List<LapName> t_lapNameList)
        {
            gameTitle = t_gameTitle;
            lapNameList = t_lapNameList;
        }
    }

    public class LapTime
    {
        public int lapID { get; internal set; }
        public float time { get; internal set; }

        public LapTime(int t_lapID, float t_time)
        {
            lapID = t_lapID;
            time = t_time;
        }
    }

    public class LapName
    {
        public int lapID { get; internal set; }
        public string lapName { get; internal set; }

        public LapName(int t_lapID,string t_lapName)
        {
            lapID = t_lapID;
            lapName = t_lapName;
        }
    }
}
