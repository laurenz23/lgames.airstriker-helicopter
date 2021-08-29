using UnityEngine;

namespace game_ideas
{
    public class Utils : MonoBehaviour
    {
        public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
        {
            position.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(position);
        }
    }
}
