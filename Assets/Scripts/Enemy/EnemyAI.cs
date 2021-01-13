using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{    
    private CharacterController _enemyCC;
    private GameObject _player;
    [SerializeField] private float _speed = 4.0f;
    private float _gravity = 20.0f;
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        _enemyCC = GetComponent<CharacterController>();
        if (_enemyCC == null)
        {
            Debug.LogError("Character Controller is null.");
        }

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        if (_enemyCC.isGrounded)
        {

            Vector3 direction = (_player.transform.position - transform.position).normalized;
            direction.y = 0;
            transform.localRotation = Quaternion.LookRotation(direction);
            _velocity = direction * _speed;
        }
        _velocity.y -= _gravity;
        _enemyCC.Move(_velocity * Time.deltaTime);
    }
}


