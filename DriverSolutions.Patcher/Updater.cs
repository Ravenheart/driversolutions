using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.Patcher
{
    public class Updater
    {
        public event EventHandler<ProgressChangedEventArgs> OnProgress;
        public event EventHandler<EventArgs> OnUpdateComplete;
        public event EventHandler<EventArgs> OnBuildComplete;

        private void Progress(string status, int percent)
        {
            if (OnProgress != null)
                OnProgress(this, new ProgressChangedEventArgs(percent, status));
        }
        private void Progress(string status, int iteration, int total)
        {
            int percent = (int)(((float)iteration / (float)total) * 100.0f);
            Progress(status, percent);
        }
        private void UpdateComplete()
        {
            if (OnUpdateComplete != null)
                OnUpdateComplete(this, EventArgs.Empty);
        }
        private void BuildComplete()
        {
            if (OnBuildComplete != null)
                OnBuildComplete(this, EventArgs.Empty);
        }


        public async Task Update()
        {
            await Task.Run(() =>
                {
                    using (WebClient web = new WebClient())
                    {
                        string hashes = web.DownloadString(Options.UpdateServer + "hash.txt");
                        var onlineFiles = HashFile.ParseList(hashes);
                        string[] localFiles = Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.TopDirectoryOnly);

                        for (int i = 0; i < localFiles.Length; i++)
                        {
                            string filePath = localFiles[i];
                            string file = Path.GetFileName(filePath);

                            if (file.ToUpperInvariant() == "CONFIG.TXT" || file.ToUpperInvariant() == "DRIVERSOLUTIONS.PATCHER.EXE")
                                continue;

                            Progress("Checking: " + file, 0);

                            var check = onlineFiles.Where(f => f.FileName == file).FirstOrDefault();
                            if (check == null)
                            {
                                //delete
                                DeleteFile(filePath);
                            }
                            else
                            {
                                //hash check
                                byte[] data = File.ReadAllBytes(filePath);
                                string hash = Tools.Hash(data);
                                if (check.Hash == hash)
                                    onlineFiles.Remove(check);
                            }
                        }

                        for (int i = 0; i < onlineFiles.Count; i++)
                        {
                            var file = onlineFiles[i].FileName;
                            Progress("Patching: " + file, i, onlineFiles.Count);

                            DeleteFile(file);
                            byte[] data = web.DownloadData(Options.UpdateServer + file + ".gzip");
                            Tools.Decompress(data, file);
                        }

                        UpdateComplete();
                    }
                });
        }

        public async Task Build()
        {
            await Task.Run(() =>
                {
                    string patchFolder = Path.Combine(Environment.CurrentDirectory, "update");
                    if (Directory.Exists(patchFolder))
                        Directory.Delete(patchFolder, true);

                    Directory.CreateDirectory(patchFolder);

                    StringBuilder bld = new StringBuilder();
                    string[] files = Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < files.Length; i++)
                    {
                        string filePath = files[i];
                        string file = Path.GetFileName(filePath);

                        if (file.ToUpperInvariant() == "CONFIG.TXT" || file.ToUpperInvariant() == "DRIVERSOLUTIONS.PATCHER.EXE")
                            continue;

                        Progress("Building: " + file, i, files.Length);
                        string fileOutput = Path.Combine(patchFolder, file + ".gzip");
                        byte[] uncompressed = File.ReadAllBytes(filePath);
                        Tools.Compress(uncompressed, fileOutput);

                        bld.AppendLine(file + "|" + Tools.Hash(uncompressed));
                    }

                    File.WriteAllText(Path.Combine(patchFolder, "hash.txt"), bld.ToString());

                    BuildComplete();
                });
        }

        private void DeleteFile(string filePath)
        {
            try
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
            }
            catch { }
        }

        private class HashFile
        {
            public string FileName { get; set; }
            public string Hash { get; set; }

            public HashFile(string hashFile)
            {
                string[] temp = hashFile.Split('|');
                this.FileName = temp[0];
                this.Hash = temp[1];
            }

            public static List<HashFile> ParseList(string hashList)
            {
                string[] data = hashList.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                List<HashFile> result = new List<HashFile>(data.Length);
                for (int i = 0; i < data.Length; i++)
                {
                    result.Add(new HashFile(data[i]));
                }

                return result;
            }
        }
    }
}
