using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lớp PlayerHit dùng để xác định các đối tượng mà người chơi va chạm
/// </summary>
public class PlayerHit : MonoBehaviour
{
    /// <summary>
    /// Phương thức xử lý khi người chơi va chạm với các đối tượng
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("breakable"))
        {
            other.GetComponent<pot>().Smash();
        }
    }
    
}
