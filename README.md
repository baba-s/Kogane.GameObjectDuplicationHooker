# UniGameObjectDuplicationHooker

ゲームオブジェクトの複製時に名前の末尾に数値を付けず、かつ transform のプロパティに誤差が生じないようにするエディタ拡張  

* 標準の Duplicate コマンドを上書きするため、このエディタ拡張を Unity プロジェクトに追加するだけで使用できます  
* 複数のゲームオブジェクトの複製に対応しています  
* Undo に対応しています  
* プレハブのインスタンスの複製に対応しています  
* 複製時にゲームオブジェクトの名前の文字列末尾の数字をインクリメントする機能もあります

## 使用例

### デフォルト

![image (14)](https://user-images.githubusercontent.com/61863367/81248830-393c5b00-9058-11ea-97d4-cd6f367a5e14.gif)

### UniGameObjectDuplicationHooker

![image (13)](https://user-images.githubusercontent.com/61863367/81248837-3b9eb500-9058-11ea-909a-c92f2d94d1ab.gif)

![image (17)](https://user-images.githubusercontent.com/61863367/81280999-4119f080-9094-11ea-9332-b99a92b669fc.gif)

## 設定

![2020-05-07_185524](https://user-images.githubusercontent.com/61863367/81281094-5db62880-9094-11ea-8633-0ba6ed0a34e5.png)

Preferences から設定を変更できます  

|項目|内容|
|:--|:--|
|Enabled|機能を有効化します（デフォルトは OFF）|
|Enabled Serial Number|複製時に名前の末尾の数字をインクリメントします（デフォルトは ON）|