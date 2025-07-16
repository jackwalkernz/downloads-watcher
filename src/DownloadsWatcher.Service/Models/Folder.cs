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
            Extensions = ValidateExtensions(extensions) ? extensions : throw new ArgumentException("Extensions must be a non-empty list of valid file extensions.", nameof(extensions));
            Path = DerivePath(path);
        }

        /// <summary>
        /// Validates a list of file extensions to ensure they are in the correct format.
        /// </summary>
        /// <param name="extensions">A list of file extensions to validate. Each extension must start with a dot and contain alphanumeric
        /// characters only.</param>
        /// <returns><see langword="true"/> if all extensions are valid; otherwise, <see langword="false"/> if the list is null
        /// or empty.</returns>
        /// <exception cref="ArgumentException">Thrown if any extension in the list is null, empty, or does not match the required format.</exception>
        private bool ValidateExtensions(List<string> extensions)
        {
            string regexPattern = @"^\.[a-zA-Z0-9]+$";
            if(extensions == null || extensions.Count == 0)
            {
                return false;
            }
            if(extensions.Any(ext => string.IsNullOrWhiteSpace(ext)))
            {
                return false;
            }
            foreach (string ext in extensions)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(ext, regexPattern))
                {
                    return false;
                }
            }
            return true;
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
