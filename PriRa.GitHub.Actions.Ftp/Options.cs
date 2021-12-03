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

        private static string CopyRight = "";
        
        // Connection.

        /// <summary>
        /// Address for the FTP host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port for the FTP host.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Username for the FTP host
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password for the FTP host
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Ignore certificate errors
        /// </summary>
        public bool IgnoreCertificateErrors { get; set; }

        // Action.

        /// <summary>
        /// Copy files from localDir
        /// </summary>
        public bool CopyLocalDir { get; set; }

        /// <summary>
        /// Delete app_offline.htm from FTP host
        /// </summary>
        public bool DeleteFileAppOfflineHtm { get; set; }

        // Options.

        /// <summary>
        /// Local directory from which to upload
        /// </summary>
        public string LocalDir { get; set; }

        /// <summary>
        /// Folders to be ignored in both source and destination, separated by a pipe (|) character.
        /// Default = ".github|.well-known"
        /// </summary>
        public string SkipDirectories { get; set; }

                private static string CMDLINE_ARGUMENTS = @"host=s port=n username=s password=s ignoreCertificateErrors:s localDir=s copyLocalDir:s deleteFileAppOfflineHtm:s skipDirectories=s";

        public static Options CheckArguments(string[] args)
        {
            //Firt check if first argument = /?
            if (args.Length > 0)
            {
                if ((args[0] == "/?") || (args[0] == "-?"))
                {
                    Usage();
                    Environment.Exit(-1);
                }
            }

            try
            {
                var result = new Options();
                var options = new GetOptionsMixed(CMDLINE_ARGUMENTS);
                options.ParseOptions(ref args);
                while (options.NextOption())
                {
                    switch (options.OptionName)
                    {
                        case "host":
                            result.Host = options.OptionValue;
                            break;
                        case "port":
                            result.Port = int.Parse(options.OptionValue);
                            break;
                        case "username":
                            result.Username = options.OptionValue;
                            break;
                        case "password":
                            result.Password = options.OptionValue;
                            break;
                        case "ignoreCertificateErrors":
                            result.IgnoreCertificateErrors = string.IsNullOrEmpty(options.OptionValue) ||
                                                             bool.Parse(options.OptionValue);
                            break;
                        case "localDir":
                            result.LocalDir = options.OptionValue;
                            break;
                        case "copyLocalDir":
                            result.CopyLocalDir = string.IsNullOrEmpty(options.OptionValue) ||
                                                  bool.Parse(options.OptionValue);
                            break;
                        case "deleteFileAppOfflineHtm":
                            result.DeleteFileAppOfflineHtm = string.IsNullOrEmpty(options.OptionValue) ||
                                                             bool.Parse(options.OptionValue);
                            break;
                        case "skipDirectories":
                            result.SkipDirectories = options.OptionValue;
                            break;
                        case "h":
                            Usage();
                            Environment.Exit(-1);
                            break;
#if DEBUG
                        case "debug":
                            if (!System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Launch();
                            break;
#endif
                        default:
                            throw new GetOptionsUserException("Option: " + options.OptionName + " not implemented.");
                    }
                }
                return result;
            }
            catch
            {
                Usage();
                Console.WriteLine();
                throw;
            }
        }

        private static void Usage()
        {
            Console.WriteLine(@$"Copyright (C) {CopyRight} PriRa.GitHub.Actions.Ftp

  --host                       Required. Address for the FTP host.

  --port                       (Default: 21) Port for the FTP host.

  --username                   Required. Username for the FTP host.

  --password                   Required. Password for the FTP host.

  --ignoreCertificateErrors    (Default: false) Ignore certificate errors.

  --localDir                   (Default: ) Local directory from which to upload.

  --copyLocalDir               (Default: false) Copy files from localDir

  --deleteFileAppOfflineHtm    (Default: false) Delete app_offline.htm from FTP host

  --skipDirectories            (Default: .github|.well-known) Folders to be ignored in both source and destination, separated by a pipe (|) character.

  --help                       Display this help screen.

  --version                    Display version information.");
        }


    }

}