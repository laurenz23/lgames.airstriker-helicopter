using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attacted to GameAssetsManager object at the hierarchy
 * This script handle the assets of the game
*/
namespace game_ideas
{
    public class GameAssetsManager : MonoBehaviour
    {
        public Transform environment;
        public Transform clouds;
        public Transform props;
        public Transform objective;
        public Transform enemies_air;
        public Transform enemies_grounds;

        public List<Transform> environment_list = new List<Transform>();
        public List<Transform> clouds_list = new List<Transform>();
        public List<Transform> props_list = new List<Transform>();
        public List<Transform> objective_list = new List<Transform>();
        public List<Transform> enemies_air_list = new List<Transform>();
        public List<Transform> enemies_grounds_list = new List<Transform>();

        private static GameAssetsManager instance;

        public static GameAssetsManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            instance = this;


            if (environment != null)
            {

                for (int x = 0; x < environment.childCount; x++)
                {
                    if (environment.GetChild(x).GetComponent<ObjectOptimizeHandler>())
                    {
                        environment_list.Add(environment.GetChild(x).GetComponent<ObjectOptimizeHandler>().transform);
                    }
                }

            }

            if (clouds != null)
            {

                for (int x = 0; x < clouds.childCount; x++)
                {
                    if (clouds.GetChild(x).GetComponent<ObjectOptimizeHandler>())
                    {
                        clouds_list.Add(clouds.GetChild(x).GetComponent<ObjectOptimizeHandler>().transform);
                    }
                }

            }

            if (props != null)
            {

                for (int x = 0; x < props.childCount; x++)
                {
                    if (props.GetChild(x).GetComponent<ObjectOptimizeHandler>())
                    {
                        props_list.Add(props.GetChild(x).GetComponent<ObjectOptimizeHandler>().transform);
                    }
                }

            }

            if (objective != null)
            {

                for (int x = 0; x < objective.childCount; x++)
                {
                    if (objective.GetChild(x).GetComponent<ObjectOptimizeHandler>())
                    {
                        objective_list.Add(objective.GetChild(x).GetComponent<ObjectOptimizeHandler>().transform);
                    }
                }

            }

            if (enemies_grounds != null)
            {

                for (int x = 0; x < enemies_grounds.childCount; x++)
                {
                    if (enemies_grounds.GetChild(x).GetComponent<ObjectOptimizeHandler>())
                    {
                        enemies_grounds_list.Add(enemies_grounds.GetChild(x).GetComponent<ObjectOptimizeHandler>().transform);
                    }
                }

            }

            if (enemies_air != null)
            {

            }

        }

    }
}
