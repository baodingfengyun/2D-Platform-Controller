using Myd.Common;
using UnityEngine;

namespace Myd.Platform
{
    /// <summary>
    /// 定义角色的朝向
    /// </summary>
    public enum Facings
    {
        Right = 1,  //向右
        Left = -1   //向左
    }

    public struct VirtualIntegerAxis
    {

    }

    public struct VirtualJoystick
    {
        public Vector2 Value
        {
            get => new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"),
                               UnityEngine.Input.GetAxisRaw("Vertical"));
        }
    }

    /// <summary>
    /// 带缓存功能的虚拟按键, 也是平台跳跃类游戏的必备功能
    /// </summary>
    public struct VisualButton
    {
        // 键码
        private KeyCode key;
        // 缓存的默认值
        private float bufferTime;
        // 是否被消费了
        private bool consumed;
        // 缓存的当前值
        private float bufferCounter;

        public VisualButton(KeyCode key) : this(key, 0) {
        }

        public VisualButton(KeyCode key, float bufferTime)
        {
            this.key = key;
            this.bufferTime = bufferTime;
            this.consumed = false;
            this.bufferCounter = 0f;
        }

        // 消费了缓存
        public void ConsumeBuffer()
        {
            this.bufferCounter = 0f;
        }

        // 按键是否按下?
        public bool Pressed()
        {
            return UnityEngine.Input.GetKeyDown(key) || hasBuffer();
        }

        // 按键是否被缓存了
        private bool hasBuffer()
        {
            return !this.consumed && this.bufferCounter > 0f;
        }

        // 是否检测到按键
        public bool Checked()
        {
            return UnityEngine.Input.GetKey(key);
        }

        // 按键更新
        public void Update(float deltaTime)
        {
            this.consumed = false;
            // 每次都减去 delta
            this.bufferCounter -= deltaTime;

            bool flag = false;
            // 如果当前有按下的按键,将缓存时间重置为默认值
            if (UnityEngine.Input.GetKeyDown(key))
            {
                this.bufferCounter = this.bufferTime;
                flag = true;
            }
            else if (UnityEngine.Input.GetKey(key))
            {
                flag = true;
            }

            // 如果没有检测到按键,就将缓存时间改为 0
            if (!flag)
            {
                this.bufferCounter = 0f;
                return;
            }
            else
            {
                //打印按键相关参数
                //Logging.Log(Print(deltaTime));
            }
        }

        private string Print(float deltaTime)
        {
            return "VB[key:" + key + ", consumed:" + consumed + ", counter: "
                + bufferCounter + " / buffer: " + bufferTime + ", deltaTime: "
                + deltaTime + " ]";
        }
    }

    /// <summary>
    /// 操作游戏的输入
    /// </summary>
    public static class GameInput
    {
        /// <summary>
        /// 跳跃键: 空格
        /// </summary>
        public static VisualButton Jump = new VisualButton(KeyCode.Space, 0.08f);
        /// <summary>
        /// 冲刺键: K
        /// </summary>
        public static VisualButton Dash = new VisualButton(KeyCode.K, 0.08f);
        /// <summary>
        /// 抓住键: J
        /// </summary>
        public static VisualButton Grab = new VisualButton(KeyCode.J);
        //
        public static VirtualJoystick Aim = new VirtualJoystick();
        //
        public static Vector2 LastAim;

        //根据当前朝向,决定移动方向.
        public static Vector2 GetAimVector(Facings defaultFacing = Facings.Right)
        {
            Vector2 value = GameInput.Aim.Value;
            //TODO 考虑辅助模式

            //TODO 考虑摇杆
            if (value == Vector2.zero)
            {
                GameInput.LastAim = Vector2.right * ((int)defaultFacing);
            }
            else
            {
                GameInput.LastAim = value;
            }
            return GameInput.LastAim.normalized;
        }

        //输入更新
        public static void Update(float deltaTime)
        {
            //跳跃输入及缓存
            Jump.Update(deltaTime);
            //冲刺输入及缓存
            Dash.Update(deltaTime);
        }
    }
}
