# C# / ASP.NET Core 学習ポートフォリオ

このリポジリは、C#の基礎から実務的なWebサイト開発までを体系的に学んだ成果をまとめたポートフォリオです。

## 🚀 概要

AI（GitHub Copilot / Gemini 等）を活用し、プロンプトエンジニアリングを駆使して一人で学習カリキュラムを完結させました。
コンソールアプリケーションによる基礎文法の習得から、Entity Framework Core を用いたデータベース連携、Razor Pages によるWebアプリケーション開発まで、17以上のチャレンジ項目をクリアしています。

## 🛠 技術スタック

- **言語**: C# 13.0
- **フレームワーク**: ASP.NET Core 9.0 (Razor Pages)
- **データベース**: SQLite / Entity Framework Core
- **フロントエンド**: Bootstrap 5.3 / Bootstrap Icons
- **ツール**: .NET CLI / EF Core Tools

## 💻 主な成果物: TodoWeb (v1.1)

実用的なToDo管理Webアプリケーションです。

### 主な機能
- **CRUD操作**: タスクの追加、表示、完了設定、削除。
- **データベース永続化**: SQLiteを使用し、アプリ終了後もデータを保持。
- **優先度管理**: 3段階（高・中・低）の優先度設定と、優先度順の自動ソート。
- **レスポンシブデザイン**: BootstrapによるモダンでクリーンなUI（スマホ対応）。

## 📝 学習の足跡 (CHALLENGES.md)

[CHALLENGES.md](./CSharpPortfolio/CHALLENGES.md) には、学習過程で解いてきた全ての課題とヒントが記録されています。
基礎から応用へとステップアップしていく様子をご覧いただけます。

## 🏃 実行方法

1. [TodoWeb](./CSharpPortfolio/TodoWeb) ディレクトリへ移動します。
   ```bash
   cd CSharpPortfolio/TodoWeb
   ```
2. アプリケーションを実行します。
   ```bash
   dotnet watch run
   ```
3. ブラウザで `http://localhost:5000` (または表示されたURL) にアクセスしてください。

---
© 2026 C# Portfolio Project
