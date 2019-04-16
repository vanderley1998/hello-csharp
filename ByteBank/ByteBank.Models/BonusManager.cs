﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Models
{
    public class BonusManager
    {
        private double _totalBonus;

        public void Register(Employee employee)
        {
            this._totalBonus += employee.GetBonus();
        }

        public void Register(Director director)
        {
            this._totalBonus += director.GetBonus();
        }

        public double TotalBonus { get; private set; }

    }
}
