using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trạng thái của kẻ địch.
/// </summary>
public enum EnemyState
{
    idle,       // Đang đứng yên
    walk,       // Đang di chuyển
    attack,     // Đang tấn công
    stagger     // Đang giật lùi
}

/// <summary>
/// Lớp Enemy dùng để xác định các đối tượng kẻ địch.
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Trạng thái hiện tại của kẻ địch.
    /// </summary>
    public EnemyState currentState;     
    /// <summary>
    /// Máu của kẻ địch.
    /// </summary>
    public FloatValue maxHealth;        
    /// <summary>
    /// Máu hiện tại của kẻ địch.
    /// </summary>
    public float health;                
    /// <summary>
    /// Tên của kẻ địch.
    /// </summary>
    public string enemyName;            
    /// <summary>
    /// Sát thương cơ bản của kẻ địch.
    /// </summary>
    public int baseAttack;              
    /// <summary>
    /// Tốc độ di chuyển của kẻ địch.
    /// </summary>
    public float moveSpeed;             

    /// <summary>
    /// Khởi tạo máu cho kẻ địch.
    /// </summary>
    private void Awake()
    {
        health = maxHealth.initialValue;    // Khởi tạo sức khỏe ban đầu bằng sức khỏe tối đa
    }

    /// <summary>
    /// Phương thức xử lý khi kẻ địch nhận sát thương.
    /// </summary>
    /// <param name="damage"> </param>
    private void TakeDamage(float damage)
    {
        health -= damage;   // Giảm sức khỏe của kẻ địch theo lượng sát thương nhận được
        if (health <= 0)
        {
            this.gameObject.SetActive(false);   // Nếu sức khỏe <= 0, tắt gameObject của kẻ địch
        }
    }

    /// <summary>
    /// Phương thức xử lý khi kẻ địch bị đẩy lùi.
    /// </summary>
    /// <param name="myRigidbody"></param>
    /// <param name="knockTime"></param>
    /// <param name="damage"> Lượng sát thương nhận vào</param>
    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));    /// Bắt đầu coroutine knock back
        TakeDamage(damage); /// Kẻ địch nhận sát thương khi bị đẩy lùi
    }

    /// <summary>
    /// Phương thức xử lý khi kẻ địch bị đẩy lùi.
    /// </summary>
    /// <param name="myRigidbody">Rigidbody của kẻ địch</param>
    /// <param name="knockTime">Thời gian bị đẩy lùi</param>
    /// <param name="damage">Lượng sát thương nhận vào từ việc bị đẩy lùi</param>
    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)   /// Nếu Rigidbody của kẻ địch không null
        {
            yield return new WaitForSeconds(knockTime);  /// Chờ một khoảng thời gian knockTime
            myRigidbody.velocity = Vector2.zero;  /// Đặt vận tốc của Rigidbody về 0
            myRigidbody.GetComponent<Enemy>().currentState = EnemyState.idle;   // Đặt trạng thái hiện tại của kẻ địch về idle
            myRigidbody.velocity = Vector2.zero;  /// Đặt vận tốc của Rigidbody về 0
        }
    }
}
