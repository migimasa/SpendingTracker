using TrackerService.Base;

namespace TrackerService.Enums
{
    public class BalanceRolloverType : Enumeration
    {
        public static readonly BalanceRolloverType Unknown = new BalanceRolloverType(0, "Unknown");
        public static readonly BalanceRolloverType ApplyToSurplus = new BalanceRolloverType(1, "Stash");
        public static readonly BalanceRolloverType Carryover = new BalanceRolloverType(2, "Carryover");
        
        private BalanceRolloverType() { }
        private BalanceRolloverType(int value, string displayName) : base(value, displayName) { }
    }
}