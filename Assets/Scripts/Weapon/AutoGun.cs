using UnityEngine;

public class AutoGun : BaseWeapon
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // トリガーアクション
    public override void OnTriggerAction()
    {
        Debug.Log("フルオートアクション");

        // 弾丸の生成
        InstantiateBullet();

        base.OnTriggerAction();
    }
}
