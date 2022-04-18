using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{
    // Start is called before the first frame update
    Vector3 hitLocation;

    public AudioClip Shot;
    public AudioClip Reload;

    protected override void FireWeapon()
    {

        if (weaponStats.bulletsInClip > 0 && !isReloading && !weaponHolder.playerController.isRunning)
        {
            GetComponent<AudioSource>().clip = Shot;
            base.FireWeapon();
            if (firingEffect)
            {
                GetComponent<AudioSource>().Play();
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
            print("Bullet count: " + weaponStats.bulletsInClip);
        }
        else if (weaponStats.bulletsInClip <= 0)
        {
            weaponHolder.StartReloading();
            GetComponent<AudioSource>().clip = Reload;
            GetComponent<AudioSource>().Play();
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitLocation, 0.2f);
    }
}