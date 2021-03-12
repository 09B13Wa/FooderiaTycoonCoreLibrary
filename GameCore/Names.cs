using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Security;
using System.Security.Policy;

namespace GameCore
{
    public class Names
    {
        protected List<string>[] RandomNames;

        public Names(string initialPath)
        {
            RandomNames = GetRandomNames(initialPath);
        }

        protected enum NamesSection
        {
            NotANamesSection,
            FirstNames,
            MiddleNames,
            LastNames
        }

        protected (List<int>, List<int>)[] LocationAdder((List<int>, List<int>)[] locations,
            NamesSection currentSection, int sectionBegin, int sectionEnd)
        {
            Dictionary<NamesSection, int> correspondingSectionsDictionary = new Dictionary<NamesSection, int>();
            correspondingSectionsDictionary.Add(NamesSection.FirstNames, 0);
            correspondingSectionsDictionary.Add(NamesSection.LastNames, 1);
            correspondingSectionsDictionary.Add(NamesSection.MiddleNames, 2);
            (List<int> beginingIndexes, List<int> endingIndexes) = locations[correspondingSectionsDictionary[currentSection]];
            if (beginingIndexes == null)
                beginingIndexes = new List<int>();
            if (endingIndexes == null)
                endingIndexes = new List<int>();
            beginingIndexes.Add(sectionBegin);
            endingIndexes.Add(sectionEnd);
            locations[correspondingSectionsDictionary[currentSection]] = (beginingIndexes, endingIndexes);
            return locations;
        }
        protected bool CheckFileValidityFromLocations((List<int>, List<int>)[] locations)
        {
            bool isValid = true;
            foreach ((List<int>, List<int>) section in locations)
            {
                (List<int> beginingIndexes, List<int> endingIndexes) = section;
                if (beginingIndexes == null || endingIndexes == null)
                {
                    isValid = false;
                    Console.Error.WriteLine("Error: no declarations for first elements, middle elements or last elements.");
                }
                int quantityOfSubSections = beginingIndexes.Count;
                for (int i = 0; i < quantityOfSubSections; i++)
                {
                    if (endingIndexes[i] - beginingIndexes[i] <= 1)
                        isValid = false;
                }
            }

            return isValid;
        }

        public (bool, (List<int>, List<int>)[], List<int>) CheckRandomNameFileValidityAndGetLocations(string path)
        {

            bool isValid;
            (List<int>, List<int>)[] locations = new (List<int>, List<int>)[3];
            List<int> linesToIgnore = new List<int>();
            {
                if (File.Exists(path))
                {
                    StreamReader reader = new StreamReader(path);
                    string contents = reader.ReadToEnd();
                    string[] lines = contents.Split('\n');
                    NamesSection currentSection = NamesSection.NotANamesSection;
                    NamesSection nextSection = NamesSection.NotANamesSection;
                    int lineCursor = 0;
                    int sectionBegin = 0;
                    int sectionEnd = 0;
                    bool sectionChange;
                    foreach (string line in lines)
                    {
                        sectionChange = true;
                        switch (line)
                        {
                            case "last_names:":
                                nextSection = NamesSection.LastNames;
                                break;
                            case "first_names:":
                                nextSection = NamesSection.FirstNames;
                                break;
                            case "middle_names:":
                                nextSection = NamesSection.MiddleNames;
                                break;
                            case "$END$":
                                nextSection = NamesSection.NotANamesSection;
                                break;
                            default:
                                sectionChange = false;
                                break;
                        }

                        if (sectionChange)
                        {
                            if (currentSection == NamesSection.NotANamesSection &&
                                nextSection != NamesSection.NotANamesSection)
                            {
                                sectionBegin = lineCursor;
                                sectionEnd = lineCursor;
                            }
                            else if (currentSection == nextSection)
                                linesToIgnore.Add(lineCursor);
                            else
                            {
                                locations = LocationAdder(locations, currentSection, sectionBegin, sectionEnd);
                                sectionBegin = lineCursor;
                                sectionEnd = lineCursor;
                            }

                            currentSection = nextSection;
                        }
                        else
                            sectionEnd += 1;

                        lineCursor += 1;
                    }

                    if (currentSection != NamesSection.NotANamesSection)
                        locations = LocationAdder(locations, currentSection, sectionBegin, sectionEnd);
                    isValid = CheckFileValidityFromLocations(locations);
                }
                else
                {
                    isValid = false;
                    locations = null;
                    linesToIgnore = null;
                }
            }
            return (isValid, locations, linesToIgnore);
        }

