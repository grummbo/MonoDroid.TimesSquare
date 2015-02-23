using System;

namespace MonoDroid.TimesSquare
{
    public enum RangeState
    {
        None,
        First,
        Middle,
        Last
    }

    public class MonthCellDescriptor : Java.Lang.Object
    {
        public DateTime DateTime { get; set; }
        public int Value { get; set; }
        public bool IsCurrentMonth { get; set; }
        public bool IsSelected { get; set; }
        public bool IsToday { get; set; }
        public bool IsSelectable { get; set; }
        public bool IsHighlighted { get; set; }
        public bool IsFuture { get; set; }
        public bool IsWeekend { get; set; }
        public RangeState RangeState { get; set; }

        public MonthCellDescriptor(DateTime date, bool isCurrentMonth, bool isSelectable, bool isSelected,
            bool isToday, bool isHighlighted, bool isFuture, bool isWeekend, int value, RangeState rangeState)
        {
            DateTime = date;
            Value = value;
            IsCurrentMonth = isCurrentMonth;
            IsSelected = isSelected;
            IsHighlighted = isHighlighted;
            IsToday = isToday;
            IsSelectable = isSelectable;
            IsFuture = isFuture;
            IsWeekend = isWeekend;
            RangeState = rangeState;
        }

        public override string ToString()
        {
            return "MonthCellDescriptor{"
                   + "Date=" + DateTime
                   + ", Value=" + Value
                   + ", IsCurrentMonth=" + IsCurrentMonth
                   + ", IsSelected=" + IsSelected
                   + ", IsToday=" + IsToday
                   + ", IsSelectable=" + IsSelectable
                   + ", IsHighlighted=" + IsHighlighted
                   + ", IsFuture=" + IsFuture
                   + ", IsWeekend=" + IsWeekend
                   + ", RangeSTate=" + RangeState
                   + "}";
        }
    }
}