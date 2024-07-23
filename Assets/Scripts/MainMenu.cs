using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Lớp MainMenu dùng để xử lý các sự kiện trong menu chính
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Phương thức để bắt đầu một game mới
    /// </summary>
    public void NewGame()
    {
        /// <summary>
        /// Load scene tiếp theo
        /// </summary>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Phương thức để thoát game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Phương thức để mở Credits
    /// </summary>
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
