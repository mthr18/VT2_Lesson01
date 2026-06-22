using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [Header("*** Base Setting ***")]
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected GameObject _shotPoint;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual void OnTriggerAction()
    {
        
    }

    // 弾丸の生成
    protected void InstantiateBullet()
    {
        GameObject bullet = Instantiate(
            _bulletPrefab,                      // 生成オブジェクト
            _shotPoint.transform.position,      // 生成位置
            Quaternion.identity                 // 生成角度
            );

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(Camera.main.transform.forward * 50.0f, ForceMode.Impulse );

        Destroy(bullet, 5);                     // n秒後に弾丸を削除

        
    }
}
