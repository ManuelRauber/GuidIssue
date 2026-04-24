namespace GuidIssue.Build
{
  public static class Constants
  {
    public static class BuildProfilePaths
    {
      private const string BasePath = "Assets/_Game/Build Profiles/";

      public static class MacOS
      {
        public const string Development = BasePath + "macOS - Development.asset";
        public const string Production = BasePath + "macOS - Production.asset";
      }
      
      public static class Linux
      {
        public const string Development = BasePath + "Linux - Development.asset";
        public const string Production = BasePath + "Linux - Production.asset";
      }
      
      public static class Windows
      {
        public const string Development = BasePath + "Windows - Development.asset";
        public const string Production = BasePath + "Windows - Production.asset";
      }
    }
  }
}
