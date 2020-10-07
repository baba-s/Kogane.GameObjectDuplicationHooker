using UnityEditor;

namespace Kogane.Internal
{
	/// <summary>
	/// 独自のゲームオブジェクト複製機能の Preferences における設定画面を管理するクラス
	/// </summary>
	internal sealed class GameObjectDuplicationHookerProvider : SettingsProvider
	{
		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GameObjectDuplicationHookerProvider( string path, SettingsScope scope )
			: base( path, scope )
		{
		}

		/// <summary>
		/// GUI を描画する時に呼び出されます
		/// </summary>
		public override void OnGUI( string searchContext )
		{
			var settings = GameObjectDuplicationHookerSettings.LoadFromEditorPrefs();

			using ( var checkScope = new EditorGUI.ChangeCheckScope() )
			{
				settings.IsEnable             = EditorGUILayout.Toggle( "Enabled", settings.IsEnable );
				settings.IsEnableSerialNumber = EditorGUILayout.Toggle( "Enabled Serial Number", settings.IsEnableSerialNumber );

				if ( checkScope.changed )
				{
					GameObjectDuplicationHookerSettings.SaveToEditorPrefs( settings );
				}
			}
		}

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// Preferences にメニューを追加します
		/// </summary>
		[SettingsProvider]
		private static SettingsProvider Create()
		{
			var path     = "Kogane/UniGameObjectDuplicationHooker";
			var provider = new GameObjectDuplicationHookerProvider( path, SettingsScope.User );

			return provider;
		}
	}
}
