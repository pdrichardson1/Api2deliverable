public class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsComplete { get; set; }
}


 public class SuperAdmin 
 {
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool? CanSeeAll { get; set; }

}
