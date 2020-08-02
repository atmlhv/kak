using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelopController : Controller
{
    [SerializeField]
    TelopFrame telopFramePrefab;

    TelopFrame telopFrame;

    //テキストを表示する範囲（一旦全部にする）
    //public const float TelopFrameWidth = 1108f;
    public const float TelopFrameHeight = 101f;

    public float TelopFrameWidth { get; private set; }

    public override void InitializeManagedItems()
    {
        base.InitializeManagedItems();
        telopFrame = Instantiate(telopFramePrefab, Vector3.zero, Quaternion.identity, transform);
        telopFrame.Initialize(Vector2.zero);
        TelopFrameWidth = telopFrame.GetComponent<RectTransform>().sizeDelta.x;
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
