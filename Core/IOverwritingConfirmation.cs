namespace Core;

public interface IOverwritingConfirmation
{
    bool ConfirmateOverwrite(string existingFile);
}