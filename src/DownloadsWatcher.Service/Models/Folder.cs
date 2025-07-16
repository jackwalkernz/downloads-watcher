namespace DownloadsWatcher.Service.Models
{
    /// <summary>
    /// A folder represents
    /// </summary>
    public class Folder : IEquatable<Folder>
    {
        /// <summary>
        /// The name of the folder. is is used to identify the folder and can be used to create a path.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// The list of file extensions that are associated with this folder.
        /// </summary>
        public List<string> Extensions { get; init; } = new List<string>();

        /// <summary>
        /// The optional absolute path of the folder. If not provided, it will be derived from the Name and the user's Downloads directory.
        /// </summary>
        public string? Path { get; init; } = null;

        public Folder(string name, List<string> extensions, string? path = null)
        {
            Name = name;
            Extensions = extensions ?? new List<string>();
            Path = DerivePath(path);
        }

        /// <summary>
        /// This method derives the path of the folder based on the Name and DownloadsRooted properties.
        /// </summary>
        /// <returns>Either the folder located in the user's downloads, or provided path, assuming it is a valid Uri.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the provided path is invalid</exception>
        private string DerivePath(string? providedPath)
        {
            if (string.IsNullOrEmpty(providedPath))
            {
                return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", Name);
            }
            else
            {
                bool isValid = Uri.TryCreate(Name, UriKind.Absolute, out Uri? uri);
                return uri?.AbsolutePath ?? throw new InvalidOperationException("The provided path is not valid.");
            }
        }

        public bool Equals(Folder? other)
        {
            if (other == null) return false;
            return Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Folder folder)
            {
                return Equals(folder);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return string.GetHashCode(Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
