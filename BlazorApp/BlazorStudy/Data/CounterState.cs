using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorStudy.Data
{
    public class CounterState
    {
        int _count;

        public Action OnStateChanged;

        [Parameter]
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                Refresh();
            }
        }

        void Refresh()
        {
            if (OnStateChanged != null)
                OnStateChanged.Invoke();
        }
    }
}
