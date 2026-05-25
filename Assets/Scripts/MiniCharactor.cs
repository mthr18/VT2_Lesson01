using UnityEngine;
using UnityEngine.InputSystem;

public class MiniCharactor : MonoBehaviour
{
    [Header("** Shooter Setting **")]
    public GameObject bulletPrefab;
    public GameObject shotPoint;
    public float shotForce = 10f;

    [Header("** Camera Joint **")]
    // カメラ軸のオブジェクト
    public GameObject cameraJoint;

    private Vector3 _inputMoveValue;
    private Vector2 _inputLookValue;
    private float _inputAttackValue;
    private Vector3 angles;

    void Start()
    {  
    }

    void Update()
    {
        Move();
        Look();
    }

    //===移動メソッド===
    /*
     * 引数   ：なし
     * 戻り値 ：なし
     */
    public void Move()
    {

        Vector3 velocity = Vector3.zero;    // 速度の変数
        velocity.z = _inputMoveValue.y;     // 入力(上下)で全身後退
        velocity.x = _inputMoveValue.x;

        transform.Translate(velocity * Time.deltaTime);
    }

    //===回転(向き)メソッド===
    /*
     * 引数   ：なし
     * 戻り値 ：なし
     */
    public void Look()
    {
        angles.x += _inputLookValue.y;      // y 入力で x 軸回転
        angles.y += _inputLookValue.x;      // x 入力で y 軸回転

        // x 軸の角度に制限を設ける
        // 範囲を設ける数学関数
        // Mathf.Clamp(対象値, 最小値, 最大値)
        angles.x = Mathf.Clamp(angles.x, -90, 90);

        transform.eulerAngles = new Vector3(0, angles.y * 0.1f, 0);
        cameraJoint.transform.eulerAngles = new Vector3(-angles.x * 0.1f, angles.y * 0.1f, 0);
        
    }

    void OnMove(InputValue value)
    {
        _inputMoveValue = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
       _inputLookValue = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        _inputAttackValue = value.Get<float>();

        GameObject bullet = Instantiate(
            bulletPrefab,           // 生成する弾のプレハブ
            shotPoint.transform.position,     // 弾丸の生成位置
            shotPoint.transform.rotation      // 弾丸の生成回転
            );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shotPoint.transform.forward * shotForce, ForceMode.Impulse);
    }
}
