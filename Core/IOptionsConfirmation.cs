namespace Core;

/// <summary>
/// The options confirmation interface.
/// It has to be implemented in EVERY program that uses Core class
/// </summary>
public interface IOptionsConfirmation
{
    /// <summary>
    /// Function to be implemented and used to know or confirm if the user
    /// wants to copy the attributes and dates from original files
    /// </summary>
    /// <param name="originalFile">Path to the original file.
    /// It can be empty</param>
    /// <returns>The bool that indicates if the user wants to copy info</returns>
    bool ConfirmCopy(string originalFile = "");


    /// <summary>
    /// Function to be implemented and used to know or confirm if the user
    /// wants to delete original files once converted
    /// </summary>
    /// <param name="originalFile">Path to the original file.
    /// It can be empty</param>
    /// <returns>The bool that indicates if the user wants to delete them</returns>
    bool ConfirmDeletion(string originalFile = "");

    /// <summary>
    /// Function to be implemented and used to know or confirm if the user
    /// wants to overwrite existing files
    /// </summary>
    /// <param name="existingFile">Path of the duplicated file
    /// It can't be empty because it has to be shown to the user</param>
    /// <returns>The bool that indicates if the user wants to overwrite it</returns>
    bool ConfirmOverwrite(string existingFile);


    /// <summary>
    /// Function to be implemented and used to know or confirm if the user
    /// enters a directory path that has to be handled
    /// </summary>
    /// <param name="directoryPath">The directory path.</param>
    void ManageDirectory(string directoryPath);
}