
using System;
using UnityEngine;

public class PlatformSetting : MonoBehaviour
{
    [Header("[Finish]")]
    public bool isFinish;

    [Header("[Rotate]")]
    public bool isRotating;
    public float rotateSpeed;

    [Header("[Bounce]")]
    public bool isBouncy;
    public float bouncePower;

    [Header("[Break]")]
    public bool isBreakable;
    public float breakSpeed;

    [Header("[Hot]")]
    public bool isHot;
    public float hotDamage;

    private MeshRenderer meshRenderer;
    private Color originalColor;
    private Animator animator;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        if (isFinish) ChangeColor(0.3f, 0.8f, 0.3f);

        if (isBouncy) ChangeColor(0.4f, 0.4f, 1f);

        if (isHot) ChangeColor(0.8f, 0.3f, 0.3f);

        originalColor = meshRenderer.material.color;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isFinish && collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }

        if (isBreakable)
        {
            animator.SetTrigger("IsShaking");
            Invoke("Breaking", breakSpeed);
        }

        if (isBouncy && collision.gameObject.CompareTag("Player"))
        {
            ChangeColor(0.8f, 0.8f, 1f);
            Invoke("RecoverColor", 0.25f);
            CharacterManager.Instance.Player.controller.JumpHigher(bouncePower);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isHot && collision.gameObject.CompareTag("Player"))
        {
            CharacterManager.Instance.Player.condition.TakeDamage(hotDamage * Time.deltaTime);
        }
    }

    private void ChangeColor(float r, float g, float b)
    {
        meshRenderer.material.color = new Color(r, g, b);
    }

    private void RecoverColor()
    {
        meshRenderer.material.color = originalColor;
    }

    private void Breaking()
    {
        Destroy(gameObject);
    }
}
