using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _maxBallSpeed = 1.0f;
    [SerializeField] private float _randMax = 0.2f;
    [SerializeField] private float _waitTime = 1.0f;
    private float _curBallSpeed = 0.0f;

    private float _curAngle = 0.0f;

    private float topCoord = 4.87f;
    private float rightCoord = 10.25f;

    public UnityEvent onRightScore;
    public UnityEvent onLeftScore;

    // Start is called before the first frame update
    void Start()
    {
        // Scale speed
        _maxBallSpeed *= .01f;

        StartCoroutine(ResetBall());
    }

    // Update is called once per frame
    void Update()
    {
        // New position vector
        Vector2 newPos = transform.position;

        // Move ball
        newPos.x += Mathf.Cos(_curAngle) * _curBallSpeed;
        newPos.y += Mathf.Sin(_curAngle) * _curBallSpeed;

        // Set position
        transform.position = newPos;

        // Bounce off ceiling
        if(Mathf.Abs(transform.position.y) >= topCoord)
        {
            _curAngle *= -1;

            // Prevent getting stuck below turnaround point
            transform.position = new Vector2(transform.position.x, -1 * (topCoord - .05f) * Mathf.Sin(transform.position.y));
        }

        // Handle scoring
        if(transform.position.x >= rightCoord)
        {
            onLeftScore.Invoke();
            StartCoroutine(ResetBall());
        }

        if(transform.position.x <= -1 * rightCoord)
        {
            onRightScore.Invoke();
            StartCoroutine(ResetBall());
        }
    }

    IEnumerator ResetBall()
    {
        // Set ball speed to zero
        _curBallSpeed = 0.0f;

        // Reset Pos
        transform.position = new Vector2(0, 0);

        // Choose random angle
        _curAngle = Random.Range(-1 * Mathf.PI / 3, Mathf.PI / 3);

        // 50% chance to turn around
        if(Random.Range(0, 10) > 5)
        {
            _curAngle += Mathf.PI;
        }

        // Wait for 1 second
        yield return new WaitForSeconds(_waitTime);

        // Reset speed
        _curBallSpeed = _maxBallSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Paddle")
        {
            // Get new horizontal component of angle
            float newHor = -1 * Mathf.Cos(_curAngle);

            // Calculate new angle
            _curAngle = Mathf.Atan2(Mathf.Sin(_curAngle), newHor);

            // Add some random
            _curAngle += Random.Range(-_randMax, _randMax);

            // Govern vertical component to rad 3 / 2
            if(Mathf.Abs(Mathf.Sin(_curAngle)) >= .866)
            {
                _curAngle = Mathf.Sign(_curAngle) * Mathf.Acos(0.866f);
            }

            // Increase speed
            _curBallSpeed += .002f;
        }
    }

    public void ResetBallFunc()
    {
        StartCoroutine(ResetBall());
    }

    public void PauseBallMovement()
    {
        _curBallSpeed = 0.0f;
    }

    public void UnPauseBallMovement()
    {
        _curBallSpeed = _maxBallSpeed;
    }
}
