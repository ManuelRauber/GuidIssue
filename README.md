# Unity ShaderGraph Guid Issue

See: https://discussions.unity.com/t/unity-6-4s-shadergraph-breaks-all-builds-on-teamcity-due-to-error-cs0246-the-type-or-namespace-not-found/1717664

## Flow

1. Check out this repo.
2. Do NOT open the project in Unity.
3. Run this in the root of the repo to build: `/Applications/Unity/Hub/Editor/6000.4.3f1/Unity.app/Contents/MacOS/Unity -accept-apiupdate -batchmode -buildTarget StandaloneOSX -nographics -executeMethod GuidIssue.Build.Builder.BuildCI -quit -logFile -`

Search the log for: `error CS0246: The type or namespace name 'GUID'`
It should appear twice, as described in the link above.

The issue only happens on the first build in a freshly checked out git repo. You can use `git clean -xdf` after a build to remove all ignored files and bring the repo back into a freshly checked out copy.
