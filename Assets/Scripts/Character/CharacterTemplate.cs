using UnityEngine;
using System.Collections;
using DevelopEngine;

/// <summary>/// 角色数据（数据库）/// </summary>
public class CharacterTemplate : MonoSingleton<CharacterTemplate> {

	/// <summary>	/// 账号名称	/// </summary>
	public string characterName;
	/// <summary>	/// 角色ID	/// </summary>
	public int characterID;

	public int jobID;
	public int lv;
	public int expCur;
	public int force;
	public int intellect;
	public int attackSpeed;
	public int maxHP;
	public int maxMP;
	public int damageMax;
	public string jobModel;
	public int gold;
	public int diamond;

}
