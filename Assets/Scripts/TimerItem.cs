﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerItem : SideBarItem
{
    [SerializeField]
    Text time;
    TimeController timeController;

    public void Initialize(Vector2 t_position)
    {
        timeController = FindObjectOfType<TimeController>();
        transform.localPosition = t_position;
    }

    private void Update()
    {
        time.text = Utility.SecondToText(timeController.currentSecond,true);
    }
}
