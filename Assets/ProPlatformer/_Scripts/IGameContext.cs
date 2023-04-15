using Myd.Platform.Core;

namespace Myd.Platform
{
    /// <summary>
    /// 游戏上下文接口
    /// </summary>
    public interface IGameContext
    {
        /// <summary>
        /// 特效控制
        /// </summary>
        IEffectControl EffectControl { get; }

        /// <summary>
        /// 声音控制
        /// </summary>
        ISoundControl SoundControl { get; }
    }
}
