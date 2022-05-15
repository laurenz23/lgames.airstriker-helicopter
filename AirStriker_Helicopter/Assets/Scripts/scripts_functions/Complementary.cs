using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// usage:      attach to complementary object(prefab)
/// function:   handles the complementary message popup when player achieved it
/// </summary>
namespace game_ideas
{
    public class Complementary : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer complementary_sprRen;
        [SerializeField] private TextMeshPro complementaryHeader;
        [SerializeField] private Sprite compliment_sprite;
        [SerializeField] private Sprite uncompliment_sprite;

        public void DisplayUncompliment(string headerValue)
        {
            complementary_sprRen.sprite = uncompliment_sprite;
            complementaryHeader.text = headerValue;
        }

        public void DisplayComplementary(string headerValue)
        {
            complementary_sprRen.sprite = compliment_sprite;
            complementaryHeader.text = headerValue;
        }
    }
}
