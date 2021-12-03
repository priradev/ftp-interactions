using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CommandLine;
using FluentFTP;

namespace PriRa.GitHub.Actions.Ftp
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            try
            {
                await Parser.Default.ParseArguments<Options>(args)
                    .WithParsedAsync(Run);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit(1);
            }
        }

        private static async Task Run(Options options)
        {
            // Get source files info.
            IEnumerable<Item> sourceFiles = new List<Item>();
            if (!string.IsNullOrEmpty(options.LocalDir))
            {
                Console.WriteLine("...Finding source files...");
                var source = Directory.GetFiles(options.LocalDir, "*", SearchOption.AllDirectories)
                                      .Select(src => new Item(src, options.LocalDir));
                source = Filter(source, options.SkipDirectories);
                if (source == null || !source.Any())
                {
                    Console.WriteLine("> No files found");
                    throw new Exception("No files found");
                }
                else
                {
                    sourceFiles = source;
                }
            }
            // create an FTP client and specify the host, username and password
            // (delete the credentials to use the "anonymous" account)
            var credentials = new NetworkCredential(options.Username, options.Password);
            using (var client = new FtpClient(options.Host, options.Port, credentials))
            {
                if (options.IgnoreCertificateErrors)
                {
                    client.ValidateCertificate += Client_ValidateCertificate;
                }

                Console.WriteLine("...Connecting to remote server...");
                await client.ConnectAsync();

                if (options.DeleteFileAppOfflineHtm)
                {
                    await DeleteFile(client, "app_offline.htm");
                }
                else if (!string.IsNullOrEmpty(options.LocalDir))
                {
                    foreach (var sourceFile in sourceFiles)
                    {
                        // upload a file
                        Console.WriteLine($"...upload: {sourceFile.Name}");
                        await client.UploadFileAsync(sourceFile.FullPath, sourceFile.Name);
                    }
                }
                else
                {
                    Console.WriteLine("Nothing to do...");
                }
            }

            Console.WriteLine("Complete!");
        }

        private static async Task DeleteFile(FtpClient client, string filename)
        {
            if (await client.FileExistsAsync(filename))
            {
                Console.WriteLine($"...delete: {filename}");
                await client.DeleteFileAsync(filename);
            }
            else
            {
                Console.WriteLine($"...can't delete NON-existing: {filename}");
            }
        }

        private static void Client_ValidateCertificate(FtpClient control, FtpSslValidationEventArgs e)
        {
            e.Accept = true;
        }

        #region " Filter "

        private static IEnumerable<Item> Filter(IEnumerable<Item> files, string directories)
        {
            var names = directories?.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (names?.Any() == true)
            {
                return files.Where(file => !DirectoryFound(file.Directory, names));
            }
            return files;
        }

        private static bool DirectoryFound(string haystack, IEnumerable<string> needles)
        {
            var haystacks = haystack.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var remaining = haystacks.Except(needles, StringComparer.OrdinalIgnoreCase);
            return remaining.Count() < haystacks.Count();
        }

        #endregion
    }
}
