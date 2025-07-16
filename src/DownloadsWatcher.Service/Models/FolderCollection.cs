using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadsWatcher.Service.Models
{
    /// <summary>
    /// This represents a collection of folders that are being watched for file changes.
    /// </summary>
    public class FolderCollection
    {
        public List<Folder> Folders { get; init; } = new List<Folder>();

        public FolderCollection(List<Folder> folders)
        {
            Folders = folders ?? new List<Folder>();
        }

        public void UpdateFolder(string name, List<string> extensions, string? path = null)
        {
        }
    }
}
