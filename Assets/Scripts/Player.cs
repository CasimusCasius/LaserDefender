using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float projectalSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.5f;
    [Range(0.05f, 1f)] [SerializeField] float verticalMoveRange = 0.5f;


    //states
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Coroutine fireCoroutine;

    //referece
    [SerializeField] GameObject playerLaserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundries();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
            fireCoroutine = StartCoroutine(FireContinuosly());
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }

    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
            GameObject laser =
               Instantiate(playerLaserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectalSpeed);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }


    private void SetUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + GetComponent<SpriteRenderer>().size.x / 2; // +- width od player
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0, 0)).x - GetComponent<SpriteRenderer>().size.x / 2;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + GetComponent<SpriteRenderer>().size.y / 2;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, verticalMoveRange, 0)).y - GetComponent<SpriteRenderer>().size.y / 2;
    }


}
