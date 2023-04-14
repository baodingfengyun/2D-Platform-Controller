using Myd.Platform.Core;

namespace Myd.Platform
{
    public interface IGameContext
    {
        IEffectControl EffectControl { get; }

        ISoundControl SoundControl { get; }
    }
}
