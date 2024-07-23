using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lớp CameraMovement dùng để xác định cách di chuyển của camera trong game
/// </summary>
public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// Đối tượng mục tiêu mà camera theo dõi
    /// </summary>
    public Transform target; 

    /// <summary>
    /// Độ mượt khi di chuyển camera
    /// </summary>
    public float smoothing;  
    /// <summary>
    /// Vị trí tối đa và tối thiểu mà camera có thể di chuyển
    /// </summary>
    public Vector2 maxPosition;  
    /// <summary>
    /// Vị trí tối đa và tối thiểu mà camera có thể di chuyển
    /// </summary>
    public Vector2 minPosition;  

    void Start()
    {
        /// Khởi tạo vị trí ban đầu của camera là vị trí của đối tượng mục tiêu
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    void LateUpdate()
    {
        /// Kiểm tra nếu vị trí hiện tại của camera khác với vị trí của đối tượng mục tiêu
        if (transform.position != target.position)
        {
            /// Lấy vị trí mới của camera là vị trí của đối tượng mục tiêu
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            
            /// Giới hạn vị trí của camera trong khoảng maxPosition và minPosition
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            
            /// Di chuyển camera một cách mượt dần đến vị trí mới
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}
