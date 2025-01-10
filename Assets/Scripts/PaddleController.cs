using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // Speed variables
    [SerializeField] private float _maxPaddleSpeed = 10.0f;
    private float _curPaddleSpeed = 0.0f;

    // KeyCodes
    [SerializeField] private KeyCode Up = KeyCode.UpArrow;
    [SerializeField] private KeyCode Down = KeyCode.DownArrow;


    // Start is called before the first frame update
    void Start()
    {
        // Scale speed
        _maxPaddleSpeed *= .01f;
        _curPaddleSpeed = _maxPaddleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Create new position vector
        Vector2 newPos = transform.position;

        // Get Inputs from player
        if(Input.GetKey(Up))
        {
            newPos.y += _curPaddleSpeed;
        }
        if(Input.GetKey(Down))
        {
            newPos.y -= _curPaddleSpeed;
        }

        // Clamp Position
        newPos.y = Mathf.Clamp(newPos.y, -4.0f, 4.0f);

        // Apply new position
        transform.position = newPos;
    }

    public void PausePaddleMovement()
    {
        _curPaddleSpeed = 0.0f;
    }

    public void UnPausePaddleMovement()
    {
        _curPaddleSpeed = _maxPaddleSpeed;
    }
}
