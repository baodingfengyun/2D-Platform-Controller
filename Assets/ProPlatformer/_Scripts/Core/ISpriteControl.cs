using UnityEngine;

namespace Myd.Platform.Core
{
    /// <summary>
    /// 精灵控制器，用于和外部实现解耦
    /// </summary>
    public interface ISpriteControl
    {
        /// <summary>
        /// 尾部
        /// </summary>
        /// <param name="face"></param>
        void Trail(int face);

        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="localScale"></param>
        void Scale(Vector2 localScale);

        /// <summary>
        /// 设置缩放比例
        /// </summary>
        /// <param name="localScale"></param>
        void SetSpriteScale(Vector2 localScale);

        /// <summary>
        /// 获取精灵位置
        /// </summary>
        Vector3 SpritePosition { get; }

        /// <summary>
        /// 闪亮
        /// </summary>
        /// <param name="enable"></param>
        void Slash(bool enable);

        /// <summary>
        /// 冲刺
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="enable"></param>
        void DashFlux(Vector2 dir, bool enable);

        /// <summary>
        /// 设置头发颜色
        /// </summary>
        /// <param name="color"></param>
        void SetHairColor(Color color);

        /// <summary>
        /// 在墙上下滑
        /// </summary>
        /// <param name="color"></param>
        /// <param name="dir"></param>
        void WallSlide(Color color, Vector2 dir);
    }
}
