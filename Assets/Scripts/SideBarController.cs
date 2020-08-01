using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarController : Controller
{
    [SerializeField]
    TimerItem timerItemPrefab;

    TimerItem timerItem;

    public const float SideBarWidth = 172f;
    public const float TimerHeight = 60f;

    public float LapTimeHeight { get; private set; }

    public override void InitializeManagedItems()
    {
        base.InitializeManagedItems();
        timerItem = Instantiate(timerItemPrefab, Vector3.zero, Quaternion.identity, transform);
        timerItem.Initialize(Vector2.zero);
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
