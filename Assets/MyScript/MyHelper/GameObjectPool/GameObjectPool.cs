using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    public interface IResetable
    {
        /// <summary>
        /// 复用池中对象的重置行为的方法
        /// </summary>
        void Reset();//reset方法是复用池中对象的重置行为的方法

        /* 对象池物体是复用状态且awake和onEnable方法调用时机都与现方法存在冲突，所以规定接口来实现该需求
         * 比如子弹在创建物体方法被创建之后立即会调用awake中的设置目标点方法和SetInformationAndUse冲突 onEnable调用时机不固定导致容易出现问题
         * 即子弹因创建出来未被设置预期方向导致沿着非预期路径飞行  */
        //所以类似于子弹类继承该接口 完善该方法后由对象池的创建物体方法中的设置信息方法之后调用就可以按预期的想法执行逻辑而不需要awake

    }

    ///<summary>
    ///创建物体设置位置旋转基础信息，并提供了复用重置的方法
    ///</summary>
    public class GameObjectPool : MonoSingleton<GameObjectPool>
	{
		private Dictionary<string, List<GameObject>> cache;

		protected override void Init()
		{
			cache = new Dictionary<string, List<GameObject>>();
		}
		
		public GameObject CreatObject(string key, GameObject prefab, Vector3 tf, Quaternion qt)
        {
            if (prefab == null || key == null) return null;
            GameObject go = FindUseable(key);
            if (go == null)
            {
                go = AddObject(key, prefab);
            }
            SetInformationAndUse(tf, qt, go);
            ResetMain(go);
            return go;
            
        }
        private GameObject FindUseable(string key)
        {
            GameObject go = null;
            if (cache.ContainsKey(key))
                go = cache[key].Find(g => !g.activeInHierarchy);
            return go;
        }
        private GameObject AddObject(string key, GameObject prefab)
        {
            GameObject go = Instantiate<GameObject>(prefab);
            if (!cache.ContainsKey(key)) cache.Add(key, new List<GameObject>());
            cache[key].Add(go);
            return go;
        }
        private void SetInformationAndUse(Vector3 tf, Quaternion qt, GameObject go)
        {
            go.transform.transform.position = tf;
            go.transform.rotation = qt;
            go.SetActive(true);
        }
        private void ResetMain(GameObject go)
        {
            IResetable[] resetables;
            if ((resetables = go.GetComponents<IResetable>()) != null)
            {
                foreach (var item in resetables)
                {
                    item.Reset();
                }
            }

        }

        /// <summary>
        /// 删除对象池中的某一类
        /// </summary>
        /// <param name="value"></param>
        public void Clear(string key)
        {
            if (cache.ContainsKey(key))
            {
                foreach (var item in cache[key])
                {
                    Destroy(item);
                }
                cache.Remove(key);
            }

        }
        /// <summary>
        /// 清除对象池中所有游戏对象
        /// </summary>
        public void ClearAll()
        {
            foreach (var item in new List<string>(cache.Keys))
            {
                Clear(item);
            }

        }



        /// <summary>
        /// 延迟多久之后将对象设为false放入对象池
        /// </summary>
        /// <param name="targetObject"></param>
        /// <param name="coolTime"></param>
        public void CollectObject(GameObject targetObject ,float coolTime)
        {
            if (coolTime == 0) targetObject.SetActive(false);
            else StartCoroutine(CollectObjectIE(targetObject, coolTime));
        }
        private IEnumerator CollectObjectIE(GameObject targetObject, float coolTime)
        {
            yield return new WaitForSeconds(coolTime);
            targetObject.SetActive(false);
        }
        





        

        
    }
}

