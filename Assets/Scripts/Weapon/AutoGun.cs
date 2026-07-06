using UnityEngine;

public class AutoGun : BaseWeapon
{
    public int bulletTimer = 0;

    void Start()
    {

    }

    void Update()
    {
        if(bulletTimer > 0)
        {
            bulletTimer--;
        }
    }

    // トリガーアクション
    public override void OnTriggerAction()
    {
        Debug.Log("フルオートアクション");

        if(bulletTimer <= 0)
        {
            // 弾丸の生成
            InstantiateBullet();
            bulletTimer = 50;
        }
        

        base.OnTriggerAction();
    }
}
