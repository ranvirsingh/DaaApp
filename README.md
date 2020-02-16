# DaaApp

## Setup

Install dotnet core 3.1
https://dotnet.microsoft.com/download/visual-studio-sdks

```powershell
# Check dotnet version
dotnet --version
# Install Entity Framework core tools
dotnet tool install --global dotnet-ef
# Run migration to setup database
dotnet ef database update
```

## Optional Setup

https://sqlitebrowser.org/