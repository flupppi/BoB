using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private CharacterController characterController;

    public float dashDuration = 0.2f; // Die Dauer des Dashs
    public float dashSpeed = 50f; // Die Geschwindigkeit des Dashes
    private bool isDashing = false; // Flag, um den aktuellen Zustand des Dashs zu verfolgen

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (inputReader.Dash && !isDashing) // Hier kannst du die Taste ändern, um den Dash auszulösen
        {
            StartCoroutine(PerformDash());
        }
    }

    IEnumerator PerformDash()
    {
        isDashing = true; // Setze den Dash-Zustand auf aktiv

        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            characterController.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
        isDashing = false; // Setze den Dash-Zustand auf inaktiv, nachdem die Dauer abgelaufen ist

    }
}
