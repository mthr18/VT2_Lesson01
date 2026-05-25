using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour
{

    public Vector3 moveInputVelocity = Vector3.zero;        // 移動操作の入力ベクトル

    public Vector3 lookInputVelocity = Vector3.zero;        // カメラ操作の入力ベクトル

    public GameObject topAxis;          // タンクの上部(砲塔)のオブジェクト参照

    public GameObject cannonAxis;       // 砲身のオブジェクト参照

    public GameObject bulletPrefab;     // 弾のプレハブ

    public GameObject shotPoint;        // 弾の発射位置オブジェクト

    public float moveSpeed = 15f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 move = Vector3.zero;
        // move.x = moveInputVelocity.x;       // 入力の X 成分を移動の X 成分に設定
        move.z = moveInputVelocity.y;       // 入力の Y 成分を移動の Z 成分に設定

        Vector3 bodyTorque = Vector3.zero;

        bodyTorque.y = moveInputVelocity.x;     // 入力の X 成分を回転の Y 成分に設定

        transform.Rotate(bodyTorque *  Time.deltaTime * 90);

        transform.Translate(move * Time.deltaTime * moveSpeed);

        // === タンク上部の回転 ===

        Vector3 topTorque = Vector3.zero;

        topTorque.y = lookInputVelocity.x;

        topAxis.transform.Rotate(topTorque / 2);

        // === 砲身の回転 === 

        Vector3 cannonTorque = Vector3.zero;


        cannonTorque.z = lookInputVelocity.y;


        cannonAxis.transform.Rotate(cannonTorque / 10);

    }

    void OnMove(InputValue value)
    {
        Debug.Log($"move value is {value.Get()}");

        moveInputVelocity = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        Debug.Log($"look value is {value.Get()}");

        lookInputVelocity = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        Debug.Log($"attack value is {value.Get()}");

        GameObject bullet = Instantiate(
            bulletPrefab,           // 生成する弾のプレハブ
            shotPoint.transform.position,     // 弾丸の生成位置
            shotPoint.transform.rotation      // 弾丸の生成回転
            );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce( shotPoint.transform.forward * 25, ForceMode.Impulse);
    }
}
