using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 10f;
    [Range(0.05f, 1f)] [SerializeField] float verticalMoveRange = 0.5f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundries();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void SetUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + GetComponent<SpriteRenderer>().size.x / 2; // +- width od player
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0, 0)).x - GetComponent<SpriteRenderer>().size.x / 2;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + GetComponent<SpriteRenderer>().size.y / 2;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, verticalMoveRange, 0)).y - GetComponent<SpriteRenderer>().size.y / 2;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }


}
