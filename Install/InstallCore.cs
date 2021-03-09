using System;
using System.Collections.Generic;
using System.IO;

namespace Install
{
    public enum FileTreeTypes
    {
        File,
        Folder,
        Link
    }

    public class InstallCore
    {
        private string DefaultDirectory;
        private string HomeDirectory;
        private string RemoteDirectory;
        private Tree FileStructure;
        private string ProjectRoot;
        private List<string> Folders;
        private List<string> Files;
        private Computer CurrentSystem;

        public InstallCore(string homeDirectory, string remoteDirectory, string projectRoot)
        {
            ProjectRoot = CheckForPreviousVersion(homeDirectory, projectRoot);
            HomeDirectory = homeDirectory;
            RemoteDirectory = remoteDirectory;
            CurrentSystem = new Computer();
            DefaultDirectory = CurrentSystem.GetDefaultDirectory();
            (Folders, Files) = StructureAnalysisBasic(RemoteDirectory);
            
        }

        public object ReadFileStructure(string fileStructure)
        {
            List<(FileTreeTypes, string)> finalFileStructure = new List<(FileTreeTypes, string)>();
            int mode = 0;
            string elementName = "";
            int bracketOffSet = 0;
            int beginingIndex = 0;
            int stoppingIndex = 0;
            foreach (char fileStructureCharacter in fileStructure)
            {
                switch (fileStructureCharacter)
                {
                    case '{':
                        mode = 1;
                        bracketOffSet += 1;
                        break;
                    case '}':
                        mode = 3;
                        break;
                    case '%':
                        if (mode == 1)
                            finalFileStructure.Add((FileTreeTypes.Folder, elementName));
                        else
                            finalFileStructure.Add((FileTreeTypes.File, elementName));
                        break;
                    case '@':
                        finalFileStructure.Add((FileTreeTypes.File, elementName));
                        break;
                    default:
                        elementName += fileStructureCharacter;
                        break;
                }
            }

            return finalFileStructure;
        }

        public (List<string>, List<string>) StructureAnalysisBasic(string currentLocation = "")
        {
            List<string> Folders = new List<string>();
            List<string> Files = new List<string>();
            try
            {
                foreach (string d in Directory.GetDirectories(currentLocation))
                {
                    Folders.Add(CurrentSystem.OsPathSeperator + d);
                    foreach (string f in Directory.GetFiles(d))
                    {
                        Files.Add(CurrentSystem.OsPathSeperator + f);
                    }
                    StructureAnalysisBasic(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }

            return (Folders, Files);
        }

        public string CheckForPreviousVersion(string directory, string projectRoot)
        {
            string modifiedProjectRoot = projectRoot;
            int numberModifier = 1;
            int projectRootLength = projectRoot.Length;
            while (Directory.Exists(modifiedProjectRoot))
            {
                modifiedProjectRoot = projectRoot + "(" + numberModifier + ")";
            }

            return CurrentSystem.OsPathSeperator + modifiedProjectRoot;
        }

        public void CreateDirectories()
        {
            string basePath = HomeDirectory + ProjectRoot;
            Directory.CreateDirectory(ProjectRoot);
            foreach (string directory in Folders)
            {
                Directory.CreateDirectory(basePath + directory);
            }
        }

        public void CopyFiles()
        {
            string basePath = HomeDirectory + ProjectRoot;
            foreach (string file in Files)
            {
                File.Copy(file, basePath + file);
            }
        }
    }
}