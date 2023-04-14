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

        public FiniteStateMachine(int size)
        {
            this.states = new S[size];
            this.currentCoroutine = new Coroutine(true);
        }

        public void AddState(S state)
        {
            this.states[(int)state.State] = state;
        }

        public void Update(float deltaTime)
        {
            State = (int)this.states[this.currState].Update(deltaTime);
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
                Logging.Log($"====Enter State[{(EActionState)this.currState}],Leave State[{(EActionState)this.prevState}] ");
                if (this.prevState != -1)
                {
                    Logging.Log($"====State[{(EActionState)this.prevState}] OnEnd ");
                    this.states[this.prevState].OnEnd();
                }
                Logging.Log($"====State[{(EActionState)this.currState}] OnBegin ");
                this.states[this.currState].OnBegin();
                if (this.states[this.currState].IsCoroutine())
                {
                    this.currentCoroutine.Replace(this.states[this.currState].Coroutine());
                    return;
                }
                this.currentCoroutine.Cancel();
            }
        }
    }
}
