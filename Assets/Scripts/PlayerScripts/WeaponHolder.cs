using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn")]

    public GameObject weaponToSpawn;

    public PlayerController playerController;
    Animator animator;
    Sprite crosshairImage;
    public WeaponComponent equippedWeapon;

    [SerializeField]
    GameObject weaponSocketLocation;
    [SerializeField]
    Transform gripIKSocketLocation;

    bool wasFiring = false;
    bool firingPressed = false;

    // Start is called before the first frame update
    public readonly int isFiringHash = Animator.StringToHash("IsFiring");
    public readonly int isReloadingHash = Animator.StringToHash("IsReloading");
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        if (weaponToSpawn)
        {
            InstantiateWeapon();
        }
    }

    public void InstantiateWeapon()
    {
        GetComponent<AudioSource>().Play();
        ClearWeapon();
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);
        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        equippedWeapon.Initialize(this);
        PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);

        gripIKSocketLocation = equippedWeapon.gripLocation;
    }

    public void ClearWeapon()
    {
        if (weaponSocketLocation.transform.childCount > 0)
        {
            Destroy(weaponSocketLocation.transform.GetChild(0).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (weaponToSpawn)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.transform.position);
        }
        
    }

    public void OnFire(InputValue value)
    {
        if (weaponToSpawn)
        {
            firingPressed = value.isPressed;

            if (firingPressed)
            {
                StartFiring();
            }
            else
            {
                //print("Stopping firing");
                StopFiring();
            }
        }
    }

    public void StartFiring()
    {
        if (weaponToSpawn)
        {
            if (equippedWeapon.weaponStats.bulletsInClip <= 0)
            {
                StartReloading();
                return;
            }
            animator.SetBool(isFiringHash, true);
            playerController.isFiring = true;
            equippedWeapon.StartFiringWeapon();
        }
        
    }
    public void StopFiring()
    {
        if (weaponToSpawn)
        {
            playerController.isFiring = false;
            animator.SetBool(isFiringHash, false);
            equippedWeapon.StopFiringWeapon();
        }
        
    }
    //input based reload
    public void OnReload(InputValue value)
    {
        if (weaponToSpawn)
        {
            playerController.isReloading = value.isPressed;
            StartReloading();
        }
        
    }

    //the action of reloading
    public void StartReloading()
    {
        if (weaponToSpawn)
        {
            if (equippedWeapon.isReloading || equippedWeapon.weaponStats.bulletsInClip == equippedWeapon.weaponStats.clipSize) return;


            if (playerController.isFiring)
            {
                StopFiring();
            }
            if (equippedWeapon.weaponStats.totalBullets <= 0) return;

            animator.SetBool(isReloadingHash, true);
            equippedWeapon.StartReloading();

            InvokeRepeating(nameof(StopReloading), 0, 0.1f);
        }
        
    }

    public void StopReloading()
    {
        if (weaponToSpawn)
        {
            if (animator.GetBool(isReloadingHash)) return;

            playerController.isReloading = false;
            equippedWeapon.StopReloading();
            animator.SetBool(isReloadingHash, false);
            CancelInvoke(nameof(StopReloading));
        }
        
    }
}
