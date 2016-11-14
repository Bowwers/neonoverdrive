using System;

public delegate void GUIValueChangedEventHandler(object Sender,
                                                 EventArgs E);

// Used to represent an editable GUI Field
// Can be used for integer types as well as for string fields
public class GUIField
{
    #region Fields
    bool isStringOnly_;
    int numericalValue_;
    string stringValue_ = null;
    string guiFieldName_ = null;
    #endregion

    #region Properties
    public bool IsStringOnly
    {
        get { return isStringOnly_; }
    }

    public string GUIFieldName
    {
        get { return guiFieldName_; }
    }
    #endregion

    public event GUIValueChangedEventHandler OnGUIValueChange;

    // Constructor
    public GUIField (string GUIFieldName, bool IsStringOnly, object StartingValue)
    {
        guiFieldName_ = GUIFieldName;
        isStringOnly_ = IsStringOnly;
        SetField(StartingValue);
    }

    // Gets the field's value as an integer
    public int GetFieldAsInteger()
    {
        if (isStringOnly_)
        {
            throw new InvalidOperationException("This GUI field can't be converted to int.");
        }

        return numericalValue_;
    }

    // Gets the field's value as an integer
    public string GetFieldAsString()
    {
        if (isStringOnly_) { return stringValue_; }
        else { return numericalValue_.ToString(); }
    }

    // Takes the approppriate value and set this field's value, then calls the event handler
    public void SetField (object NewValue)
    {
        if (NewValue is string)
        {
            stringValue_ = (string)NewValue;
            if (OnGUIValueChange != null)
            {
                OnGUIValueChange.Invoke(this, new EventArgs());
            }
        }
        else if (NewValue is int)
        {
            numericalValue_ = (int)NewValue;
            if (OnGUIValueChange != null)
            {
                OnGUIValueChange.Invoke(this, new EventArgs());
            }
        }
        else
        {
            throw new ArgumentException("Wrong type passed to GUI field");
        }
    }
}
