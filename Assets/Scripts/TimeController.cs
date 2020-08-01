using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : Controller
{
    float initialTime;
    float pausedTime;
    public float currentSecond { get; private set; }
    ClockState clockState;

    public LapDataManager.LapTimes applicatedLapTimes { get; private set; }
    public int nextLapIndex { get; private set; }

    public override void InitializeController(GameManager gameManager)
    {
        base.InitializeController(gameManager);
        applicatedLapTimes = LapDataManager.FastestLapTimes("DQ11S", "player1");
        StopClock();
    }
    

    public void StartClock()
    {

        switch (clockState)
        {
            case ClockState.Pause:
                initialTime += Time.time - pausedTime;
                clockState = ClockState.Clocking;
                break;

            case ClockState.Stop:

                initialTime = Time.time;
                currentSecond = 0;
                clockState = ClockState.Clocking;
                break;

            default:
                break;

        }

    }
    
    public void StopClock()
    {
        clockState = ClockState.Stop;
        currentSecond = 0;
    }
    
    public void PauseClock()
    {
        clockState = ClockState.Pause;
        pausedTime = Time.time;
    }

    public void FinishClock()
    {
        clockState = ClockState.Finished;
    }
    
    private void Update()
    {
        if (clockState == ClockState.Clocking)
        {
            currentSecond = Time.time - initialTime;
            if(currentSecond > applicatedLapTimes.lapTimeList[nextLapIndex].time)
            {
                if(nextLapIndex < applicatedLapTimes.lapTimeList.Count - 1)
                {
                    nextLapIndex++;
                }
                else
                {
                    FinishClock();
                }
            }
        }
    }

    public enum ClockState
    {
        Clocking,
        Pause,
        Stop,
        Finished
        
    }

}
