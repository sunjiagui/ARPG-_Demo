using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.Diagnostics.CodeAnalysis;

namespace ns
{
	///<summary>
	///
	///</summary>
	[Table("T_Account")]
	public class Account
	{
		[PrimaryKey]
		public int AccountID {  get; set; }
		public string AccountName { get; set; }
	}
}

