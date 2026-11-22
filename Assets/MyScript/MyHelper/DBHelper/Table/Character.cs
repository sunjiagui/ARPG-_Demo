using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
namespace ns
{
	///<summary>
	///
	///</summary>
	[Table ("T_Character")]
	public class Character
	{
        [PrimaryKey]
		public int CharacterID {  get; set; }
        public int JobID {  get; set; }     //加载技能
        public int Lv {  get; set; }
        public int ExpCur {  get; set; }
        public int Force {  get; set; }
        public int Intellect {  get; set; }
        public int Speed {  get; set; }
        public int MaxHP {  get; set; }
        public int MaxMP {  get; set; }
        public int DamageMax {  get; set; }
        public string JobModel {  get; set; }  //加载角色


    }
}

