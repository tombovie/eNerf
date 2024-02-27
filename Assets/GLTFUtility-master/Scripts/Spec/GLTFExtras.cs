using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Siccity.GLTFUtility
{
	[Serializable]
	public class GLTFExtrasProcessor
	{
		public virtual void ProcessExtras(GameObject importedObject, AnimationClip[] animations, JObject extras)
		{
		}
	}
}