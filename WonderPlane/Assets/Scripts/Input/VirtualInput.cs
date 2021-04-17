using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{
    public class VirtualInput : Singleton<VirtualInput>
    {
        public bool moveForward;
        public bool moveBackward;
        public bool moveAscending;
        public bool moveDescending;
        public bool attack;
        public bool automic;
    }
}
