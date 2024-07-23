using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lớp Interactable dùng để xác định các đối tượng có thể tương tác được với người chơi
/// </summary>
public class Interactable : MonoBehaviour
{

    /// <summary>
    /// Tín hiệu được phát ra khi người chơi vào phạm vi tương tác
    /// </summary>
    public Signal context;  

    /// <summary>
    /// Biến đánh dấu xem người chơi có trong phạm vi tương tác hay không
    /// </summary>
    public bool playerInRange;  

    /// <summary>
    /// Phương thức xử lý khi người chơi va chạm với đối tượng tương tác
    /// </summary>
    /// <param name="other">Collider của đối tượng khác</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem đối tượng va chạm có tag là "Player" và không phải là trigger
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();  // Phát ra tín hiệu khi người chơi vào phạm vi tương tác
            playerInRange = true;  // Đánh dấu là người chơi đã vào phạm vi tương tác
        }
    }

    /// <summary>
    /// Phương thức xử lý khi người chơi rời khỏi phạm vi tương tác
    /// </summary>
    /// <param name="other">Collider của đối tượng khác</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        // Kiểm tra xem đối tượng rời khỏi có tag là "Player" và không phải là trigger
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();  // Phát ra tín hiệu khi người chơi rời khỏi phạm vi tương tác
            playerInRange = false;  // Đánh dấu là người chơi đã rời khỏi phạm vi tương tác
        }
    }
}
