using LCrm.Domain.Entries.Commands;

namespace LCrm.Domain.Tests.Entries;

public class CreateEntryTest : EntryTest<CreateEntryCommand>
{
    [Fact]
    public void WhenCreatedWithValidName_ShouldCreateEntry()
    {
        Given(
        );
        When(
            Create_entry_for_name("the first entry")
        );
        Then(
            Entry_created_with_name("the first entry")
        );
    }

    [Fact]
    public void WhenCreatedMultipleWithValidName_ShouldCreateMultipleEntries()
    {
        Given(
        );
        When(
            Create_entry_for_name("the first entry"),
            Create_entry_for_name("the second entry"),
            Create_entry_for_name("the third entry")
        );
        Then(
            Entry_created_with_name("the first entry"),
            Entry_created_with_name("the second entry"),
            Entry_created_with_name("the third entry")
        );
    }

    protected CreateEntryCommand Create_entry_for_name(string name)
    {
        return new CreateEntryCommand(Id, name);
    }
}