using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Lớp Credits dùng để xử lý Credits
/// </summary>
public class Credits : MonoBehaviour
{
    /// <summary>
    /// Animator của Credits
    /// </summary>
    public Animator anima;
    /// <summary>
    /// Thời gian chạy animation
    /// </summary>
    public float animationLenght;
    
    void Start()
    {
        /// <summary>
        /// Chạy animation Credits
        /// </summary>
        anima.Play("Credits");
        Invoke("LoadNextScene", animationLenght);
    }

    /// <summary>
    /// Phương thức để chuyển sang scene MainMenu
    /// </summary>
    void LoadNextScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Phương thức để bỏ qua Credits
    /// </summary>
    public void Skip()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
