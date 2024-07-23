using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Lớp Sign dùng để xác định các đối tượng biển báo có thể tương tác được với người chơi.
/// Kế thừa từ lớp Interactable.
/// </summary>
public class Sign : Interactable
{
    /// <summary>
    /// Biến đánh dấu xem người chơi có trong phạm vi tương tác hay không.
    /// </summary>
    public GameObject dialogBox;  

    /// <summary>
    /// Component văn bản để hiển thị hộp thoại.
    /// </summary>
    public TextMeshProUGUI dialogText;      

    /// <summary>
    /// Biến lưu trữ nội dung hộp thoại.
    /// </summary>
    public string dialog;                   

    void Update()
    {
        /// Kiểm tra xem người chơi có trong phạm vi tương tác và nhấn phím E không.
        if(Input.GetKeyDown(KeyCode.E) && playerInRange) 
        {
            if(dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    /// <summary>
    /// Tắt hộp thoại và gửi tín hiệu ngữ cảnh khi người chơi ra khỏi vùng tương tác.
    /// </summary>
    /// <param name="other">Collider của đối tượng tương tác.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();            // Gửi tín hiệu ngữ cảnh.
            playerInRange = false;      // Đặt trạng thái người chơi không trong phạm vi.
            dialogBox.SetActive(false); // Tắt hộp thoại.
        }
    }
}
