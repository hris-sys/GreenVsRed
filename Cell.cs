using System;
using System.Diagnostics.CodeAnalysis;

namespace GreenVsRed
{
    public class Cell : IComparable<Cell>
    {

        public Cell(int state)
        {
            this.CurrentState = state;

            this.OriginalState = state;

            this.NextState = state;
        }

        public int OriginalState { get; set; }

        public int CurrentState { get; set; }

        public int NextState { get; set; }

        public int CompareTo([AllowNull] Cell otherCell)
        {
            int result = 0;

            if (this.CurrentState == 0 &&
                this.CurrentState != otherCell.CurrentState)
            {
                result++;
            }
            else if (this.CurrentState == 1 && 
                     this.CurrentState == otherCell.CurrentState)
            {
                result++;
            }

            return result;
        }

        public void SetNextState()
        {
            this.CurrentState = this.NextState;
        }
    }
}
