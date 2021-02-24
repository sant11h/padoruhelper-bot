SET ASPNETCORE_ENVIRONMENT=Development
SET LAUNCHER_PATH=bin\Debug\netcoreapp3.1\PadoruHelperBotApp.exe
cd /d "C:\Program Files\IIS Express\"
iisexpress.exe /config:"D:\Santi\Documents\GitHub\padoruhelper-bot\.vs\PadoruHelperBot\config\applicationhost.config" /site:"PadoruHelperBotApp" /apppool:"PadoruHelperBotApp AppPool"