        protected List<string>[] GetRandomNames(string path)
        {
            (bool fileValidity, (List<int>, List<int>)[] locations, List<int> linesToIgnore) =
                CheckRandomNameFileValidityAndGetLocations(path);
            List<string>[] finalNames = new List<string>[3];
            if (fileValidity)
            {
                StreamReader reader = new StreamReader(path);
                string contents = reader.ReadToEnd();
                string[] lines = contents.Split('\n');
                List<string> firstNames = new List<string>();
                List<string> middleNames = new List<string>();
                List<string> lastNames = new List<string>();
                List<string>[] groupedNames = {firstNames, lastNames, middleNames};
                int[] sectionLengths = {0, 0, 0};
                for (int locationCursor = 0; locationCursor < 3; locationCursor++)
                {
                    (List<int> beginingIndexes, List<int> endingIndexes) = locations[locationCursor];
                    int numberOfSections = beginingIndexes.Count;
                    for (int indexesCursor = 0; indexesCursor < numberOfSections; indexesCursor++)
                    {
                        int beginingIndex = beginingIndexes[indexesCursor];
                        int endingIndex = endingIndexes[indexesCursor];
                        for (int indexCursor = beginingIndex + 1; indexCursor <= endingIndex; indexCursor++)
                        {
                            if (!linesToIgnore.Contains(indexCursor))
                            {
                                groupedNames[locationCursor].Add(lines[indexCursor]);
                                sectionLengths[locationCursor] += 1;
                            }
                        }
                    }
                }

                finalNames = groupedNames;
            }
            else
            {
                Console.WriteLine("Given file not valid");
                finalNames = null;
            }
            
            return finalNames;
        }

        public (string[], string[]) GetRandomName(string path, int numberOfMiddleNames)
        {
            Random rng = new Random();
            List<string>[] possibleNames = RandomNames;
            string firstName = "";
            string lastName = "";
            string[] standardName;
            standardName = new string[2];
            string[] middleNames = new string[numberOfMiddleNames];
            if (possibleNames != null)
            {
                List<string> firstNames = possibleNames[0];
                List<string> middleNamesFromSource = possibleNames[2];
                List<string> lastNames = possibleNames[1];
                int numberOfFirstNamesFromSource = firstNames.Count;
                int numberOfMiddleNamesFromSource = middleNamesFromSource.Count;
                int numberOfLastNamesFromSource = lastNames.Count;
                if (numberOfFirstNamesFromSource > 1)
                    firstName = firstNames[rng.Next(0, numberOfFirstNamesFromSource)];
                else if (numberOfFirstNamesFromSource == 1)
                    firstName = firstNames[0];
                if (numberOfMiddleNamesFromSource > numberOfMiddleNames)
                {
                    List<int> chosenMiddleNames = new List<int>();
                    for (int i = 0; i < numberOfMiddleNames; i++)
                    {
                        int randomIndex;
                        do
                        {
                            randomIndex = rng.Next(0, numberOfMiddleNamesFromSource);
                        } while (chosenMiddleNames.Contains(randomIndex));

                        chosenMiddleNames.Add(randomIndex);
                        middleNames[i] = middleNamesFromSource[randomIndex];
                    }
                }
                else if (numberOfMiddleNamesFromSource > 0)
                    middleNames = middleNamesFromSource.ToArray();
                else
                    Console.WriteLine("no middle names provided");

                if (numberOfLastNamesFromSource > 1)
                    lastName = lastNames[rng.Next(0, numberOfLastNamesFromSource)];
                else if (numberOfLastNamesFromSource == 1)
                    lastName = lastNames[0];
            }
            else
            {
                middleNames = null;
            }

            standardName = new[] {firstName, lastName};
            return (standardName, middleNames);
        }

        public List<string>[] GetAvailableNamesLists()
        {
            return RandomNames;
        }

        ~Names()
        {
            Console.WriteLine("Names succesfully destroyed");
        }
        
