using System.Collections;
using Myd.Common;
using Myd.Platform.Core;
using UnityEngine;

namespace Myd.Platform
{
    /// <summary>
    /// 游戏状态
    /// </summary>
    enum EGameState
    {
        Load,   //加载中
        Play,   //游戏中
        Pause,  //游戏暂停
        Fail,   //游戏失败
    }

    /// <summary>
    /// 游戏玩法(GamePlay)
    /// 实现游戏上下文接口
    /// </summary>
    public class Game : MonoBehaviour, IGameContext
    {
        public static Game Instance;

        //关卡
        [SerializeField]
        public Level level;
        //场景特效管理器
        [SerializeField]
        private SceneEffectManager sceneEffectManager;
        //摄像机
        [SerializeField]
        private SceneCamera gameCamera;
        //玩家
        Player player;
        //游戏状态
        EGameState gameState;

        private bool m_LogSwtich = true;

        /// <summary>
        /// 启动步骤 1
        /// </summary>
        void Awake()
        {
            Instance = this;

            gameState = EGameState.Load;

            player = new Player(this);
            Logging.Log("一开始的初始化, 进入加载状态, 创建玩家对象");
        }

        /// <summary>
        /// 启动步骤 2
        /// </summary>
        /// <returns></returns>
        IEnumerator Start()
        {
            yield return null;

            //加载玩家
            player.Reload(level.Bounds, level.StartPosition);
            this.gameState = EGameState.Play;
            Logging.Log("加载玩家实体到关卡, 进入游戏 Play 状态");
            yield return null;
        }

        /// <summary>
        /// gameplay 运行的每一帧更新
        /// </summary>
        public void Update()
        {
            // 每帧之间的固定时间
            float deltaTime = Time.unscaledDeltaTime;
            if (UpdateTime(deltaTime))
            {
                if (this.gameState == EGameState.Play)
                {
                    GameInput.Update(deltaTime);
                    //更新玩家逻辑数据
                    player.Update(deltaTime);
                    //更新摄像机
                    gameCamera.SetCameraPosition(player.GetCameraPosition());
                }
            }
        }

        #region 冻帧
        private float freezeTime;
        private int freezeCount; //统计实际暂停了多少帧

        /// <summary>
        /// 更新冻帧数据，如果不冻帧，返回true
        /// </summary>
        /// <param name="deltaTime">实际帧(花费的时间)</param>
        /// <returns></returns>
        public bool UpdateTime(float deltaTime)
        {
            // 如果冻帧值大于 0 的话, 最好是实际帧的倍数.
            if (freezeTime > 0f)
            {
                // 取 预期(冻)帧 和 实际帧 之间的时间差, 最小值为 0
                freezeTime = Mathf.Max(freezeTime - deltaTime, 0f);
                freezeCount++;
                if (m_LogSwtich && freezeTime == 0f)
                {
                    Logging.Log("冻帧结束, 共计暂停了 " + freezeCount + " 帧.");
                }
                return false;
            }
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            return true;
        }

        /// <summary>
        /// 冻帧(外部根据情况设置时长)
        /// </summary>
        /// <param name="freezeTime">冻帧时长,最好是 deltaTime 的整数倍</param>
        public void Freeze(float freezeTime)
        {
            this.freezeTime = Mathf.Max(this.freezeTime, freezeTime);
            if (this.freezeTime > 0)
            {
                Time.timeScale = 0;
                freezeCount = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            if (m_LogSwtich && freezeTime > 0f)
            {
                Logging.Log("设置冻帧时间(秒): " + freezeTime);
            }
        }
        #endregion

        //震动屏幕
        public void CameraShake(Vector2 dir, float duration)
        {
            this.gameCamera.Shake(dir, duration);
        }

        //接口实现
        public IEffectControl EffectControl { get=>this.sceneEffectManager; }

        //接口实现
        public ISoundControl SoundControl { get; }
        
    }

}
