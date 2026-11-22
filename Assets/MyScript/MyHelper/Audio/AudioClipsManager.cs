using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class AudioClipsManager:MonoSingleton<AudioClipsManager>
	{
		public AudioClip gameStartClip;
		public AudioClip fightClip;
		public AudioClip buttonClip;
        protected override void Init()
        {
            base.Init();
			DontDestroyOnLoad(gameObject);
        }
    }
}

