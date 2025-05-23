using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public float fallSpeed;

    [Header("Jump")]
    public bool isCharging;
    public float ChargeCost;
    public float chargingSpeed;
    public float chargedPower;
    public float maxCharge;
    public LayerMask groundLayerMask;

    private Camera playerCamera;
    public Rigidbody _rigidbody;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (transform.position.y < -20f && !GameManager.Instance.isGameOver)
        {
            GameManager.Instance.GameOver();
        }

        if (isCharging)
        {
           ChargingJump();
        }

        if (IsFalling())
        {
            _rigidbody.AddForce(Vector2.down * fallSpeed * Time.deltaTime, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
         Move();
    }

    private void Move()
    {
        Vector3 camForward = playerCamera.transform.forward;
        Vector3 camRight = playerCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 dir = camForward * curMovementInput.y + camRight * curMovementInput.x;

        dir = IsGrounded()? dir * moveSpeed : dir * moveSpeed * 0.8f;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
            if (context.phase == InputActionPhase.Performed)
            {
                curMovementInput = context.ReadValue<Vector2>();
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                curMovementInput = Vector2.zero;
            }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && IsGrounded())
        {
            isCharging = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * jumpPower * (1 + chargedPower), ForceMode.Impulse);
            }

            JumpCancle();
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    bool IsFalling()
    {
        return GetComponent<Rigidbody>().velocity.y < -0.1f;
    }

    private void ChargingJump()
    {
        if (CharacterManager.Instance.Player.condition.UseStamina(ChargeCost * Time.deltaTime) == false)
        {
            JumpCancle();
        }
        else
        {
            chargedPower += chargingSpeed * Time.deltaTime;
            chargedPower = Mathf.Clamp(chargedPower, 0, maxCharge);

            float ratio = Mathf.Clamp01(chargedPower / maxCharge);
            float b = Mathf.Lerp(1f, 0.1f, ratio);

            meshRenderer.material.color = new Color(1, 1, b);
        }
    }

    private void JumpCancle()
    {
        isCharging = false;
        meshRenderer.material.color = Color.white;
        chargedPower = 0;
    }

    public void JumpHigher(float power)
    {
        _rigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }
}




