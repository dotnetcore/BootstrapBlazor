#!/bin/sh

if [ $1 = "" ]; then
    echo Please provider SolutionDir like: "$(MSBuildThisFileDirectory)"
    exit 1
fi

if [ $2 = "" ]; then
    echo Please provider TargetDir like: "$(TargetDir)publish"
    exit 1
fi

sourceDir=$2/wwwroot/
targetDir=$1../../dist/

echo "Ready to copy files to dist"

echo copy $sourceDir $targetDir 

\cp -rf $sourceDir $targetDir

\cp -f $targetDir/404.html $targetDir/index.html

echo Sync success!
