using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetList : MonoBehaviour
{
	public AssetListItem prefab;
	
	public VisualDefAsset[] assets;
	
	void Start()
	{
		foreach (VisualDefAsset asset in assets)
		{
			AssetListItem item = Instantiate(prefab, transform);
			item.image.sprite = asset.preview;
			item.asset = asset;
		}
		prefab.gameObject.SetActive(false);
	}
}
