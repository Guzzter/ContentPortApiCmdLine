using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tridion.ContentManager.ImportExport;
using Tridion.ContentManager.ImportExport.Client;

namespace ContentPortApi
{
    public class Porter : IDisposable
    {
        ImportExportServiceClient _client = null;

        public Porter()
        {
            _client = new ImportExportServiceClient("basicHttp_2013");
        }
        
        /// <summary>
        /// Overloaded function for a single selection
        /// </summary>
        /// <param name="singleSelection"></param>
        /// <returns>A tridion process Id</returns>
        public string ExportPackage(Selection singleSelection)
        {
            return ExportPackage(new Selection[] { singleSelection });
        }

        /// <summary>
        /// Create zip package on the CMS server with the selected items
        /// </summary>
        /// <param name="selection">selection of items that need to be exported</param>
        /// <param name="loglevel">Optional loglevel</param>
        /// <param name="bpm">Optional blueprint mode for exporting</param>
        /// <returns>A tridion process Id</returns>
        public string ExportPackage(Selection[] selection, LogLevel loglevel = LogLevel.Normal, BluePrintMode bpm = BluePrintMode.ExportSharedItemsFromOwningPublication)
        {
            ExportInstruction exportInstruction = new ExportInstruction()
            {
                BluePrintMode = bpm,
                LogLevel = loglevel
            };

            string processId = _client.StartExport(selection, exportInstruction);

            return processId;
        }

        public string StartImport(string serverPackageName, ImportInstruction importInstruction)
        {
            string processId = _client.StartImport(serverPackageName, importInstruction);
            return processId;
        }

        /// <summary>
        /// Uploads a zip package from local machine to Tridion CMS Server
        /// </summary>
        /// <param name="packageLocation">package location and filename on the local machine</param>
        /// <returns>string containing uploaded package name on the server.</returns>
        public string UploadPackage(string packageLocation)
        {
            var uploadClient = new ImportExportStreamUploadClient();

            using (var packageStream = File.OpenRead(packageLocation))
            {
                return uploadClient.UploadPackageStream(packageStream);
            }
        }

        /// <summary>
        /// Helper method to check if process is finished on the server
        /// </summary>
        /// <param name="processId">Server process ID reference</param>
        /// <returns>The end state of the process (finished/aborted/aborted by user</returns>
        public ProcessState WaitForProcessFinish(string processId)
        {
            Console.Write("Waiting for process to finish on server: .");
            do
            {
                Thread.Sleep(1000);
                ProcessState? processState = _client.GetProcessState(processId);

                if (processState == ProcessState.Finished ||
                    processState == ProcessState.Aborted ||
                    processState == ProcessState.AbortedByUser)
                {
                    Console.WriteLine(Environment.NewLine + "Done!");
                    return processState.Value;
                }
                else
                {
                    Console.Write(".");
                }
            }
            while (true);
            
        }

        #region Downloaders
        /// <summary>
        /// Downloads the import/export log file to local machine
        /// </summary>
        /// <param name="processId">Server process ID reference</param>
        /// <param name="logLocation">Location where the zip file needs to be saved (overwrites existing zipfiles)</param>
        public void DownloadLogFile(string processId, string logLocation)
        {
            // when log is specified: download the log
            if (!string.IsNullOrWhiteSpace(processId) && !string.IsNullOrWhiteSpace(logLocation))
            {
                using (var downloadClient = new ImportExportStreamDownloadClient())
                {
                    using (var packageStream = downloadClient.DownloadProcessLogFile(processId, deleteFromServerAfterDownload: true))
                    {
                        using (var fileStream = File.Create(logLocation))
                        {
                            packageStream.CopyTo(fileStream);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Downloads the exported zip package to local machine
        /// </summary>
        /// <param name="processId">Server process ID reference</param>
        /// <param name="packageLocation">Location where the zip file needs to be saved (overwrites existing zipfiles)</param>
        public void DownloadPackage(string processId, string packageLocation)
        {
            using (var downloadClient = new ImportExportStreamDownloadClient())
            {
                using (var packageStream = downloadClient.DownloadPackage(processId, deleteFromServerAfterDownload: true))
                {
                    using (var fileStream = File.Create(packageLocation))
                    {
                        packageStream.CopyTo(fileStream);
                    }
                }
            }
        }
        #endregion


        public void Dispose()
        {
            if (_client != null)
            {
                _client.Close();
                _client = null;
            }
        }

        
    }
}
