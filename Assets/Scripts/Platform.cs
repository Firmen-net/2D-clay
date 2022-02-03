using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platform : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
    }

    public void move()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-100, 0), ForceMode2D.Impulse);
    }
}