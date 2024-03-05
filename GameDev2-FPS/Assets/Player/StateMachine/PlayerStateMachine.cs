using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerStateMachine : MonoBehaviour
{
    //Ground State Variables
    public float max_speed = 6;
    public float acceleration = 60;
    public float gravity = 20;
    public float stop_speed = 0.5f;
    //public float jump_impulse = 10f;
    public float friction = 4;

    //State Machine  Variables
    private PlayerBaseState current_state;
    public PlayerGroundState ground_state = new PlayerGroundState();
    public PlayerAirState air_state = new PlayerAirState();

    //Debug
    public TMP_Text debug_text;

    //Player Input
    [HideInInspector] public Vector2 move_input;

    //Movement Variables
    [HideInInspector] public CharacterController character_controller;
    [HideInInspector] public Vector3 player_velocity;
    [HideInInspector] public Vector3 wish_dir = Vector3.zero;
    [HideInInspector] public bool jump_button_pressed = false;

    // Start is called before the first frame update
    void Start()
    {
        //Character Component
        character_controller = GetComponent<CharacterController>();

        current_state = ground_state;
        current_state.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        current_state.UpdateState(this);
        DebugText();
    }

    void FixedUpdate()
    {
        FindWishDir();
        current_state.FixedUpdateState(this);
        MovePlayer();
    }

    public void SwitchState(PlayerBaseState cur_state, PlayerBaseState new_state)
    {
        current_state.ExitState(this);
        current_state = new_state;
        current_state.EnterState(this);
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }

    public void GetJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) jump_button_pressed = true;
        if (context.phase == InputActionPhase.Canceled) jump_button_pressed = false;
    }

    public void DebugText()
    {
        debug_text.text = "Wish Dir: " + wish_dir.ToString();
        debug_text.text += "\nPlayer Velocity: " + player_velocity.ToString();
        debug_text.text += "\nPlayey Speed: " + new Vector3(player_velocity.x, 0, player_velocity.z).magnitude.ToString();
        debug_text.text += "\nState: " + current_state.ToString();
    }

    public void FindWishDir()
    {
        //Find wish dir
        wish_dir = transform.right * move_input.x + transform.forward * move_input.y;
        wish_dir = wish_dir.normalized;
    }

    public void MovePlayer()
    {
        //Move the player.
        character_controller.Move(player_velocity * Time.deltaTime);
    }
}
