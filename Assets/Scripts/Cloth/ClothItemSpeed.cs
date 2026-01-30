using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{

    public class ClothItemSpeed : ClothItemBase
    {
        public float speed = 2f;

        protected override void Collect()
        {
            base.Collect();
            Player.Instance.ChangeSpeed(speed, duration);
        }
    }

}