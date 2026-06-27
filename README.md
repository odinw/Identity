# Identity


## 說明
此為最小化身份驗證範例，重點呈現 ASP.NET Core 身分驗證相關的實作概念與常用技術。

可用於微服務架構中，負責註冊、登入的相關功能。

## 核心技術

- .NET 10 / ASP.NET Core
- ASP.NET Core Identity
- Dependency Injection（DI）
- RESTful controllers（Identity/Controllers/）
- 密碼雜湊（使用 OWASP 推薦的 Argon2，具優秀的抗 GPU 破解能力)
- JWT 授權示範

## 主要功能

- 使用者註冊、登入與登出流程示意（Identity/Controllers/AccountController.cs）
- 測試示例（.http）

## 執行方式

1. 安裝需求：.NET 10 SDK
2. 使用 SQL/Identiry.sql 建立範例資料庫
3. 使用 Visual Studio 開啟 Identity.slnx 並執行 (依環境修改 appsettings 的資料庫字串)
4. 啟動後，使用 .http 來測試 API


## 重要檔案

- Identity/Controllers/AccountController.cs — 註冊/登入/登出範例
- Program.cs — DI、CORS、ExcptionHandler
- appsettings — 環境、資料庫連線設定、JWT設定

## 注意事項

- 僅做核心流程示範，後續可補上日誌、安全性等項目。