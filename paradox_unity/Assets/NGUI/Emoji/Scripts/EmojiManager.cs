using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EmojiManager : MonoBehaviour {
	public bool isNeedtoInitEmojiFont;
	public bool isNeedtoDeleteEmojiFont;
	private static EmojiManager self = null;
	public static EmojiManager shared()
	{
		return self;
	}
	public NGUIFont emojiSymblosFont;
	private Hashtable emojiMap;
	
	// Use this for initialization
	void Start () {
		self = this;
		initEmojiDatas();
		if(isNeedtoDeleteEmojiFont)
			removeAllEmojiSymblosFont();
		if(isNeedtoInitEmojiFont)
			initEmojiSymblosFont();
	}
	
	//make emoji string....
	public static string processText(string pStrOri)
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		//change ios >= 5 to 4 style encoding emoji
		if(int.Parse(""+iPhoneSettings.systemVersion[0]) >= 5)
		{
			//convert this emoji to ios 4 version.
			pStrOri = ObjcInvoke.emoji5To4(pStrOri);
		}
#endif
		return pStrOri;
	}
	
	/// <summary>
	/// Gets the name of the emoji png.
	/// </summary>
	/// <returns>
	/// The emoji png name.
	/// </returns>
	/// <param name='pCode'>
	/// P code.
	/// </param>
	private string getEmojiPngName(string pCode)
	{
		if(emojiMap.Contains(pCode))
		{
			return (string)emojiMap[pCode];
		}
		else return "";
	}
	
	private int countEmojiAmount(string pStr)
	{
		int amount = 0;
		for(int i = 0 ; i< pStr.Length; i++)
		{
			try{
				char c = pStr[i];
				if(emojiMap.ContainsKey("" + c))
				{
					amount++;
					//maybe need to mark this emoji??
					
				}
			}
			catch(Exception e)
			{
				Debug.Log("Exception at emoji analyzing = " + e);
				continue;
			}
		}
		return amount;
	}
	
	void initEmojiSymblosFont()
	{
		if(emojiSymblosFont != null)
		{
			IEnumerator keyEnumerator = emojiMap.Keys.GetEnumerator();
			while(keyEnumerator.MoveNext())
			{
				emojiSymblosFont.AddSymbol((string)keyEnumerator.Current,(string)emojiMap[keyEnumerator.Current]);
			}
		}
	}
	
	void removeAllEmojiSymblosFont()
	{
		if(emojiSymblosFont != null)
		{
			IEnumerator keyEnumerator = emojiMap.Keys.GetEnumerator();
			while(keyEnumerator.MoveNext())
			{
				emojiSymblosFont.RemoveSymbol((string)keyEnumerator.Current);
			}
		}
	}
	
	void initEmojiDatas()
	{
		emojiMap = new Hashtable();
		emojiMap.Add("\ue415","e415");
		emojiMap.Add("\ue056","e056");
		emojiMap.Add("\ue057","e057");
		emojiMap.Add("\ue414","e414");
		emojiMap.Add("\ue405","e405");
		emojiMap.Add("\ue106","e106");
		emojiMap.Add("\ue418","e418");
		
		emojiMap.Add("\ue417","e417");
		emojiMap.Add("\ue40d","e40d");
		emojiMap.Add("\ue40a","e40a");
		emojiMap.Add("\ue404","e404");
		emojiMap.Add("\ue105","e105");
		emojiMap.Add("\ue409","e409");
		emojiMap.Add("\ue40e","e40e");
		
		emojiMap.Add("\ue402","e402");
		emojiMap.Add("\ue108","e108");
		emojiMap.Add("\ue403","e403");
		emojiMap.Add("\ue058","e058");
		emojiMap.Add("\ue407","e407");
		emojiMap.Add("\ue401","e401");
		emojiMap.Add("\ue40f","e40f");
		
		emojiMap.Add("\ue40b","e40b");
		emojiMap.Add("\ue406","e406");
		emojiMap.Add("\ue413","e413");
		emojiMap.Add("\ue411","e411");
		emojiMap.Add("\ue412","e412");
		emojiMap.Add("\ue410","e410");
		emojiMap.Add("\ue107","e107");
		
		emojiMap.Add("\ue059","e059");
		emojiMap.Add("\ue416","e416");
		emojiMap.Add("\ue408","e408");
		emojiMap.Add("\ue40c","e40c");
		emojiMap.Add("\ue11a","e11a");
		emojiMap.Add("\ue10c","e10c");
		emojiMap.Add("\ue32c","e32c");
		
		emojiMap.Add("\ue32a","e32a");
		emojiMap.Add("\ue32d","e32d");
		emojiMap.Add("\ue328","e328");
		emojiMap.Add("\ue32b","e32b");
		emojiMap.Add("\ue022","e022");
		emojiMap.Add("\ue023","e023");
		emojiMap.Add("\ue327","e327");
		
		emojiMap.Add("\ue329","e329");
		emojiMap.Add("\ue32e","e32e");
		emojiMap.Add("\ue32f","e32f");
		emojiMap.Add("\ue335","e335");
		emojiMap.Add("\ue334","e334");
		emojiMap.Add("\ue021","e021");
		emojiMap.Add("\ue337","e337");
		
		emojiMap.Add("\ue020","e020");
		emojiMap.Add("\ue336","e336");
		emojiMap.Add("\ue13c","e13c");
		emojiMap.Add("\ue330","e330");
		emojiMap.Add("\ue331","e331");
		emojiMap.Add("\ue03e","e03e");
		emojiMap.Add("\ue326","e326");
		
		emojiMap.Add("\ue11d","e11d");
		emojiMap.Add("\ue05a","e05a");
		emojiMap.Add("\ue00e","e00e");
		emojiMap.Add("\ue421","e421");
		emojiMap.Add("\ue420","e420");
		emojiMap.Add("\ue00d","e00d");
		emojiMap.Add("\ue010","e010");
		
		emojiMap.Add("\ue011","e011");
		emojiMap.Add("\ue41e","e41e");
		emojiMap.Add("\ue012","e012");
		emojiMap.Add("\ue422","e422");
		emojiMap.Add("\ue22e","e22e");
		emojiMap.Add("\ue22f","e22f");
		emojiMap.Add("\ue231","e231");
		
		emojiMap.Add("\ue230","e230");
		emojiMap.Add("\ue427","e427");
		emojiMap.Add("\ue41d","e41d");
		emojiMap.Add("\ue00f","e00f");
		emojiMap.Add("\ue41f","e41f");
		emojiMap.Add("\ue14c","e14c");
		emojiMap.Add("\ue201","e201");
		
		emojiMap.Add("\ue115","e115");
		emojiMap.Add("\ue428","e428");
		emojiMap.Add("\ue51f","e51f");
		emojiMap.Add("\ue429","e429");
		emojiMap.Add("\ue424","e424");
		emojiMap.Add("\ue423","e423");
		emojiMap.Add("\ue253","e253");
		
		emojiMap.Add("\ue426","e426");
		emojiMap.Add("\ue111","e111");
		emojiMap.Add("\ue425","e425");
		emojiMap.Add("\ue31e","e31e");
		emojiMap.Add("\ue31f","e31f");
		emojiMap.Add("\ue31d","e31d");
		emojiMap.Add("\ue001","e001");
		
		emojiMap.Add("\ue002","e002");
		emojiMap.Add("\ue005","e005");
		emojiMap.Add("\ue004","e004");
		emojiMap.Add("\ue51a","e51a");
		emojiMap.Add("\ue519","e519");
		emojiMap.Add("\ue518","e518");
		emojiMap.Add("\ue515","e515");
		
		emojiMap.Add("\ue516","e516");
		emojiMap.Add("\ue517","e517");
		emojiMap.Add("\ue51b","e51b");
		emojiMap.Add("\ue152","e152");
		emojiMap.Add("\ue04e","e04e");
		emojiMap.Add("\ue51c","e51c");
		emojiMap.Add("\ue51e","e51e");
	}
}
