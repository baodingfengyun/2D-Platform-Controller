using System.Collections;
using Myd.Common;

namespace Myd.Platform
{
    /// <summary>
    /// 动作状态: 正常 冲刺 爬墙(抓在垂直的墙壁上爬)
    /// </summary>
    public enum EActionState
    {
        Normal,
        Dash,
        Climb,
        Size,   // 不可达状态, 标记状态的数量
    }

    /// <summary>
    /// 定义基础的行为状态(抽象)类
    /// </summary>
    public abstract class BaseActionState
    {
        /// <summary>
        /// 动作状态
        /// </summary>
        protected EActionState state;
        /// <summary>
        /// 玩家控制器(上下文)
        /// </summary>
        protected PlayerController ctx;

        //在此状态的时间
        protected float onStateTime;
        //统计在此状态的帧数
        protected int onStateFrames;

        protected BaseActionState(EActionState state, PlayerController context)
        {
            this.state = state;
            this.ctx = context;
        }

        public EActionState State { get => state; }

        //每一帧都执行的逻辑
        public abstract EActionState Update(float deltaTime);

        //协程(持续动作)
        public abstract IEnumerator Coroutine();

        //开始触发
        public abstract void OnBegin();

        //结束触发
        public abstract void OnEnd();

        //是否持续运行
        public abstract bool IsCoroutine();
    }

    /// <summary>
    /// 有限状态机
    /// </summary>
    public class FiniteStateMachine<S> where S : BaseActionState
    {
        //状态集合
        private S[] states;

        //当前状态值
        private int currState = -1;
        //前一个状态值
        private int prevState = -1;
        //当前的协程
        private Coroutine currentCoroutine;
        //日志开关
        private bool m_LogSwitch = true;

        public FiniteStateMachine(int size)
        {
            this.states = new S[size];
            this.currentCoroutine = new Coroutine(true);
        }

        public void AddState(S state)
        {
            this.states[(int)state.State] = state;
        }

        //状态机更新
        public void Update(float deltaTime)
        {
            S s = this.states[this.currState];
            //先更新当前状态
            State = (int)s.Update(deltaTime);
            //再更新协程
            if (this.currentCoroutine.Active)
            {
                this.currentCoroutine.Update(deltaTime);
            }
        }

        public int State
        {
            get
            {
                return this.currState;
            }
            set
            {
                if (this.currState == value)
                    return;
                this.prevState = this.currState;
                this.currState = value;

                EActionState curEa = (EActionState)this.currState;
                EActionState prevEa = (EActionState)this.prevState;

                // 打印状态切换日志
                if (m_LogSwitch)
                {
                    Logging.Log($"====进入状态[{curEa}], 离开状态[{prevEa}]");
                }
                if (this.prevState != -1)
                {
                    if (m_LogSwitch)
                    {
                        Logging.Log($"====动作状态[{prevEa}] 结束");
                    }
                    this.states[this.prevState].OnEnd(); //结束旧状态
                }
                if (m_LogSwitch)
                {
                    Logging.Log($"====动作状态[{curEa}] 开始");
                }

                S s = this.states[this.currState];
                s.OnBegin(); //开始新状态
                if (s.IsCoroutine())
                {
                    //替换
                    this.currentCoroutine.Replace(s.Coroutine());
                    return;
                }
                //取消
                this.currentCoroutine.Cancel();
            }
        }
    }
}
