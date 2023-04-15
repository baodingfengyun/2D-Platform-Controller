using UnityEngine;

namespace Myd.Platform
{
    /// <summary>
    /// 关卡
    /// </summary>
    public class Level : MonoBehaviour
    {
        /// <summary>
        /// 关卡等级
        /// </summary>
        public int levelId;

        /// <summary>
        /// 关卡边界
        /// </summary>
        public Bounds Bounds;

        /// <summary>
        /// 起始位置
        /// </summary>
        public Vector2 StartPosition;

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(Bounds.center, Bounds.size);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(StartPosition, 0.5f);
        }
    }
}
