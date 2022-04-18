using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : HealthComponent
{
    public GameObject loseUI;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PlayerEvents.Invoke_OnHealthInitialized(this);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if(CurrentHealth <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementComponent>().isPaused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            loseUI.SetActive(true);
        }
    }

}
