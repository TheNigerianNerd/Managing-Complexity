namespace TDList.Models;

public class UserChoice
{
    public UserChoice() { }
    public bool IsValid(int choice) => Enum.IsDefined(typeof(Choices), choice);
}

public enum Choices
{
    CreateTD = 1,
    ReadTD,
    UpdateTD,
    DeleteTD,
    QuitTD
}