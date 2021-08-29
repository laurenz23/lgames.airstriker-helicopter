using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage:  get the selected unit by player from main menu scene to game play scene
///         of what unit player will be use to play according to selection
/// </summary>
namespace game_ideas
{
    public class CharacterSelectedManager : MonoBehaviour
    {

        [Header("In Game Units Prefab")]
        [SerializeField] private GameObject[] inGameUnits; // reference of game unit (game play) objects

        private ProfilePlayerDataManager profilePlayerDataManager;

        private int unitIndex;

        private void Start()
        {
            if (profilePlayerDataManager == null)
            {
                profilePlayerDataManager = ProfilePlayerDataManager.GetInstance();
            }

            unitIndex = profilePlayerDataManager.profileSelectedData.selectedUnit; // get the selected unit

            CreateSelectedCharacter(unitIndex); // instantiate the selected unit to game play scene
        }

        // call this function to instantiate the selected unit to game play scene
        private void CreateSelectedCharacter(int index)
        {

            for (int x = 0; x < inGameUnits.Length; x++) // get the inGameUnits lenght reference
            {
                if (x == index) // x is equal to selected index value then,
                {
                    GameObject characterSelectedObj = Instantiate(inGameUnits[x]) as GameObject; // instantiate the object

                    characterSelectedObj.name = "UNIT_" + x + ": Character_Player (InGame)"; // rename the object

                    characterSelectedObj.GetComponent<PlayerManager>().SetSelectedUnit(index); // get the playermanager script and set the selected unit index, for reference
                }
            }

        }

    }
}
