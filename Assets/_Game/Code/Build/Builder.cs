using System;
using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build.Profile;
using UnityEngine;

namespace GuidIssue.Build
{
  public static class Builder
  {
    private static BuildFlavor GetBuildFlavorFromEnvironment()
    {
      var buildFlavor = Environment.GetEnvironmentVariable("BUILD_FLAVOR");

      return buildFlavor == "Production"
        ? BuildFlavor.Production
        : BuildFlavor.Development;
    }

    private static string HumanBuildTarget(BuildTarget target) =>
      target switch
      {
        BuildTarget.StandaloneOSX => "StandaloneOSX",
        BuildTarget.StandaloneLinux64 => "StandaloneLinux64",
        BuildTarget.StandaloneWindows64 => "StandaloneWindows64",
        _ => throw new ArgumentOutOfRangeException(nameof(target), target, null),
      };

    /// <summary>
    ///   Called from TeamCity to start a build.
    /// </summary>
    [PublicAPI]
    public static void BuildCI()
    {
      var buildFlavor = GetBuildFlavorFromEnvironment();
      var activeTarget = EditorUserBuildSettings.activeBuildTarget;

      Debug.Log($"Building for {HumanBuildTarget(activeTarget)} with flavor {buildFlavor}");

      var args = new BuildArgs
      {
        ProfilePath = activeTarget switch
        {
          BuildTarget.StandaloneOSX => buildFlavor == BuildFlavor.Production
            ? Constants.BuildProfilePaths.MacOS.Production
            : Constants.BuildProfilePaths.MacOS.Development,
          BuildTarget.StandaloneLinux64 => buildFlavor == BuildFlavor.Production
            ? Constants.BuildProfilePaths.Linux.Production
            : Constants.BuildProfilePaths.Linux.Development,
          BuildTarget.StandaloneWindows64 => buildFlavor == BuildFlavor.Production
            ? Constants.BuildProfilePaths.Windows.Production
            : Constants.BuildProfilePaths.Windows.Development,
          _ => throw new ArgumentOutOfRangeException(),
        },
        ExecutableName = activeTarget == BuildTarget.StandaloneWindows64
          ? "GuidIssue.exe"
          : "GuidIssue",
      };

      Build(args);
    }

    private static void Build(BuildArgs args)
    {
      var buildProfile = AssetDatabase.LoadAssetAtPath<BuildProfile>(args.ProfilePath);

      if (buildProfile == null)
      {
        Debug.LogError($"Build profile {args.ProfilePath} not found.");
        return;
      }

      var options = new BuildPlayerWithProfileOptions
      {
        buildProfile = buildProfile, locationPathName = Path.Combine("Build", args.ExecutableName),
      };

      BuildPipeline.BuildPlayer(options);
    }

    private struct BuildArgs
    {
      public string ProfilePath { get; set; }
      public string ExecutableName { get; set; }
    }
  }
}
