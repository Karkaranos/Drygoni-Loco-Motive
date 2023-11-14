using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    public float scrollSpeed = 3;

    public const float ScrollWidth = 8;
    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        pos.x -= scrollSpeed * Time.deltaTime;

        if (transform.position.x < -ScrollWidth)
        {
            Offscreen(ref pos);
        }

        transform.position = pos;
    }

    public virtual void Offscreen(ref Vector2 pos)
    {
        pos.x += 2 * ScrollWidth;
    }
}
