namespace UnitTests.BaseEntities;
using NUnitAssert = Assert;

public class AssertionGroup
{
    private readonly List<Action> assertions = new List<Action>();

    public AssertionGroup Append(Action assertAction)
    {
        assertions.Add(assertAction);
        return this;
    }

    public AssertionGroup AppendMultiple(params Action[] assertActions)
    {
        assertActions.ToList().ForEach(action => assertions.Add(action));

        return this;
    }

    public void Assert()
    {
        var messagesList = new List<string>();

        foreach (var assertion in assertions)
        {
            try
            {
                assertion();
            }
            catch (Exception e)
            {
                messagesList.Add(e.Message);
            }
        }

        if (messagesList.Any())
        {
            string message = $"\n{string.Join("\n", messagesList)}";

            NUnitAssert.Fail(message);
        }
    }
}