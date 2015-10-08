//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tiled Sprite is obsolete. This script is kept only for backwards compatibility.
/// </summary>

[ExecuteInEditMode]
public class NGUITiledSprite : NGUISlicedSprite
{
	override public Type type { get { return NGUISprite.Type.Tiled; } }
}
