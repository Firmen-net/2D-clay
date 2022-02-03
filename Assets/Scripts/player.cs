using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private float jumpForce = 10;

    [SerializeField]
    public LineRenderer line;

    private Rigidbody2D rb;

    [SerializeField]
    public Transform firePoint;

    [SerializeField]
    public float maxRay = 15;

    [SerializeField]
    public AudioSource[] audioSource;

    private void Start()
    {
        audioSource[2].Play();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            audioSource[0].PlayOneShot(audioSource[0].clip, 1f);
            StartCoroutine(Shoot());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("enemy"))
        {
            SceneManager.LoadScene("Level1");
        }
        if (col.CompareTag("finish"))
        {
            Debug.Log("Finish");
            SceneManager.LoadScene("Menu");
        }
    }

    private IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, maxRay);
        if (hitInfo)
        {
            Wall wall = hitInfo.transform.GetComponent<Wall>();
            if (wall != null)
            {
                audioSource[1].PlayOneShot(audioSource[1].clip, 0.5f);
                wall.die();
            }

            line.SetPosition(0, firePoint.position);
            line.SetPosition(1, hitInfo.point);
        }
        else
        {
            line.SetPosition(0, firePoint.position);
            line.SetPosition(1, firePoint.position + firePoint.right * 20);
        }

        line.enabled = true;
        yield return new WaitForSeconds(0.02f);
        line.enabled = false;
    }
}