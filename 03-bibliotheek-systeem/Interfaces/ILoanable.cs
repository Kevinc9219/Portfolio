namespace BibliotheekSysteem.Interfaces;

public interface ILoanable
{
    bool Borrow(Models.Member member);
    bool Return();
}
