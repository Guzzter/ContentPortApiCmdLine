using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tridion.ContentManager.ImportExport;

namespace ContentPortApi
{
    public class BatchPorter
    {
        private ExportConfig _ec = null;
        public BatchPorter(string configFile)
        {
            _ec = new ExportConfig(configFile).Read();
        }

        public void Go()
        {
            //depending on config do import/export
            using (var porter = new Porter())
            {
                DoExport(porter);
            }
        }

        private void DoExport(Porter port)
        {
            LogLevel ll = _ec.General.LogLevelAsEnum();
            BluePrintMode bpm = _ec.General.BluePrintModeAsEnum();

            foreach (var p in _ec.Package)
            {
                List<Selection> selections = new List<Selection>();
                if (p.ItemsSelection != null)
                {
                    selections.Add(new ItemsSelection(p.ItemsSelection));
                }
                if (p.TaxonomiesSelection != null)
                {
                    foreach (var ts in p.TaxonomiesSelection)
                    {
                        selections.Add(new TaxonomiesSelection(ts));
                    }
                }
                if (p.SubtreeSelection != null)
                {
                    foreach (var s in p.SubtreeSelection)
                    {
                        selections.Add(new SubtreeSelection(s, true));
                    }
                }
                //Selection selection = new ItemsSelection(new string[] { "tcm:4-134-8" });
                string processId = port.ExportPackage(selections.ToArray(), ll, bpm);
                ProcessState processState = port.WaitForProcessFinish(processId);

                string packagefile = Path.Combine(_ec.General.ExportDirectory, p.ZipFile);
                string logfile = packagefile.Replace(".zip", ".txt");
                if (processState == ProcessState.Finished)
                {
                    port.DownloadPackage(processId, packagefile);

                }
                if (ll != LogLevel.None)
                {
                    port.DownloadLogFile(processId, logfile);
                }

            }

        }

        private void DoImport(Porter port)
        {
            string serverPackageName = port.UploadPackage(@"C:\Packages\ParentPublication.zip");

            string processId = port.StartImport(serverPackageName, new ImportInstruction());

            var processState = port.WaitForProcessFinish(processId);

            if (processState == ProcessState.Finished)
            {
                port.DownloadPackage(processId, @"C:\Packages\ChildPublication.zip");
                port.DownloadLogFile(processId, @"C:\Packages\ChildPublication_import.txt");
            }
        }
    }
}
