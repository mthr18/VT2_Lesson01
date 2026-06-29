using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniCharactor : MonoBehaviour
{

    public enum AnimationPattern
    {
        Idle,
        Walk,
        HoldRight,
    }

    [Header("** Shooter Setting **")]
    public GameObject bulletPrefab;
    public GameObject shotPoint;
    public float shotForce = 10f;

    [Header("** Animator Setting **")]
    public Animator animator;

    [Header("** Camera Joint **")]
    // カメラ軸のオブジェクト
    public GameObject cameraJoint;

    [Header("** Weapon Setting **")]
    public BaseWeapon weapon;

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

        if( _inputAttackValue > 0.0f)
        {
            weapon.OnTriggerAction();   // 武器のトリガーアクションを呼び出す
        }

        if(_inputMoveValue.magnitude > 0.1f)
        {
            animator.SetInteger("State", (int)AnimationPattern.Walk);
        }
        else
        {
            animator.SetInteger("State", (int)AnimationPattern.Idle);
        }
        if(_inputAttackValue > 0.1f)
        {
            animator.SetInteger("State", (int)AnimationPattern.HoldRight);
        }
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
        angles.x = Mathf.Clamp(angles.x, -70, 70);

        transform.eulerAngles = new Vector3(0, angles.y, 0);
        cameraJoint.transform.eulerAngles = new Vector3(-angles.x, angles.y, 0);
        
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
    }
}
