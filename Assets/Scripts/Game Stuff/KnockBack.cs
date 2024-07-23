using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lớp KnockBack dùng để xử lý khi tiếp xúc với đối tượng, đẩy lùi đối tượng
/// </summary>
public class KnockBack : MonoBehaviour
{
    /// <summary>
    /// Lực đẩy khi va chạm với đối tượng khác
    /// </summary>
    public float thrust;  

    /// <summary>
    /// Thời gian làm choáng đối tượng khác
    /// </summary>
    public float knockTime; 
    
    /// <summary>
    /// Số lượng sát thương gây ra khi va chạm với đối tượng khác
    /// </summary>
    public float damage;  

    /// <summary>
    /// Phương thức xử lý khi đối tượng va chạm với đối tượng khác
    /// </summary>
    /// <param name="other">Collider của đối tượng khác</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        /// Kiểm tra nếu đối tượng va chạm là "breakable" và đối tượng hiện tại là "Player"
        if (other.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            /// Gọi hàm Smash trên đối tượng "pot" để phá vỡ
            other.gameObject.GetComponent<pot>().Smash();
        }

        /// Kiểm tra nếu đối tượng va chạm là "enemy" hoặc "Player"
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();  // Lấy Rigidbody2D của đối tượng va chạm
            if(hit != null)
            {
                /// Tính toán vector chênh lệch giữa vị trí của đối tượng va chạm và đối tượng hiện tại
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;  // Chuẩn hóa và nhân với lực đẩy

                /// Áp dụng lực đẩy lên đối tượng va chạm
                hit.AddForce(difference, ForceMode2D.Impulse);

                /// Nếu đối tượng va chạm là "enemy" và là va chạm trigger
                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    /// Đặt trạng thái của đối tượng "enemy" thành stagger (làm choáng)
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;

                    /// Gọi hàm Knock trên đối tượng "Enemy" để xử lý va chạm và làm choáng
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }

                /// Nếu đối tượng va chạm là "Player"
                if (other.gameObject.CompareTag("Player"))
                {
                    /// Kiểm tra nếu trạng thái của người chơi không phải là stagger (làm choáng)
                    if(other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        /// Đặt trạng thái của người chơi thành stagger (làm choáng)
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;

                        /// Gọi hàm Knock trên đối tượng "PlayerMovement" để xử lý va chạm và làm choáng
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                    
                }
                
            }
        }
    }
}
