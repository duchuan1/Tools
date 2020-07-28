chdir /d %cd%
cd 1.2.1
del *.nupkg
NuGet.exe pack
echo "build success"
NuGet.exe push -Source \\192.168.0.150\Nuget *.nupkg
echo "publish success"
pause
