using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Look Properties")]
    [SerializeField] Transform head;
    [Range(0f, 1f)]
    [SerializeField] float mouseSensibility = .5f;
    [Range(10f, 90f)]
    [SerializeField] float maxHeadRotation = 90f;
    [Range(-90f, -10f)]
    [SerializeField] float minHeadRotation = -90f;
    [Header("Move Properties")]
    //[SerializeField] InputAction moveAction;
    [Range(0f, 1f)]
    [SerializeField] float speed = .5f;
    [Header("Hand Properties")]
    [SerializeField] Transform objectGrabbed;
    PlayerInput pi;
    CharacterController cc;

    Vector2 look;
    InputAction moveAction;
    Vector2 move;
    float headRotation = 0;
    public bool CanMove { get; set; }

    public delegate void TriggerAction();
    public event TriggerAction OnTriggerAction;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        pi = GetComponent<PlayerInput>();
        moveAction = pi.actions.FindActionMap("Player").FindAction("Move");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!CanMove) return;
        move = moveAction.ReadValue<Vector2>();
        cc.Move(speed * transform.TransformDirection(new Vector3(move.x, 0, move.y)));
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!CanMove) return;
        look = context.ReadValue<Vector2>();
        transform.Rotate(mouseSensibility * new Vector3(0, look.x));
        headRotation += look.y * mouseSensibility;
        headRotation = Mathf.Clamp(headRotation, minHeadRotation, maxHeadRotation);
        head.localEulerAngles = new Vector3(headRotation, 0);
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (!CanMove) return;
        if (objectGrabbed && context.started)
            OnTriggerAction.Invoke();
    }
}
