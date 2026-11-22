using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ns
{
	///<summary>
	///
	///</summary>
	[RequireComponent(typeof(AudioSource))]
	public class AudioManager:MonoSingleton<AudioManager>
	{
		private AudioSource src;
        protected override void Init()
        {
            base.Init();
			DontDestroyOnLoad(gameObject);

			src = GetComponent<AudioSource>();
			if (src != null) return;
            src = this.AddComponent<AudioSource>();//如果忘记添加组件
        }

		/// <summary>
		/// 音乐不会打断一般用于开枪等快速的声音
		/// </summary>
		/// <param name="clip"></param>
		/// <param name="pitch">音高变化量</param>
		/// <param name="isRandom">是否让声音不单调</param>
		/// <param name="deltaTime">几秒之后播放</param>
		public void PlaySoundArow(AudioClip clip, bool isRandom = false ,float pitch = 0, float deltaTime = 0)
		{
			StartCoroutine(Play(clip, pitch, isRandom, deltaTime));
		}
		
		/// <summary>
		/// 音乐会被打断
		/// </summary>
        public IEnumerator PlaySound(AudioClip clip,float delayTime = 0)
        {
			yield return TimeLog(delayTime);
			src.clip = clip;
			src.Play();
        }


		IEnumerator TimeLog(float delayTime)
		{
			yield return new WaitForSeconds(delayTime);
		}


        private IEnumerator Play(AudioClip clip, float pitch, bool isRandom, float deltaTime)
        {
            if (clip == null) yield break;
            float addPitch = isRandom ? Random.Range(pitch, -pitch) : pitch;
            src.pitch = 1 + addPitch;
            yield return new WaitForSeconds(deltaTime);
            src.PlayOneShot(clip);
        }
    }
}

