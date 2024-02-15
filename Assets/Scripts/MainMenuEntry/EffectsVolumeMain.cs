using UnityEngine;
using UnityEngine.UI;

public class EffectsVolumeMain : MonoBehaviour
{
    [SerializeField] private Color backgroundDisabled;
    [SerializeField] private Image effects;

    private void Start()
    {
        effects.color = SerializeDataView.Data.SerializedData.enabledSFXSounds ? Color.white : backgroundDisabled;
    }

    public void Toggle()
    {
        bool enabled = effects.color == Color.white;

        SerializeDataView.Data.SerializedData.enabledSFXSounds = !enabled;

        effects.color = SerializeDataView.Data.SerializedData.enabledSFXSounds ? Color.white : backgroundDisabled;
    }
}
