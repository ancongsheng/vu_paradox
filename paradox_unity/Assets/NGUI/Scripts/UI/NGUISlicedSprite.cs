//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Sliced sprite is obsolete. It's only kept for backwards compatibility.
/// </summary>

[ExecuteInEditMode]
public class NGUISlicedSprite : NGUISprite
{
	override public Type type { get { return NGUISprite.Type.Sliced; } }
}
