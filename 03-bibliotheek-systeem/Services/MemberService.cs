using BibliotheekSysteem.Models;
using Newtonsoft.Json;

namespace BibliotheekSysteem.Services;

public class MemberService
{
    private List<Member> _members = new();
    private const string FilePath = "Data/members.json";

    public void Load()
    {
        if (!File.Exists(FilePath)) return;
        _members = JsonConvert.DeserializeObject<List<Member>>(File.ReadAllText(FilePath)) ?? new();
    }

    public void Save()
    {
        Directory.CreateDirectory("Data");
        File.WriteAllText(FilePath, JsonConvert.SerializeObject(_members, Formatting.Indented));
    }

    public void AddMember(Member member)
    {
        _members.Add(member);
        Save();
    }

    public List<Member> GetAll() => _members;

    public Member? FindByNumber(string memberNumber) =>
        _members.FirstOrDefault(m => m.MemberNumber.Equals(memberNumber, StringComparison.OrdinalIgnoreCase));

    public string GenerateMemberNumber() => $"LID{(_members.Count + 1):D4}";
}
