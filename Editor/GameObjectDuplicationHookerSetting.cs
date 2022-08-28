using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    /// <summary>
    /// 独自のゲームオブジェクト複製機能の設定を管理するクラス
    /// </summary>
    [FilePath( "UserSettings/Kogane/GameObjectDuplicationHooker.asset", FilePathAttribute.Location.ProjectFolder )]
    internal sealed class GameObjectDuplicationHookerSetting : ScriptableSingleton<GameObjectDuplicationHookerSetting>
    {
        //================================================================================
        // 変数(SerializeField)
        //================================================================================
        [SerializeField] private bool m_isEnable;                    // 有効な場合 true
        [SerializeField] private bool m_isEnableSerialNumber = true; // 文字列末尾の数値の連番対応を有効化する場合 true

        //================================================================================
        // プロパティ
        //================================================================================
        public bool IsEnable             => m_isEnable;
        public bool IsEnableSerialNumber => m_isEnableSerialNumber;

        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// 保存します
        /// </summary>
        public void Save()
        {
            Save( true );
        }
    }
}