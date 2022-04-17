using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunComponent : WeaponComponent
{
    public int projectiles;
    public float spreadRange;

    Vector3 hitLocation;


    protected override void FireWeapon()
    {
        if (weaponStats.bulletsInClip > 0 && !isReloading && !weaponHolder.playerController.isRunning)
        {
            base.FireWeapon();
            if (firingEffect)
            {
                firingEffect.Play();
            }

            Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayers))
            {
                hitLocation = hit.point;

                DealDamage(hit);

                Vector3 hitDirection = hit.point - mainCamera.transform.position;

                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red, 1);
            }

            for (int i = 0; i < projectiles; i++)
            {
                Debug.Log("Projectile " + i);
                float projectilePos = Random.Range(0, spreadRange) - spreadRange / 2;

                screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2 + (projectilePos) , Screen.height / 2, 0));

                if (Physics.Raycast(screenRay, out hit, weaponStats.fireDistance, weaponStats.weaponHitLayers))
                {
                    hitLocation = hit.point;

                    DealDamage(hit);

                    Vector3 hitDirection = hit.point - mainCamera.transform.position;

                    Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red, 1);
                }
            }

            

            print("Bullet count: " + weaponStats.bulletsInClip);

        }
        else if (weaponStats.bulletsInClip <= 0)
        {
            weaponHolder.StartReloading();
        }
    }

    void DealDamage(RaycastHit hitInfo)
    {
        if (!hitInfo.transform.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
            damageable?.TakeDamage(weaponStats.damage);
        }
        
    }
}
