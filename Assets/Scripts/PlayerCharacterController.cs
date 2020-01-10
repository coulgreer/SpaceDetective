using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public float speed = 3.0f;

    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition() {
        float horizontalDelta = Input.GetAxis("Horizontal");

        Vector2 move = new Vector2(horizontalDelta, 0);

        Vector2 position = rigidbody2D.position;
        position += move * speed * Time.deltaTime;
        rigidbody2D.MovePosition(position);
    }
}
