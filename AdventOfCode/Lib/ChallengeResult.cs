namespace AdventOfCode.Lib;


public abstract class ChallengeResult
{
    public abstract void Write(TextWriter textWriter);
}


internal sealed class ValueChallengeResult<T> : ChallengeResult
{
    private readonly T _value;

    public ValueChallengeResult(T value) => _value = value;
    public T Value => _value;

    public override void Write(TextWriter textWriter) => textWriter.WriteLine(_value);
}