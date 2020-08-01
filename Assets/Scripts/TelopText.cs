using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelopText : MonoBehaviour
{
    [SerializeField]
    Text teloptext;

    public void Initialize(Vector2 t_position) //float t_time, string t_lapName, Vector2 t_position, Vector2 t_sizeDelta)
    {
        transform.localPosition = t_position;
        //baseImage.rectTransform.sizeDelta = t_sizeDelta;

        teloptext.text = "testtttttttttttttt";
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
