using UnityEngine;

public class Playerpush : MonoBehaviour
{



    public float distance = 1f; // Set public so I can change the value
    public LayerMask boxMask;
    GameObject box;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask); // This will allow an object turn according to the player

        if (hit.collider != null && Input.GetKey(KeyCode.E))
        {
            box = hit.collider.gameObject;
        }
    }

    void OnDrawGizmos() // This will show a line with a color such as yellow
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }
}
