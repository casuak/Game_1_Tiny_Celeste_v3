using TinyCeleste._01_Framework;
using TinyCeleste._02_Modules._03_Player._01_Action._01_Climb;
using TinyCeleste._02_Modules._03_Player._02_Other._02_Command;
using TinyCeleste._02_Modules._08_Proxy;
using TinyCeleste._04_Extension._01_UnityComponent;

namespace TinyCeleste._02_Modules._03_Player._01_Action._04_Face
{
    public class Face : EntityComponent
    {
        // 朝向
        public FaceEnum faceEnum = FaceEnum.Right;

        public void Switch()
        {
            faceEnum = (FaceEnum) (-(int) faceEnum);
        }

        private C_Transform2DProxy transform2DProxy;
        private Command command;
        private C_ClimbTimeBar climbTimeBar;

        private void Awake()
        {
            transform2DProxy = GetComponentNotNull<C_Transform2DProxy>();
            command = GetComponentNotNull<Command>();
            climbTimeBar = GetComponentNotNull<C_ClimbTimeBar>();
        }

        public void FaceSystem()
        {
            var transform = transform2DProxy.transform;
            if (command.walkInt != 0)
            {
                faceEnum = (FaceEnum) command.walkInt;
            }

            transform.SetScaleX((int) faceEnum);
            // 体力条UI不需要反转（负负得正）
            climbTimeBar.foreground.transform.SetScaleX((int) faceEnum);
        }
    }

    public enum FaceEnum
    {
        Right = 1,
        Left = -1
    }
}