        public static (string[], string[]) GetRandomName(List<string>[] randomNames, int numberOfMiddleNames = 0)
        {
            Random rng = new Random();
            List<string>[] possibleNames = randomNames;
            string firstName = "";
            string lastName = "";
            string[] standardName; 
            standardName = new string[2];
            string[] middleNames = new string[numberOfMiddleNames];
            if (possibleNames != null)
            {
                List<string> firstNames = possibleNames[0];
                List<string> middleNamesFromSource = possibleNames[2];
                List<string> lastNames = possibleNames[1];
                int numberOfFirstNamesFromSource = firstNames.Count;
                int numberOfMiddleNamesFromSource = middleNamesFromSource.Count;
                int numberOfLastNamesFromSource = lastNames.Count;
                if (numberOfFirstNamesFromSource > 1)
                    firstName = firstNames[rng.Next(0, numberOfFirstNamesFromSource)];
                else if (numberOfFirstNamesFromSource == 1)
                    firstName = firstNames[0];
                if (numberOfMiddleNamesFromSource > numberOfMiddleNames)
                {
                    List<int> chosenMiddleNames = new List<int>();
                    for (int i = 0; i < numberOfMiddleNames; i++)
                    {
                        int randomIndex;
                        do
                        {
                            randomIndex = rng.Next(0, numberOfMiddleNamesFromSource);
                        } while (chosenMiddleNames.Contains(randomIndex));
                        chosenMiddleNames.Add(randomIndex);
                        middleNames[i] = middleNamesFromSource[randomIndex];
                    }
                }
                else if (numberOfMiddleNamesFromSource > 0)
                    middleNames = middleNamesFromSource.ToArray();
                else
                    Console.WriteLine("no middle names provided");

                if (numberOfLastNamesFromSource > 1)
                    lastName = lastNames[rng.Next(0, numberOfLastNamesFromSource)];
                else if (numberOfLastNamesFromSource == 1)
                    lastName = lastNames[0];
            }
            else
            {
                middleNames = null;
            }
            standardName = new[] {firstName, lastName};
            return (standardName, middleNames);
        }
        
    }

    class NamesStaticMethods
    {
        public static (string[], string[]) GetRandomName(List<string>[] randomNames, int numberOfMiddleNames = 0)
        {
            Random rng = new Random();
            List<string>[] possibleNames = randomNames;
            string firstName = "";
            string lastName = "";
            string[] standardName; 
            standardName = new string[2];
            string[] middleNames = new string[numberOfMiddleNames];
            if (possibleNames != null)
            {
                List<string> firstNames = possibleNames[0];
                List<string> middleNamesFromSource = possibleNames[2];
                List<string> lastNames = possibleNames[1];
                int numberOfFirstNamesFromSource = firstNames.Count;
                int numberOfMiddleNamesFromSource = middleNamesFromSource.Count;
                int numberOfLastNamesFromSource = lastNames.Count;
                if (numberOfFirstNamesFromSource > 1)
                    firstName = firstNames[rng.Next(0, numberOfFirstNamesFromSource)];
                else if (numberOfFirstNamesFromSource == 1)
                    firstName = firstNames[0];
                if (numberOfMiddleNamesFromSource > numberOfMiddleNames)
                {
                    List<int> chosenMiddleNames = new List<int>();
                    for (int i = 0; i < numberOfMiddleNames; i++)
                    {
                        int randomIndex;
                        do
                        {
                            randomIndex = rng.Next(0, numberOfMiddleNamesFromSource);
                        } while (chosenMiddleNames.Contains(randomIndex));
                        chosenMiddleNames.Add(randomIndex);
                        middleNames[i] = middleNamesFromSource[randomIndex];
                    }
                }
                else if (numberOfMiddleNamesFromSource > 0)
                    middleNames = middleNamesFromSource.ToArray();
                else
                    Console.WriteLine("no middle names provided");

                if (numberOfLastNamesFromSource > 1)
                    lastName = lastNames[rng.Next(0, numberOfLastNamesFromSource)];
                else if (numberOfLastNamesFromSource == 1)
                    lastName = lastNames[0];
            }
            else
            {
                middleNames = null;
            }
            standardName = new[] {firstName, lastName};
            return (standardName, middleNames);
        }
    }
}