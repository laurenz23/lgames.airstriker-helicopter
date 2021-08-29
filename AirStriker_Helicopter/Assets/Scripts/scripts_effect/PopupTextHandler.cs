using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// usage: attached to popup text object not to text itself but it's parent
/// goal: handles the text output of popup text
/// </summary>

namespace game_ideas
{
    public class PopupTextHandler : MonoBehaviour
    {

        [SerializeField] private TextMeshPro textMesh = null;

        // call this method to display the text
        public void SetTextValue(string text)
        {

            textMesh.text = text;

        }

    }
}
