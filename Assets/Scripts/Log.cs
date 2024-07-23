using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lớp Log là một loại Enemy (kẻ địch) trong game.
/// Kế thừa từ lớp Enemy.
/// </summary>
public class Log : Enemy
{
    /// <summary>
    /// Rigidbody2D của Log.
    /// </summary>
    public Rigidbody2D myRigidbody;
    /// <summary>
    /// Đối tuong mục tiêu (Player).
    /// </summary>/
    public Transform target;            
    /// <summary>
    /// Bán kính để bắt đầu truy đuổi.
    /// </summary>
    public float chaseRadius;           
    /// <summary>
    /// Bán kính để tấn công.
    /// </summary>
    public float attackRadius;          
    /// <summary>
    /// Vị trí ban đầu (home position).
    /// </summary>
    public Transform homePosition;      
    /// <summary>
    /// Animator của Log.
    /// </summary>
    public Animator anim;               

    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();    // Lấy component Rigidbody2D
        currentState = EnemyState.idle;                 // Đặt trạng thái ban đầu là idle
        anim = GetComponent<Animator>();               // Lấy component Animator
        target = GameObject.FindWithTag("Player").transform;   // Tìm đối tượng với tag là "Player"
    }

    
    void FixedUpdate()
    {
        /// Kiểm tra trạng thái của Log
        CheckDistance();    
    }

    /// <summary>
    /// Phương thức kiểm tra khoảng cách với Player.
    /// </summary>
    void CheckDistance()
    {
        /// Nếu khoảng cách với Player nhỏ hơn hoặc bằng chaseRadius và lớn hơn attackRadius
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                    && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            /// Nếu đang ở trạng thái idle hoặc walk và không phải là stagger
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                /// Di chuyển Log đến vị trí của Player
                Vector3 temp = Vector3.MoveTowards(transform.position,
                        target.position, moveSpeed * Time.deltaTime);

                /// Thay đổi animation dựa trên hướng di chuyển
                changeAnim(temp - transform.position);

                /// Di chuyển Rigidbody đến vị trí mới
                myRigidbody.MovePosition(temp);

                /// Thay đổi trạng thái của Log thành walk
                ChangeState(EnemyState.walk);

                /// Bật cờ wakeUp trong Animator
                anim.SetBool("wakeUp", true);
            }
        }
        else
        {
            /// Nếu không nằm trong vùng chaseRadius, tắt cờ wakeUp trong Animator
            anim.SetBool("wakeUp", false);
        }
    }

    /// <summary>
    /// Phương thức SetAnimFloat dùng để thiết lập giá trị cho các tham số float trong Animator.
    /// </summary>
    /// <param name="setVector">Vector chứa giá trị float cần thiết lập</param>
    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    /// <summary>
    /// Phương thức changeAnim dùng để thay đổi animation của Log dựa trên hướng di chuyển.
    /// </summary>
    /// <param name="direction">Hướng di chuyển của Log</param>
    private void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    /// <summary>
    /// Phương thức ChangeState dùng để thay đổi trạng thái của Log.
    /// </summary>
    /// <param name="newState">Trạng thái mới của Log</param>
    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
