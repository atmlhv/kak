using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelopText : MonoBehaviour
{
    [SerializeField]
    Text teloptext;

    [SerializeField]
    private MoveDirection Direction = MoveDirection.Left;

    public enum MoveDirection
    {
        Up,
        Down,
        Right,
        Left,
    }

    [SerializeField]
    private float MoveVelocity = 5f;

    private Vector3 MoveVector3Value = Vector3.zero;

    public void Initialize(Vector2 t_position, string t_telopText) //float t_time, string t_lapName, Vector2 t_position, Vector2 t_sizeDelta)
    {
        transform.localPosition = t_position;
        teloptext.text = t_telopText;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (teloptext.text == string.Empty)
            return;

        switch (Direction)
        {
            case MoveDirection.Up:
                MoveVector3Value = Vector3.up * MoveVelocity * Time.deltaTime;
                break;
            case MoveDirection.Down:
                MoveVector3Value = Vector3.down * MoveVelocity * Time.deltaTime;
                break;
            case MoveDirection.Right:
                MoveVector3Value = Vector3.right * MoveVelocity * Time.deltaTime;
                break;
            case MoveDirection.Left:
                MoveVector3Value = Vector3.left * MoveVelocity * Time.deltaTime;
                break;
        }

        transform.position += MoveVector3Value;
    }

}
