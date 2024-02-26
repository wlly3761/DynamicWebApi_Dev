namespace ApplicationCommon.Model;
public class PositionOptions
{
    public const string Position = "Position";
    public string Title { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Ces { get; set; } = string.Empty;
    public Jwt Jwt { get; set; }=new Jwt();
}
public class Jwt
{
    public string a { get; set; } = string.Empty;
    public string b { get; set; } = string.Empty;
}
