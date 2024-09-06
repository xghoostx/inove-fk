namespace InoveFk.Core.Base;

public abstract class Specification<T>
{
    private Dictionary<string, bool> _specifications = [];

    public void AddSpecification(string key, bool value) => _specifications.Add(key, value);
    public bool IsSatisfy => _specifications.Values.All(value => value);
    public bool IsNotSatisfy => !IsSatisfy;
    public IEnumerable<KeyValuePair<string, bool>> IsNotSatisfyList => 
        from spec in _specifications where spec.Value == false select spec;

    public abstract void Speficitations(T obj);
}
