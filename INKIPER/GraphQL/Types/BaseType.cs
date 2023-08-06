namespace INKIPER.GraphQL.Types;



public class BaseType
{
    public required String uuid { get; set; }
    public String createdAt { get; set; }
    public String updatedAt { get; set; }
    public String deletedAt { get; set; }
}