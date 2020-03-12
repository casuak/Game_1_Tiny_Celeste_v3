using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._02_Camera
{
    /// <summary>
    /// 摄像机的边界碰撞器
    /// </summary>
    public class C_CameraBound : EntityComponent
    {
        public BoxCollider2D boxCollider2D;
        public new Camera camera;

        [ContextMenu("ResetBound")]
        private void ResetBound()
        {
            boxCollider2D.offset = Vector2.zero;
            var cameraHeight = camera.orthographicSize * 2;
            var cameraWidth = cameraHeight * camera.aspect;
            boxCollider2D.size = new Vector2(cameraWidth, cameraHeight);
        }
    }
}