using System;
using CommandLine;

namespace PriRa.GitHub.Actions.Ftp
{
    public class Options
    {
        public Options()
        {
            Host = string.Empty;
            Port = 21;
            Username = string.Empty;
            Password = string.Empty;
            LocalDir = string.Empty;
            IgnoreCertificateErrors = false;
            SkipDirectories = string.Empty;
        }

        // Connection.

        [Option("host",
                Required = true,
                HelpText = "Address for the FTP host.")]
        public string Host { get; set; }

        [Option("port",
                Required = false, Default = 21,
                HelpText = "Port for the FTP host.")]
        public int Port { get; set; }

        [Option("username",
                Required = true,
                HelpText = "Username for the FTP host.")]
        public string Username { get; set; }

        [Option("password",
                Required = true,
                HelpText = "Password for the FTP host.")]
        public string Password { get; set; }

        [Option("ignoreCertificateErrors",
                Required = false, Default = false,
                HelpText = "Ignore certificate errors.")]
        public bool IgnoreCertificateErrors { get; set; }

        // Action.

        [Option("localDir",
                Required = false, Default = "",
                HelpText = "Local directory from which to upload.")]
        public string LocalDir { get; set; }

        [Option("deleteFileAppOfflineHtm",
                Required = false, Default = false,
                HelpText = "Delete app_offline.htm from FTP host")]
        public bool DeleteFileAppOfflineHtm { get; set; }

        // Options.

        [Option("skipDirectories",
                Required = false, Default = ".github|.well-known",
                HelpText =
                        "Folders to be ignored in both source and destination, separated by a pipe (|) character.")]
        public string SkipDirectories { get; set; }

    }
}