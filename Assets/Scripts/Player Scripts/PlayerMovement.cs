using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Các trạng thái của người chơi
/// </summary>
public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}
/// <summary>
/// Lớp PlayerMovement dùng để xử lý di chuyển của người chơi
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Trạng thái hiện tại của người chơi
    /// </summary>
    public PlayerState currentState;    
    /// <summary>
    /// Tốc độ di chuyển của người chơi
    /// </summary>
    public float speed;                 
    /// <summary>
    /// Rigidbody của người chơi
    /// </summary>
    private Rigidbody2D myRigidbody;    
    /// <summary>
    /// Vector biến đổi vị trí
    /// </summary>
    private Vector3 change;            
    /// <summary>
    /// Animator của người chơi
    /// </summary>
    private Animator animator;          
    /// <summary>
    /// Máu hiện tại của người chơi
    /// </summary>
    public FloatValue currentHealth;    
    /// <summary>
    /// Tín hiệu khi máu người chơi thay đổi
    /// </summary>
    public Signal playerHealthSignal;   
    /// <summary>
    /// Vị trí khởi đầu của người chơi
    /// </summary>
    public VectorValue startingPosition;

    
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue; // Đặt vị trí ban đầu của người chơi
    }

    
    void Update()
    {
        change = Vector3.zero;   // Đặt change về vector zero để chuẩn bị cập nhật vị trí mới
        change.x = Input.GetAxisRaw("Horizontal");   // Lấy input ngang (trái/phải)
        change.y = Input.GetAxisRaw("Vertical");     // Lấy input dọc (lên/xuống)

        // Nếu nhấn nút tấn công và không ở trong trạng thái tấn công hoặc giật lùi
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack
            && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());    // Bắt đầu coroutine tấn công
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();   // Cập nhật hoạt hình và di chuyển nếu đang đi hoặc đứng
        }
    }

    /// <summary>
    /// Coroutine thực hiện tấn công
    /// </summary>
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);    // Đặt trạng thái attacking của animator là true
        currentState = PlayerState.attack;      // Đặt trạng thái hiện tại của người chơi là attack
        yield return null;  // Chờ một frame để tránh lặp lại ngay lập tức
        animator.SetBool("attacking", false);   // Đặt trạng thái attacking của animator là false
        yield return new WaitForSeconds(.3f);   // Chờ 0.3 giây
        currentState = PlayerState.walk;        // Sau khi tấn công xong, quay lại trạng thái đi bộ
    }

    /// <summary>
    /// Cập nhật hoạt ảnh và di chuyển của người chơi
    /// </summary>
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();    // Nếu có thay đổi vị trí, di chuyển nhân vật
            animator.SetFloat("moveX", change.x);    // Đặt giá trị di chuyển ngang của animator
            animator.SetFloat("moveY", change.y);    // Đặt giá trị di chuyển dọc của animator
            animator.SetBool("moving", true);        // Đặt trạng thái di chuyển của animator là true
        }
        else
        {
            animator.SetBool("moving", false);   // Nếu không di chuyển, đặt trạng thái di chuyển của animator là false
        }
    }

    /// <summary>
    /// Di chuyển nhân vật
    /// </summary>
    void MoveCharacter()
    {
        change.Normalize(); // Chuẩn hóa vector change để di chuyển theo hướng đã chỉ định
        myRigidbody.MovePosition(transform.position + change * speed * Time.fixedDeltaTime); // Di chuyển nhân vật
    }

     /// <summary>
    /// Xử lý khi người chơi va chạm với đối tượng khác
    /// </summary>
    /// <param name="knockTime">Thời gian đẩy lùi</param>
    /// <param name="damage">Sát thương nhận được</param>
    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;   // Giảm sức khỏe của người chơi
        playerHealthSignal.Raise();            // Gửi tín hiệu sức khỏe của người chơi đã thay đổi
        if (currentHealth.RuntimeValue > 0)    // Nếu sức khỏe vẫn còn lớn hơn 0
        {
            StartCoroutine(KnockCo(knockTime));    // Bắt đầu coroutine knock back
        }
        else
        {
            this.gameObject.SetActive(false);   // Ngược lại, tắt gameObject của người chơi
        }
    }

    /// <summary>
    /// Coroutine để xử lý knock back
    /// </summary>
    /// <param name="knockTime">Thời gian đẩy lùi</param>
    /// <returns></returns>
    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidbody != null)    // Nếu Rigidbody của người chơi không null
        {
            yield return new WaitForSeconds(knockTime);  // Chờ một khoảng thời gian knockTime
            myRigidbody.velocity = Vector2.zero;  // Đặt vận tốc của Rigidbody về 0
            currentState = PlayerState.idle;      // Đặt trạng thái hiện tại của người chơi là idle
            myRigidbody.velocity = Vector2.zero;  // Đặt vận tốc của Rigidbody về 0
        }
    }
}