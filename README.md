# Kogane Game Object Duplication Hooker

ゲームオブジェクトの複製時に名前の末尾に数値を付けず、かつ transform のプロパティに誤差が生じないようにするエディタ拡張

* 標準の Duplicate コマンドの挙動を上書きします
* 複数のゲームオブジェクトの複製に対応しています
* Undo に対応しています
* プレハブのインスタンスの複製に対応しています
* 複製時にゲームオブジェクトの名前の文字列末尾の数字をインクリメントする機能もあります

## 使用例

### デフォルト

![image (14)](https://user-images.githubusercontent.com/61863367/81248830-393c5b00-9058-11ea-97d4-cd6f367a5e14.gif)

### Kogane Game Object Duplication Hooker

![image (13)](https://user-images.githubusercontent.com/61863367/81248837-3b9eb500-9058-11ea-909a-c92f2d94d1ab.gif)

![image (17)](https://user-images.githubusercontent.com/61863367/81280999-4119f080-9094-11ea-9332-b99a92b669fc.gif)

## 設定

![2022-08-28_144729](https://user-images.githubusercontent.com/6134875/187059642-04beccb3-ba3a-408f-b297-1497719a92f4.png)

Project Settings から設定を変更できます

|項目|内容|
|:--|:--|
|Is Enable|機能を有効化します（デフォルトは OFF）|
|Is Enable Serial Number|複製時に名前の末尾の数字をインクリメントします（デフォルトは ON）|
