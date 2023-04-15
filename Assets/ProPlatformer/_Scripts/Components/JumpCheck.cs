using Myd.Common;

namespace Myd.Platform
{
    /// <summary>
    /// 跳跃检查组件
    /// </summary>
    public class JumpCheck
    {
        //土狼时间
        private float timer;
        //对应的帧数
        private int graceCount;
        // 支持土狼跳跃的开关
        private bool jumpGrace;

        private PlayerController controller;

        public float Timer => timer;

        public JumpCheck(PlayerController playerController, bool jumpGrace)
        {
            this.controller = playerController;
            this.ResetTime();
            this.jumpGrace = jumpGrace;
        }

        public void ResetTime()
        {
            this.timer = 0f;
            this.graceCount = 0;
        }

        /// <summary>
        /// 跳跃检测更新
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            if (controller.OnGround)
            {
                //重置土狼时间
                timer = Constants.JumpGraceTime;
                graceCount = 0;
            }
            else
            {
                //Jump Grace
                if (timer > 0)
                {
                    timer -= deltaTime;
                    graceCount++;
                    if (timer <= 0)
                    {
                        Logging.Log("土狼时间帧数:" + graceCount);
                    }
                }
            }
        }

        /// <summary>
        /// 是否允许跳跃(支持土狼跳跃检测) (在空中 2-3 帧内按跳跃键视为在地面上)
        /// </summary>
        /// <returns></returns>
        public bool AllowJump()
        {
            return jumpGrace ? timer > 0 : controller.OnGround;
        }
    }
}