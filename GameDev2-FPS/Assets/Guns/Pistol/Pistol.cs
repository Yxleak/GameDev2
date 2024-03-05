using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using TMPro;
using Unity.VisualScripting;

public class Pistol : Gun
{
    
    protected override void PrimaryFire()
    {
        //Check for delay
        if (shoot_delay_timer <= 0)
        {

            if (primary_fire_is_shooting || primary_fire_hold)
            {
                primary_fire_is_shooting = false;
                shoot_delay_timer = gunData.primary_fire_delay; //Set the timer

                //Set direction of ray
                Vector2 dir = Quaternion.AngleAxis(Random.Range(-gunData.spread, gunData.spread), Vector3.up) * cam.transform.forward;
                dir = Quaternion.AngleAxis(Random.Range(-gunData.spread, gunData.spread), Vector3.right) * dir;

                //Raycast
                ray = new Ray(cam.transform.position, dir);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, gunData.range))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.yellow, 0.05f);
                }

                //Subtract Ammo
                ammo_in_clip--;
                if (ammo_in_clip <= 0) ammo_in_clip = gunData.ammo_per_clip;
            }
        }
}

    private void SecondaryFire()
    {

    }
}
