namespace DownloadsWatcher.Tests.ModelTests
{
    [Category("Unit")]
    [TestFixture]
    internal class Folder_Tests
    {
        [Test]
        public void Folder_Equals_ShouldBeEqual_WhenFoldersShareTheSameName()
        {
            string folderName = "TestFolder";
            Service.Models.Folder folder1 = new Service.Models.Folder(folderName, new List<string> { ".txt" });
            Service.Models.Folder folder2 = new Service.Models.Folder(folderName, new List<string> { ".txt" });

            Assert.That(folder1, Is.EqualTo(folder2));
        }

        [Test]
        public void Folder_Equals_ShouldBeEqual_WhenFoldersHaveSameNameAndExtensions()
        {
            Service.Models.Folder folder1 = new Service.Models.Folder("TestFolder", new List<string> { ".txt", ".jpg" });
            Service.Models.Folder folder2 = new Service.Models.Folder("TestFolder", new List<string> { ".txt", ".jpg" });
            Assert.That(folder1, Is.EqualTo(folder2));
        }

        [Test]
        public void Folder_Equals_ShouldBeEqual_WhenFoldersSameNameDifferentCases()
        {
            Service.Models.Folder folder1 = new Service.Models.Folder("TestFolder", new List<string> { ".txt" });
            Service.Models.Folder folder2 = new Service.Models.Folder("testfolder", new List<string> { ".jpg" });
            Assert.That(folder1, Is.EqualTo(folder2));
        }

        [Test]
        public void Folder_Equals_ShouldNotBeEqual_WhenFoldersHaveDifferentNames()
        {
            Service.Models.Folder folder1 = new Service.Models.Folder("Folder1", new List<string> { ".txt" });
            Service.Models.Folder folder2 = new Service.Models.Folder("Folder2", new List<string> { ".txt" });
            Assert.That(folder1, Is.Not.EqualTo(folder2));
        }

        [Test]
        public void Folder_Equals_ShouldNotBeEqual_WhenComparedToNull()
        {
            Service.Models.Folder folder = new Service.Models.Folder("TestFolder", new List<string> { ".txt" });
            Assert.That(folder, Is.Not.EqualTo(null));
        }
    }
}
