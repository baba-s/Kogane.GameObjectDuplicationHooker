using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kogane.Internal
{
	/// <summary>
	/// ゲームオブジェクトの複製時に名前の末尾に数値を付けず、かつ transform のプロパティに誤差が生じないようにするエディタ拡張
	/// </summary>
	[InitializeOnLoad]
	internal static class GameObjectDuplicationHooker
	{
		//================================================================================
		// 変数(static)
		//================================================================================
		private static bool m_isDuplicating; // 複製中の場合 true

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		static GameObjectDuplicationHooker()
		{
			// 複製コマンドを検知するためにコールバックを登録
			// hierarchyChanged だと Event.current を検知できない
			EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGui;
			//EditorApplication.hierarchyChanged += OnChanged;
		}

		/// <summary>
		/// Hierarchy の項目を描画する時に呼び出されます
		/// </summary>
		private static void HierarchyWindowItemOnGui( int instanceID, Rect selectionRect )
		{
			// Preferences で有効になっているかどうかを確認する
			var settings = GameObjectDuplicationHookerSettings.LoadFromEditorPrefs();
			var isEnable = settings.IsEnable;

			if ( !isEnable ) return;

			// 複製コマンド以外のイベントは無視
			var current     = Event.current;
			var commandName = current.commandName;

			if ( string.IsNullOrWhiteSpace( commandName ) ) return;
			if ( commandName != "Duplicate" ) return;

			var type = current.type;

			if ( type != EventType.ExecuteCommand && type != EventType.ValidateCommand ) return;

			// 独自の複製コマンドで処理を上書きするため、
			// Unity 通常の複製コマンドを検知したら使用済みにする
			current.Use();

			// commandName が Duplicate のイベントは1フレームの間に何回も来るため
			// すでに独自の複製処理を実行している場合はここから先には進めない
			if ( m_isDuplicating ) return;
			m_isDuplicating = true;

			var duplicatedGameObjectList = new List<GameObject>();

			// 現在選択中のゲームオブジェクトを順番に複製していく
			// Selection.gameObjects は順不同で毎回順番が変わってしまうため sibilingIndex でソート
			foreach ( var original in Selection.gameObjects.OrderBy( x => x.transform.GetSiblingIndex() ) )
			{
				// 複製後に名前の末尾に数値を付けないようにするために名前をキャッシュ
				// スケーリング値などに誤差が生じないようにするために transform のプロパティもキャッシュ
				var name          = original.name;
				var localPosition = original.transform.localPosition;
				var localRotation = original.transform.localRotation;
				var localScale    = original.transform.localScale;

				GameObject clone;

				// プレハブのインスタンスかどうかで使用する Instantiate を切り替える
				if ( PrefabUtility.GetPrefabAssetType( original ) == PrefabAssetType.NotAPrefab )
				{
					clone = Object.Instantiate( original, original.transform.parent );
				}
				else
				{
					// プレハブのインスタンスの場合は元のプレハブから Instantiate する
					var prefab = PrefabUtility.GetCorrespondingObjectFromSource( original );
					clone = ( GameObject ) PrefabUtility.InstantiatePrefab( prefab, original.transform.parent );
				}

				// 連番対応が有効になっている場合は
				// 名前の文字列末尾の数字をインクリメントします
				if ( settings.IsEnableSerialNumber )
				{
					name = GetNameWithSerialNumber( name );
				}

				// キャッシュしておいたパラメータを設定
				clone.name                    = name;
				clone.transform.localPosition = localPosition;
				clone.transform.localRotation = localRotation;
				clone.transform.localScale    = localScale;

				// 複製完了後に複製したゲームオブジェクトを
				// Hierarchy で選択状態にするために一時リストに保持
				duplicatedGameObjectList.Add( clone );

				// Undo できるように登録
				Undo.RegisterCreatedObjectUndo( clone, "Duplicate" );
			}

			EditorApplication.delayCall += () =>
			{
				// 複製したゲームオブジェクトを Hierarchy で選択状態にする
				// この処理を1フレーム待たないと独自の複製コマンドが正常に動作しなかった
				Selection.instanceIDs = duplicatedGameObjectList
						.Select( x => x.GetInstanceID() )
						.ToArray()
					;

				duplicatedGameObjectList.Clear();
				m_isDuplicating = false;
			};
		}

		/// <summary>
		/// 指定された名前の文字列末尾の数字をインクリメントして返します
		/// </summary>
		private static string GetNameWithSerialNumber( string name )
		{
			var match       = Regex.Match( name, "([0-9]*$)" );
			var valueString = match.Value;

			if ( string.IsNullOrWhiteSpace( valueString ) ) return name;

			var value = int.Parse( valueString ) + 1;

			// 元の数値の桁数を超えないように Math.Min を使用
			// この処理を行わないと、例えば Item99 をインクリメントした時に
			// Ite100 になってしまう
			var digits = Math.Min( GetDigits( value ), valueString.Length );

			var result = name.Remove( name.Length - digits, digits ) + value;

			return result;
		}

		/// <summary>
		/// 指定された数字の桁数を返します
		/// </summary>
		private static int GetDigits( int num )
		{
			return num == 0 ? 1 : ( int ) Math.Log10( num ) + 1;
		}
	}
}