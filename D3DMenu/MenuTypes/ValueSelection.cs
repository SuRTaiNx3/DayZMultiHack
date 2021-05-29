using D3DMenu.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3DMenu.MenuTypes
{
    [ValueField("Value")]
    public class ValueSelection : MenuItemBase
    {
        public float Value;

        public float MaxValue;

        public float MinValue;

        public float ValueStep;

        public ValueSelection(string name, string title, float value, float maxValue, float minValue, float valueStep, bool save)
        {
            this.Name = name;
            this.Title = title;
            this.Value = value;
            this.MaxValue = maxValue;
            this.MinValue = minValue;
            this.ValueStep = valueStep;
            this.Save = save;
        }

        public override void Next()
        {
            if (this.Value < this.MaxValue)
                this.Value += this.ValueStep;
        }

        public override void Previous()
        {
            if (this.Value > this.MinValue)
                this.Value -= this.ValueStep;
        }
    }
}
