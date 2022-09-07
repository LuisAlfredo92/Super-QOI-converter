namespace Core;

public interface IOptionsConfirmation
{
    bool ConfirmCopy(string originalFile = "");
    bool ConfirmDeletion(string originalFile = "");
    bool ConfirmOverwrite(string existingFile);
    void ManageDirectory(string directoryPath);
}