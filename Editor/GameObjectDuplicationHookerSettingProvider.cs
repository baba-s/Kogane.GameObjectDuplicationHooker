using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    /// <summary>
    /// 独自のゲームオブジェクト複製機能の Project Settings における設定画面を管理するクラス
    /// </summary>
    internal sealed class GameObjectDuplicationHookerSettingProvider : SettingsProvider
    {
        //================================================================================
        // 定数
        //================================================================================
        private const string PATH = "Kogane/Game Object Duplication Hooker";

        //================================================================================
        // 変数
        //================================================================================
        private Editor m_editor;

        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private GameObjectDuplicationHookerSettingProvider
        (
            string              path,
            SettingsScope       scopes,
            IEnumerable<string> keywords = null
        ) : base( path, scopes, keywords )
        {
        }

        /// <summary>
        /// アクティブになった時に呼び出されます
        /// </summary>
        public override void OnActivate( string searchContext, VisualElement rootElement )
        {
            var instance = GameObjectDuplicationHookerSetting.instance;

            instance.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;

            Editor.CreateCachedEditor( instance, null, ref m_editor );
        }

        /// <summary>
        /// GUI を描画する時に呼び出されます
        /// </summary>
        public override void OnGUI( string searchContext )
        {
            using var changeCheckScope = new EditorGUI.ChangeCheckScope();

            m_editor.OnInspectorGUI();

            if ( !changeCheckScope.changed ) return;

            GameObjectDuplicationHookerSetting.instance.Save();
        }

        //================================================================================
        // 関数(static)
        //================================================================================
        /// <summary>
        /// Project Settings にメニューを追加します
        /// </summary>
        [SettingsProvider]
        private static SettingsProvider CreateSettingProvider()
        {
            return new GameObjectDuplicationHookerSettingProvider
            (
                path: PATH,
                scopes: SettingsScope.Project
            );
        }
    }
}