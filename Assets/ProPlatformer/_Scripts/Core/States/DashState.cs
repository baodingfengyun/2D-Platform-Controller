using System;
using System.Collections;
using Myd.Common;
using UnityEngine;

namespace Myd.Platform
{
    /// <summary>
    /// 冲刺状态
    /// </summary>
    public class DashState : BaseActionState
    {
        /// <summary>
        /// 冲刺方向
        /// </summary>
        private Vector2 DashDir;
        /// <summary>
        /// 记录冲刺之前的速度, 冲刺结束之后需要恢复
        /// </summary>
        private Vector2 beforeDashSpeed;

        //统计冲刺痕迹的帧数
        private int dashTrailCount;
        //调试日志开关
        private bool m_LogSwitch = true;

        public DashState(PlayerController context) : base(EActionState.Dash, context)
        {
        }

        public override void OnBegin()
        {
            this.onStateTime = 0f;
            this.onStateFrames = 0;
            ctx.launched = false;
            // 冻帧 0.05 秒 (大约 2~3 帧)
            ctx.EffectControl.Freeze(Constants.DashFreezeTime);

            ctx.WallSlideTimer = Constants.WallSlideTime;
            ctx.DashCooldownTimer = Constants.DashCooldown;
            ctx.DashRefillCooldownTimer = Constants.DashRefillCooldown;
            // 保存
            beforeDashSpeed = ctx.Speed;
            // 设置当前值 0
            ctx.Speed = Vector2.zero;
            DashDir = Vector2.zero;
            ctx.DashTrailTimer = 0;
            // 是否从地面上冲刺
            ctx.DashStartedOnGround = ctx.OnGround;
            
        }

        public override void OnEnd()
        {
            //CallDashEvents();
            ctx.PlayDashFluxEffect(DashDir, false);
            if (this.onStateTime > 0f || this.onStateFrames > 0)
            {
                Logging.Log("DushState 时间(秒):" + this.onStateTime +
                    ", 帧数:" + this.onStateFrames);
            }
        }

        public override EActionState Update(float deltaTime)
        {
            this.onStateTime += deltaTime;
            this.onStateFrames++;
            //Trail
            #region Trail
            if (ctx.DashTrailTimer > 0)
            {
                dashTrailCount++;
                ctx.DashTrailTimer -= deltaTime;
                if (ctx.DashTrailTimer <= 0)
                {
                    ctx.PlayTrailEffect((int)ctx.Facing);
                    if (m_LogSwitch)
                    {
                        Logging.Log("冲刺痕迹帧数:" + dashTrailCount);
                    }
                }
            }
            #endregion

            //Grab Holdables
            //Super Jump
            #region SuperJump
            if (DashDir.y == 0)
            {
                //Super Jump
                if (ctx.CanUnDuck && GameInput.Jump.Pressed() &&
                    ctx.JumpCheck.AllowJump())
                {
                    ctx.SuperJump();
                    return EActionState.Normal;
                }
            }
            #endregion

            //Super Wall Jump
            #region SuperWallJump
            if (DashDir.x == 0 && DashDir.y == 1)
            {
                //向上Dash情况下，检测SuperWallJump
                if (GameInput.Jump.Pressed() && ctx.CanUnDuck)
                {
                    if (ctx.WallJumpCheck(1))
                    {
                        ctx.SuperWallJump(-1);
                        return EActionState.Normal;
                    }
                    else if (ctx.WallJumpCheck(-1))
                    {
                        ctx.SuperWallJump(1);
                        return EActionState.Normal;
                    }
                }
            }
            else
            {
                //Dash状态下执行WallJump，并切换到Normal状态
                if (GameInput.Jump.Pressed() && ctx.CanUnDuck)
                {
                    if (ctx.WallJumpCheck(1))
                    {
                        ctx.WallJump(-1);
                        return EActionState.Normal;
                    }
                    else if (ctx.WallJumpCheck(-1))
                    {
                        ctx.WallJump(1);
                        return EActionState.Normal;
                    }
                }
            }
            #endregion

            return state;
        }

        /// <summary>
        /// 冲刺过程处理
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Coroutine()
        {
            // 第一次返回
            yield return null;

            #region 前半段
            var dir = ctx.LastAim;
            var newSpeed = dir * Constants.DashSpeed;
            //惯性
            if (Math.Sign(beforeDashSpeed.x) == Math.Sign(newSpeed.x) &&
                Math.Abs(beforeDashSpeed.x) > Math.Abs(newSpeed.x))
            {
                newSpeed.x = beforeDashSpeed.x;
            }
            ctx.Speed = newSpeed;

            DashDir = dir;
            if (DashDir.x != 0)
            {
                ctx.Facing = (Facings)Math.Sign(DashDir.x);
            }

            ctx.PlayDashFluxEffect(DashDir, true);

            ctx.PlayDashEffect(ctx.Position, dir);
            ctx.SpriteControl.Slash(true);
            ctx.PlayTrailEffect((int)ctx.Facing);
            // 设置冲刺痕迹时长
            ctx.DashTrailTimer = Constants.DashTrailTime;
            dashTrailCount = 0;
            yield return Constants.DashTime;
            #endregion

            #region 后半段
            ctx.SpriteControl.Slash(false);
            ctx.PlayTrailEffect((int)ctx.Facing);
            if (this.DashDir.y >= 0)
            {
                ctx.Speed = DashDir * Constants.EndDashSpeed;
                //ctx.Speed.x *= swapCancel.X;
                //ctx.Speed.y *= swapCancel.Y;
            }
            if (ctx.Speed.y > 0)
                ctx.Speed.y *= Constants.EndDashUpMult;

            this.ctx.SetState((int)EActionState.Normal);
            #endregion
        }

        public override bool IsCoroutine()
        {
            return true;
        }
    }
}
