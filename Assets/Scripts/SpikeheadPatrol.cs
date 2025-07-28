using UnityEngine;

public class SpikedheadPatrol : MonoBehaviour
{
    public float patrolDistance = 3f;  // How far left and right from start position to patrol
    public float speed = 2f;

    private float leftX;
    private float rightX;
    private bool movingRight = true;

    void Start()
    {
        leftX = transform.position.x - patrolDistance;
        rightX = transform.position.x + patrolDistance;
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightX)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftX)
            {
                movingRight = true;
            }
        }

        // Flip sprite to face direction
        Vector3 scale = transform.localScale;
        scale.x = movingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}

