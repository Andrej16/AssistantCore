namespace AssistantCore.DataSources;

public class SaluteCell
{
    public bool IsOn { get; set; }

    public SaluteCell(bool initialState)
    {
        IsOn = initialState;
    }
}
