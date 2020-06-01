using System;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
	/// <summary>
	/// 独自のゲームオブジェクト複製機能の設定を管理するクラス
	/// </summary>
	[Serializable]
	internal sealed class GameObjectDuplicationHookerSettings
	{
		//================================================================================
		// 定数
		//================================================================================
		private const string KEY = "UNI_GAME_OBJECT_DUPLICATION_HOOKER";

		//================================================================================
		// 変数(SerializeField)
		//================================================================================
		[SerializeField] private bool m_isEnable             = false; // 有効な場合 true
		[SerializeField] private bool m_isEnableSerialNumber = true;  // 文字列末尾の数値の連番対応を有効化する場合 true

		//================================================================================
		// プロパティ
		//================================================================================
		public bool IsEnable
		{
			get => m_isEnable;
			set => m_isEnable = value;
		}

		public bool IsEnableSerialNumber
		{
			get => m_isEnableSerialNumber;
			set => m_isEnableSerialNumber = value;
		}

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// EditorPrefs から読み込みます
		/// </summary>
		public static GameObjectDuplicationHookerSettings LoadFromEditorPrefs()
		{
			var json = EditorPrefs.GetString( KEY );
			var settings = JsonUtility.FromJson<GameObjectDuplicationHookerSettings>( json ) ??
			               new GameObjectDuplicationHookerSettings();

			return settings;
		}

		/// <summary>
		/// EditorPrefs に保存します
		/// </summary>
		public static void SaveToEditorPrefs( GameObjectDuplicationHookerSettings setting )
		{
			var json = JsonUtility.ToJson( setting );

			EditorPrefs.SetString( KEY, json );
		}
	}
}