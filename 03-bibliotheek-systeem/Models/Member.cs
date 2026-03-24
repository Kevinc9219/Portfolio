namespace BibliotheekSysteem.Models;

public class Member
{
    private string _name;
    private string _email;
    private string _memberNumber;

    public string Name { get => _name; set => _name = value; }
    public string Email { get => _email; set => _email = value; }
    public string MemberNumber { get => _memberNumber; set => _memberNumber = value; }

    public Member() { }

    public Member(string name, string email, string memberNumber)
    {
        _name = name;
        _email = email;
        _memberNumber = memberNumber;
    }

    public override string ToString() => $"[{_memberNumber}] {_name} ({_email})";
}
