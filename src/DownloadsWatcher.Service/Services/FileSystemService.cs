using DownloadsWatcher.Service.Abstractions;

namespace DownloadsWatcher.Service.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly Serilog.ILogger _logger;

        public void AppendAllText(string path, string content)
        {
            try
            {
                System.IO.File.AppendAllText(path, content);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to append text to file: {Path}", path);
                throw;
            }
        }

        public void CopyFile(string sourcePath, string destinationPath)
        {
            try
            {
                System.IO.File.Copy(sourcePath, destinationPath, overwrite: true);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to copy file from {SourcePath} to {DestinationPath}", sourcePath, destinationPath);
                throw;
            }
        }

        public void CreateDirectory(string path)
        {
            try
            {
                System.IO.Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to create directory: {Path}", path);
                throw;
            }
        }

        public void DeleteDirectory(string path, bool recursive = false)
        {
            try
            {
                System.IO.Directory.Delete(path, recursive);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to delete directory: {Path}, Recursive: {Recursive}", path, recursive);
                throw;
            }
        }

        public void DeleteFile(string path)
        {
            try
            {
                System.IO.File.Delete(path);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to delete file: {Path}", path);
                throw;
            }
        }

        public bool DirectoryExists(string path)
        {
            try
            {
                return System.IO.Directory.Exists(path);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to check if directory exists: {Path}", path);
                throw;
            }
        }

        public bool FileExists(string path)
        {
            try
            {
                return System.IO.File.Exists(path);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to check if file exists: {Path}", path);
                throw;
            }
        }

        public IEnumerable<string> GetDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            try
            {
                return System.IO.Directory.GetDirectories(path, searchPattern, searchOption);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to get directories in path: {Path} with searchPattern: {SearchPattern} and searchOption: {SearchOption}", path, searchPattern, searchOption);
                throw;
            }
        }

        public IEnumerable<string> GetFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            try
            {
                return System.IO.Directory.GetFiles(path, searchPattern, searchOption);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to get files in path: {Path} with searchPattern: {SearchPattern} and searchOption: {SearchOption}", path, searchPattern, searchOption);
                throw;
            }
        }

        public void MoveFile(string sourcePath, string destinationPath)
        {
            try
            {
                System.IO.File.Move(sourcePath, destinationPath, overwrite: true);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to move file from {SourcePath} to {DestinationPath}", sourcePath, destinationPath);
                throw;
            }
        }

        public string ReadAllText(string path)
        {
            try
            {
                return System.IO.File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to read all text from file: {Path}", path);
                throw;
            }
        }

        public void WriteAllText(string path, string content)
        {
            try
            {
                System.IO.File.WriteAllText(path, content);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to write all text to file: {Path}", path);
                throw;
            }
        }
    }
}
