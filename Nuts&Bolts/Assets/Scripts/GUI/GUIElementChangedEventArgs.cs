using System;

public class GUIElementChangedEventArgs : EventArgs
{
    public string GUIFieldName { get; set; }
    public GUIField Value { get; set; }
}
