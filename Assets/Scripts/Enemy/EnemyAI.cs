using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Attack,
        Chase,
        Idle
    }

    private CharacterController _enemyCC;
    private GameObject _player;
    private Health _playerHealth;
    [SerializeField] private float _speed = 4.0f;
    private float _gravity = 20.0f;
    private float _yVelocity;
    private Vector3 _velocity;
    [SerializeField] private int _damageAmount;
    [SerializeField] private EnemyState _currentState;
    private float _lastAttack = -1.5f;
    private float _coolDownAttack = 1.0f;
    private float _jumpHeight = 8.0f;
    private bool _playerIsFarAway = false;
    private float _lastJump = 0;

    // Start is called before the first frame update
    void Start()
    {
        _currentState = EnemyState.Chase;

        _enemyCC = GetComponent<CharacterController>();
        if (_enemyCC == null)
        {
            Debug.LogError("Character Controller is null.");
        }

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<Health>();
        if (_player == null || _playerHealth == null)
        {
            Debug.LogError("Player or Health are null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(_currentState)
        {
            case EnemyState.Attack:
                Attacking();
                break;
            case EnemyState.Chase:
                CalculateMovement();
                break;
            case EnemyState.Idle:

                break;
            default:
                break;
        }

        CalculateDistance();
    }

    private void CalculateMovement()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        direction.y = 0;
        transform.localRotation = Quaternion.LookRotation(direction);
        _velocity = direction * _speed;

        if (_enemyCC.isGrounded)
        {           
            if (_playerIsFarAway)
            {
                _yVelocity += _jumpHeight;
                _playerIsFarAway = false;
            }        
        }
        else
        {
            _yVelocity -= _gravity * Time.deltaTime;
        }

        _velocity.y = _yVelocity;
        _enemyCC.Move(_velocity * Time.deltaTime);
    }

    private void Attacking()
    {
      
        if ((Time.time - _lastAttack) > _coolDownAttack)
        {   if (_playerHealth != null) 
            {
                _playerHealth.GetDamage(_damageAmount);
                _lastAttack = Time.time;
            }                          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        { 
        _currentState = EnemyState.Attack;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _currentState = EnemyState.Chase;
        }
    }

    private void CalculateDistance()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        
        if ((Time.time - _lastJump > 5.0f) && distance > 10.0f)
        {
            _playerIsFarAway = true;
            _lastJump = Time.time;
        }
         
    }
}


