using System;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Gameflex.UISystem.Scripts
{
    public class UILevelTextUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelTMPText;
        [SerializeField] private IntVariable _level;

        private void Start()
        {
            _levelTMPText.text = _level.Value.ToString();
        }
    }
}

