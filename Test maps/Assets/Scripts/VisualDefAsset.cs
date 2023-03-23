using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/VisualDefAsset", fileName = "New VisualDefAsset")]
public class VisualDefAsset : ScriptableObject
{
	public Sprite preview;
	public GameObject asset;
}
