namespace INKIPER.GraphQL.Types;



public class BaseType
{
    public int Id { get; set; }
    public required String Uuid { get; set; }
    public String CreatedAt { get; set; }
    public String UpdatedAt { get; set; }
    public String DeletedAt { get; set; }
}