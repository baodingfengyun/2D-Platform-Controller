using UnityEngine;

namespace Myd.Platform.Core
{
    /// <summary>
    /// 特效控制器，用于和外部实现解耦
    /// 定义了 7 种操作反馈特效, 让游戏看起来更真实.
    /// </summary>
    public interface IEffectControl
    {
        /// <summary>
        /// 冲刺线
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="dir">方向</param>
        void DashLine(Vector3 position, Vector2 dir);

        /// <summary>
        /// 涟漪
        /// </summary>
        /// <param name="position">中心位置</param>
        void Ripple(Vector3 position);

        /// <summary>
        /// 屏幕震动
        /// </summary>
        /// <param name="dir"></param>
        void CameraShake(Vector2 dir);

        /// <summary>
        /// 起跳尘雾
        /// </summary>
        /// <param name="position">起跳位置</param>
        /// <param name="color">颜色</param>
        /// <param name="dir">方向</param>
        void JumpDust(Vector3 position, Color color, Vector2 dir);

        /// <summary>
        /// 落地尘雾
        /// </summary>
        /// <param name="position">落地位置</param>
        /// <param name="color">颜色</param>
        void LandDust(Vector3 position, Color color);

        /// <summary>
        /// 速度圈
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="dir">方向</param>
        void SpeedRing(Vector3 position, Vector2 dir);

        /// <summary>
        /// 顿帧
        /// </summary>
        /// <param name="time"></param>
        void Freeze(float time);
    }
}
