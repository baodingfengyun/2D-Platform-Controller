﻿

using Myd.Common;
using Myd.Platform;
using Myd.Platform.Core;
using UnityEngine;

namespace Myd.Platform
{
    /// <summary>
    /// 玩家类：包含
    /// 1、玩家显示器
    /// 2、玩家控制器（核心控制器）
    /// 并允许两者在内部进行交互
    /// </summary>
    public class Player
    {
        // 玩家渲染器
        private PlayerRenderer playerRenderer;

        // 玩家控制器
        private PlayerController playerController;

        // 保留游戏上下文
        private IGameContext gameContext;

        public Player(IGameContext gameContext)
        {
            this.gameContext = gameContext;
        }

        //加载玩家实体
        public void Reload(Bounds bounds, Vector2 startPosition)
        {
            //实例化玩家渲染器,"Assets/ProPlatformer/_Prefabs/PlayerRenderer.prefab"
            this.playerRenderer = Object.Instantiate(Resources.Load<PlayerRenderer>("PlayerRenderer"));
            this.playerRenderer.Reload();

            //实例化玩家控制器
            this.playerController = new PlayerController(playerRenderer, gameContext.EffectControl);
            this.playerController.Init(bounds, startPosition);

            //示例化玩家参数
            PlayerParams playerParams = Resources.Load<PlayerParams>("PlayerParam");
            playerParams.SetReloadCallback(() => this.playerController.RefreshAbility());
            playerParams.ReloadParams();
        }

        //玩家更新
        public void Update(float deltaTime)
        {
            //数据更新
            playerController.Update(deltaTime);
            //渲染更新
            Render();
        }

        //渲染
        private void Render()
        {
            playerRenderer.Render(Time.deltaTime);

            Vector2 scale = playerRenderer.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (int)playerController.Facing;
            playerRenderer.transform.localScale = scale;
            playerRenderer.transform.position = playerController.Position;

            //if (!lastFrameOnGround && this.playerController.OnGround)
            //{
            //    this.playerRenderer.PlayMoveEffect(true, this.playerController.GroundColor);
            //}
            //else if (lastFrameOnGround && !this.playerController.OnGround)
            //{
            //    this.playerRenderer.PlayMoveEffect(false, this.playerController.GroundColor);
            //}
            //this.playerRenderer.UpdateMoveEffect();

            this.lastFrameOnGround = this.playerController.OnGround;
        }

        //最近一帧是否在地上
        private bool lastFrameOnGround;

        public Vector2 GetCameraPosition()
        {
            if (this.playerController == null)
            {
                return Vector3.zero;
            }
            return playerController.GetCameraPosition();
        }
    }

}
