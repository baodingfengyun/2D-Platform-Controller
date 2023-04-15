using UnityEngine;

namespace Myd.Platform.Core
{
    /// <summary>
    /// 摄像机接口
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// 设置摄像机位置
        /// </summary>
        /// <param name="cameraPosition">镜头位置</param>
        void SetCameraPosition(Vector2 cameraPosition);
    }
}
