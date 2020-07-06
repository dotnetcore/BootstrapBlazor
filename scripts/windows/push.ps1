# 判断是否设置 NugetKey
if ($env:NugetKey -eq $null)
{
    echo "请设置环境变量 NugetKey 上传秘钥"
    exit
}

# 判断是否设置 NugetSource
if ($env:NugetSource -eq $null)
{
    echo "请设置环境变量 NugetSource 上传 Nuget 地址"
    exit
}

# 获取当前 PM 中选中的项目
$currentProj = Get-Project

# 获取当前项目所在文件夹
$projectFolder = Split-Path -parent $currentProj.FullName

# 获取当前项目打包文件输出目录
$outFolder = -Join($projectFolder, "\bin\Release\")

# 删除 nupkg 文件防止有老文件干扰
Remove-Item $outFolder*.nupkg -recurse

# 获取打包文件不包含版本号的文件名
$pkgName = -Join($outFolder, $currentProj.Name)

# 带版本号的文件名
$pkgFileName = -Join($pkgName, "*.nupkg")

$spkgFileName = -Join($pkgName, "*.snupkg")

# 开始打包
dotnet pack -c Release $currentProj.FullName

# 判断是否打包成功
$done = Test-Path $pkgFileName

if ($done)
{
    # 开始上传
    dotnet nuget push -k $env:NugetKey -s $env:NugetSource $pkgFileName --skip-duplicate --no-symbols $spkgFileName
}
else 
{
    Write-Host "打包失败"
}
