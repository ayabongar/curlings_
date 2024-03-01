using System;

public class IntelliQuestionArgs : EventArgs
{
    public decimal? SelectedOption { get; set; }
    public Decimal FieldId { get; set; }
    public bool IsParent { get; set; }
    public int RowIndex { get; set; }
    public IntelliQuestionArgs(decimal? option, decimal fieldId, bool parent, int rowIndex)
    {
        SelectedOption = option;
        FieldId = fieldId;
        IsParent = parent;
        RowIndex = rowIndex;
    }

    public IntelliQuestionArgs()
    {
        // TODO: Complete member initialization
    }
}
