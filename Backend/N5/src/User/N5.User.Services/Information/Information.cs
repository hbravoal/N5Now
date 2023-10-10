using System.Diagnostics.CodeAnalysis;

namespace N5.User.Services.Information
{
    /// <summary>
    /// Developer information.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProjectInformation 
    {

        public ProjectInformation()
        {
            this.Name = "N5 App.";
            this.Version = "1.0.0.0";
            this.TypeVersion = "Release";
            this.Copyright = "Henry Bravo.https://github.com/hbravoal";
        }

        public string Name { get; }
        public string Version { get; }
        public string TypeVersion { get; }
        public string Copyright { get; }
    }
}