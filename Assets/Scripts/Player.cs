using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController _cc;
    private GameObject _camera;
    [SerializeField] private float _playerSpeed = 6.0f;
    private Vector3 _direction, _velocity;
    private float _jumpHeight = 8.0f;
    private float _gravity = 20.0f;
    [SerializeField] private bool _isGrounded;
    

    // Start is called before the first frame update
    void Start()
    {
        _cc = GetComponent<CharacterController>();
        if (_cc == null)
        {
            Debug.LogError("CC is null.");
        }

        _camera = GameObject.FindWithTag("MainCamera");
        if (_camera == null)
        {
            Debug.LogError("Main Camera is null.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _cc.isGrounded;
        PlayerMovement();

        PlayerLook();

       
    }
    void PlayerLook()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);


        Vector3 currentCameraRotation = _camera.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY;
        _camera.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }

    void PlayerMovement()
    {
        if (_isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            _direction = new Vector3(horizontal, 0, vertical);
            _velocity = _direction * _playerSpeed;


            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = _jumpHeight;
            }

        }

        _velocity.y -= _gravity * Time.deltaTime; ;
        _cc.Move(_velocity * Time.deltaTime);
    }

    
}
