// See https://aka.ms/new-console-template for more information

// check arguments
using FlickrRestore;
using System.Text.Json;

if (args.Length != 2)
{
    Console.WriteLine("Number of arguments is wrong!");
    PrintHelp();
} else if (args.Length == 1 && 
          (args[0] == "help" || args[0] == "--help" || args[0] == "/h"))
{
    PrintHelp();
}

// actual script
var sourceFolder = args[0];
var targetFolder = args[1];

if (!Directory.Exists(sourceFolder))
{
    Console.WriteLine($"Source folder {sourceFolder} does not exist.");
    return;
}
if (!Directory.Exists(targetFolder))
{
    Console.WriteLine($"Target folder {targetFolder} does not exist.");
    return;
}
if (Directory.GetFiles(targetFolder).Length > 0)
{
    Console.WriteLine($"Target folder {targetFolder} is not empty.");
    return;
}

// folders exist
var sourceImages = Directory.GetFiles(sourceFolder, "*.jpg");
var sourceMetadata = Directory.GetFiles(sourceFolder, "*.json");

Console.WriteLine($"Found {sourceImages.Length} images.");
Console.WriteLine($"Found {sourceMetadata.Length} metadata files.");
Console.WriteLine("Restoring files, please wait...");

int restoredCounter = 0;

// for each found image
foreach (var image in sourceImages)
{
    var imageNumber = Path.GetFileNameWithoutExtension(image);
    imageNumber = imageNumber.Substring(imageNumber.IndexOf("_") + 1);
    imageNumber = imageNumber.Substring(0, imageNumber.IndexOf("_"));

    // find metadata file
    foreach(var metadata in sourceMetadata)
    {
        var metadataImageNumber = Path.GetFileNameWithoutExtension(metadata);
        metadataImageNumber = metadataImageNumber.Substring(6);

        if (imageNumber == metadataImageNumber)
        {
            RestoreImageWithMetadata(image, metadata);
            restoredCounter++;
            break;
        }
    }
}

Console.WriteLine($"Restored {restoredCounter} files.");

// copies an image to a new location with a filename based on its original file creation date and sets this datetime as file creation date
void RestoreImageWithMetadata(string image, string metadata)
{
    var jsonString = File.ReadAllText(metadata);
    var json = JsonSerializer.Deserialize<Metadata>(jsonString);

    var dateTimeOfImage = DateTime.Parse(json.date_taken);
    var originalFilename = ConvertDatetimeToFilename(dateTimeOfImage);

    int number = 1;
    var filename = targetFolder + Path.DirectorySeparatorChar + originalFilename + "_" + number.ToString("000") + ".jpg";
    while (File.Exists(filename))
    {
        number++;
        filename = targetFolder + Path.DirectorySeparatorChar + originalFilename + "_" + number.ToString("000") + ".jpg";
    }

    File.Copy(image, filename, false);
    File.SetCreationTime(filename, dateTimeOfImage);
}

// converts a datetime object into a string that is used by Samsung Android phones for camera image filenames
string ConvertDatetimeToFilename(DateTime dateTime)
{
    string filename = dateTime.Year.ToString();
    filename += dateTime.Month < 10 ? "0" + dateTime.Month : dateTime.Month;
    filename += dateTime.Day < 10 ? "0" + dateTime.Day : dateTime.Day;
    filename += "_" + (dateTime.Hour < 10 ? "0" + dateTime.Hour : dateTime.Hour);
    filename += dateTime.Minute < 10 ? "0" + dateTime.Minute : dateTime.Minute;
    filename += dateTime.Second < 10 ? "0" + dateTime.Second : dateTime.Second;

    return filename;
}

void PrintHelp()
{
    Console.WriteLine("Flickr Restore");
    Console.WriteLine("--------------");
    Console.WriteLine("");
    Console.WriteLine("This script restores metadata from your photos that are downloaded from flickr.");
    Console.WriteLine("Please separate your downloaded data in one source folder and create a new target folder.");
    Console.WriteLine("Example: put all your extracted images from downloaded *.zip into a folder called 'source' and add metadata in json file format.");
    Console.WriteLine("Create an empty folder called 'target'.");
    Console.WriteLine("Start this script with the names of source and target folders like:");
    Console.WriteLine("flickrrestore source target");
    Console.WriteLine("This script now copies all images from source to target and restores them with filename and filedate from json metadata.");
}