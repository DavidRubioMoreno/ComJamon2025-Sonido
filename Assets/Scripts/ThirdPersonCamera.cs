using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target; // Referencia al jugador
    public Vector3 offset = new Vector3(0, 2, -4); // Offset detrás del jugador
    public float lookOffsetRight = 1.5f; // Distancia a la derecha donde la cámara apuntará
    public float sensitivity = 2.0f; // Sensibilidad del ratón/mando
    public float minYAngle = -30f; // Ángulo mínimo de la cámara
    public float maxYAngle = 60f; // Ángulo máximo de la cámara

    private float rotationX = 0f; // Rotación en el eje X (horizontal)
    private float rotationY = 0f; // Rotación en el eje Y (vertical)

    private Vector2 rotacion = Vector2.zero;

    void Start()
    {
        // Inicializa la rotación con la rotación actual de la cámara
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Obtener entrada del ratón y del mando
        float mouseX = rotacion.x * sensitivity;
        float mouseY = rotacion.y * sensitivity;
        //float joystickX = Input.GetAxis("RightStickX") * sensitivity;
        //float joystickY = Input.GetAxis("RightStickY") * sensitivity;

        // Aplicar movimiento de cámara sumando ratón y joystick
        rotationX += mouseX;
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, minYAngle, maxYAngle); // Limita la inclinación vertical

        // Rotación de la cámara
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Desplazamiento fijo a la derecha en coordenadas globales
        Vector3 rightOffset = transform.right * lookOffsetRight;
        Vector3 lookTarget = target.position + rightOffset; // Punto a mirar (ligeramente a la derecha del jugador)

        // Aplicar la posición y la dirección de la cámara
        transform.position = desiredPosition;
        transform.LookAt(lookTarget);
    }
    public void Move(InputAction.CallbackContext callback)
    {
        rotacion= callback.ReadValue<Vector2>();
        Debug.Log("ee");
    }
}


