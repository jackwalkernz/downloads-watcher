using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadsWatcher.Service.Abstractions
{
    /// <summary>
    /// Provides an interface for file system operations, including directory and file management.
    /// </summary>
    /// <remarks>This interface defines methods for checking the existence of files and directories, creating
    /// and deleting them, as well as moving, copying, and retrieving file and directory information. It also includes
    /// methods for reading and writing text to files. Implementations of this interface should handle necessary
    /// permissions and path validations.</remarks>
    public interface IFileSystemService
    {
        /// <summary>
        /// Determines whether the specified directory exists.
        /// </summary>
        /// <param name="path">The path to the directory to check. This can be a relative or absolute path.</param>
        /// <returns><see langword="true"/> if the directory exists; otherwise, <see langword="false"/>.</returns>
        public bool DirectoryExists(string path);

        /// <summary>
        /// Determines whether the specified file exists at the given path.
        /// </summary>
        /// <param name="path">The path to the file to check for existence. Cannot be null or empty.</param>
        /// <returns><see langword="true"/> if the file exists at the specified path; otherwise, <see langword="false"/>.</returns>
        public bool FileExists(string path);

        /// <summary>
        /// Creates a new directory at the specified path.
        /// </summary>
        /// <param name="path">The path where the new directory will be created. This path cannot be null or empty.</param>
        public void CreateDirectory(string path);

        /// <summary>
        /// Deletes the specified directory.
        /// </summary>
        /// <remarks>Use this method to remove directories from the file system. If <paramref
        /// name="recursive"/> is set to <see langword="true"/>, all subdirectories and files within the specified
        /// directory will also be deleted.</remarks>
        /// <param name="path">The path of the directory to delete. This cannot be null or empty.</param>
        /// <param name="recursive"><see langword="true"/> to delete the directory and its contents recursively; otherwise, <see
        /// langword="false"/> to delete only the directory if it is empty.</param>
        public void DeleteDirectory(string path, bool recursive = false);

        /// <summary>
        /// Deletes the specified file from the file system.
        /// </summary>
        /// <param name="path">The path of the file to be deleted. Must not be null or empty.</param>
        public void DeleteFile(string path);

        /// <summary>
        /// Moves a file from the specified source path to the specified destination path.
        /// </summary>
        /// <remarks>This method moves the file to the new location and deletes the original file. Ensure
        /// that the application has the necessary permissions to access both the source and destination
        /// paths.</remarks>
        /// <param name="sourcePath">The path of the file to move. This path must be a valid file path and cannot be null or empty.</param>
        /// <param name="destinationPath">The path to move the file to. This path must be a valid file path and cannot be null or empty.</param>
        public void MoveFile(string sourcePath, string destinationPath);

        /// <summary>
        /// Copies a file from the specified source path to the specified destination path.
        /// </summary>
        /// <remarks>This method overwrites the file at the destination path if it already exists. Ensure
        /// that the application has the necessary permissions to access both the source and destination
        /// paths.</remarks>
        /// <param name="sourcePath">The path of the file to be copied. Must be a valid file path and cannot be null or empty.</param>
        /// <param name="destinationPath">The path where the file will be copied to. Must be a valid file path and cannot be null or empty.</param>
        public void CopyFile(string sourcePath, string destinationPath);

        /// <summary>
        /// Retrieves the names of files from a specified directory that match a given search pattern.
        /// </summary>
        /// <param name="path">The directory path to search for files. This cannot be null or empty.</param>
        /// <param name="searchPattern">The search string to match against the names of files in the directory. The default is "*", which returns
        /// all files.</param>
        /// <param name="searchOption">Specifies whether to search only the top directory or all subdirectories. The default is <see
        /// cref="SearchOption.TopDirectoryOnly"/>.</param>
        /// <returns>An enumerable collection of file names that match the search pattern in the specified directory.</returns>
        public IEnumerable<string> GetFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Retrieves the names of subdirectories that match the specified search pattern in a specified path.
        /// </summary>
        /// <param name="path">The path to search for directories. This cannot be null or an empty string.</param>
        /// <param name="searchPattern">The search string to match against the names of directories. The default value is "*", which returns all
        /// directories.</param>
        /// <param name="searchOption">Specifies whether to search only the top directory or all subdirectories. The default is <see
        /// cref="SearchOption.TopDirectoryOnly"/>.</param>
        /// <returns>An enumerable collection of directory names that match the specified search pattern and option.</returns>
        public IEnumerable<string> GetDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Reads all text from the specified file.
        /// </summary>
        /// <param name="path">The file path from which to read the text. The path must not be null or empty.</param>
        /// <returns>A string containing all the text from the file.</returns>
        public string ReadAllText(string path);

        /// <summary>
        /// Writes the specified text to a file at the given path, overwriting any existing content.
        /// </summary>
        /// <remarks>This method creates a new file, writes the specified content to the file, and then
        /// closes the file. If the target file already exists, it is overwritten. Ensure that the path is accessible
        /// and that the application has the necessary permissions to write to the specified location.</remarks>
        /// <param name="path">The file path where the text will be written. Must be a valid file path and cannot be null or empty.</param>
        /// <param name="content">The text content to write to the file. If null, the file will be created or truncated to zero length.</param>
        public void WriteAllText(string path, string content);

        /// <summary>
        /// Appends the specified text to the file at the given path, creating the file if it does not exist.
        /// </summary>
        /// <remarks>This method opens the file, appends the specified text, and then closes the file. If
        /// the file does not exist, it is created.</remarks>
        /// <param name="path">The path to the file where the text will be appended. This cannot be null or empty.</param>
        /// <param name="content">The text content to append to the file. If the file does not exist, it will be created with this content.</param>
        public void AppendAllText(string path, string content);

    }
}
