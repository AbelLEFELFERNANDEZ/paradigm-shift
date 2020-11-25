using UnityEngine;

public class MoveBetweenPoints : Resetteable
{

    public Transform pointA;
    public Transform pointB;
    public float speed = 2;

    private bool aToB = true;

    private void Start()
    {
        transform.position = pointA.position;
    }

    private void Update()
    {
        if (aToB)
        {
            if(transform.position != pointB.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            } else
            {
                aToB = false;
            }
        } else
        {
            if (transform.position != pointA.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            }
            else
            {
                aToB = true;
            }
        }
        
    }

    public override void Reset()
    {
        transform.position = pointA.position;
    }

}
