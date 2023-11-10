namespace INKIPER.GraphQL.Types;



public class BaseType
{
    public required String uuid { get; set; }
    public String createdAt { get; set; }
    public String updatedAt { get; set; }
    public String deletedAt { get; set; }
    
    public string TimeAgo
    {
        get
        {
            long unixTimestamp = long.Parse(createdAt);
            DateTime pastDateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).DateTime;
            TimeSpan timeDifference = DateTime.Now - pastDateTime;

            if (timeDifference.TotalSeconds < 60)
            {
                return $"{(int)timeDifference.TotalSeconds} seconds ago";
            }
            else if (timeDifference.TotalMinutes < 60)
            {
                return $"{(int)timeDifference.TotalMinutes} minutes ago";
            }
            else if (timeDifference.TotalHours < 24)
            {
                return $"{(int)timeDifference.TotalHours} hours ago";
            }
            else if (timeDifference.TotalDays < 30)
            {
                return $"{(int)timeDifference.TotalDays} days ago";
            }
            else if (timeDifference.TotalDays < 365)
            {
                int months = (int)(timeDifference.TotalDays / 30);
                return $"{months} {(months == 1 ? "month" : "months")} ago";
            }
            else
            {
                int years = (int)(timeDifference.TotalDays / 365);
                return $"{years} {(years == 1 ? "year" : "years")} ago";
            }
        }
    }
}