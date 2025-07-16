using DownloadsWatcher.Service.Abstractions;
using DownloadsWatcher.Service.Models;
using System.Text.Json;

namespace DownloadsWatcher.Service.Services
{
    /// <summary>
    /// This service is responsible for reading the settings from a JSON file.
    /// </summary>
    public class SettingsConfigurationService
    {
        private readonly Serilog.ILogger _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly IFileSystemService _fileSystemService;

        public SettingsConfigurationService(Serilog.ILogger logger, JsonSerializerOptions jsonSerializerOptions, IFileSystemService fileSystemService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jsonSerializerOptions = jsonSerializerOptions ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        }

        /// <summary>
        /// This method returns the path to the settings file.
        /// </summary>
        /// <returns></returns>
        private string GetSettingsFilePath()
        {
            string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(applicationDataPath, "DownloadsWatcher", "settings.json");
        }

        /// <summary>
        /// Retrieves the collection of folders from the settings file.
        /// </summary>
        /// <remarks>This method reads the settings file specified by the application configuration and
        /// deserializes its content into a <see cref="FolderCollection"/> object. If the settings file is not found or
        /// cannot be deserialized, an exception is thrown.</remarks>
        /// <returns>A <see cref="FolderCollection"/> object representing the folders defined in the settings file.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the settings file does not exist at the specified path.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the settings file content cannot be deserialized into a <see cref="FolderCollection"/>.</exception>
        public FolderCollection GetFolderCollection()
        {
            string settingsFilePath = GetSettingsFilePath();
            try
            {
                if (!_fileSystemService.FileExists(settingsFilePath))
                {
                    _logger.Warning("Settings file at path {Path} not found", settingsFilePath);
                    throw new FileNotFoundException("The settings file does not exist.", settingsFilePath);
                }
                string json = _fileSystemService.ReadAllText(settingsFilePath);
                FolderCollection? folderCollection = JsonSerializer.Deserialize<FolderCollection>(json, _jsonSerializerOptions);
                if (folderCollection == null)
                {
                    throw new InvalidOperationException("Could not deserialize the folder collection");
                }
                return folderCollection;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates the settings configuration file with the current folder collection.
        /// </summary>
        /// <remarks>This method serializes the current folder collection to JSON and writes it to the
        /// settings configuration file. If the directory for the settings file does not exist, it will be created. If
        /// an error occurs during the process, the method logs the error and returns <see langword="false"/>.</remarks>
        /// <param name="newCollection">The new collection instance that has been updated</param>
        /// <returns><see langword="true"/> if the settings configuration file was successfully updated; otherwise, <see
        /// langword="false"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the settings file path is invalid.</exception>
        public bool UpdateSettingsConfigurationFile(FolderCollection newCollection)
        {
            
            string settingsFilePath = GetSettingsFilePath();
            string directoryPath = Path.GetDirectoryName(settingsFilePath) ?? throw new InvalidOperationException("Settings file path is invalid.");
            try
            {
                ArgumentNullException.ThrowIfNull(newCollection, nameof(newCollection));
                if (!_fileSystemService.DirectoryExists(directoryPath))
                    _fileSystemService.CreateDirectory(directoryPath);
                string json = JsonSerializer.Serialize(newCollection, _jsonSerializerOptions);
                _fileSystemService.WriteAllText(settingsFilePath, json);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return false;
            }
        }
    }
}
