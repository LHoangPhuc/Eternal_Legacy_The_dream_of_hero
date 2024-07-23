using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Lớp HeartManager dùng để quản lý thanh máu của người chơi.
/// </summary>
public class HeartManager : MonoBehaviour
{
    /// <summary>
    /// Mảng hình ảnh biểu thị thanh máu
    /// </summary>
    public Image[] hearts;              
    /// <summary>
    /// Sprite biểu thị thanh máu đầy
    /// </summary>
    public Sprite fullHeart;            
    /// <summary>
    /// Sprite biểu thị thanh máu nửa đầy
    /// </summary>
    public Sprite halfFullHeart;       
    /// <summary>
    /// Sprite biểu thị thanh máu trống
    /// </summary>
    public Sprite emptyHeart;    
    /// <summary>
    /// Số lượng thanh máu ban đầu
    /// </summary>
    public FloatValue heartContainers;  
    /// <summary>
    /// Máu hiện tại của người chơi
    /// </summary>
    public FloatValue playerCurrentHealth;  

    void Start()
    {
        /// Khởi tạo trạng thái ban đầu của các thanh máu
        InitHearts();   
    }

    /// <summary>
    /// Khởi tạo trạng thái ban đầu của các thanh máu (đều là thanh máu đầy).
    /// </summary>
    public void InitHearts()
    {
        for(int i = 0; i < heartContainers.initialValue; i++) 
        {
            /// Kích hoạt hình ảnh biểu thị thanh máu tương ứng
            hearts[i].gameObject.SetActive(true);   
            /// Đặt sprite là thanh máu đầy
            hearts[i].sprite = fullHeart;            
        }
    }
    
    /// <summary>
    /// Cập nhật trạng thái của các thanh máu dựa trên sức khỏe hiện tại của người chơi.
    /// </summary>
    public void UpdateHearts()
    {
        /// Tính toán sức khỏe hiện tại của người chơi
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;   
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            if(i <= tempHealth - 1)
            {
                // Thanh máu đầy
                hearts[i].sprite = fullHeart;
            }
            else if(i >= tempHealth)
            {
                // Thanh máu trống
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                // Thanh máu nửa đầy
                hearts[i].sprite = halfFullHeart;
            }
        }
    }
}
