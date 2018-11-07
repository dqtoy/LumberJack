using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{

    Rigidbody2D _eagleRigidbody;
    LevelController _levelController;
    [SerializeField] Vector2 _speed;
    [SerializeField] int _eagleLifes;

    // Use this for initialization
    void Start()
    {
        _eagleRigidbody = GetComponent<Rigidbody2D>();
        _levelController = FindObjectOfType<LevelController>();
        _levelController.EagleInPlay(_eagleLifes);
        //_eagleRigidbody.AddForce(_speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (_eagleRigidbody.velocity.magnitude < _speed.magnitude)
        {
            _eagleRigidbody.velocity += _speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FlipEagle();
        if (collision.gameObject.CompareTag("Ball"))
        {
            _levelController.EagleHit();
        }
    }

    private void FlipEagle()
    {
        if (_eagleRigidbody.velocity.x > 1)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (_eagleRigidbody.velocity.x <= 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
