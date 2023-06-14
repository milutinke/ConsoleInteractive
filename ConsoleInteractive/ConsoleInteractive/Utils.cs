using System;
using System.IO;
using System.Linq;

namespace ConsoleInteractive;

public static class Utils
{
    public static int GetDefaultBufferWidth => IsRunningInDocker ? 80 : Console.BufferWidth;

    private static bool IsRunningInDocker
    {
        get
        {
            // Check the DOTNET_RUNNING_IN_CONTAINER environment variable
            if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
                return true;

            // Check for the existence of the .dockerenv file
            if (File.Exists("/.dockerenv"))
                return true;

            // Check the /proc/1/cgroup file
            var cgroupPath = "/proc/1/cgroup";
            if (!File.Exists(cgroupPath)) return false;
            var lines = File.ReadLines(cgroupPath);
            return lines.Any(line => line.Contains("docker"));
        }
    }
}