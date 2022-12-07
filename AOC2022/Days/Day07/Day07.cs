using System;

public class Day07 : IDay
{
    public void FirstChallenge(string[] lines)
    {
        Directory root = BuildDirectoryTree(lines);

        long filteredSum = 0;
        Console.WriteLine(SumDirectorySize(root, ref filteredSum));
        Console.WriteLine(filteredSum);
    }

    private static Directory BuildDirectoryTree(string[] lines)
    {
        Directory root = new Directory("/", null);
        Directory currentDirectory = root;
        foreach (string line in lines)
        {
            if (line.StartsWith("$ cd "))
            {
                string directory = line.Substring(5);
                if (directory == "/")
                    currentDirectory = root;
                else if (directory == "..")
                {
                    currentDirectory = currentDirectory.Parent;
                }
                else
                {
                    currentDirectory = currentDirectory.SubDirectories[directory];
                }
            }
            else if (line.StartsWith("$ ls"))
            {
                continue;
            }
            else if (line.StartsWith("dir"))
            {
                currentDirectory.SubDirectories.Add(line.Substring(4), new Directory(line.Substring(4), currentDirectory));
            }
            else if (char.IsDigit(line[0]))
            {
                string[] fileString = line.Split(' ');
                currentDirectory.Files.Add(new File(fileString[1], int.Parse(fileString[0])));
            }
        }

        return root;
    }

    private static long SumDirectorySize(Directory currentDirectory, ref long filteredSum)
    {
        long totalSize = 0;

        foreach (File file in currentDirectory.Files)
        {
            totalSize += file.Size;
        }

        foreach (Directory subdirectory in currentDirectory.SubDirectories.Values)
        {            
            totalSize += SumDirectorySize(subdirectory, ref filteredSum);
        }

        if (totalSize < 100000)
            filteredSum += totalSize;
        
        return totalSize;
    }

    public void SecondChallenge(string[] lines)
    {
        Directory root = BuildDirectoryTree(lines);

        long maxSize = 70000000, uigh = 0;
        long emptySpace = maxSize - SumDirectorySize(root, ref uigh);
        long minSize = 30000000 - emptySpace;

        FindDirectoryToDelete(root, ref maxSize, minSize);
        Console.WriteLine(maxSize);
    }

    private static long FindDirectoryToDelete(Directory currentDirectory, ref long size, long minSize)
    {
        long totalSize = 0;

        foreach (File file in currentDirectory.Files)
        {
            totalSize += file.Size;
        }

        foreach (Directory subdirectory in currentDirectory.SubDirectories.Values)
        {
            totalSize += FindDirectoryToDelete(subdirectory, ref size, minSize);
        }

        if (totalSize > minSize && totalSize < size)
            size = totalSize;

        return totalSize;
    }

    public class Directory
    {
        public string Name { get; set; }
        public Directory Parent { get; set; }
        public Dictionary<string, Directory> SubDirectories { get; set; }
        public List<File> Files { get; set; }

        public Directory(string name, Directory parent)
        {
            Name = name;
            Parent = parent;
            SubDirectories = new Dictionary<string, Directory>();
            Files = new List<File>();
        }
    }

    public class File
    {
        public string Name { get; set; }
        public int Size { get; set; }

        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }
}
