using UnityEngine;

public class SemiAutoGun : BaseWeapon
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
        Debug.Log("セミオートアクション");

        // 弾丸の生成
        InstantiateBullet();

        base.OnTriggerAction();
    }
}
