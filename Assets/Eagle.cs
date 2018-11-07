using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{

    Rigidbody2D _eagleRigidbody;
    LevelController _levelController;
    [SerializeField] Animator _eagleBodyAnimator;
    [SerializeField] Animator _gameObjectAnimator;
    [SerializeField] Vector2 _speed;
    [SerializeField] int _eagleLifes;
    [SerializeField] bool _isEagleDead = false;

    public void SetEagleState(bool state)
    {
        _isEagleDead = state;
    }

    void Start()
    {
        _eagleRigidbody = GetComponent<Rigidbody2D>();
        _levelController = FindObjectOfType<LevelController>();
        _levelController.EagleInPlay(_eagleLifes);
    }

    void Update()
    {
        if (!_isEagleDead)
        {
            KeepSpeedOfEagle();
        }
        else
        {
            _eagleRigidbody.velocity = Vector2.zero;
        }
    }

    private void KeepSpeedOfEagle()
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
            ProcessHitByBall();
            ProcessEffectsOfHIt();
        }
    }

    private void ProcessEffectsOfHIt()
    {
        FindObjectOfType<SFXController>().PlayHitEagle();
        _eagleBodyAnimator.SetTrigger("EagleHit");
    }

    private void ProcessHitByBall()
    {
        _levelController.EagleHit();
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

    public void PlayDeathEagleAnimation()
    {
        _gameObjectAnimator.SetTrigger("RunOutOfLife");
    }

    public void EagleFreez()
    {
        _eagleRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void EagleUnfreez()
    {
        _eagleRigidbody.constraints = RigidbodyConstraints2D.None;
        _eagleRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
