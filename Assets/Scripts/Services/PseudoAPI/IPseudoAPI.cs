using Cysharp.Threading.Tasks;

public interface IPseudoAPI
{
    UniTask<Colors> GetColors();
    UniTask<Texts> GetTexts();
}
