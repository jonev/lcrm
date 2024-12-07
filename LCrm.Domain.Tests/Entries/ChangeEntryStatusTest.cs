using LCrm.Domain.Entries;
using LCrm.Domain.Entries.Commands;

namespace LCrm.Domain.Tests.Entries;

public class ChangeEntryStatusTest : EntryTest<ChangeEntryStatusCommand>
{
    [Fact]
    public void WhenStatusUpdated_ShouldStatusUpdated()
    {
        Given(
            Create_entry_for_name("Entry1")
        );
        When(
            Change_entry_status(EntryStatus.Active)
        );
        Then(
            Entry_status_updated(EntryStatus.Active)
        );
    }
    
    [Fact]
    public void WhenStatusUpdatedMultipleTimes_ShouldStatusUpdated()
    {
        Given(
            Create_entry_for_name("Entry1")
        );
        When(
            Change_entry_status(EntryStatus.Active), Change_entry_status(EntryStatus.Rejected)
        );
        Then(
            Entry_status_updated(EntryStatus.Active), Entry_status_updated(EntryStatus.Rejected)
        );
    }

    private CreateEntryCommand Create_entry_for_name(string name)
    {
        return new CreateEntryCommand(Id, name);
    }

    private ChangeEntryStatusCommand Change_entry_status(EntryStatus status)
    {
        return new ChangeEntryStatusCommand(Id, status);
    }
}