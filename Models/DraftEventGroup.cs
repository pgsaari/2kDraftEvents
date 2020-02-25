using System.Collections.Generic;

public class DraftEventGroup {
    public double Probability { get; set; }
    public int[] PickRange { get; set; }
    public List<DraftEvent> DraftEvents { get; set; }
}