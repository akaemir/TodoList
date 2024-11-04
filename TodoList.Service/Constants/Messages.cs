namespace TodoList.Service.Constants;

public static class Messages
{
    public const string TodoAddedMessage = "Yapılacak iş Eklendi!";
    public const string TodoUpdatedMessage = "Yapılacak iş Güncellendi!";
    public const string TodoDeletedMessage = "Yapılacak iş Silindi!";

    public static string TodoIsNotPresentMessage(Guid id)
    {
        return $"Doesn't match id : {id}";
    }
}