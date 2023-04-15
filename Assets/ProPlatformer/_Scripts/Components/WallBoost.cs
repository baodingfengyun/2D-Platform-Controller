using UnityEngine;

namespace Myd.Platform
{
    /// <summary>
    /// Wall Boost组件
    /// 当攀墙时跳跃，如果没有指定X轴方向，则系统自动产生一个推离墙面的力。且给予一定的持续时间
    /// If you climb jump and then do a sideways input within this timer, switch to wall jump
    /// </summary>
    public class WallBoost
    {
        //墙上跳跃推动时间
        private float timer;
        //统计帧数
        private int boostCount;
        //跳跃方向
        private int dir;

        private PlayerController controller;
        public float Timer => timer;

        public WallBoost(PlayerController playerController)
        {
            this.controller = playerController;
            this.dir = 0;
            this.ResetTime();
        }

        public void ResetTime()
        {
            this.timer = 0f;
            this.boostCount = 0;
        }

        /// <summary>
        /// 抓墙跳跃的检测
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            if (timer > 0)
            {
                timer -= deltaTime;
                boostCount++;

                // 按键方向一致,执行反弹推动作用
                if (controller.MoveX == dir)
                {
                    controller.Speed.x = Constants.WallJumpHSpeed * controller.MoveX;
                    timer = 0;
                    Myd.Common.Logging.Log("抓墙跳跃(反弹)推动速度:" + controller.Speed.x
                        + ", 按键反应帧数:" + boostCount + ", 时长剩余: " + timer);
                }
            }
        }

        /// <summary>
        /// 跳跃时，激活
        /// </summary>
        public void Active()
        {
            // x轴静止的情况下
            if (controller.MoveX == 0)
            {
                Debug.Log("====WallBoost抓墙跳跃(反弹)");
                // 设置反方向(向后跳)
                this.dir = -(int)controller.Facing;
                // 设置按键反应时长
                this.timer = Constants.ClimbJumpBoostTime;
                this.boostCount = 0;
            }
        }
    }
}