using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class EmojiSpriteLabel{
	
	private List<GameObject> sprites;
	private NGUILabel label;
	private List<Vector3> mSymblosVerts = new List<Vector3>();
	private List<string> mSymblosNames = new List<string>();
	
	private float offsetOfY = 2f;
	
	private EmojiSpriteLabel(){}
	
	public static EmojiSpriteLabel create(NGUILabel pLabel)
	{
		EmojiSpriteLabel instance = new EmojiSpriteLabel();
		instance.label = pLabel;
		instance.sprites = new List<GameObject>();
		return instance;
	}
	
	public void createSymblos()
	{
		if(Application.isPlaying)
		{
			if(mSymblosVerts.Count <=0) return;
			if(label == null ) return;	
			if(label.font.dynamicSymbolsFont == null) return;

            //float fontSize = label.font.size;
            float fontSize = label.transform.localScale.x;
            float relateScale = label.transform.localScale.x / label.font.size;

            float lineFillLengthPixel = label.LineWidthBase * fontSize;
			float xLeftSide = 0;
			float yTopSide = 0; //note:this is not "Top". center Y of Top Line...
			//count the left of label's Fill rect x.
			if(label.pivot == NGUIWidget.Pivot.Left || label.pivot == NGUIWidget.Pivot.TopLeft || label.pivot == NGUIWidget.Pivot.BottomLeft)
			{
				xLeftSide = label.transform.localPosition.x;
			}
			else if(label.pivot == NGUIWidget.Pivot.Right || label.pivot == NGUIWidget.Pivot.TopRight || label.pivot == NGUIWidget.Pivot.BottomRight)
			{
				//chiuan fixe When label has maxLength,and pivot == TopRight...
				float tLength = label.lineWidth > 0 ? label.lineWidth : lineFillLengthPixel;
				xLeftSide = label.transform.localPosition.x - tLength;
				//Debug.Log(label.transform.localPosition.x + " - " + tLength + " = " + xLeftSide);
			}
			else
            {
                float tLength = label.lineWidth > 0 ? label.lineWidth : lineFillLengthPixel;
                xLeftSide = label.transform.localPosition.x - tLength / 2f;
			}
			//start count yTopSide
			if(label.pivot == NGUIWidget.Pivot.TopLeft || label.pivot == NGUIWidget.Pivot.TopRight || label.pivot == NGUIWidget.Pivot.Top)
			{
                yTopSide = label.transform.localPosition.y - fontSize * 0.5f;
			}
			else if(label.pivot == NGUIWidget.Pivot.BottomLeft || label.pivot == NGUIWidget.Pivot.BottomRight || label.pivot == NGUIWidget.Pivot.Bottom)
            {
                yTopSide = label.transform.localPosition.y + label.relativeSize.y * fontSize - fontSize * 0.5f;
			}
			else
			{
                yTopSide = label.transform.localPosition.y + label.relativeSize.y * 0.5f * fontSize - fontSize * 0.5f;
			}

			//Debug.Log("xLeftSide = " + xLeftSide);
			//Debug.Log("yTopSide = " + yTopSide);
            //Debug.LogError("lfp: " + lineFillLengthPixel);
            //Debug.LogError("label x: " + label.transform.localPosition.x);

			float lineSeed = 0;
			if(label.lineWidth > 0 && label.pivot != NGUIWidget.Pivot.Right && label.pivot != NGUIWidget.Pivot.TopRight && label.pivot != NGUIWidget.Pivot.BottomRight)
			{
                lineSeed = label.lineWidth * 1.0f / fontSize;
				//Debug.LogWarning("line seed = " + lineSeed);
			}

			for(int i =0;i<mSymblosVerts.Count; i++)
			{
				GameObject go = new GameObject("emoji"+i);
				go.transform.parent = label.transform.parent;
				go.layer = label.gameObject.layer;
				
				NGUISprite emoji = NGUITools.AddSprite(go,label.font.dynamicSymbolsFont.atlas,mSymblosNames[i]);
                //Debug.LogError("Adding emoji sprite @" + XUIActiveBase.logObjName(go) + " label text: " + label.text);
				go.transform.localScale = Vector3.one; //why is new parent?
				emoji.pivot = NGUIWidget.Pivot.Left;
				emoji.MakePixelPerfect();
				emoji.transform.localPosition = Vector3.zero; //reset to zero of parent.
                float y = emoji.transform.localScale.y / emoji.transform.localScale.x;
                emoji.transform.localScale = new Vector3(fontSize, fontSize * y, 1f);
				// 
				go.transform.localPosition = new Vector3(
                    xLeftSide + fontSize * mSymblosVerts[i].x,
                    yTopSide + (fontSize * mSymblosVerts[i].y + offsetOfY-6.104168f-2f),
					label.transform.localPosition.z - 1f);
				sprites.Add(go);
			}
		}
	}
	
	public void addSymbloVert(Vector3 pV,string pSpriteName)
	{
		if(Application.isPlaying)
		{
#if UNITY_EDITOR
			//Debug.Log("add symblo vert = " + pV);
#endif
			mSymblosVerts.Add(pV);
			mSymblosNames.Add(pSpriteName);
		}
	}
	
	public void destroy()
	{
		//maybe destroy all sprites
		if(sprites != null && sprites.Count > 0 )
		{
			for(int i=0; i < sprites.Count; i++)
			{
				if(sprites[i] != null)
				{
					//because maybe destroy will make some NGUIPanel refresh issul.
					GameObject.Destroy(sprites[i]); 
				}
			}
		}
		mSymblosVerts.Clear();
		mSymblosNames.Clear();
	}
	
	//set color of each sprite??
	public void setColor(Color pColor)
	{
		//maybe destroy all sprites
		if(sprites != null && sprites.Count > 0 )
		{
			for(int i=0; i < sprites.Count; i++)
			{
				if(sprites[i] != null)
				{
					//because maybe destroy will make some NGUIPanel refresh issul.
					setColorWidgetsInChildren(sprites[i],pColor);
				}
			}
		}
	}
	
	#region help method
	
	/// <summary>
	/// Sets the color widgets in children. contain self
	/// </summary>
	/// <param name='pGo'>
	/// P go.
	/// </param>
	/// <param name='pC'>
	/// P c.
	/// </param>
	public void setColorWidgetsInChildren(GameObject pGo,Color pC)
	{
		if(pGo == null)return;
		NGUIWidget[] widgets = pGo.transform.GetComponentsInChildren<NGUIWidget>();
		for(int i = 0; i < widgets.Length; i++)
		{
			widgets[i].color = new Color(pC.r,pC.g,pC.b,widgets[i].color.a);
		}
	}
	
	public void setColorWidgetsInChildren<T>(GameObject pGo,Color pC) where T:NGUIWidget
	{
		if(pGo == null)return;
		T[] widgets = pGo.transform.GetComponentsInChildren<T>();
		for(int i = 0; i < widgets.Length; i++)
		{
			widgets[i].color = new Color(pC.r,pC.g,pC.b,widgets[i].color.a);
		}
	}
	
	#endregion
}
