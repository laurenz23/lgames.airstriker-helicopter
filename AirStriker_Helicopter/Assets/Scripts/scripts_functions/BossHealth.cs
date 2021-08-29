using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace game_ideas
{
    public class BossHealth : MonoBehaviour
    {
        public EnemyHandler enemyHandler;

        private TextMeshPro textMesh;

        // Start is called before the first frame update
        void Start()
        {
            textMesh = GetComponent<TextMeshPro>();
        }

        // Update is called once per frame
        void Update()
        {
            if (enemyHandler != null)
            {
                transform.position = new Vector3(enemyHandler.transform.position.x, enemyHandler.transform.position.y + 5f, enemyHandler.transform.position.z);
                textMesh.SetText(enemyHandler.enemyData.health.ToString());
            }
        }
    }
}
