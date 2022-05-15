using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// usage:      this class is attached to GamePlayerUIManager prefab
/// function:   handles the complement headers in game
///             more complement attracts(increase) diamond drops to player
///             if the player is hit once by enemies, it will reset complementary progress
/// </summary>
namespace game_ideas
{
    public class ComplementHandler : MonoBehaviour
    {

        [SerializeField] private Image complementFill_img;
        [SerializeField] private PoolingManager poolingManager;
        [SerializeField] private CameraManager cameraManager;

        private int complementLevel = 1; // use to increase the complementAmount required. MAX LEVEL 10
        private float complementAmount; // reference for complementCurrentAmount

        // increase complement to be able player to get diamond tokens
        public void IncreaseComplement(int complementValue)
        {
            complementAmount += complementValue;

            complementFill_img.fillAmount = ComputeComplement(complementAmount);

            if (complementFill_img.fillAmount >= 1f)
            {
                // while complementLevel is not set to maximum level which is 10. Keep increasing
                // and don't increase the level if complement level is already set to 10
                if (complementLevel < 10) 
                {
                    complementLevel++;
                }

                complementAmount = 0f; // reset the complement amount
                complementFill_img.fillAmount = complementAmount; // reset complement progress to empty
                DisplayComplementary(); // display the complementary header
            }
        }

        // reset the comlementary values
        public void ResetComplementProgress()
        {
            complementAmount = 0f;
            complementFill_img.fillAmount = complementAmount;

            // display reset message if complement level is greater than 1 to give information for player
            // that complement is reset
            if (complementLevel > 1)
            {
                complementLevel = 1; // reset complement level
                DisplayComplementary();
            }
        }

        // display complement header
        private void DisplayComplementary()
        {
            GameObject pooledObject = poolingManager.GetPooledObjectEffects("complementary"); 
            pooledObject.SetActive(true); // display complementary header
            pooledObject.transform.position = new Vector3(pooledObject.transform.position.x, cameraManager.transform.position.y, cameraManager.transform.position.z + 2.5f);
            Complementary complementary = pooledObject.GetComponent<Complementary>();

            switch (complementLevel)
            {
                case 1: complementary.DisplayUncompliment("RESET");
                    break;
                case 2: complementary.DisplayComplementary("NICE");
                    break;
                case 3: complementary.DisplayComplementary("GOOD");
                    break;
                case 4: complementary.DisplayComplementary("GREAT");
                    break;
                case 5: complementary.DisplayComplementary("AWESOME");
                    break;
                case 6: complementary.DisplayComplementary("AMAZING");
                    break;
                case 7: complementary.DisplayComplementary("IMPRESSIVE");
                    break;
                case 8: complementary.DisplayComplementary("INCREDIBLE");
                    break;
                case 9: complementary.DisplayComplementary("UNBELIEVABLE");
                    break;
                case 10:complementary.DisplayComplementary("REMARKABLE");
                    break;
            }
        }

        // calculate the points by multipliying to complement level and return the value
        private float ComputeComplement(float value)
        {
            return value / (100f * complementLevel);
        }

    }
}
