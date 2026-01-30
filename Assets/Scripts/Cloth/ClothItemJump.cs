using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{

    public class ClothItemJump : ClothItemBase
    {
        public float jumpSpeed = 1.3f;

        protected override void Collect()
        {
            base.Collect();
            Player.Instance.ChangeJumpSpeed(jumpSpeed, duration);
        }
    }

}