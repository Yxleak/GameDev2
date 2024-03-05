using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using TMPro;

public class Gun : MonoBehaviour
{
    //Debug
    public TMP_Text debug_text;

    //Gun Variables
    public GunData gunData;
    public Camera cam;
    protected Ray ray;

    //Ammo
    protected int ammo_in_clip;

    //Shooting
    protected bool primary_fire_is_shooting = false;
    protected bool primary_fire_hold = false;
    protected float shoot_delay_timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        ammo_in_clip = gunData.ammo_per_clip;
    }

    // Update is called once per frame
    void Update()
    {
        debug_text.text = "Ammo: " + ammo_in_clip.ToString();

        if (shoot_delay_timer > 0) shoot_delay_timer -= Time.deltaTime; //Subtract from timer
        PrimaryFire();
    }

    public void GetPrimaryFireInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            primary_fire_is_shooting = true;
        }

        if (gunData.automatic)
        {
            if (context.interaction is HoldInteraction && context.phase == InputActionPhase.Performed)
            {
                primary_fire_hold = true;
            }
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            primary_fire_is_shooting = false;
            primary_fire_hold = false;
        }
    }

    public void GetSecondaryFireInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) SecondaryFire();
    }

    protected virtual void PrimaryFire()
    {

    }    
    protected virtual void SecondaryFire()
    {

    }
}
