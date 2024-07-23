using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR;

/// <summary>
/// Lớp PauseMenu dùng để xử lý việc dừng game
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Biến kiểm tra xem game có đang dừng không
    /// </summary>
    private static bool gameIsPause = false;
    /// <summary>
    /// Biến lưu trữ menu dừng game
    /// </summary>
    [SerializeField] private GameObject pauseMenu;
    /// <summary>
    /// Biến lưu trữ menu thoát game
    /// </summary>
    [SerializeField] private GameObject quitMenu;
    
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Phương thức để tiếp tục game
    /// </summary>
    public void Resume()
    {
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }

    /// <summary>
    /// Phương thức để dừng game
    /// </summary>
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    /// <summary>
    /// Phương thức để thoát game
    /// </summary>
    public void Quit()
    {
        quitMenu.SetActive(true);
    }

    /// <summary>
    /// Phương thức để quay lại game
    /// </summary>
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Phương thức để thoát game 
    /// </summary>
    public void QuitDesktop()
    {
        Application.Quit();
    }
}
