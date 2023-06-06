#!/bin/zsh
dotnet build -c Release UltralightSharp.Core.WinX64 && dotnet build -c Release UltralightSharp.Core.LinuxX64 && dotnet build -c Release UltralightSharp.Core.OsxX64 && dotnet build -c Release UltralightSharp.Core && dotnet build -c Release UltralightSharp && dotnet build -c Release SilkNetSandbox
echo "Ready to build or test."
