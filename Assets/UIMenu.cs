using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    public Slider _suaraSlider;
    public void SuaraVolume() => AudioSetting.Instance.SuaraVolume(_suaraSlider.value);
}
